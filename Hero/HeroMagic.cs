using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMagic : MonoBehaviour
{
    public bool isTest;
    public bool HeroLocal { get; private set; }

    public Transform castStaffPoint;
    public List<Ability> HeroAbilities = new List<Ability>();
    public List<Ability> ActiveAbilities = new List<Ability>();
    public List<Spell> ActiveSpells = new List<Spell>();
    public List<Cooldown> Cooldowns = new List<Cooldown>();
    public int max_Mana;
    public int Cur_Mana { get; private set; }

    public Stat reg_Mana;
    //[SerializeField] private Stat regenTimeMana;
    private float rtm = 5f;

    public Hero_UI uiHero;
    public AudioSource auio;
    public AudioSource auio_Alt;
    [SerializeField] private Health hlt;

    void Start()
    {
        Cur_Mana = max_Mana;
        if(auio == null)
        {
            auio = GetComponent<AudioSource>();
        }

        if (auio_Alt == null)
        {
            auio_Alt = castStaffPoint.gameObject.GetComponent<AudioSource>();
        }
    }

    public void StopAiSpell()
    {
        CancelInvoke();
    }

    public void StartAiSpell()
    {
        float r = Random.Range(100f, 140f);
        InvokeRepeating("AiSpell", r, 30);
    }

    void Update()
    {
        if (Cur_Mana < max_Mana)
        {
            rtm -= Time.deltaTime;

            if (rtm <= 0)
            {
                rtm = 5f;//regenTimeMana.GetValue();
                Cur_Mana += reg_Mana.GetValue();
            }
        }

        if(Cooldowns.Count > 0)
        {
            for (int i = 0; i < Cooldowns.Count; i++)
            {
                if (!Cooldowns[i].isReady)
                {
                    if(Cooldowns[i].Durration <= 0)
                    {
                        Cooldowns[i].SetReadyCD();
                    }
                    else
                    {
                        Cooldowns[i].Durration -= Time.deltaTime;
                    }
                }
            }
        }
    }

    public void UseMana(int mana)
    {
        Cur_Mana -= mana;

        if (Cur_Mana > max_Mana)
        {
            Cur_Mana = max_Mana;
        }

        if (Cur_Mana < 0)
        {
            Cur_Mana = 0;
        }
    }

    public void GetMana(int mana)
    {
        Cur_Mana += mana;

        if(Cur_Mana > max_Mana)
        {
            Cur_Mana = max_Mana;
        }

        if (Cur_Mana < 0)
        {
            Cur_Mana = 0;
        }
    }

    public void ManaTick(int ticks, int mana, float rate)
    {
        StartCoroutine(TickManaFucntion(ticks, mana, rate));
    }

    IEnumerator TickManaFucntion(int t, int m, float r)
    {
        for (int i = 0; i < t; i++)
        {
            if(!hlt.isDead)
            {
                GetMana(m);
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(r);
        }
    }

    public void SetHeroLocal(Player ply)
    {
        HeroHealth hh = GetComponent<HeroHealth>();
        HeroExperiance hx = GetComponent<HeroExperiance>();
        hx.SetLocalXp(ply);

        if (hh.idHealth == ply.idPlayer && ply.isLocal)
        {
            HeroLocal = true;
            uiHero = GameObject.FindGameObjectWithTag("HeroUI").GetComponent<Hero_UI>();
        }
        else
        {
            HeroLocal = false;
        }
    }

    public void NewAbility(int btn)
    {
        if(HeroLocal)
        {
            uiHero.AbilityButtons[btn].SetButtonActive(true);
        }
        else
        {
            if(HeroAbilities.Count > 0)
            {
                HeroAbilities[0].Initialize(gameObject);
                ActiveAbilities.Add(HeroAbilities[0]);
                Cooldowns.Add(new Cooldown(true, 0f));
                HeroAbilities.Remove(HeroAbilities[0]);
            }
        }
    }

    public void AbilityInitialize(int ab, int btn)
    {
        HeroAbilities[ab].Initialize(gameObject); 
        ActiveAbilities.Add(HeroAbilities[ab]);
        Cooldowns.Add(new Cooldown(true, 0f));

        if (HeroLocal && uiHero != null)
        {
            uiHero.AbilityButtons[btn].Initialize(HeroAbilities[ab]);
            HeroAbilities.Remove(HeroAbilities[ab]);
        }
    }

    public void AiSpell()
    {
        if(hlt != null || hlt.isDead)
        { 
            if (ActiveAbilities.Count > 0)
            {
                if (ActiveAbilities.Count == 0)
                {
                    CastMagic(0);
                    Cooldowns[0].ActivateCD(ActiveAbilities[0].AbilityCooldown);
                }
                else
                {
                    int r = Random.Range(0, ActiveAbilities.Count);
                    if (Cooldowns[r].isReady)
                    {
                        CastMagic(r);
                        Cooldowns[r].ActivateCD(ActiveAbilities[r].AbilityCooldown);
                    }
                }
            }
        }
    }

    public void CastMagic(int spell)
    {
        if(spell >= 0 || spell <= ActiveSpells.Count)
        {
            ActiveSpells[spell].CastSpellAuto();
        }
        else
        {
            Debug.LogError("Spell int is " + spell + " ActiveSpells.Count is " +  ActiveSpells.Count);
        }
        
    }
}
