using UnityEngine;

public class Archer : Unit
{
    [Space]
    [SerializeField] private ArrowProjectile arrowProjectile;

    public override void Attack(Vector2Int amount, GameObject trg)// CMD
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

                if (dist > 10)
                {
                    StartCoroutine(AtkDelay(0.5f, amount, hitAngle, enem));
                    arrowProjectile.FireProjectile(trg, 0.5f, 3.5f);
                    //Debug.Log("LONGSHOT");
                }
                else if (dist <= 10 && dist > 4)
                {
                    StartCoroutine(AtkDelay(0.25f, amount, hitAngle, enem));
                    arrowProjectile.FireProjectile(trg, 0.25f, 2);
                    //Debug.Log("MIDSHOT");
                }
                else
                {
                    StartCoroutine(AtkDelay(0.1f, amount, hitAngle, enem));
                    arrowProjectile.FireProjectile(trg, 0.1f, 0);
                    //Debug.Log("ShotSHOT");
                }

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

    public override void StrikeAnim()
    {
        anim.SetTrigger("Attack");
        base.StrikeAnim();
    }

    public void SetUpStatsUnit(StatArrow arrow, StatBow bow, Vector3Int ssw)
    {
        attackDamage.SetBaseValue(arrow.weaponDamage);
        armorPercing.SetBaseValue(arrow.armorPercing);
        attackSpeed.SetBaseValue(bow.attackSpeed);
        AttackRange = bow.range;

        max_Stamina = ssw.x;
        cur_Stamina = max_Stamina;
        strenght.SetBaseValue(ssw.y);
        weightCarry = ssw.z;
        SetUpStamina(strenght.GetBaseValue(), weightCarry);
    }
}
