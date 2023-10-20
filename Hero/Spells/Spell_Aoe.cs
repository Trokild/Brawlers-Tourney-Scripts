using UnityEngine;

public class Spell_Aoe : Spell
{
    protected Click clickRef;
    protected AoeLight aoeL;
    protected LayerMask clickAbleLayer;
    [Space]
    public GameObject peGround;
    public bool isFriendly;
    public float sRange;
    public float sizeAoe;
    public float castTim;

    protected override void Start()
    {
        base.Start();
        clickAbleLayer = LayerMask.GetMask("ClickAble");
    }

    public override void UseSpell(int s)
    {
        clickRef.SwitchStateAoe(isFriendly, s);
    }

    public void FindAoeLight()
    {
        clickRef = gameObject.GetComponent<Health>().clickRef as Click;
        if(clickRef != null)
        {
            if (!clickRef.isLocalHost)
            {
                return;
            }
            if (clickRef.aoeLight_go != null)
            {
                aoeL = clickRef.aoeLight_go.GetComponent<AoeLight>();
            }
            else
            {
                GameObject AoeLit = GameObject.FindGameObjectWithTag("AoeLight");
                if (AoeLit != null)
                {
                    aoeL = AoeLit.GetComponent<AoeLight>();
                }
            }
        }

        if (aoeL != null)
        {
            aoeL.InitalizeLight(this);
        }
        else
        {
            Debug.LogError("Cant find the Light", gameObject);
        }
    }

    public virtual void CastAoeSpell(Vector3 pos)
    {

    }
}
