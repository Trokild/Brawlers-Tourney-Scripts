using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Dash : Spell
{
    protected Click clickRef;
    public float sRange;

    protected override void Start()
    {
        base.Start();
        clickRef = gameObject.GetComponent<Health>().clickRef as Click;
        if(clickRef == null)
        {
            Debug.LogError("that didnt click");
        }
    }

    public override void UseSpell(int s)
    {
        clickRef.SwitchStateMoveTarget(s);
    }

    public virtual void DashSpell(Vector3 pos)
    {
        StartCoroutine(DashTo(pos, 0.5f));
        magicHero.UseMana(manaCost);
        hro.RotateToSpell(pos);
        hro.anim.SetTrigger("Dash");
    }

    private IEnumerator DashTo(Vector3 pos, float t)
    {
        float cd = t;
        float tmr = 0f;
        Vector3 curPos = transform.position;
        magicHero.auio.PlayOneShot(SpellSound);
        yield return new WaitForSeconds(0.25f);
        while (tmr < cd)
        {
            transform.position = Vector3.Lerp(curPos, pos, (tmr / cd));
            tmr += Time.deltaTime;
            Debug.Log("Dash Over?");
            yield return null;
        }
        hro.RotateToSpellStop();
        yield break;
    }

    public bool CanDash(Vector3 pos)
    {
        if(magicHero.Cur_Mana >= manaCost)
        {
            if (hro.CanWalkablePoint(pos, sRange) && IsFowardClear())
            {
                return true;
            }
            else
            { return false; }
        }
        else
        {
            return false;
        }

    }

    bool IsFowardClear()
    {
        RaycastHit rH;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast((transform.position + new Vector3(0, 0.5f, 0)), fwd, out rH, 10))
        {
            if (rH.collider.GetComponent<Unit>() != null)
            {
                return true;
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    //public static bool GetPath(NavMeshPath path, Vector3 fromPos, Vector3 toPos, int passableMask)
    //{
    //    path.ClearCorners();

    //    if (NavMesh.CalculatePath(fromPos, toPos, passableMask, path) == false)
    //        return false;

    //    return true;
    //}

    //public static float GetPathLength(NavMeshPath path)
    //{
    //    float lng = 0.0f;

    //    if ((path.status != NavMeshPathStatus.PathInvalid) && (path.corners.SafeLength() > 1))
    //    {
    //        for (int i = 1; i < path.corners.Length; ++i)
    //        {
    //            lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
    //        }
    //    }

    //    return lng;
    //}

}
