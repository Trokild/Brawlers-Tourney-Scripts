using System.Collections;
using UnityEngine;

public class Infantry : Unit
{
    [Header("__Infantry__")]
    private bool superChargeInMotion = false;
    public bool superCharger;
    public Stat chargeDamage;
    public int weaponInt { get; private set; }
    public AudioClip[] weaponSound;

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

    public override void Func_AttackOrd()
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

                if (superCharger && !superChargeInMotion)
                {
                    if (distanceToEnemy < 10 && distanceToEnemy > 6)
                    {
                        if (IsClearWayToTarget(target))
                        {
                            StartCoroutine(SuperCharge(target, 0.5f));
                        }
                    }
                }

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

    public override void Func_Chase()
    {
        //FuNe();
        secLeft -= Time.deltaTime;
        if (secLeft <= 0)
        {
            FindNearestEnemie();
            secLeft = secLook;
        }

        if (movAvoidInMotion)
        {
            float distanceToDest1 = Vector3.Distance(movOrdAvoid, transform.position);

            if(nearestEnemy != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);

                if (distanceToEnemy - rSizeTrg <= AttackRange && target)
                {
                    if(target == null)
                    {
                        target = nearestEnemy.transform;
                    }
                    AttackState();  //CMD
                    movAvoidInMotion = false;
                    isChasing = false;
                }
            }

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

                if (superCharger && !superChargeInMotion)
                {
                    if (distanceToEnemy < 10 && distanceToEnemy > 5 && velo > 4.2f)
                    {
                        //float angle = Quaternion.Angle(transform.rotation, target.rotation); Does not work because the it calc the
                        if (IsClearWayToTarget(target)) // IsFarFowardClear() &&                          angle diffrence between .this and target.
                        {                                                                //    not if .this is facing the target
                            StartCoroutine(SuperCharge(target, 0.5f));
                        }
                        else
                        {
                            //Debug.Log("ForwardNotClear");
                        }
                    }
                    //else if(distanceToEnemy < 11)
                    //{
                    //    Debug.Log("Dist andor Velo wrong Dist:" + distanceToEnemy + " Velo:" + velo);
                    //}
                }

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

    private IEnumerator SuperCharge(Transform targt, float secs)
    {
        if (!superChargeInMotion && !health.isDead)
        {
            Unit un = target.gameObject.GetComponent<Unit>();
            if (un != null && un.velo > 3)
            {
                yield break;
            }

            superChargeInMotion = true;
            anim.SetTrigger("Charge");

            float elatim = 0;
            Vector3 Dir = (targt.position - transform.position).normalized;
            Vector3 startp = this.transform.position;

            while (elatim < secs)
            {
                transform.position = Vector3.Lerp(startp, (targt.position - (Dir * (2.5f + rSizeTrg))), (elatim / secs));
                elatim += Time.deltaTime;
                yield return new WaitForEndOfFrame(); //Needs to be sec, endofframe causes issues when you have diffrent fps
            }

            //this.transform.position = (targt.position - (Dir * (2.3f + rSizeTrg)));
            float dist = Vector3.Distance(transform.position, target.position);

            if (dist < 3.71f) // also check if unit is facing the enemie
            {
                if (!health.isDead)
                {
                    Health enem = target.GetComponent<Health>();
                    float angle = Quaternion.Angle(transform.rotation, target.transform.rotation);
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
                      //  Debug.Log("Chardge dumg "+ dist, gameObject);
                        enem.TakeDamage((attackDamage.GetValue() + chargeDamage.GetValue()), armorPercing.GetValue(), hitAngle, idUnit);
                    }
                    else
                    {
                     //   Debug.Log("Chardge isDead");
                    }
                }
            }
            else
            {
              //  Debug.Log("Chardge " + dist);
            }
            superChargeInMotion = false;
        }
    }

    public override void StrikeAnim()
    {
        int randNum = Random.Range(1, 3);
        anim.SetTrigger("Attack" + randNum);
        audioS.pitch = Random.Range(0.92f, 1.05f);

        switch(weaponInt)
        {
            case 0:
                audioS.PlayOneShot(weaponSound[0]);
                break;

            case 1:
                audioS.PlayOneShot(weaponSound[1]);
                break;

            case 2:
                audioS.PlayOneShot(weaponSound[1]);
                break;

            case 3:
                audioS.PlayOneShot(weaponSound[2]);
                break;

            case 4:
                audioS.PlayOneShot(weaponSound[2]);
                break;

            case 5:
                audioS.PlayOneShot(weaponSound[3]);
                break;

            case 6:
                audioS.PlayOneShot(weaponSound[3]);
                break;
        }
    }
}
