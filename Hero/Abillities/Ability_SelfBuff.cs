using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Self Buff")]
public class Ability_SelfBuff : Ability
{
    private Spell_SelfBuff spellSelf;
    public Buff_Affliction Buff; //durration, type, amout
    public GameObject ParticleEffect;

    public override void Initialize(GameObject obj)
    {
        magicHero = obj.GetComponent<HeroMagic>();
        spellSelf = obj.AddComponent<Spell_SelfBuff>();
        magicHero.ActiveSpells.Add(spellSelf);
        spellSelf.SetSelfBuff(Buff, ParticleEffect, Buff.Durration);
    }

    public override void TriggerAbility_NoBtn(GameObject caster)
    {
        Spell_SelfBuff slf = caster.GetComponent<Spell_SelfBuff>();
        if (slf == null)
        {
            Debug.LogError("slf == null", caster);
        }
        slf.CastSpellAuto();
    }
}
