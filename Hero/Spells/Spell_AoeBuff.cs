using System.Collections;
using UnityEngine;

public class Spell_AoeBuff : Spell_Aoe
{
    public Buff_Affliction buff; // Durr, Type, amount

    public override void CastAoeSpell(Vector3 pos)
    {
        StartCoroutine(AoeSpellBuff(pos, castTim));
    }

    public override void CastSpellAuto()
    {
        if(isFriendly)
        {
            StartCoroutine(AoeSpellBuff(this.transform.position, castTim));
        }
    }

    private IEnumerator AoeSpellBuff(Vector3 pos, float time)
    {
        if (magicHero.Cur_Mana >= manaCost)
        {
            hro.RotateToSpell(pos);
            GameObject bpsys = Instantiate(peGround, hro.transform.position, Quaternion.identity);
            hro.anim.SetTrigger("CastB");
            float sdlay = 0.5f;
            yield return new WaitForSeconds(time - sdlay);
            magicHero.auio.PlayOneShot(SpellSound);
            yield return new WaitForSeconds(sdlay);
            CastBuff(pos);

            hro.RotateToSpellStop();
            yield return new WaitForSeconds(2);
            Destroy(bpsys);
        }
    }

    void CastBuff(Vector3 pos)
    {
        Collider[] hitColiders = Physics.OverlapSphere(pos, sizeAoe, clickAbleLayer);
        int i = 0;
        if (hitColiders.Length > 0)
        {
            while (i < hitColiders.Length)
            {
                GameObject go = hitColiders[i].gameObject;
                UnitHealth hth = go.GetComponent<UnitHealth>();
                if (isFriendly && hth != null)
                {
                    if (hth != null && hth.healthTeam == hro.unitTeam)
                    {
                        hth.TakeBuff(buff);
                    }
                }
                i++;
            }
            magicHero.UseMana(manaCost);
        }
    }
}
