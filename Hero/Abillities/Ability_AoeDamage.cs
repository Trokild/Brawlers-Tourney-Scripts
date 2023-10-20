using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Aoe Damage")]
public class Ability_AoeDamage : Ability
{
    [Range(0, 100)]
    public int ArmorPercing;
    public int Damage;
    private Spell_AoeDmg spellAoe;
    public GameObject ParticleEffectAoe;
    public float Range;
    [Range(1, 10)]
    public float Aoe;
    public float CastTime;
    public float Duration;

    public override void Initialize(GameObject obj)
    {
        magicHero = obj.GetComponent<HeroMagic>();
        spellAoe = obj.AddComponent<Spell_AoeDmg>();
        magicHero.ActiveSpells.Add(spellAoe);
        spellAoe.manaCost = ManaCost;
        spellAoe.isFriendly = false;
        spellAoe.castTim = CastTime;
        spellAoe.Durration = Duration;
        spellAoe.damage_ArmorPerc.x = Damage;
        spellAoe.damage_ArmorPerc.y = ArmorPercing;
        spellAoe.sRange = Range;
        spellAoe.amnt = Amount;
        spellAoe.sizeAoe = Aoe;
        spellAoe.FindAoeLight();
        spellAoe.peGround = ParticleEffectAoe;
        spellAoe.SpellSound = SoundEffect;
    }

    public override void TriggerAbility_NoBtn(GameObject caster)
    {
        Spell_AoeDmg aoe = caster.GetComponent<Spell_AoeDmg>();
        if (aoe == null)
        {
            Debug.LogError("aoeBuff == null", caster);
        }
        aoe.CastSpellAuto();
        Debug.LogError("CastSpellAuto");
    }
}
