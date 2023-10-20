using UnityEngine;

public class Thrower : Unit
{
    [Space]
    [SerializeField] private RockProjectile rockProjectile;

    public Stat attackDmgRock;
    public Stat armorPrcRock;
    public Stat attackSpdRock;

    [SerializeField]
    private float meeleRange = 2f;
    private bool hasMelee = false;
    //public bool isTest = false;
    //public GameObject testTrg;

    //private void Start()
    //{
    //    if (isTest)
    //    {
    //        InvokeRepeating("TestThrow", 2, 4);
    //    }
    //}

    //void TestThrow()
    //{
    //    Vector2Int rand123 = new Vector2Int(1, 0);
    //    Throw(rand123, testTrg);
    //}

    public override void StrikeAnim()
    {
        anim.SetTrigger("Attack1");
        base.StrikeAnim();
    }

    void ThrowAnim()
    {
        anim.SetTrigger("Attack2");
        base.StrikeAnim();
    }

    public void Throw(Vector2Int amount, GameObject trg)// CMD
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
                float dist = Vector3.Distance(this.transform.position, trg.transform.position);

                if (dist > 12)
                {
                    StartCoroutine(AtkDelay(1.1f, amount, hitAngle, enem));
                    rockProjectile.FireProjectile(trg, 1.1f, 4);
                    ThrowAnim();
                    //Debug.Log("LONGSHOT");
                }
                else if (dist <= 12 && dist - rSizeTrg > meeleRange)
                {
                    StartCoroutine(AtkDelay(0.75f, amount, hitAngle, enem));
                    rockProjectile.FireProjectile(trg, 0.75f, 2.5f);
                    ThrowAnim();
                    //Debug.Log("MIDSHOT dist:" + dist + " - rSizeTrg:" + rSizeTrg + " is " + (dist - rSizeTrg) + ", Melee range is:" + meeleRange);
                }
                else
                {
                    Vector2Int dmgMelee = new Vector2Int(attackDamage.GetValue(), armorPercing.GetValue());
                    StartCoroutine(AtkDelay(0.1f, dmgMelee, hitAngle, enem));
                    //rockProjectile.FireProjectile(trg, 0.15f, 0);
                    Debug.Log("Melee Throw?!");
                    StrikeAnim();
                }
            }
            else
            {
                target = null;
                IdleState();
                isAttacking = false;
            }
        }
    }

    //Maby add a function to look for enemies in throwing range
    protected override void Func_AtkTrg()
    {
        float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (distanceToEnemy - rSizeTrg < AttackRange)
        {
            if(distanceToEnemy - rSizeTrg < meeleRange)
            {
                if (hasMelee)
                {
                    AttckFunc();
                }
            }
            else
            {
                AttckFuncThrow();
            }
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

    void AttckFuncThrow()
    {
        if (cooldownAtk <= 0)
        {
            atkSpd = 100f / attackSpdRock.GetValue();

            bool p = CheckStaminaBool();
            if (p)
            {
                cooldownAtk = (atkSpd * 3f);
            }
            else
            {
                cooldownAtk = atkSpd;
            }

            Health hl = target.GetComponent<Health>();
            if (target != null && !hl.isDead)
            {
                Vector2Int dmgAmt = new Vector2Int(attackDmgRock.GetValue(), armorPrcRock.GetValue());
                Throw(dmgAmt, target.gameObject); //CMD
            }
            else
            {
                FindNearestEnemie();
            }
        }
    }

    public void SetUpStatsThrow(StatRock rock)
    {
        attackDmgRock.SetBaseValue(rock.weaponDamage);
        armorPrcRock.SetBaseValue(rock.armorPercing);
        attackSpdRock.SetBaseValue(rock.attackSpeed);
        AttackRange = rock.range;
    }

    void SetRange(float rng, bool set)
    {
        if(set)
        {

        }
        else
        {
            //AttackRange += bonus;
        }     
    }

    public void SetUpStatsMelee(StatWeapon wep)
    {
        attackDamage.SetBaseValue(wep.weaponDamage);
        armorPercing.SetBaseValue(wep.armorPercing);
        attackSpeed.SetBaseValue(wep.attackSpeed);
    }

    public void SetUpStats(Vector3Int ssw, bool melee)
    {
        hasMelee = melee;
        max_Stamina = ssw.x;
        cur_Stamina = max_Stamina;
        strenght.SetBaseValue(ssw.y);
        weightCarry = ssw.z;
        SetUpStamina(strenght.GetBaseValue(), weightCarry);
    }
}
