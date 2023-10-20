using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitHealth : Health
{
    public Stat reg_Health;
    public List<Affliction> buffs = new List<Affliction>();

    public bool shielded = false;
    public Stat shieldArmor;

    [SerializeField] protected ParticleSystem[] particleDeath;
    [SerializeField] protected Animator anim;

    [SerializeField] protected Unit unitRef;
    [SerializeField] protected MapDot dotMap;
    [SerializeField] protected AudioClip[] deathSound;
    [SerializeField] protected AudioClip[] deathSoundLocal;

    protected override void Start()
    {
        base.Start();
        if(anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    public override void TeamId(int team, int id, int color)
    {
        healthTeam = team;
        idHealth = id;

        unitRef = GetComponent<Unit>();
        unitRef.unitTeam = team;
        unitRef.idUnit = id;

        MainSystem.AddUnitList(unitRef);
        
        if(dotMap != null)
        {
            dotMap.SetColorDot(color);
        }

        Unit_Appearance appearance = GetComponentInChildren<Unit_Appearance>();
        if (appearance != null)
        {
            if (!shielded)
            {
                appearance.ChangeUnitColor(color); // unit_spawner scrpit could do this action?
            }
            else
            {
                appearance.ChangeOtherColor(color);
            }
        }
        else
        {
            Debug.LogError("cant find Apperance");
        }
    }

    public void SetBaseHealthShield(int maxH, StatArmor ar, StatArmor shi)
    {
        max_Health = maxH;
        Cur_Health = max_Health;
        armor.SetBaseValue(ar.armor);
        shieldArmor.SetBaseValue(shi.armor);
    }

    public override void TakeDamage(int amount, int armPerc, int side, int murderer)
    {
        if (isDead == false)
        {
            int dmg;
            int arm = armor.GetValue() - Mathf.RoundToInt(armor.GetValue() * (armPerc / 100f));

            if (!shielded)
            {

                if (side == 2)
                {
                    dmg = (Mathf.RoundToInt(amount * 1.75f)) - arm;

                }
                else if (side == 1)
                {
                    dmg = (Mathf.RoundToInt(amount * 1.25f)) - arm;
                }
                else
                {
                    dmg = amount - arm;
                }
            }
            else
            {
                if (side == 2)
                {
                    dmg = (Mathf.RoundToInt(amount * 1.75f)) - arm;

                }
                else if (side == 1)
                {
                    dmg = amount - arm;
                }
                else
                {
                    int shi = shieldArmor.GetValue() - Mathf.RoundToInt(shieldArmor.GetValue() * (armPerc / 100f));
                    dmg = amount - (arm + shi);
                }
            }
            dmg = Mathf.Clamp(dmg, 1, int.MaxValue);
            //           Debug.Log("Damage: " + dmg, gameObject);
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
                            //Visuall reward
                            p.gold += goldReward;
                            p.PlayerXp(xpReward, transform.position);

                            if (p.isLocal)
                            {
                                PopUpTxt.SetActive(true);
                                PopUpTxt.GetComponent<TextMeshProUGUI>().SetText("+" + goldReward.ToString());

                                int dint = Random.Range(0, deathSoundLocal.Length);
                                audioS.PlayOneShot(deathSoundLocal[dint]);
                            }
                            else
                            {
                                int dint = Random.Range(0, deathSound.Length);
                                audioS.PlayOneShot(deathSound[dint]);
                            }
                            rewardGiven = true;
                        }
                    }
                }
            }
            else
            {
                anim.SetTrigger("Hit");
            }
        }
    }

    protected override void Death()
    {
        Unit un = GetComponent<Unit>();

        if (un != null && !isDead)
        {
            MainSystem.RemoveUnitList(un);
            un.mother.NumUnits -= 1;
            un.OffLights();
            GetComponent<Collider>().enabled = false;
            un.navMeshAgent.enabled = false;
            un.enabled = false;

            isDead = true;
        }
        int randNum = Random.Range(1, 3);
        anim.SetBool("isDead", true);
        anim.SetTrigger("Death" + randNum);

        StartCoroutine(SinkGrave());

        if (particleDeath.Length > 0)
        {
            for (int i = 0; i < particleDeath.Length; i++)
            {
                particleDeath[i].Play();
                particleDeath[i].enableEmission = true;
            }
        }

        if(buffs.Count > 1)
        {
            foreach (Affliction ba in buffs.ToArray())
            {
                if(ba != null)
                {
                    RemoveBuffPassive(ba);
                }
            }
        }
        else if (buffs.Count == 1)
        {
            RemoveBuffPassive(buffs[0]);
        }

        if(dotMap != null)
        {
            dotMap.DotDead();
        }
    }

    protected virtual IEnumerator SinkGrave()
    {
        yield return new WaitForSeconds(20f);
        float secs = 5f;
        float elatim = 0;
        while (elatim < secs)
        {
            this.transform.position -= new Vector3(0, 0.003f, 0);
            elatim += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        isRipeDead = true;
        gameObject.SetActive(false);

        if (PopUpTxt.activeSelf == true)
        {
            PopUpTxt.SetActive(false);
        }
        Destroy(this.gameObject);
    }

    public virtual void TakeBuff(Buff_Affliction aff)
    {
        canvas.NewEffect(aff);
        StartCoroutine(BuffStartStop(aff));
    }

    public virtual void TakeBuffPassive(Affliction aff, bool Oncanvas)
    {
        if (!buffs.Contains(aff))
        {
            if (Oncanvas)
            {
                canvas.AddEffectPassiv(aff);
            }
            AddBuff(aff);
        }
    }

    public virtual void RemoveBuffPassive(Affliction aff)
    {
        if (buffs.Contains(aff))
        {
            canvas.RemoveEffectPassiv(aff);
            RemoveBuff(aff);
        }
    }

    IEnumerator BuffStartStop(Buff_Affliction aff)
    {
        AddBuff(aff);
        yield return new WaitForSeconds(aff.Durration);
        RemoveBuff(aff);
    }

    protected virtual void AddBuff(Affliction baf)
    {
        buffs.Add(baf);
        foreach (BuffClass bc in baf.BuffAfflition)
        {
            switch (bc.Buff)
            {
                case BuffClass.BuffType.Armor:
                    armor.AddModifier(bc.BuffAmount);
                    break;
                case BuffClass.BuffType.AttackSpeed:
                    unitRef.attackSpeed.AddModifier(bc.BuffAmount);
                    break;
                case BuffClass.BuffType.Damage:
                    unitRef.attackDamage.AddModifier(bc.BuffAmount);
                    break;
                case BuffClass.BuffType.ArmorPercing:
                    unitRef.armorPercing.AddModifier(bc.BuffAmount);
                    break;
                case BuffClass.BuffType.Speed:
                    unitRef.navMeshAgent.speed += bc.BuffAmount;
                    break;
            }
        }
    }

    protected virtual void RemoveBuff(Affliction baf)
    {
        foreach (BuffClass bc in baf.BuffAfflition)
        {
            switch (bc.Buff)
            {
                case BuffClass.BuffType.Armor:
                    armor.RemoveModifier(bc.BuffAmount);
                    break;
                case BuffClass.BuffType.AttackSpeed:
                    unitRef.attackSpeed.RemoveModifier(bc.BuffAmount);
                    break;
                case BuffClass.BuffType.Damage:
                    unitRef.attackDamage.RemoveModifier(bc.BuffAmount);
                    break;
                case BuffClass.BuffType.ArmorPercing:
                    unitRef.armorPercing.RemoveModifier(bc.BuffAmount);
                    break;
                case BuffClass.BuffType.Speed:
                    unitRef.navMeshAgent.speed -= bc.BuffAmount;
                    break;
            }
        }
        buffs.Remove(baf);
    }
}
