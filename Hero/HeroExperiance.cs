using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroExperiance : MonoBehaviour
{
    public bool LocalXp { get; private set; }
    public int level_Hero { get; private set; }
    [SerializeField] private int maxLvl;
    [SerializeField] private GameObject pe_lvlUp;
    [SerializeField] private AudioClip sound_lvlUp;

    public int xpToNextLvl { get; private set; }
    public int cur_Xp { get; private set; }

    public float xpDist { get; private set; }
    [SerializeField] private float howFarGetXp;
    private HeroMagic magicHero;
    private HeroHealth healthHero;
    private Hero unitHero;
    AudioSource audio;

    private void Start()
    {
        level_Hero = 1;
        xpDist = howFarGetXp;
        magicHero = GetComponent<HeroMagic>();
        healthHero = GetComponent<HeroHealth>();
        unitHero = GetComponent<Hero>();
        audio = GetComponent<AudioSource>();
        xpToNextLvl = 150;
    }

    public void SetLocalXp(Player ply)
    {
        LocalXp = ply.isLocal;
    }

    public void GainXp(int xp)
    {
        if(level_Hero >= maxLvl || healthHero.isDead)
        {
            return;
        }

        cur_Xp += xp;
        if(cur_Xp >= xpToNextLvl)
        {
            cur_Xp = 0;
            float nxtLvlXp = xpToNextLvl * 1.2f;
            xpToNextLvl = Mathf.RoundToInt(nxtLvlXp);
            LevelUpHero();
        }
    }

    // Local Have to be able to activate HeroUI logic,
    // while AI have to be able to unlock and use new abilites.
    void LevelUpHero()
    {
        level_Hero++;

        foreach (Player p in MainSystem.PlayerList)
        {
            if (p.idPlayer == healthHero.idHealth) // && p.isLocal
            {
                p.book += 1;
            }
        }

        switch (level_Hero)
        {
            case 2:
                unitHero.attackDamage.AddBaseValue(5);
                unitHero.attackSpeed.AddBaseValue(5);
                healthHero.max_Health += 50;
                healthHero.Heal(50, false);
                break;
            case 3:
                unitHero.attackDamage.AddBaseValue(2);
                unitHero.attackSpeed.AddBaseValue(2);
                healthHero.max_Health += 20;
                healthHero.Heal(20, false);
                magicHero.NewAbility(1);
                break;
            case 4:
                unitHero.attackDamage.AddBaseValue(2);
                unitHero.attackSpeed.AddBaseValue(2);
                healthHero.max_Health += 10;
                healthHero.Heal(10, false);
                break;
            case 6:
                unitHero.attackDamage.AddBaseValue(3);
                unitHero.attackSpeed.AddBaseValue(2);
                healthHero.max_Health += 10;
                healthHero.Heal(10, false);
                magicHero.NewAbility(2);
                break;
            case 7:
                unitHero.attackDamage.AddBaseValue(2);
                unitHero.attackSpeed.AddBaseValue(1);
                healthHero.max_Health += 20;
                healthHero.Heal(20, false);
                break;
            case 8:
                unitHero.attackDamage.AddBaseValue(2);
                unitHero.attackSpeed.AddBaseValue(1);
                healthHero.max_Health += 10;
                healthHero.Heal(10, false);
                magicHero.NewAbility(3);
                break;
            case 9:
                healthHero.max_Health += 10;
                healthHero.Heal(10, false);
                break;
            case 10:
                healthHero.max_Health += 10;
                healthHero.Heal(10, false);
                break;
            case 11:
                healthHero.max_Health += 10;
                healthHero.Heal(10, false);
                break;
            case 12:
                healthHero.max_Health += 10;
                healthHero.Heal(10, false);
                break;
        }

        GameObject peLvlUp = (GameObject)Instantiate(pe_lvlUp, transform.position, transform.rotation);
        peLvlUp.transform.SetParent(this.transform);
        Destroy(peLvlUp, 3f);
        audio.PlayOneShot(sound_lvlUp);
    }
}
