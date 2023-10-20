using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Passive Buff")]
public class Ability_PassiveBuff : Ability
{
    private Spell_PassiveBuff spellPassive;
    public Buff_Affliction Buff; //durration, type, amout
    public float Range;
    public bool isFriendly;
    public bool isOnSelf;
    public bool isPassive;

    public override void Initialize(GameObject obj)
    {
        magicHero = obj.GetComponent<HeroMagic>();
        spellPassive = obj.AddComponent<Spell_PassiveBuff>();
        magicHero.ActiveSpells.Add(spellPassive);
        spellPassive.SetUpPassive(Buff);
    }

    public override void TriggerAbility_NoBtn(GameObject caster)
    {
        Debug.LogWarning("Cannot Trigger Passiv Ability, noBtn");
    }
}
