using UnityEngine;

public abstract class Ability : ScriptableObject
{
    protected HeroMagic magicHero;
    public float AbilityCooldown = 1f;
    public int Amount;
    public int ManaCost;
    public Sprite AbilitySprite;
    public Sprite Choose_AbilitySprite;
    public AudioClip SoundEffect;
    //public int BookCost;

    public abstract void Initialize(GameObject obj);
    //public abstract void TriggerAbility(int btn);
    public abstract void TriggerAbility_NoBtn(GameObject caster);
}
