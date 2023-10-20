using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Whirlwind")]
public class Ability_SwipeCol : Ability
{
    private Spell_Whirlwind swipe;

    [Range(0, 100)]
    public int ArmorPercing;
    public int Spins;
    public float DurrationSpin;

    public GameObject SwiperPrefab;
    public bool isBuffTrigger;
    public Buff_Affliction BuffTriggerBonus;

    public override void Initialize(GameObject obj)
    {
        magicHero = obj.GetComponent<HeroMagic>();
        swipe = obj.AddComponent<Spell_Whirlwind>();
        magicHero.ActiveSpells.Add(swipe);
        swipe.manaCost = ManaCost;
        swipe.dmgAp = new Vector2Int(Amount, ArmorPercing);
        swipe.spns = Spins;
        swipe.durrSpin = DurrationSpin;
        swipe.colliderPrefab = SwiperPrefab;
        if(isBuffTrigger)
        {
            swipe.triggrBuff = BuffTriggerBonus;
        }
        swipe.hasBuffTrigger = isBuffTrigger;

        swipe.SpellSound = SoundEffect;
    }

    public override void TriggerAbility_NoBtn(GameObject caster)
    {
        Spell_Whirlwind aoe = caster.GetComponent<Spell_Whirlwind>();
        if (aoe == null)
        {
            Debug.LogError("Spell_Whirlwind == null", caster);
        }
        aoe.CastSpellAuto();
        Debug.LogError("Whildwind");
    }
}
