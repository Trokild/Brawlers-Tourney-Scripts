using UnityEngine;

public class Spell_SelfBuff : Spell
{
    private Buff_Affliction buff;
    private UnitHealth myHealth;
    private Unit myUnit; //;-);-);-)
    private GameObject particleEffectPrefab;
    private float cooldown;

    protected override void Start()
    {
        base.Start();
        myHealth = GetComponent<UnitHealth>();
        myUnit = GetComponent<Unit>();
    }

    public void SetSelfBuff(Buff_Affliction bff, GameObject pe, float tim)
    {
        buff = bff;
        particleEffectPrefab = pe;
        cooldown = tim;
    }

    public override void UseSpell(int s)
    {
        if (magicHero.Cur_Mana >= manaCost)
        {
            SelfBuff();
            magicHero.uiHero.AbilityButtons[s].StartCooldown();
            magicHero.UseMana(manaCost);
        }
    }

    public override void CastSpellAuto()
    {
        if(myUnit.currentstate == Unit.UnitState.Attack && magicHero.Cur_Mana >= manaCost)
        {
            magicHero.UseMana(manaCost);
            SelfBuff();
        }

    }

    public void SelfBuff()
    {
        GameObject go_psys = Instantiate(particleEffectPrefab, this.transform.position, Quaternion.identity);
        SpellCtrl_SelfParticleEffect sc_spe = go_psys.GetComponent<SpellCtrl_SelfParticleEffect>();
        sc_spe.StartParticleSystems(cooldown, this.transform);
        myUnit.anim.SetTrigger("CastB");
        if (myHealth != null)
        {
            myHealth.TakeBuff(buff);
        }
        else
        {
            myHealth = GetComponent<UnitHealth>();
            myHealth.TakeBuff(buff);
        }
    }
}
