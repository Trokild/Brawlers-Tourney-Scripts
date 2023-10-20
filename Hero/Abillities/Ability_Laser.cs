using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Laser")]
public class Ability_Laser : Ability
{
    private Spell_Laser laser;
    public float laserRange;
    public float CastTime;
    public bool isHealing;
    public bool canCastSelf;
    public GameObject Casting_ParticleSys;
    public GameObject Hit_ParticleSys;
    public GameObject Beam;
    //public LineRenderer Laser;

    public override void Initialize(GameObject obj)
    {
        magicHero = obj.GetComponent<HeroMagic>();
        laser = obj.AddComponent<Spell_Laser>();
        magicHero.ActiveSpells.Add(laser);
        laser.SetAbility(Amount, ManaCost, CastTime);
        laser.SetTargebility(isHealing, canCastSelf);
        laser.castPsys = Casting_ParticleSys;
        laser.hitPys = Hit_ParticleSys;
        laser.sRange = laserRange;
        laser.laserBeam = Beam;
        laser.SpellSound = SoundEffect;
    }

    //public override void TriggerAbility(int btn)
    //{
    //    laser.CastSpell(btn);
    //}

    public override void TriggerAbility_NoBtn(GameObject caster)
    {
        Spell_Laser las = caster.GetComponent<Spell_Laser>();
        if (las == null)
        {
            Debug.LogError("las == null", caster);
        }
        las.LaserAuto();
    }
}
