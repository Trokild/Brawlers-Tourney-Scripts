using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeLight : MonoBehaviour
{
    [SerializeField] private float area;
    [SerializeField] private float range;
    [SerializeField] private Light light;
    [SerializeField]
    private Color inRangeColor;
    [SerializeField]
    private Color OutRangeColor;
    public bool isInRange {get; private set;}
    public bool isActivaded { get; private set; }
    public float dist { get; private set; }
    private Transform myHero;

    private void Update()
    {
        if(!isActivaded)
        {
            return;
        }

        dist = Vector3.Distance(transform.position, myHero.position);
        if(dist <=range && !isInRange)
        {
            RangeLight_In();
        }
        else if(dist > range && isInRange)
        {
            RangeLight_Out();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, area);
    }

    public void SetLightArea(float a)
    {
        area = a;
        light.spotAngle = (area * 10);
    }

    void RangeLight_In()
    {
        isInRange = true;
        light.color = inRangeColor;
    }

    void RangeLight_Out()
    {
        isInRange = false;
        light.color = OutRangeColor;
    }

    public void InitalizeLight(Spell_Aoe hero)
    {
        myHero = hero.gameObject.transform;
        range = hero.sRange;
        SetLightArea(hero.sizeAoe);
    }

    public void TurnAoeLight_On()
    {
        if (!isActivaded)
        {
            isActivaded = true;
            light.enabled = true;
        }
    }

    public void TurnAoeLight_Off()
    {
        if (isActivaded)
        {
            isActivaded = false;
            light.enabled = false;
        }
    }
}
