using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavalry : Unit
{
    [Header("__Cavalry__")]
    public bool canCharge;
    public Stat chargeDamage;
    public int weaponInt { get; private set; }
    public AudioClip[] weaponSound;
    [SerializeField] private Vector3 sizeBoxChase;
    [Range(2, 10)]
    [SerializeField] private float minSpdChg;

    public void SetUpStatsUnit(StatWeapon st, Vector3Int ssw, int chg)
    {
        attackDamage.SetBaseValue(st.weaponDamage);
        armorPercing.SetBaseValue(st.armorPercing);
        attackSpeed.SetBaseValue(st.attackSpeed);

        max_Stamina = ssw.x;
        cur_Stamina = max_Stamina;
        strenght.SetBaseValue(ssw.y);
        weightCarry = ssw.z;
        SetUpStamina(strenght.GetBaseValue(), weightCarry);
        chargeDamage.SetBaseValue(chg);
        chargeDamage.AddModifier(st.chargeDamage);

        weaponInt = st.statInt;
    }

    public void SetSpeed(Vector2 spd)
    {
        navMeshAgent.speed += spd.x;
        navMeshAgent.speed += spd.y;
    }

    public override void Func_Chase()
    {
        FuNe();

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

                    if (canCharge)
                    {
                        if (velo >= minSpdChg)
                        {
                            Unit un = target.GetComponent<Unit>();
                            if (un != null && !un.CheckStagnate)
                            {
                                if (un.currentUnitType ==  UnitType.Infantry || un.currentUnitType == UnitType.Archer)
                                {
                                    
                                    un.GetPushed(this.transform, 2f);
                                    un.Stagnate();

                                    Health ht = target.GetComponent<Health>();
                                    if (ht != null && !ht.isDead)
                                    {
                                        float angle = Quaternion.Angle(transform.rotation, target.rotation);
                                        int hitAngle;

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

                                        // IF UNIT IS INRAGE OF ATTACK
                                        Vector3 multi = new Vector3(0.5f, 0.5f, 0.5f);
                                        navMeshAgent.velocity = Vector3.Scale(navMeshAgent.velocity, multi);
                                        ht.TakeDamage((attackDamage.GetValue() + chargeDamage.GetValue()), armorPercing.GetValue(), hitAngle, idUnit);
                                    }
                                }
                            }
                            else
                            {
                                Debug.LogError("can't find <Unit> or is stagnate");
                            }
                        }
                    }
                    isChasing = false;
                }
                else if (distanceToEnemy >= Range)
                {
                    FuNe();
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
                FuNe(); // DO YOU NEED THIS?
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

    protected override void Func_AtkMoveOrd()
    {
        secLeft -= Time.deltaTime;
        if (secLeft <= 0)
        {
            FindEnemieCharge();
            secLeft = secLook;
        }

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

    public override void StrikeAnim()
    {
        int randNum = Random.Range(1, 3);
        anim.SetTrigger("Attack" + randNum);
        switch (weaponInt)
        {
            case 0:
                audioS.PlayOneShot(weaponSound[0]);
                audioS.pitch = Random.Range(0.92f, 1.05f);
                break;

            case 1:
                audioS.PlayOneShot(weaponSound[1]);
                audioS.pitch = Random.Range(0.92f, 1.05f);
                break;

            case 2:
                audioS.PlayOneShot(weaponSound[0]);
                audioS.pitch = Random.Range(0.92f, 1.05f);
                break;

            case 3:
                audioS.PlayOneShot(weaponSound[0]);
                audioS.pitch = Random.Range(1f, 1.1f);
                break;


        }
    }

    public override void GetPushed(Transform sh, float hi)
    {
        Debug.LogError("Cannot push cavalry");
    }

    void FindEnemieCharge()
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

        Collider[] hitColi = Physics.OverlapBox((transform.position + new Vector3(0, 0, 6)), sizeBoxChase, transform.rotation , clickAbleLayer);

        //Debug.Log(hitColi);
        if (hitColi.Length > 0)
        {
            int i = 0;
            while (i < hitColi.Length)
            {
                Health ene = hitColi[i].GetComponent<Health>();
                if (ene != null && !ene.isDead)
                {
                    if (ene.healthTeam != unitTeam)
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
}
