using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Dash")]
public class Ability_Dash : Ability
{
    public float Range;
    private Spell_Dash spellDash;

    public override void Initialize(GameObject obj)
    {
        magicHero = obj.GetComponent<HeroMagic>();
        spellDash = obj.AddComponent<Spell_Dash>();
        magicHero.ActiveSpells.Add(spellDash);
        spellDash.manaCost = ManaCost;
        spellDash.sRange = Range;
        spellDash.SpellSound = SoundEffect;
    }

    public override void TriggerAbility_NoBtn(GameObject caster)
    {
        Debug.LogError("CastSpellAuto");
    }
}
