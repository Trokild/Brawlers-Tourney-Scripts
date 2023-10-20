using System.Collections;
using FoW;
using UnityEngine;
using TMPro;

public class HeroHealth : UnitHealth
{
    public float respawnTime;
    public float rt { get; private set; }
    private HeroMagic magic;
    [SerializeField] private ColorLightHero colHeroLight;
    [SerializeField] private Unit_Appearance wardrobe;
    [SerializeField] private HideInFog hif;
    [SerializeField] private Animator rewardAnim;
    private Hero_UI _heroUI;
    private Hero_StatsUI _heroStatUI;
    private bool isLocalHero = false;

    private void Update()
    {
        if(isRipeDead)
        {
            rt += Time.deltaTime;
            if(rt >= respawnTime)
            {
                isRipeDead = false;
                Respawn();
            }
        }
    }

    public override void TeamId(int team, int id, int color)
    {
        healthTeam = team;
        idHealth = id;
        Cur_Health = max_Health;
        magic = GetComponent<HeroMagic>();
        Hero hro = GetComponent<Hero>();
        hro.HeroStart(team, id);
        unitRef = hro;

        if (colHeroLight != null)
        {
            colHeroLight.LightColorChange(color);
        }

        if (dotMap != null)
        {
            dotMap.SetColorDot(color);
        }

        if (wardrobe != null)
        {
            wardrobe.ChangeOtherColor(color);
            if(hif != null)
            {
                foreach(SkinnedMeshRenderer sren in wardrobe._MeshUnitBody)
                {
                    hif.GetTheMesh(sren);
                }
                foreach (MeshRenderer ren in wardrobe._MeshUnitOther)
                {
                    hif.GetTheMesh(ren);
                }
            }
        }
        MainSystem.AddHeroList(unitRef);
    }

    public void SetLocalHero(Hero_UI ui, Hero_StatsUI sui)
    {
        _heroUI = ui;
        _heroStatUI = sui;
        isLocalHero = true;
    }

    public override void TakeDamage(int amount, int armPerc, int side, int murderer)
    {
        if (isDead == false)
        {
            int dmg;
            int arm = armor.GetValue() - Mathf.RoundToInt(armor.GetValue() * (armPerc / 100f));


            if (side == 2)
            {
                dmg = (amount + (amount * side)) - arm;

            }
            else if (side == 1)
            {
                dmg = amount - arm;
            }
            else
            {

                dmg = amount - arm;
            }

            dmg = Mathf.Clamp(dmg, 1, int.MaxValue);
            Cur_Health -= dmg;

            Healthbar();

            if (Cur_Health <= 0)
            {
                Cur_Health = 0;
                Death();

                if (!rewardGiven)
                {
                    foreach (Player p in MainSystem.PlayerList)
                    {
                        if (p.idPlayer == murderer) // && p.isLocal
                        {
                            p.gold += goldReward;
                            p.PlayerXp(xpReward, transform.position);

                            if (p.isLocal)
                            {
                                PopUpTxt.SetActive(true);
                                rewardAnim.SetTrigger("Reward");
                                PopUpTxt.GetComponent<TextMeshProUGUI>().SetText("+" + goldReward.ToString());
                            }
                            rewardGiven = true;
                        }
                    }
                }
            }
        }
    }

    protected override void Death()
    {
        Unit un = GetComponent<Unit>();

        if (un != null && !isDead)
        {
            MainSystem.RemoveHeroList(un);
            un.OffLights();
            GetComponent<Collider>().enabled = false;
            un.navMeshAgent.enabled = false;
            un.enabled = false;
            rt = 0;
            isDead = true;
        }

        if(magic != null)
        {
            magic.UseMana(magic.Cur_Mana);
            magic.StopAiSpell();
        }

        if (particleDeath.Length > 0)
        {
            for (int i = 0; i < particleDeath.Length; i++)
            {
                particleDeath[i].Play();
                particleDeath[i].enableEmission = true;
            }
        }
        dotMap.DotDead();
        int randNum = Random.Range(1, 3);
        anim.SetBool("isDead", true);
        anim.SetTrigger("Death" + randNum);

        //un.Stagnate();
        StartCoroutine(SinkGrave());
    }

    protected override IEnumerator SinkGrave()
    {
        yield return new WaitForSeconds(20f);
        float secs = 5f;
        float elatim = 0;
        while (elatim < secs)
        {
            transform.position -= new Vector3(0, 0.003f, 0);
            elatim += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        isRipeDead = true;
        Debug.Log("SinkGrave hero");
    }

    protected override void AddBuff(Affliction baf)
    {
        if(!isLocalHero)
        {
            base.AddBuff(baf);
            return;
        }

        buffs.Add(baf);
        foreach (BuffClass bc in baf.BuffAfflition)
        {
            switch (bc.Buff)
            {
                case BuffClass.BuffType.Armor:
                    armor.AddModifier(bc.BuffAmount);
                    _heroStatUI.GreenText_Armor(armor.GetValue());
                    break;
                case BuffClass.BuffType.AttackSpeed:
                    unitRef.attackSpeed.AddModifier(bc.BuffAmount);
                    _heroStatUI.GreenText_AttackSpd(unitRef.attackSpeed.GetValue());
                    break;
                case BuffClass.BuffType.Damage:
                    unitRef.attackDamage.AddModifier(bc.BuffAmount);
                    _heroStatUI.GreenText_Damage(unitRef.attackDamage.GetValue());
                    break;
                case BuffClass.BuffType.ArmorPercing:
                    unitRef.armorPercing.AddModifier(bc.BuffAmount);
                    _heroStatUI.GreenText_AP(unitRef.armorPercing.GetValue());
                    break;
            }
        }
    }

    protected override void RemoveBuff(Affliction baf)
    {
        if (!isLocalHero)
        {
            base.RemoveBuff(baf);
            return;
        }

        foreach (BuffClass bc in baf.BuffAfflition)
        {
            switch (bc.Buff)
            {
                case BuffClass.BuffType.Armor:
                    armor.RemoveModifier(bc.BuffAmount);
                    if(armor.GetModifiersValue() == 0)
                    {
                        _heroStatUI.NormalText_Armor(armor.GetValue());
                    }
                    else
                    {
                        _heroStatUI.GreenText_Armor(armor.GetValue());
                    }
                    break;
                case BuffClass.BuffType.AttackSpeed:
                    unitRef.attackSpeed.RemoveModifier(bc.BuffAmount);
                    if (unitRef.attackSpeed.GetModifiersValue() == 0)
                    {
                        _heroStatUI.NormalText_AttackSpd(unitRef.attackSpeed.GetValue());
                    }
                    else
                    {
                        _heroStatUI.GreenText_AttackSpd(unitRef.attackSpeed.GetValue());
                    }
                    break;
                case BuffClass.BuffType.Damage:
                    unitRef.attackDamage.RemoveModifier(bc.BuffAmount);
                    if (unitRef.attackDamage.GetModifiersValue() == 0)
                    {
                        _heroStatUI.NormalText_Damage(unitRef.attackDamage.GetValue());
                    }
                    else
                    {
                        _heroStatUI.GreenText_Damage(unitRef.attackDamage.GetValue());
                    }
                    break;
                case BuffClass.BuffType.ArmorPercing:
                    unitRef.armorPercing.RemoveModifier(bc.BuffAmount);
                    if (unitRef.armorPercing.GetModifiersValue() == 0)
                    {
                        _heroStatUI.NormalText_AP(unitRef.armorPercing.GetValue());
                    }
                    else
                    {
                        _heroStatUI.GreenText_AP(unitRef.armorPercing.GetValue());
                    }
                    break;
            }
        }
        buffs.Remove(baf);
    }

    void Respawn()
    {
        Debug.Log("Respawn hero");
        foreach (Spawner_Manager sm in MainSystem.Spawner)
        {
            if(sm.smPlayerRef != null)
            {
                if (sm.smPlayerRef.idPlayer == idHealth)
                {
                    transform.position = sm.heroSpawn.position;
                    //Debug.Log(sm.smPlayerRef.idPlayer + " " + idHealth);
                }
            }
        }
        FullHealth();
        dotMap.DotBack();
        magic.GetMana(magic.max_Mana);
        Unit un = GetComponent<Unit>();
        MainSystem.RespawnHero(un);
        GetComponent<Collider>().enabled = true;
        un.navMeshAgent.enabled = true;
        un.enabled = true;
        anim.SetBool("isDead", false);
        anim.SetTrigger("CastB");
        PopUpTxt.SetActive(false);
        rewardGiven = false;
        isDead = false;
        Healthbar();
        if (!magic.HeroLocal)
        {
            magic.StartAiSpell();
        }
    }
}
