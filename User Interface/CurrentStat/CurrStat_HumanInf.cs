using TMPro;
using UnityEngine;

public class CurrStat_HumanInf : CurrentStats
{
    private Infantry_Spawner theBase;
    //public StatWeapon curStatWeapon;
    //public StatArmor curStatShield;

    [Header("__Base__")]
    [Space]
    [SerializeField] private GameObject BaseCurrStat_GO;

    [Space]
    [SerializeField] private TextMeshProUGUI maxUnitsTxt;
    [SerializeField] private TextMeshProUGUI spawnRateTxt;
    [SerializeField] private TextMeshProUGUI ladsStrongTxt;
    [SerializeField] private TextMeshProUGUI armorBuildTxt;
    [SerializeField] private TextMeshProUGUI healthBuildTxt;

    public void ConnectInfantry(Infantry_Spawner ispw)
    {
        theBase = ispw;
        currentBase = BaseType.Barracks;
        InfantryCurrentStat();
        if (showUnit != null)
        {
            showUnit.UiShopSetColorInt_Human(theBase.buildingColInt);
        }
        else
        {
            Debug.LogError("No ShowUnit", gameObject);
        }
    }

    public override void ShowTheUnit()
    {
        BaseCurrStat_GO.SetActive(false);
        UnitCurrStat_GO.SetActive(true);
        // weapon, armor, shield
        if (theBase.hasShield)
        {
            showUnit.UnitShow(curStatWeapon.statInt, curStatArmor.statInt, curStatShield.statInt);
        }
        else
        {
            showUnit.UnitShowStart(curStatWeapon.statInt, curStatArmor.statInt);
        }
        nameHeader.SetText(unitNameHeader);
    }

    public override void ShowTheBase()
    {
        showUnit.BaseShow();

        nameHeader.SetText(baseNameHeader);
        BaseCurrStat_GO.SetActive(true);
        UnitCurrStat_GO.SetActive(false);

        UpdateCurretBase();
    }

    void InfantryCurrentStat()
    {
        unitNameHeader = "Infantry";
        baseNameHeader = "Barracks";
        nameHeader.SetText(unitNameHeader);
    }

    public void UpdateCurrentStatWeapon(StatWeapon sw)
    {
        showUnit.InfantryWep(sw.statInt);
        curStatWeapon = sw;
        dmgTxt.SetText(sw.weaponDamage.ToString());
        atkSpdTxt.SetText(sw.attackSpeed.ToString());
        armPercTxt.SetText(sw.armorPercing.ToString());
        int weg;

        if (theBase.hasShield)
        {
            weg = (curStatWeapon.weight + curStatArmor.weight + curStatShield.weight);
            WeightTxt.SetText(weg.ToString());
        }
        else
        {
            weg = (curStatWeapon.weight + curStatArmor.weight);
            WeightTxt.SetText(weg.ToString());
        }
        maxBarVal = weg + theBase.unitStat_inf.z;
        float val = (theBase.unitStat_inf.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateCurrentStatArmor(StatArmor ar)
    {
        switch (currentBase)
        {
            case BaseType.Barracks:
                showUnit.InfantryArm(ar.statInt);
                break;
            case BaseType.Archery:
                showUnit.ArcherArm(ar.statInt);
                break;
            case BaseType.Garrison:
                showUnit.SpearArm(ar.statInt);
                break;
        }

        curStatArmor = ar;
        int weg;

        if (theBase.hasShield)
        {
            ArmTxt.SetText("(+" + curStatShield.armor.ToString() + ") " + curStatArmor.armor.ToString());
            weg = (curStatWeapon.weight + curStatArmor.weight + curStatShield.weight);
            WeightTxt.SetText(weg.ToString());
        }
        else
        {
            ArmTxt.SetText(ar.armor.ToString());
            weg = (curStatWeapon.weight + curStatArmor.weight);
            WeightTxt.SetText(weg.ToString());
        }

        maxBarVal = weg + theBase.unitStat_inf.z;
        float val = (theBase.unitStat_inf.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateCurrentStatShield(StatArmor shi)
    {
        showUnit.InfantryArm(curStatArmor.statInt, shi.statInt);
        curStatShield = shi;

        ArmTxt.SetText("(+" + curStatShield.armor.ToString() + ") " + curStatArmor.armor.ToString());
        int weg = (curStatWeapon.weight + curStatArmor.weight + curStatShield.weight);
        WeightTxt.SetText(weg.ToString());

        maxBarVal = weg + theBase.unitStat_inf.z;
        float val = (theBase.unitStat_inf.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public override void UpdateAllStats()
    {
        //Debug.Log("UpdateAllStats Inf");
        dmgTxt.SetText(theBase.weaponArsenal[theBase.weaponLevel].weaponDamage.ToString());
        atkSpdTxt.SetText(theBase.weaponArsenal[theBase.weaponLevel].attackSpeed.ToString());
        armPercTxt.SetText(theBase.weaponArsenal[theBase.weaponLevel].armorPercing.ToString());

        HealthPTxt.SetText(theBase.unitStat_inf.x.ToString());
        //int charDmg = theBase.chargeBonus_inf + theBase.weaponArsenal[theBase.weaponLevel].chargeDamage;
        //int charDmg = 0;
        //ChargeTxt.SetText("No Charge");

        int wgt;
        if (!theBase.hasShield)
        {
            ArmTxt.SetText(theBase.armorWardrobe[theBase.armorLevel].armor.ToString());
            wgt = (theBase.weaponArsenal[theBase.weaponLevel].weight + theBase.armorWardrobe[theBase.armorLevel].weight);
            WeightTxt.SetText(wgt.ToString());
        }
        else
        {
            ArmTxt.SetText("(+" + theBase.shieldStack[theBase.shieldLevel].armor.ToString() + ") " + theBase.armorWardrobe[theBase.armorLevel].armor.ToString());
            wgt = (theBase.weaponArsenal[theBase.weaponLevel].weight + theBase.armorWardrobe[theBase.armorLevel].weight + theBase.armorWardrobe[theBase.armorLevel].weight);
            WeightTxt.SetText(wgt.ToString());
            curStatShield = theBase.shieldStack[theBase.shieldLevel];
        }

        StrengthTxt.SetText(theBase.unitStat_inf.z.ToString());

        curStatArmor = theBase.armorWardrobe[theBase.armorLevel];
        curStatWeapon = theBase.weaponArsenal[theBase.weaponLevel];

        maxBarVal = wgt + theBase.unitStat_inf.z;
        float val = (theBase.unitStat_inf.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;

        if (!started)
        {
            int colInt = theBase.playerRef.colorInt;
            //showUnit.SetTheColor(colInt);
            started = true;
        }
    }

    public override void UpdateCurretBase()
    {
        Unit_Spawner us = theBase;
        BuildingHealth bh = theBase.GetComponent<BuildingHealth>();

        maxUnitsTxt.SetText(us.MaxNumUnits.ToString());
        float unitPerSec = (us.timeColum * us.Colums) + (us.timeRow * us.Rows) + (us.timeArea * us.MaxAreas);
        float ups = Mathf.Round(unitPerSec);
        spawnRateTxt.SetText(ups.ToString());

        if (us.isStrong)
        {
            ladsStrongTxt.SetText("Yes");
            ladsStrongTxt.color = green;
        }
        else
        {
            ladsStrongTxt.SetText("No");
            ladsStrongTxt.color = orange;
        }
        armorBuildTxt.SetText(bh.armor.GetValue().ToString());
        healthBuildTxt.SetText(bh.max_Health.ToString());
    }
}
