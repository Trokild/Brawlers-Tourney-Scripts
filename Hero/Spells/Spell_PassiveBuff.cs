using System.Collections.Generic;
using UnityEngine;

public class Spell_PassiveBuff : Spell
{
    private float sRng = 20;
    private List<Affliction> buffs = new List<Affliction>();
    private LayerMask clickAbleLayer;
    private UnitHealth myHealth;
    private List<UnitHealth> uni = new List<UnitHealth>();
    private enum BuffsList {OneBuff, OneDeBuff ,JustBuffs, JustDeBuffs, BothBuffs }
    private BuffsList BuffListHas;
    private bool started;

    protected override void Start()
    {
        base.Start(); 
        clickAbleLayer = LayerMask.GetMask("ClickAble");
        myHealth = GetComponent<UnitHealth>();
    }

    public void SetUpPassive(Affliction b)
    {
        buffs.Add(b);
        if (b.OnSelf)
        {
            if(myHealth != null)
            {
                myHealth.TakeBuffPassive(b, false);
            }
            else
            {
                myHealth = GetComponent<UnitHealth>();
                myHealth.TakeBuffPassive(b, false);
            }
        }
        SetUp();
    }

    void SetUp()
    {
        if (buffs.Count > 1)
        {
            bool buf = false;
            bool dbuf = false;

            foreach (Affliction bufnList in buffs)
            {
                if (bufnList.DeBuff)
                {
                    dbuf = true;
                }
                else
                {
                    buf = true;
                }
            }

            if (buf == true && dbuf == true)
            {
                BuffListHas = BuffsList.BothBuffs;
            }
            else if (buf == true && dbuf == false)
            {
                BuffListHas = BuffsList.JustBuffs;
            }
            else if (buf == false && dbuf == true)
            {
                BuffListHas = BuffsList.JustDeBuffs;
            }
        }
        else if (buffs.Count == 1)
        {
            if (buffs[0].DeBuff)
            {
                BuffListHas = BuffsList.OneDeBuff;
            }
            else
            {
                BuffListHas = BuffsList.OneBuff;
            }
        }

        if (!started)
        {
            StartPassiveBuff(2f);
            started = true;
        }
    }

    void StartPassiveBuff(float rTime)
    {
        InvokeRepeating("ReapeatPassiveBuffs", rTime, rTime);
    }

    void ReapeatPassiveBuffs()
    {
        if(buffs.Count > 0)
        {
            switch (BuffListHas)
            {
                case BuffsList.OneBuff:
                    OneBuff_Func();
                    break;
                case BuffsList.OneDeBuff:
                    OneDeBuff_Func();
                    break;
                case BuffsList.JustBuffs:
                    Buffs_Func();
                    break;
                case BuffsList.JustDeBuffs:
                    DeBuffs_Func();
                    break;
                case BuffsList.BothBuffs:
                    Both_Func();
                    break;
            }
            RemoveRangePassiveBuffs();
        }
        else
        {
            if(uni.Count > 0)
            {
                uni.Clear();
                CancelInvoke("ReapeatPassiveBuffs");
                started = false;
            }
        }
    }

    void OneBuff_Func()
    {
        Collider[] hitColiders = Physics.OverlapSphere(transform.position, sRng, clickAbleLayer);
        Health thisH = myHealth as Health;
        int i = 0;
        if (hitColiders.Length > 0)
        {
            while (i < hitColiders.Length)
            {
                GameObject go = hitColiders[i].gameObject;
                UnitHealth hth = go.GetComponent<UnitHealth>();
                if (hth != null && hth.currentHealthType != Health.HealthType.Building && hth != thisH)
                {
                    if (hth != null && hth.healthTeam == hro.unitTeam)
                    {
                        if (!uni.Contains(hth) && !hth.isDead)
                        {
                            uni.Add(hth);
                            hth.TakeBuffPassive(buffs[0], true);
                        }
                    }
                }
                i++;
            }
        }
    }

    void OneDeBuff_Func()
    {
        Collider[] hitColiders = Physics.OverlapSphere(transform.position, sRng, clickAbleLayer);
        Health thisH = myHealth as Health;
        int i = 0;
        if (hitColiders.Length > 0)
        {
            while (i < hitColiders.Length)
            {
                GameObject go = hitColiders[i].gameObject;
                UnitHealth hth = go.GetComponent<UnitHealth>();
                if (hth != null && hth.currentHealthType != Health.HealthType.Building && hth != thisH)
                {
                    if (hth != null && hth.healthTeam != hro.unitTeam)
                    {
                        if (!uni.Contains(hth) && !hth.isDead)
                        {
                            uni.Add(hth);
                            hth.TakeBuffPassive(buffs[0], true);
                        }
                    }
                }
                i++;
            }
        }
    }

    void Buffs_Func()
    {
        Collider[] hitColiders = Physics.OverlapSphere(transform.position, sRng, clickAbleLayer);
        Health thisH = myHealth as Health;
        int i = 0;
        if (hitColiders.Length > 0)
        {
            while (i < hitColiders.Length)
            {
                GameObject go = hitColiders[i].gameObject;
                UnitHealth hth = go.GetComponent<UnitHealth>();
                if (hth != null && hth.currentHealthType != Health.HealthType.Building && hth != thisH)
                {
                    if (hth != null && hth.healthTeam == hro.unitTeam)
                    {
                        if (!uni.Contains(hth) && !hth.isDead)
                        {
                            uni.Add(hth);
                            foreach(Affliction ba in buffs)
                            {
                                hth.TakeBuffPassive(ba, true);
                            }                        
                        }
                    }
                }
                i++;
            }
        }
    }

    void DeBuffs_Func()
    {
        Collider[] hitColiders = Physics.OverlapSphere(transform.position, sRng, clickAbleLayer);
        Health thisH = myHealth as Health;
        int i = 0;
        if (hitColiders.Length > 0)
        {
            while (i < hitColiders.Length)
            {
                GameObject go = hitColiders[i].gameObject;
                UnitHealth hth = go.GetComponent<UnitHealth>();
                if (hth != null && hth.currentHealthType != Health.HealthType.Building && hth != thisH)
                {
                    if (hth != null && hth.healthTeam != hro.unitTeam)
                    {
                        if (!uni.Contains(hth) && !hth.isDead)
                        {
                            uni.Add(hth);
                            foreach (Affliction ba in buffs)
                            {
                                hth.TakeBuffPassive(ba, true);
                            }
                        }
                    }
                }
                i++;
            }
        }
    }

    void Both_Func()
    {
        Collider[] hitColiders = Physics.OverlapSphere(transform.position, sRng, clickAbleLayer);
        Health thisH = myHealth as Health;
        int i = 0;
        if (hitColiders.Length > 0)
        {
            while (i < hitColiders.Length)
            {
                GameObject go = hitColiders[i].gameObject;
                UnitHealth hth = go.GetComponent<UnitHealth>();
                if (hth != null && hth.currentHealthType != Health.HealthType.Building && hth != thisH)
                {
                    if (hth != null)
                    {
                        if (hth.healthTeam == hro.unitTeam)
                        {
                            if (!uni.Contains(hth) && !hth.isDead)
                            {
                                uni.Add(hth);
                                foreach (Affliction ba in buffs)
                                {
                                    if (!ba.DeBuff)
                                    {
                                        hth.TakeBuffPassive(ba, true);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!uni.Contains(hth) && !hth.isDead)
                            {
                                uni.Add(hth);
                                foreach (Affliction ba in buffs)
                                {
                                    if (ba.DeBuff)
                                    {
                                        hth.TakeBuffPassive(ba, true);
                                    }
                                }
                            }
                        }
                    }
                }
                i++;
            }
        }
    }

    void RemoveRangePassiveBuffs()
    {
        if(uni.Count > 0)
        {
            foreach (UnitHealth ht in uni.ToArray())
            {
                if(ht != null)
                {
                    float dist = Vector3.Distance(transform.position, ht.transform.position);
                    if (dist > sRng)
                    {
                        uni.Remove(ht);
                        if(buffs.Count > 1)
                        {
                            foreach(Affliction Alfs in buffs)
                            {
                                ht.RemoveBuffPassive(Alfs);
                            }
                        }
                        else if(buffs.Count == 1)
                        {
                            ht.RemoveBuffPassive(buffs[0]);
                        }
                    }
                }
                else
                {
                    uni.Remove(ht);
                }
            }
        }
    }

    public void RemovePassiveBuff(Affliction b)
    {
        if (uni.Count > 0)
        {
            foreach (UnitHealth ht in uni.ToArray())
            {
                if (ht != null)
                {
                    if(ht.buffs.Contains(b))
                    {
                        ht.RemoveBuffPassive(b);
                        uni.Remove(ht);
                    }
                }
                else
                {
                    uni.Remove(ht);
                }
            }
        }
        buffs.Remove(b);
        SetUp();
    }
}
