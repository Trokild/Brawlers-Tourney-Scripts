using UnityEngine;

public class Spell_Target : Spell
{
    protected Click clickRef;
    public float sRange;
    protected float castTime;
    protected bool hasCastTime;
    public bool isHeal;
    protected bool castSelf;
    protected int amount;

    protected override void Start()
    {
        base.Start();
        if(hro.clickRef.isLocalHost)
        {
            clickRef = hro.clickRef as Click;
            if (clickRef == null)
            {
                Debug.LogError("ugh..");
            }
        }
    }

    public void SetAbility(int amo, int manc, float tim)
    {
        amount = amo;
        manaCost = manc;

        if (tim > 0)
        {
            castTime = tim;
            hasCastTime = true;
        }
        else
        {
            tim = 0;
            hasCastTime = false;
        }
    }

    public void SetTargebility(bool heal, bool self)
    {
        isHeal = heal;
        castSelf = self;
    }

    public virtual void TargetSpell(GameObject trg)
    {
        Debug.Log(trg + " TargetSpell");
    }

}
