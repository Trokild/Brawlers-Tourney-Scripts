using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Laser Storm")]
public class Ability_UltimateLaser : Ability
{
    private Spell_StormLaser ssl;
    public AudioClip LaserSound;
    public bool canCastSelf;
    public bool isFriendly;
    public float StormRange;
    [Range(1, 100)]
    public int StormCalls;
    [Range(0.1f, 4f)]
    public float StormSpeed;

    public GameObject Casting_ParticleSys;
    public GameObject Hit_ParticleSys;
    public GameObject Beam;

    public override void Initialize(GameObject obj)
    {
        magicHero = obj.GetComponent<HeroMagic>();
        ssl = obj.AddComponent<Spell_StormLaser>();
        magicHero.ActiveSpells.Add(ssl);
        ssl.SetAbility(Amount, ManaCost, canCastSelf, isFriendly);
        ssl.SetValuesOther(StormRange, StormCalls, StormSpeed);
        ssl.Cast_PartiSys = Casting_ParticleSys;
        ssl.Hit_PartiSys = Hit_ParticleSys;
        ssl.Bem = Beam;
        ssl.SpellSound = SoundEffect;
        ssl.BeamSound = LaserSound;
    }

    public override void TriggerAbility_NoBtn(GameObject caster)
    {
        ssl.CastStormLaserAuto();
        Debug.LogError("CastStormLaserAuto");
    }
}
