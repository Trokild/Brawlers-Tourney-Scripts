using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Unit : MonoBehaviour
{
    #region ::Unit Identity::
    public int unitTeam;
    public int idUnit;
    public int UnitValue { get; private set; }

    public enum UnitState { Idle, MoveOrder, AttackOrder, MoveAttackOrder, Chase, Attack, Stagnate } //Stagnate
    public UnitState currentstate;

    public enum UnitType {Infantry, Cavalry, Archer, Hero, Spearman}
    public UnitType currentUnitType;

    protected bool isIdle = true;
    protected bool isMoveOrder = false;
    protected bool isAttackOrder = false;
    protected bool isMoveAttackOrder = false;
    protected bool isChasing = false;
    protected bool isAttacking = false;
    protected bool isStagnate = false;
    private bool isShoved = false;
    public bool CheckStagnate
    {
        get
        {
            return isStagnate;
        }
        set
        {
            isStagnate = value;
        }
    }

    protected bool atkOrderInMotion = false;
    protected bool movOrderInMotion = false;
    protected bool movAvoidInMotion = false;
    #endregion

    #region ::Stats::
    [Header("__Unit Stats__")]

    public Stat attackDamage;
    public Stat armorPercing;
    public Stat attackSpeed;
    protected float baseSpeed;
    //[SerializeField] protected float VisionFogRange = 25f;

    public Stat strenght;
    public int weightCarry;
    public enum UnitWeight { VeryLight, Light, Heavy, VeryHeavy}
    public UnitWeight currentweight;

    public int max_Stamina;
    public int cur_Stamina { get; protected set; }
    public Stat reg_Stamina;
    private int cost_Stamina;
    private float speedUnit;
    public bool tierd { get; private set; }
    public enum UnitForm { Tierd, Fresh}
    public UnitForm currentform;

    [SerializeField] private float regenTime = 5f;
    private float rts;

    public float Range;
    public float AttackRange;
    protected float atkSpd;
    protected float cooldownAtk;

    private float speedMemory;
    private int AnimStateMemory;
    private IEnumerator corMemory;
    #endregion

    #region ::Target & Movement::
    //[HideInInspector]
    public Transform target = null;
    //[HideInInspector]
    public GameObject nearestEnemy = null;
    [HideInInspector]
    public float shortestDist = Mathf.Infinity;
    [HideInInspector]
    public Vector3 atkOrdPos;
    [HideInInspector]
    public Vector3 movOrdPos;
    protected Vector3 movOrdAvoid;

    public float secLook;
    protected float secLeft;

    protected float rSizeTrg;

    public float velo { get; protected set; }
    protected Vector3 previous;

    public float distanceAntenna;
    public float timeToCheckForObstacles;
    protected float tmAntChk;
    protected bool isAntenaCK = false;
    private bool wasLastHitleft = false;

    [HideInInspector]
    public float walkRange;
    [HideInInspector]
    public Vector3 newposition;
    [HideInInspector]
    public bool currentlySelected = false;

    public List<GameObject> enemies = new List<GameObject>();
    private float animSpeed;
    #endregion

    #region ::References::
    [SerializeField]
    protected LayerMask clickAbleLayer;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public ClickParent clickRef;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public UnitHealth health;
    [HideInInspector]
    public Unit_Spawner mother;

    [SerializeField] private Unit_Appearance appearance;
    [SerializeField] private Transform transMdl;
    protected AudioSource audioS;
    [HideInInspector]
    public Outline selectionOutline;

    #endregion

    void OnEnable()
    {
        if(anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
        baseSpeed = navMeshAgent.speed;
        audioS = GetComponent<AudioSource>();
        secLeft = secLook;
        animSpeed = anim.speed;
        rts = regenTime;

        if (appearance == null)
        {
            appearance = GetComponentInChildren<Unit_Appearance>();
        }
        tmAntChk = timeToCheckForObstacles;

        switch(currentUnitType)
        {
            case UnitType.Infantry:
                UnitValue = 0;
                break;
            case UnitType.Spearman:
                UnitValue = 1;
                break;
            case UnitType.Archer:
                UnitValue = 2;
                break;
            case UnitType.Cavalry:
                UnitValue = 3;
                break;
        }
        currentform = UnitForm.Fresh;
    }

    // SetUpStartingStatUint takes in diffrent parameters and makes it impossible to have a common inherens
    //public virtual void SetUpStatsUnit(StatWeapon st, Vector3Int ssw) // ssw.x = staminia, .y = stg, .z = weight
    //{
    //    attackDamage.SetBaseValue(st.weaponDamage);
    //    armorPercing.SetBaseValue(st.armorPercing);
    //    attackSpeed.SetBaseValue(st.attackSpeed);

    //    max_Stamina = ssw.x;
    //    cur_Stamina = max_Stamina;
    //    strenght.SetBaseValue(ssw.y);
    //    weightCarry = ssw.z;
    //    SetUpStamina(strenght.GetBaseValue(), weightCarry);
    //}

    protected void SetUpStamina(int stg, int wg)
    {
        float diff_StgWg;
        if(wg > 0)
        {
            diff_StgWg = (stg* 1f / wg* 1f);

            if (diff_StgWg >= 2)
            {
                //Debug.Log("Very Good");
                float spd = navMeshAgent.speed;
                cost_Stamina = max_Stamina / 100;
                currentweight = UnitWeight.VeryLight;
            }
            else if(diff_StgWg < 2 && diff_StgWg >= 1)
            {
                //Debug.Log("Good");
                cost_Stamina = max_Stamina / 40;
                currentweight = UnitWeight.Light;
            }
            else if(diff_StgWg < 1 && diff_StgWg >= 0.5f)
            {
                //Debug.Log("Bad");
                float spd = navMeshAgent.speed;
                navMeshAgent.speed = (spd * 0.9f);
                cost_Stamina = max_Stamina / 15;
                currentweight = UnitWeight.Heavy;
            }
            else if (diff_StgWg < 0.5f)
            {
                //Debug.Log("Very Bad");
                float spd = navMeshAgent.speed;
                navMeshAgent.speed = (spd * 0.8f);
                cost_Stamina = max_Stamina / 5;
                currentweight = UnitWeight.VeryHeavy;
            }
            else
            {
                //Debug.LogError("Very VERY Bad");
                float spd = navMeshAgent.speed;
                navMeshAgent.speed = (spd * 0.8f);
                cost_Stamina = max_Stamina / 5;
                currentweight = UnitWeight.VeryHeavy;
            }

        }
        else
        {
            cost_Stamina = max_Stamina / 100;
            currentweight = UnitWeight.VeryLight;
        }
        speedUnit = navMeshAgent.speed;
    }

    protected bool CheckStaminaBool()
    {
        float p = ((cur_Stamina * 1f) / (max_Stamina * 1f));
        if (p <= 0.2f)
        {
            tierd = true;
            return true;
        }
        else
        {
            tierd = false;
            return false;
        }
    }

    void CheckStamina()
    {
        float p = cur_Stamina / max_Stamina;
        if (p <= 0.2f)
        {
            tierd = true;
            if(speedUnit == navMeshAgent.speed)
            {
                navMeshAgent.speed = (speedUnit * 0.8f);
            }
        }
        else
        {
            tierd = false;
            if (speedUnit != navMeshAgent.speed)
            {
                navMeshAgent.speed = speedUnit;
            }
        }
    }

    void GainStamina(int stam)
    {
        if (cur_Stamina < max_Stamina)
        {
            cur_Stamina += stam;
            if (cur_Stamina > max_Stamina)
            {
                cur_Stamina = max_Stamina;
            }
            CheckStamina();
        }
    }

    public void StaminaTick(int ticks, int healA, float rate)
    {
        StartCoroutine(TickStamFucntion(ticks, healA, rate));
    }

    IEnumerator TickStamFucntion(int t, int h, float r)
    {
        for (int i = 0; i < t; i++)
        {
            if (!health.isDead)
            {
                GainStamina(h);
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(r);
        }
    }

    protected virtual void Update()
    {
        velo = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;

        float procentSpeed = navMeshAgent.velocity.magnitude / baseSpeed; //navMeshAgent.speed
        anim.SetFloat("speedPercent", procentSpeed, .1f, Time.deltaTime);

        switch (currentstate)
        {
            case UnitState.Idle:
                if (isIdle == true)
                {
                    Func_Idle();
                }
                break;

            case UnitState.MoveOrder:
                if (isMoveOrder == true)
                {
                    Func_MoveOrd();
                }
                break;

            case UnitState.MoveAttackOrder:
                if (isMoveAttackOrder == true)
                {
                    Func_AtkMoveOrd();
                }
                break;

            case UnitState.AttackOrder:
                if (isAttackOrder)
                {
                    Func_AttackOrd();
                }

                break;

            case UnitState.Chase:
                if (isChasing)
                {
                    Func_Chase();
                }
                break;

            case UnitState.Attack:

                if (isAttacking)
                {
                    Func_Attack();
                }
                break;

                //case UnitState.Stagnate:

                //    if(isStagnate)
                //    {
                //        Func_Stagnate();
                //    }
                //    break;
        }
    }

    #region Orders

    public void ClickMe()
    {
        if (selectionOutline != null && !health.isDead)
        {
            if (!currentlySelected)
            {
                currentlySelected = true;
                selectionOutline.enabled = true;
            }
            else
            {
                currentlySelected = false;
                selectionOutline.enabled = false;
            }
        }
    }

    public void DeSelect()
    {
        if (selectionOutline != null)
        {
            currentlySelected = false;
            selectionOutline.enabled = false;
        }
    }

    public void OffLights()
    {
        if (selectionOutline != null && currentlySelected)
        {
            currentlySelected = false;
            selectionOutline.enabled = false;
        }
    }

    public void MoveOrder(Vector3 position)
    {
        if (Vector3.Distance(position, this.transform.position) > walkRange && !health.isDead && !isStagnate)
        {
            Vector3 newPos;

            if(WalkablePoint(position, 10, out newPos))
            {
                MoveOrderState(); //CMD
                atkOrderInMotion = false;
                movOrderInMotion = true;
                movAvoidInMotion = false;

                if (navMeshAgent.enabled == false)
                {
                    navMeshAgent.enabled = true;
                }
                navMeshAgent.isStopped = false;
                movOrdPos = newPos;
                navMeshAgent.SetDestination(newPos);
            }
            else
            {
                Debug.LogError("Unable to walk there ser");
            }
        }
    }

    public void AttackOrder(GameObject getTarget) //CMD
    {
        if (!health.isDead && !isStagnate)
        {
            //RpcAttackOrder(getTarget);
            target = getTarget.transform;
            rSizeTrg = getTarget.GetComponent<Health>().sizeRange;
            atkOrderInMotion = false;
            movOrderInMotion = false;
            navMeshAgent.isStopped = false;
            AttackOrdState(); //CMD
        }
    }

    public void AttackMoveOrder(Vector3 position) //CMD
    {
        //RpcAttackMoveOrder(position);
        if (Vector3.Distance(position, this.transform.position) > walkRange && !health.isDead && !isStagnate)
        {
            Vector3 newPos;
            if (WalkablePoint(position, 10, out newPos))
            {
                atkOrderInMotion = true;
                movOrderInMotion = false;
                AtkMoveState(); //
                if (navMeshAgent.enabled == false)
                {
                    navMeshAgent.enabled = true;
                }
                navMeshAgent.isStopped = false;
                atkOrdPos = newPos;
                navMeshAgent.SetDestination(newPos);
            }
            else
            {
                Debug.LogError("Can't Attack there ser");
            }
        }
    }

    public void MovePatch(Vector3 position)
    {
        if (Vector3.Distance(position, this.transform.position) > walkRange && !health.isDead)
        {
            Vector3 newPos;
            if (WalkablePoint(position, 10, out newPos))
            {
                if (navMeshAgent.enabled == false)
                {
                    navMeshAgent.enabled = true;
                }
                navMeshAgent.isStopped = false;

                navMeshAgent.SetDestination(newPos);
                movOrdAvoid = newPos;
            }
            else
            {
                Debug.LogError("Can't go round there ser");
            }
        }
    }

    protected void GoToTarger(Vector3 ChaseTrg) //CMD
    {
        if(!health.isDead)
        {
            //RpcGoToTarger(ChaseTrg);
            navMeshAgent.SetDestination(ChaseTrg);
        }
    }

    public virtual void Attack(Vector2Int amount, GameObject trg)// CMD
    {
        if (!health.isDead)
        {
            Health enem = trg.GetComponent<Health>();
            float angle = Quaternion.Angle(transform.rotation, trg.transform.rotation);
            int hitAngle;
            //        Debug.Log("Angle:" + angle);

            if (angle > 120)//Front
            {
                hitAngle = 0;
            }
            else if (angle <= 120 && angle > 60)//Side
            {
                hitAngle = 1;

            }
            else if (angle <= 60)//Behind
            {
                hitAngle = 2;

            }
            else//Front
            {
                hitAngle = 0;
            }

            if (enem != null && !enem.isDead)
            {
                StartCoroutine(AtkDelay(0.5f, amount, hitAngle, enem));
                cur_Stamina -= cost_Stamina;
                StrikeAnim();
            }
            else
            {
                target = null;
                IdleState();
                isAttacking = false;
            }
        }
    }

    protected IEnumerator AtkDelay(float delayT, Vector2Int amount, int anl, Health trg)
    {
        yield return new WaitForSeconds(delayT);
        if (trg != null && !trg.isDead)
        {
            trg.TakeDamage(amount.x, amount.y, anl, idUnit);
        }
        else
        {
            target = null;
            IdleState();
            isAttacking = false;
        }
    }

    public virtual void StrikeAnim()   //RPC
    {
        audioS.pitch = Random.Range(0.92f, 1.05f);
    }

    public virtual void GetPushed(Transform sh, float hi)
    {
        StartCoroutine(Shove(sh));
        StartCoroutine(HitUp(hi, 0.7f));
    }

    private IEnumerator Shove(Transform shover)
    {
        isShoved = true;
        Vector3 velref = Vector3.zero;
        anim.SetTrigger("Fall");
        anim.SetBool("Falling", true);
        float countDown = 18;
        float timr = 0f;
        float startTime = Time.time;
        Vector3 Dir = (transform.position - shover.position).normalized;
        while (countDown > 0)
        {
            if(timr < 0.2f)
            {
                timr += 0.01f;
            }
            if (!isShoved)
            {
                countDown -= 0.6f;
                transform.position = Vector3.SmoothDamp(transform.position, (transform.position + (Dir * 1.1f)), ref velref, (0.05f + timr));

            }
            else
            {
                countDown -= 0.2f;
                transform.position = Vector3.SmoothDamp(transform.position, (transform.position + (Dir * 1.8f)), ref velref, (0.05f + timr));
            }


            yield return new WaitForEndOfFrame(); //Needs to be sec, endofframe causes issues when you have diffrent fps
        }
        yield break;
    }

    private IEnumerator HitUp(float hig ,float tim)
    {
        Vector3 sunrise = new Vector3(0, 0, 0);
        Vector3 sunset = new Vector3(0, hig, 0);
        Vector3 center = (sunrise + sunset) * 0.5F;
        CanvasLookAt cla = GetComponentInChildren<CanvasLookAt>();
        Transform can = cla.gameObject.transform;

        float elatim = 0;

        while (elatim < tim)
        {
            Vector3 riseRelCenter = sunrise - center;
            Vector3 setRelCenter = sunset - center;

            Vector3 newVek = Vector3.Slerp(riseRelCenter, setRelCenter, (elatim / tim));
            float newVekY = newVek.y;
            elatim += Time.deltaTime;
            transMdl.localPosition = new Vector3(0, newVekY, 0);
            transMdl.localPosition += center;
            can.localPosition = new Vector3(0, (transMdl.localPosition.y + 2), 0);

            yield return new WaitForEndOfFrame(); //Needs to be sec, endofframe causes issues when you have diffrent fps
        }

        yield return new WaitForSeconds(0.1f);
        elatim = 0;

        while (elatim < (tim + tim))
        {

            Vector3 riseRelCenter = sunrise - center;
            Vector3 setRelCenter = sunset - center;

            Vector3 newVek = Vector3.Slerp(setRelCenter, riseRelCenter, (elatim / (tim + tim)));
            float newVekY = newVek.y;
            tim -= 0.01f;
            elatim += Time.deltaTime;
            transMdl.localPosition = new Vector3(0, newVekY, 0);
            transMdl.localPosition += center;
            can.localPosition = new Vector3(0, (transMdl.localPosition.y + 2), 0);

            yield return new WaitForEndOfFrame(); //Needs to be sec, endofframe causes issues when you have diffrent fps
        }
        transMdl.localPosition = new Vector3(0,0,0);
        anim.SetTrigger("HitGround");

        if(isShoved)
        {
            isShoved = false;
        }
        //yield return new WaitForSeconds(0.1f);
        //StopCoroutine(Shove);

        yield return new WaitForSeconds(4f);
        if (!health.isDead)
        {
            anim.SetBool("Falling", false);
            IdleState();
        }

        yield break;
    }

    #endregion

    #region States
    public void IdleState() //CMD
    {
        // RPC Idle
        currentstate = UnitState.Idle;
        navMeshAgent.isStopped = true; //Can only be called on active agent on an navmesh
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        anim.SetInteger("State", 0);
        anim.speed = animSpeed;

        isIdle = true;
        isMoveOrder = false;
        isAttackOrder = false;
        isMoveAttackOrder = false;
        isChasing = false;
        isAttacking = false;
        isStagnate = false;

        if(atkOrderInMotion)
        {
            AttackMoveOrder(atkOrdPos);
        }
        //Check of heavy and tierd unit is and put correct anim speed
    }

    public void MoveOrderState() //CMD
    {
        //  RpcMoveOrdState();
        currentstate = UnitState.MoveOrder;
        navMeshAgent.isStopped = false;
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
        anim.SetInteger("State", 0);

        isIdle = false;
        isMoveOrder = true;
        isAttackOrder = false;
        isMoveAttackOrder = false;
        isChasing = false;
        isAttacking = false;
        isStagnate = false;
        movAvoidInMotion = false;
        //Check of heavy and tierd unit is and put correct anim speed
    }

    public void AttackOrdState() //CMD
    {
        //  RpcAttackOrdState();
        currentstate = UnitState.AttackOrder;
        navMeshAgent.isStopped = false;
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
        anim.SetInteger("State", 1);

        isIdle = false;
        isMoveOrder = false;
        isAttackOrder = true;
        isMoveAttackOrder = false;
        isChasing = false;
        isAttacking = false;
        isStagnate = false;
        movAvoidInMotion = false;
        //Check of heavy and tierd unit is and put correct anim speed
    }

    public void AtkMoveState() //CMD
    {
        //RpcAtkMoveState();
        currentstate = UnitState.MoveAttackOrder;
        navMeshAgent.isStopped = false;
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
        anim.SetInteger("State", 0);

        isMoveAttackOrder = true;
        isIdle = false;
        isMoveOrder = false;
        isAttackOrder = false;
        isChasing = false;
        isAttacking = false;
        isStagnate = false;
        movAvoidInMotion = false;
        //Check of heavy and tierd unit is and put correct anim speed
    }

    public void ChaseState(GameObject ChaseTrg) //CMD
    {
        //RpcChase(ChaseTrg);
        if (ChaseTrg != null)
        {
            GoToTarger(ChaseTrg.transform.position); //CMD
            rSizeTrg = ChaseTrg.GetComponent<Health>().sizeRange;
        }

        navMeshAgent.isStopped = false;
        currentstate = UnitState.Chase;
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        isChasing = true;
        anim.SetInteger("State", 1);

        isIdle = false;
        isMoveOrder = false;
        isMoveAttackOrder = false;
        isAttackOrder = false;
        isAttacking = false;
        isStagnate = false;
    }

    public void AttackState() //CMD
    {
        //RpcAttack();
        currentstate = UnitState.Attack;
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        anim.SetInteger("State", 1);
        anim.speed = animSpeed;

        isIdle = false;
        isMoveOrder = false;
        isMoveAttackOrder = false;
        isAttackOrder = false;
        isChasing = false;
        isAttacking = true;
        isStagnate = false;
    }

    public void Stagnate() //CMD
    {
        currentstate = UnitState.Stagnate;
        navMeshAgent.isStopped = true;
        target = null;
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        anim.speed = animSpeed;
        //navMeshAgent.enabled = false;

        isIdle = false;
        isMoveOrder = false;
        isAttackOrder = false;
        isMoveAttackOrder = false;
        isChasing = false;
        isAttacking = false;
        isStagnate = true;
        movAvoidInMotion = false;
    }
    #endregion

    #region Update Functions
    protected void Func_Idle()
    {
        secLeft -= Time.deltaTime;
        if (secLeft <= 0)
        {
            FindNearestEnemie();
            secLeft = secLook;
        }

        rts -= Time.deltaTime;
        if (rts <= 0)
        {
            rts = regenTime;
            GainStamina(reg_Stamina.GetValue());

            if(health.Cur_Health < health.max_Health)
            {
                health.Heal(health.reg_Health.GetValue(), false);
            }
        }

        if (nearestEnemy != null && shortestDist <= Range)
        {
            ChaseState(nearestEnemy); //CMD
            isIdle = false;
        }
    }

    protected void Func_MoveOrd()
    {
        if (movAvoidInMotion)
        {
            float distanceToDest1 = Vector3.Distance(movOrdAvoid, transform.position);

            if (distanceToDest1 <= 1)
            {
                movAvoidInMotion = false;
                MovePatch(movOrdPos); //CMD
            }
        }
        else
        {
            float distanceToDest = Vector3.Distance(movOrdPos, transform.position);
            //Debug.Log(distanceToDest);
            if (distanceToDest <= 1)
            {
                movOrderInMotion = false;
                isMoveOrder = false;
                IdleState(); //CMD
            }

            if (velo < 1f)
            {
                tmAntChk -= Time.deltaTime;

                if (tmAntChk < 0 && isAntenaCK == false)
                {
                    isAntenaCK = true;
                    Antenna();
                }
            }
        }
    }

    protected virtual void Func_AtkMoveOrd()
    {
        secLeft -= Time.deltaTime;
        if (secLeft <= 0)
        {
            FindNearestEnemie();
            secLeft = secLook;
        }
        //FuNe();

        if (movAvoidInMotion)
        {
            float distanceToDest1 = Vector3.Distance(movOrdAvoid, transform.position);

            if (distanceToDest1 <= 1)
            {
                movAvoidInMotion = false;
                AttackMoveOrder(atkOrdPos); //CMD
            }
        }
        else
        {
            if (nearestEnemy != null && shortestDist <= Range)
            {
                ChaseState(nearestEnemy); //CMD
                isMoveAttackOrder = false;
            }

            float distanceToDest = Vector3.Distance(atkOrdPos, transform.position);

            if (distanceToDest <= 1)
            {
                isMoveAttackOrder = false;
                atkOrderInMotion = false;
                IdleState(); //CMD
            }

            if (velo < 1)
            {
                tmAntChk -= Time.deltaTime;

                if (tmAntChk < 0 && isAntenaCK == false)
                {
                    isAntenaCK = true;
                    Antenna();
                }
            }
        }
    }

    public virtual void Func_AttackOrd()
    {
        if (movAvoidInMotion)
        {
            float distanceToDest1 = Vector3.Distance(movOrdAvoid, transform.position);

            if (distanceToDest1 <= 1)
            {
                movAvoidInMotion = false;
                if (target != null)
                {
                    GoToTarger(target.position); //CMD
                }
                else
                {
                    IdleState(); //CMD
                }
            }
        }
        else
        {
            if (target != null)
            {
                if (target.GetComponent<Health>().isDead)
                {
                    target = null;
                    return;
                }

                GoToTarger(target.position); //CMD

                float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);

                if (distanceToEnemy - rSizeTrg <= AttackRange && target)
                {
                    AttackState(); //CMD
                    isAttackOrder = false;
                }

                if (velo < 1)
                {
                    tmAntChk -= Time.deltaTime;

                    if (tmAntChk < 0 && isAntenaCK == false)
                    {
                        isAntenaCK = true;
                        Antenna();
                    }
                }
            }
            else
            {
                isAttackOrder = false;
                IdleState(); //CMD
            }
        }
    }

    public virtual void Func_Chase()
    {
        secLeft -= Time.deltaTime;
        if (secLeft <= 0)
        {
            FindNearestEnemie();
            secLeft = secLook;
        }

        if (movAvoidInMotion)
        {
            float distanceToDest1 = Vector3.Distance(movOrdAvoid, transform.position);

            if (distanceToDest1 <= 1)
            {
                movAvoidInMotion = false;
                if (nearestEnemy != null && shortestDist <= Range)
                {
                    ChaseState(nearestEnemy); //CMD
                }
                else
                {
                    IdleState(); //CMD
                }
            }
        }
        else
        {
            if (nearestEnemy != null && shortestDist <= Range)
            {
                target = nearestEnemy.transform;
            }

            if (target != null)
            {
                GoToTarger(target.position); //CMD

                if (velo < 1)
                {
                    tmAntChk -= Time.deltaTime;

                    if (tmAntChk < 0 && isAntenaCK == false)
                    {
                        isAntenaCK = true;
                        Antenna();
                    }
                }

                float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);

                if (distanceToEnemy - rSizeTrg <= AttackRange && target)
                {
                    AttackState();  //CMD
                    isChasing = false;
                }
                else if (distanceToEnemy >= Range)
                {
                    FindNearestEnemie();
                    if (nearestEnemy != null && shortestDist <= Range)
                    {
                        target = nearestEnemy.transform;
                    }
                    else
                    {
                        isChasing = false;
                        if (atkOrderInMotion)
                        {
                            AttackMoveOrder(atkOrdPos); //CMD
                        }
                        else
                        {
                            IdleState(); //CMD
                        }
                    }
                }
            }
            else
            {
                FindNearestEnemie(); // DO YOU NEED THIS?
                if (nearestEnemy != null && shortestDist <= Range)
                {
                    target = nearestEnemy.transform;
                }
                else
                {
                    isChasing = false;
                    if (atkOrderInMotion)
                    {
                        AttackMoveOrder(atkOrdPos); //CMD
                    }
                    else
                    {
                        IdleState();  //CMD
                    }
                }
            }
        }
    }

    protected void Func_Attack()
    {
        if (cooldownAtk > 0)
        {
            cooldownAtk -= Time.deltaTime;
        }
        else if (cooldownAtk < 0)
        {
            cooldownAtk = 0f;
        }

        if (target != null)
        {
            Func_AtkTrg();
        }
        else
        {
            FindNearestEnemie();
            //FuNe();
            if (nearestEnemy != null && shortestDist < AttackRange)
            {
                target = nearestEnemy.transform;
            }
            else if (nearestEnemy != null && shortestDist < Range)
            {
                ChaseState(nearestEnemy);  //CMD
                isAttacking = false;
            }
            else
            {
                if (atkOrderInMotion)
                {
                    AttackMoveOrder(atkOrdPos);  //CMD
                }
                else
                {
                    IdleState();  //CMD
                }
                isAttacking = false;
            }
        }
    }

    protected virtual void Func_AtkTrg()
    {
        float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (distanceToEnemy - rSizeTrg < AttackRange)
        {
            AttckFunc();
        }
        else if (distanceToEnemy - rSizeTrg > AttackRange && distanceToEnemy < Range)
        {
            ChaseState(target.gameObject);  //CMD
            isAttacking = false;
        }
        else
        {
            IdleState();  //CMD
            isAttacking = false;
        }
    }

    protected virtual void AttckFunc()
    {
        if (cooldownAtk <= 0)
        {
            atkSpd = 100f / attackSpeed.GetValue();

            bool p = CheckStaminaBool();
            if (p)
            {
                cooldownAtk = (atkSpd * 2.5f);
            }
            else
            {
                cooldownAtk = atkSpd;
            }

            Health hl = target.GetComponent<Health>();
            if (target != null && !hl.isDead)
            {
                Vector2Int dmgAmt = new Vector2Int(attackDamage.GetValue(), armorPercing.GetValue());
                Attack(dmgAmt, target.gameObject); //CMD
            }
            else
            {
                FindNearestEnemie();
            }
        }
    }

    protected void FuNe()
    {
        if (clickRef != null)
        {
            //teamUnits = clickRef.selectableObjects;
            enemies = clickRef.EnemyObjects;
        }

        if (target != null)
        {
            if (target.GetComponent<Health>().isDead)
            {
                target = null;
            }
        }

        nearestEnemy = null;
        shortestDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDist)
                {
                    shortestDist = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

        }
    }

    protected void FindNearestEnemie()
    {
        if (target != null)
        {
            if (target.GetComponent<Health>().isDead)
            {
                target = null;
            }
        }

        nearestEnemy = null;
        shortestDist = Mathf.Infinity;

        Collider[] hitColi = Physics.OverlapSphere(transform.position, Range, clickAbleLayer);
        if (hitColi.Length > 0)
        {
            int i = 0;
            while (i < hitColi.Length)
            {
                Health ene = hitColi[i].GetComponent<Health>();
                if (ene != null && !ene.isDead)
                {
                    if(ene.healthTeam != unitTeam)
                    {
                        float distance = Vector3.Distance(transform.position, ene.transform.position);
                        if (distance < shortestDist)
                        {
                            shortestDist = distance;
                            nearestEnemy = ene.gameObject;
                        }
                    }
                }
                i++;
            }
        }
        else
        {
            nearestEnemy = null; ;
        }
    }
    #endregion

    #region Avoidance

    bool WalkablePoint(Vector3 center, float range, out Vector3 result)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(center, out hit, range, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    public bool CanWalkablePoint(Vector3 center, float range)
    {
        NavMeshHit hit;
        float dist = Vector3.Distance(transform.position, center);
        if(dist > range)
        {
            return false;
        }

        if (NavMesh.SamplePosition(center, out hit, range, NavMesh.AllAreas))
        {
            return true;
        }
        return false;
    }

    protected void Antenna()
    {
        if (IsFowardClear() && IsSoftSidesClear())
        {
            isAntenaCK = false;
            tmAntChk = timeToCheckForObstacles;

            return;
        }
        else
        {
            if (!wasLastHitleft)
            {
                CheckLeftFirst();
            }
            else
            {
                CheckRightFirst();
            }
        }
    }

    void CheckLeftFirst()
    {
        if (IsLeftClear())
        {
            isAntenaCK = false;
            tmAntChk = timeToCheckForObstacles;

            Vector3 movPosis = transform.right * -4 + transform.position;
            movAvoidInMotion = true;

            MovePatch(movPosis);
            return;
        }
        else if (IsRightClear())
        {
            isAntenaCK = false;
            tmAntChk = timeToCheckForObstacles;

            Vector3 movPosis = transform.right * 4 + transform.position;
            movAvoidInMotion = true;

            MovePatch(movPosis);
        }
        else
        {
            isAntenaCK = false;
            tmAntChk = timeToCheckForObstacles;
        }
    }

    void CheckRightFirst()
    {
        if (IsRightClear())
        {
            isAntenaCK = false;
            tmAntChk = timeToCheckForObstacles;

            Vector3 movPosis = transform.right * 4 + transform.position;
            movAvoidInMotion = true;

            MovePatch(movPosis);
        }
        else if (IsLeftClear())
        {
            isAntenaCK = false;
            tmAntChk = timeToCheckForObstacles;

            Vector3 movPosis = transform.right * -4 + transform.position;
            movAvoidInMotion = true;

            MovePatch(movPosis);
            return;
        }
        else
        {
            isAntenaCK = false;
            tmAntChk = timeToCheckForObstacles;
        }
    }

    protected bool IsFowardClear()
    {
        RaycastHit rH;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast((transform.position + new Vector3(0, 0.5f, 0)), fwd, out rH, distanceAntenna))
        {
            //if (rH.collider.GetComponent<Unit>() != null)
            //{
            //}
            return false;
        }
        else
        {
            return true;
        }
    }

    protected bool IsClearWayToTarget(Transform targ)
    {
        RaycastHit rH;
        //Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 dir = targ.position - transform.position;

        float distTarg = Vector3.Distance(targ.position, transform.position);

        if (Physics.Raycast((transform.position + new Vector3(0, 0.5f, 0)), dir, out rH, distTarg))
        {
            if (rH.collider.transform != targ)
            {
                //Debug.Log(rH.collider.gameObject + "is not target for: ", this.gameObject);
                //if(rH.collider.gameObject == this.gameObject)
                //{
                //    Debug.LogError("Check yoself");
                //}
                return false;
            }
            else
            {
                //Debug.Log("HIT");
                return true;
            }
        }
        else
        {
            //Debug.Log("hit nothing");
            return false;
        }
    }

    protected bool IsFarFowardClear()
    {
        RaycastHit rH;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast((transform.position + new Vector3(0, 0.5f, 0)), fwd, out rH, 10))
        {
            if (rH.collider.GetComponent<Unit>() != null)
            {
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool IsSoftSidesClear()
    {
        RaycastHit rH;

        Vector3 lside1 = ((transform.forward * 2.5f) - transform.right) / 3;
        Vector3 rside1 = ((transform.forward * 2.5f) + transform.right) / 3;

        if (Physics.Raycast((transform.position + new Vector3(0, 0.5f, 0)), lside1, out rH, distanceAntenna))
        {
            //Debug.LogError("Hit Soft Side Left");
            return false;
        }
        else if (Physics.Raycast((transform.position + new Vector3(0, 0.5f, 0)), rside1, out rH, distanceAntenna))
        {
            //Debug.LogError("Hit Soft Side Right");
            return false;
        }
        else
        {
            return true;
        }


    }

    private bool IsLeftClear()
    {
        RaycastHit rH;

        Vector3 lside3 = (transform.forward - (transform.right * 2.5f)) / 3;
        Vector3 lside = -transform.right;

        if (Physics.Raycast((transform.position + new Vector3(0, 0.5f, 0)), lside3, out rH, distanceAntenna))
        {
            if (!wasLastHitleft)
            {
                wasLastHitleft = true;
            }
            //Debug.LogError("Hit Hard Left");
            return false;
        }
        else if (Physics.Raycast((transform.position + new Vector3(0, 0.5f, 0)), lside, out rH, distanceAntenna))
        {
            if (!wasLastHitleft)
            {
                wasLastHitleft = true;
            }
            //Debug.LogError("Hit Straight Left");
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool IsRightClear()
    {
        RaycastHit rH;

        Vector3 rside3 = (transform.forward + (transform.right * 2.5f)) / 3;
        Vector3 rside = transform.right;

        if (Physics.Raycast((transform.position + new Vector3(0, 0.5f, 0)), rside3, out rH, distanceAntenna))
        {
            if (wasLastHitleft)
            {
                wasLastHitleft = false;
            }
            //Debug.LogError("Hit Hard Right");
            return false;
        }
        else if (Physics.Raycast((transform.position + new Vector3(0, 0.5f, 0)), rside, out rH, distanceAntenna))
        {
            if (wasLastHitleft)
            {
                wasLastHitleft = false;
            }
            //Debug.LogError("Hit Straight Right");
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion

# region Gizmos
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);

        Gizmos.color = Color.blue;
        Vector3 fwd = transform.TransformDirection(Vector3.forward * 2f);
        Gizmos.DrawRay(transform.position, fwd);

        Gizmos.color = Color.magenta;
        Vector3 Rside1 = (((transform.forward * 2.5f) + transform.right) / 3);
        Gizmos.DrawRay(transform.position, Rside1);

        Gizmos.color = Color.yellow;
        Vector3 Lside1 = (((transform.forward * 2.5f) - transform.right) / 3);
        Gizmos.DrawRay(transform.position, Lside1);


        Gizmos.color = Color.magenta;
        Vector3 Right2 = ((transform.forward + transform.right) / 1.5f);
        Gizmos.DrawRay(transform.position, Right2);

        Gizmos.color = Color.cyan;
        Vector3 Left2 = ((transform.forward - transform.right) / 1.5f);
        Gizmos.DrawRay(transform.position, Left2);


        Gizmos.color = Color.magenta;
        Vector3 Right3 = ((transform.forward + (transform.right * 2.5f)) / 3);
        Gizmos.DrawRay(transform.position, Right3);

        Gizmos.color = Color.white;
        Vector3 Left3 = ((transform.forward - (transform.right * 2.5f)) / 3);
        Gizmos.DrawRay(transform.position, Left3);

        Gizmos.color = Color.magenta;
        Vector3 Right = transform.right;
        Gizmos.DrawRay(transform.position, Right);

        Gizmos.color = Color.green;
        Vector3 Left = -transform.right;
        Gizmos.DrawRay(transform.position, Left);
    }
    #endregion 
}
