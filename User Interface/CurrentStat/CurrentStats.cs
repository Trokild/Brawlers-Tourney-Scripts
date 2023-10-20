using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CurrentStats : MonoBehaviour
{
    [SerializeField] protected ShowUnit showUnit;
    protected bool started = false;

    public enum RaceType { Human, Orc}
    public RaceType raceCurrStats;
    public enum BaseType { Barracks, Archery, Garrison, Stable, Hub, Boulder } //infantry Barracks, Archery Range, Spear Garrison, stallion ranch
    public BaseType currentBase;

    [Space]
    [SerializeField] protected TextMeshProUGUI nameHeader;
    [Space]
    [Header("__Unit__")]
    [Space]
    [SerializeField] protected GameObject UnitCurrStat_GO;
    protected string unitNameHeader;
    [Space]
    [SerializeField] protected TextMeshProUGUI dmgTxt;
    [SerializeField] protected TextMeshProUGUI atkSpdTxt;
    [SerializeField] protected TextMeshProUGUI armPercTxt;
    [SerializeField] protected TextMeshProUGUI ArmTxt;
    [SerializeField] protected TextMeshProUGUI HealthPTxt;
    [SerializeField] protected TextMeshProUGUI StrengthTxt;
    [SerializeField] protected TextMeshProUGUI WeightTxt;

    [SerializeField] protected TextMeshProUGUI rangeTxt;
    [SerializeField] protected TextMeshProUGUI BonusCavTxt;
    [SerializeField] protected TextMeshProUGUI ChargeTxt;

    [Space]
    public StatArmor curStatArmor;

    public StatWeapon curStatWeapon;
    public StatArmor curStatShield;
    public StatArrow curArrow;
    public StatBow curBow;
    public StatSpear curSpear;
    public StatCharge curCharge;
    public StatRock curRock;
    public StatGoogels curGoogle;

    [Space]
    [SerializeField] protected Image Strenght_bar;
    protected int maxBarVal;

    [SerializeField]
    protected Color green;
    [SerializeField]
    protected Color orange;
    [Space]
    protected string baseNameHeader;

    public virtual void ShowTheUnit()
    {
        //BaseCurrStat_GO.SetActive(false);
        //UnitCurrStat_GO.SetActive(true);
        //// weapon, armor, shield
        //if (theBase.hasShield)
        //{
        //    showUnit.UnitShow(curStatWeapon.statInt, curStatArmor.statInt, curStatShield.statInt);
        //}
        //else
        //{
        //    showUnit.UnitShowStart(curStatWeapon.statInt, curStatArmor.statInt);
        //}
        nameHeader.SetText(unitNameHeader);
    }

    public virtual void UpdateAllStats()
    {
        ////Debug.Log("UpdateAllStats Inf");
        //dmgTxt.SetText(theBase.weaponArsenal[theBase.weaponLevel].weaponDamage.ToString());
        //atkSpdTxt.SetText(theBase.weaponArsenal[theBase.weaponLevel].attackSpeed.ToString());
        //armPercTxt.SetText(theBase.weaponArsenal[theBase.weaponLevel].armorPercing.ToString());

        //HealthPTxt.SetText(theBase.unitStat_inf.x.ToString());
        ////int charDmg = theBase.chargeBonus_inf + theBase.weaponArsenal[theBase.weaponLevel].chargeDamage;
        ////int charDmg = 0;
        ////ChargeTxt.SetText("No Charge");

        //int wgt;
        //if (!theBase.hasShield)
        //{
        //    ArmTxt.SetText(theBase.armorWardrobe[theBase.armorLevel].armor.ToString());
        //    wgt = (theBase.weaponArsenal[theBase.weaponLevel].weight + theBase.armorWardrobe[theBase.armorLevel].weight);
        //    WeightTxt.SetText(wgt.ToString());
        //}
        //else
        //{
        //    ArmTxt.SetText("(+" + theBase.shieldStack[theBase.shieldLevel].armor.ToString() + ") " + theBase.armorWardrobe[theBase.armorLevel].armor.ToString());
        //    wgt = (theBase.weaponArsenal[theBase.weaponLevel].weight + theBase.armorWardrobe[theBase.armorLevel].weight + theBase.armorWardrobe[theBase.armorLevel].weight);
        //    WeightTxt.SetText(wgt.ToString());
        //    curStatShield = theBase.shieldStack[theBase.shieldLevel];
        //}

        //StrengthTxt.SetText(theBase.unitStat_inf.z.ToString());

        //curStatArmor = theBase.armorWardrobe[theBase.armorLevel];
        //curStatWeapon = theBase.weaponArsenal[theBase.weaponLevel];

        //maxBarVal = wgt + theBase.unitStat_inf.z;
        //float val = (theBase.unitStat_inf.z * 1f) / maxBarVal;
        //Strenght_bar.fillAmount = val;

        //if (!started)
        //{
        //    int colInt = theBase.playerRef.colorInt;
        //    //showUnit.SetTheColor(colInt);
        //    started = true;
        //}
    }

    public virtual void ShowTheBase()
    {
        switch (currentBase)
        {
            case BaseType.Barracks:
                showUnit.BaseShow();
                break;

            case BaseType.Hub:
                showUnit.BaseShowDual();
                break;

            default:
                showUnit.BaseShow();
                break;
        }

        nameHeader.SetText(baseNameHeader);
        //BaseCurrStat_GO.SetActive(true);
        UnitCurrStat_GO.SetActive(false);

        UpdateCurretBase();
    }

    public void ConnectShowUnit(ShowUnit su)
    {
        showUnit = su;
    }

    #region Base
    public virtual void UpdateCurretBase()
    {
        //Unit_Spawner us;
        //BuildingHealth bh;
        //switch (currentBase)
        //{
        //    case (BaseType.Barracks):
        //        us = theBase;
        //        bh = theBase.GetComponent<BuildingHealth>();
        //        break;

        //    case (BaseType.Archery):
        //        us = TheArch;
        //        bh = TheArch.GetComponent<BuildingHealth>();
        //        break;

        //    case (BaseType.Garrison):
        //        us = TheSpear;
        //        bh = TheSpear.GetComponent<BuildingHealth>();
        //        break;

        //    case (BaseType.Hub):
        //        us = theHub;
        //        bh = theHub.GetComponent<BuildingHealth>();
        //        break;

        //    case (BaseType.Boulder):
        //        us = TheThrow;
        //        bh = TheThrow.GetComponent<BuildingHealth>();
        //        break;

        //    default:
        //        us = theBase;
        //        bh = theBase.GetComponent<BuildingHealth>();
        //        break;
        //}
        //maxUnitsTxt.SetText(us.MaxNumUnits.ToString());
        //float unitPerSec = (us.timeColum * us.Colums) + (us.timeRow * us.Rows) + (us.timeArea * us.MaxAreas);
        //float ups = Mathf.Round(unitPerSec);
        //spawnRateTxt.SetText(ups.ToString());

        //if (us.isStrong)
        //{
        //    ladsStrongTxt.SetText("Yes");
        //    ladsStrongTxt.color = green;
        //}
        //else
        //{
        //    ladsStrongTxt.SetText("No");
        //    ladsStrongTxt.color = orange;
        //}
        //armorBuildTxt.SetText(bh.armor.GetValue().ToString());
        //healthBuildTxt.SetText(bh.max_Health.ToString());
    }
    #endregion

    //public void ShowTheBase()
    //{
    //    switch (currentBase)
    //    {
    //        case BaseType.Barracks:
    //            showUnit.BaseShow();
    //            break;

    //        case BaseType.Hub:
    //            showUnit.BaseShowDual();
    //            break;

    //        default:
    //            showUnit.BaseShow();
    //            break;
    //    }

    //    nameHeader.SetText(baseNameHeader);
    //    //BaseCurrStat_GO.SetActive(true);
    //    UnitCurrStat_GO.SetActive(false);

    //    UpdateCurretBase();
    //}

    //public void ShowTheUnit()
    //{
    //    BaseCurrStat_GO.SetActive(false);
    //    UnitCurrStat_GO.SetActive(true);
    //    // weapon, armor, shield
    //    if (theBase.hasShield)
    //    {
    //        showUnit.UnitShow(curStatWeapon.statInt, curStatArmor.statInt, curStatShield.statInt);
    //    }  
    //    else
    //    {
    //        showUnit.UnitShowStart(curStatWeapon.statInt, curStatArmor.statInt);
    //    }
    //    nameHeader.SetText(unitNameHeader);
    //}
}
