using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Aoe Buff")]
public class Ability_AoeBuff : Ability
{
    private Spell_AoeBuff spellAoe;
    public GameObject ParticleEffectGround;
    public Buff_Affliction Buff; //durration, type, amout
    public float Range;
    public float Aoe;
    public float CastTime;
    public bool DeBuff;

    public override void Initialize(GameObject obj)
    {
        magicHero = obj.GetComponent<HeroMagic>();
        spellAoe = obj.AddComponent<Spell_AoeBuff>();
        magicHero.ActiveSpells.Add(spellAoe);
        spellAoe.buff = Buff;
        spellAoe.manaCost = ManaCost;
        spellAoe.isFriendly = !DeBuff;
        spellAoe.castTim = CastTime;
        spellAoe.sRange = Range;
        spellAoe.sizeAoe = Aoe;
        spellAoe.peGround = ParticleEffectGround;
        spellAoe.SpellSound = SoundEffect;
        spellAoe.FindAoeLight();
    }

    public override void TriggerAbility_NoBtn(GameObject caster)
    {
        Spell_AoeBuff aoeBuff = caster.GetComponent<Spell_AoeBuff>();
        if (aoeBuff == null)
        {
            Debug.LogError("aoeBuff == null", caster);
        }
        aoeBuff.CastSpellAuto();
        Debug.LogError("CastSpellAuto");
    }
}
