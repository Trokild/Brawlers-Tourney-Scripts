using UnityEngine;

public class Hero : Unit
{
    private bool isRotateSpell = false;
    private Vector3 rotatePos;
    private bool hasWeapon;
    private HeroInventory inventory;

    protected override void Update()
    {
        velo = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;

        float procentSpeed = navMeshAgent.velocity.magnitude / baseSpeed;
        anim.SetFloat("speedPercent", procentSpeed, .1f, Time.deltaTime);

        switch (currentstate)
        {
            case UnitState.Idle:
                if (isIdle == true)
                {
                    Func_Idle();
                    Range = 8f;
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
                    Range = 20f;
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

            case UnitState.Stagnate:

                if (isStagnate)
                {
                    if(isRotateSpell)
                    {
                        Vector3 dir = rotatePos - transform.position;
                        if(dir != Vector3.zero)
                        {
                            Quaternion lookRotation = Quaternion.LookRotation(dir);
                            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5).eulerAngles;
                            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
                        }
                    }
                }
                break;
        }
    }

    protected override void AttckFunc()
    {
        if (cooldownAtk <= 0)
        {
            atkSpd = 100f / attackSpeed.GetValue();

            bool p = CheckStaminaBool();
            if (p)
            {
                cooldownAtk = (atkSpd * 1.5f);
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
                if(hasWeapon) //No Animation Delay
                {
                    UnitHealth unhl = hl as UnitHealth;
                    if(unhl != null)
                    {
                        Weapon wep = inventory.WeaponSlot;
                        if (wep != null)
                        {
                            int prc = Random.Range(0, 100);
                            if(prc < wep.Proc)
                            {
                                Buff_Affliction wepBuff = inventory.WeaponSlot.BuffWeapon;
                                if (wepBuff.DeBuff)
                                {
                                    unhl.TakeBuff(wepBuff);
                                }
                                else
                                {
                                    health.TakeBuff(wepBuff);
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("Empty inventory slot");
                            hasWeapon = false;
                        }
                    }
                    else
                    {
                        Debug.Log("Attacking Building?");
                    }
                }
            }
            else
            {
                FindNearestEnemie();
            }
        }
    }

    public void HeroStart(int team, int id)
    {
        unitTeam = team;
        idUnit = id;
        cur_Stamina = max_Stamina;
        inventory = GetComponent<HeroInventory>();
    }

    public void WeaponHero_Equipt()
    {
        if(inventory != null)
        {
            if(inventory.WeaponSlot != null)
            {
                hasWeapon = true;
            }
            else
            {
                hasWeapon = false;
            }
        }
        else
        {
            Debug.LogError("No HeroInventory");
            hasWeapon = false;
        }
    }

    public void WeaponHero_Unequipt()
    {
        hasWeapon = false;
    }

    public override void StrikeAnim()
    {
        int randNum = Random.Range(1, 4);
        anim.SetTrigger("Attack" + randNum);

        //audio missing
    }

    public void RotateToSpell(Vector3 posR)
    {
        Stagnate();
        isRotateSpell = true;
        rotatePos = posR;
    }

    public void RotateToSpellStop()
    {
        IdleState();
        isRotateSpell = false;
    }
}
