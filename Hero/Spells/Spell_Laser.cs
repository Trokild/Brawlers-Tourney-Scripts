using System.Collections;
using UnityEngine;


public class Spell_Laser : Spell_Target
{
    public GameObject castPsys;
    public GameObject hitPys;
    public GameObject laserBeam;

    public override void UseSpell(int s)
    {
        clickRef.SwitchStateTarget(isHeal, castSelf, s);
    }

    public override void CastSpellAuto()
    {
        LaserAuto();
    }

    public override void TargetSpell(GameObject go)
    {
        StartCoroutine(Lasor(go));
    }

    private IEnumerator Lasor(GameObject tr)
    {
        if(magicHero.Cur_Mana >= manaCost)
        {
            hro.anim.SetTrigger("CastB");
            GameObject psys = Instantiate(castPsys, magicHero.castStaffPoint.position, Quaternion.identity);
            psys.transform.parent = magicHero.castStaffPoint;
            hro.Stagnate();
            magicHero.auio.PlayOneShot(SpellSound);
            yield return new WaitForSeconds(1f);

            tr.GetComponent<Health>().Heal(amount, true);
            magicHero.UseMana(manaCost);

            Vector3 sp = new Vector3(tr.transform.position.x, (tr.transform.position.y + 2f), tr.transform.position.z);
            GameObject hsys = Instantiate(hitPys, sp, Quaternion.identity);
            hsys.transform.parent = tr.transform;

            GameObject lb = Instantiate(laserBeam, magicHero.castStaffPoint.position, Quaternion.identity);

            LineRenderer line = lb.GetComponentInChildren<LineRenderer>();
            line.SetPosition(0, magicHero.castStaffPoint.position);
            line.SetPosition(1, sp);

            lb.transform.parent = magicHero.castStaffPoint;
            hro.IdleState();

            yield return new WaitForSeconds(3f);
            Destroy(psys);
            yield return new WaitForSeconds(2f);
            Destroy(hsys);
            Destroy(lb);
        }
    }

    public void LaserAuto()
    {
        if (magicHero.Cur_Mana >= manaCost)
        {
            LayerMask clickAbleLayer = LayerMask.GetMask("ClickAble");
            Collider[] hitColiders = Physics.OverlapSphere(transform.position, sRange, clickAbleLayer);
            Health thisH = this.GetComponent<Health>();
            if (hitColiders.Length > 0)
            {
                for (int i = 0; i < hitColiders.Length; i++)
                {
                    GameObject go = hitColiders[i].gameObject;
                    Health hth = go.GetComponent<Health>();
                    if (hth != null && isHeal && hth.currentHealthType != Health.HealthType.Building)
                    {
                        if (hth.healthTeam == hro.unitTeam && !hth.isDead)
                        {
                            if (hth.Cur_Health < hth.max_Health)
                            {
                                TargetSpell(go);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
