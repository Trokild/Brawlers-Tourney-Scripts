using UnityEngine;

public class Spearman : Unit {

    [Header("__Spearman__")]
    public Stat bonusCavalry;
    public int weaponInt { get; private set; }
    public AudioClip[] weaponSound;

    public void SetUpStatsUnit(StatSpear st, Vector3Int ssw)
    {
        attackDamage.SetBaseValue(st.weaponDamage);
        armorPercing.SetBaseValue(st.armorPercing);
        attackSpeed.SetBaseValue(st.attackSpeed);

        max_Stamina = ssw.x;
        cur_Stamina = max_Stamina;
        strenght.SetBaseValue(ssw.y);
        weightCarry = ssw.z;
        SetUpStamina(strenght.GetBaseValue(), weightCarry);
        bonusCavalry.SetBaseValue(st.bonusVsCav);

        weaponInt = st.statInt;
    }


    public override void StrikeAnim()
    {
        anim.SetTrigger("Attack");

        switch (weaponInt)
        {
            case 0:
                audioS.PlayOneShot(weaponSound[0]);
                audioS.pitch = Random.Range(1f, 1.1f);
                break;

            case 1:
                audioS.PlayOneShot(weaponSound[1]);
                audioS.pitch = Random.Range(0.92f, 1.05f);
                break;

            case 2:
                audioS.PlayOneShot(weaponSound[1]);
                audioS.pitch = Random.Range(0.92f, 1.05f);
                break;

            case 3:
                audioS.PlayOneShot(weaponSound[1]);
                audioS.pitch = Random.Range(1f, 1.1f);
                break;

            case 4:
                audioS.PlayOneShot(weaponSound[1]);
                audioS.pitch = Random.Range(1f, 1.1f);
                break;
        }
    }

    public override void Attack(Vector2Int amount, GameObject trg)// CMD
    {
        if (!health.isDead)
        {
            Health enem = trg.GetComponent<Health>();
            Vector2Int dmg = amount;
            if (enem.currentHealthType == Health.HealthType.Cavalry)
            {
                dmg = new Vector2Int(amount.x + bonusCavalry.GetValue(), amount.y);
            }
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
                StartCoroutine(AtkDelay(0.5f, dmg, hitAngle, enem));
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
}
