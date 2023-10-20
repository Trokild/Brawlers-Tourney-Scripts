using System.Collections;
using UnityEngine;

public class Spell_AoeDmg : Spell_Aoe
{
    public Vector2Int damage_ArmorPerc;
    public float Durration;
    public int amnt;

    private readonly float sdlay = 0.5f;

    public override void CastAoeSpell(Vector3 pos)
    {
        StartCoroutine(AoeSpellDmg(pos, castTim));
    }

    public override void CastSpellAuto()
    {
        if (!isFriendly && magicHero.Cur_Mana >= manaCost)
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
                    if (hth != null)
                    {
                        if (hth.healthTeam != hro.unitTeam && !hth.isDead)
                        {
                            if (hth.Cur_Health < hth.max_Health)
                            {
                                CastAoeSpell(go.transform.position);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }

    private IEnumerator AoeSpellDmg(Vector3 pos, float time)
    {
        if (magicHero.Cur_Mana >= manaCost)
        {
            magicHero.UseMana(manaCost);
            hro.RotateToSpell(pos);
            hro.anim.SetTrigger("CastC");
            hro.anim.SetBool("IsCasting", true);

            yield return new WaitForSeconds(time - sdlay);

            magicHero.auio.PlayOneShot(SpellSound);
            GameObject bpsys = Instantiate(peGround, pos, Quaternion.identity);
            SpellCtrl_AoePrefab sc_pb = bpsys.GetComponent<SpellCtrl_AoePrefab>();
            if(sc_pb != null)
            {
                sc_pb.InitializeRain(damage_ArmorPerc, team_Id, Durration, sizeAoe, amnt);
            }
            else
            {
                Debug.LogError("This Gameobject: " + bpsys + " does not have 'SpellCtrlAoe_Prefab'");
            }


            yield return new WaitForSeconds(Durration);
            hro.anim.SetTrigger("EndCast");
            hro.anim.SetBool("IsCasting", false);
            hro.RotateToSpellStop();
        }
    }
}
