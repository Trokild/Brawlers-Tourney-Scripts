using TMPro;
using UnityEngine;

public class CurrStat_Dual : CurrentStats
{
    private Dualweild_Spawner theHub;
    //public StatCharge curCharge;
    //public StatWeapon curStatWeapon;

    [Header("__Base__")]
    [Space]
    [SerializeField] private GameObject BaseCurrStat_GO;

    [Space]
    [SerializeField] private TextMeshProUGUI maxUnitsTxt;
    [SerializeField] private TextMeshProUGUI spawnRateTxt;
    [SerializeField] private TextMeshProUGUI ladsStrongTxt;
    [SerializeField] private TextMeshProUGUI armorBuildTxt;
    [SerializeField] private TextMeshProUGUI healthBuildTxt;

    public void ConnectDual(Dualweild_Spawner dpw)
    {
        theHub = dpw;
        currentBase = BaseType.Hub;
        DualCurrentStat();

        if (showUnit != null)
        {
            showUnit.UiShopSetColorInt_Orc(theHub.buildingColInt);
        }
        else
        {
            Debug.LogError("No ShowUnit", gameObject);
        }
    }

    void DualCurrentStat()
    {
        unitNameHeader = "Urk Beserkers";
        baseNameHeader = "Urk Hub";

        nameHeader.SetText(unitNameHeader);
    }

    public void UpdateCurrentDualWeapon(StatWeapon sw)
    {
        showUnit.Dual_Wep(sw.statInt);
        curStatWeapon = sw;
        dmgTxt.SetText(sw.weaponDamage.ToString());
        atkSpdTxt.SetText(sw.attackSpeed.ToString());
        armPercTxt.SetText(sw.armorPercing.ToString());
        int weg;

        weg = (curStatWeapon.weight + curStatArmor.weight);
        WeightTxt.SetText(weg.ToString());
        maxBarVal = weg + theHub.unitStat_inf.z;
        float val = (theHub.unitStat_inf.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateCurrentDualArmor(StatArmor ar)
    {
        showUnit.Dual_Arm(ar.statInt);
        curStatArmor = ar;

        int weg;
        ArmTxt.SetText(ar.armor.ToString());
        weg = (curStatWeapon.weight + curStatArmor.weight);
        WeightTxt.SetText(weg.ToString());

        maxBarVal = weg + theHub.unitStat_inf.z;
        float val = (theHub.unitStat_inf.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateCurrentCharge(StatCharge ch)
    {
        curCharge = ch;
        ChargeTxt.SetText(ch.ChargeDmg.ToString());
        showUnit.Dual_Charge(ch.ChargeInt);
    }

    public void UpdateAllStatDualWeild()
    {
        Debug.Log("UpdateAllStats Inf");
        dmgTxt.SetText(theHub.weaponArsenal[theHub.weaponLevel].weaponDamage.ToString());
        atkSpdTxt.SetText(theHub.weaponArsenal[theHub.weaponLevel].attackSpeed.ToString());
        armPercTxt.SetText(theHub.weaponArsenal[theHub.weaponLevel].armorPercing.ToString());

        HealthPTxt.SetText(theHub.unitStat_inf.x.ToString());
        int charDmg = theHub.chargeStat[theHub.chargeLvl].ChargeDmg + theHub.weaponArsenal[theHub.weaponLevel].chargeDamage;
        ChargeTxt.SetText(charDmg.ToString());

        int wgt;
        ArmTxt.SetText(theHub.armorWardrobe[theHub.armorLevel].armor.ToString());
        wgt = (theHub.weaponArsenal[theHub.weaponLevel].weight + theHub.armorWardrobe[theHub.armorLevel].weight);
        WeightTxt.SetText(wgt.ToString());

        StrengthTxt.SetText(theHub.unitStat_inf.z.ToString());

        curStatArmor = theHub.armorWardrobe[theHub.armorLevel];
        curStatWeapon = theHub.weaponArsenal[theHub.weaponLevel];
        curCharge = theHub.chargeStat[theHub.chargeLvl];

        maxBarVal = wgt + theHub.unitStat_inf.z;
        float val = (theHub.unitStat_inf.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;

        if (!started)
        {
            int colInt = theHub.playerRef.colorInt;
            //showUnit.SetTheColor(colInt);
            started = true;
        }
    }

    public void ShowTheDual()
    {
        BaseCurrStat_GO.SetActive(false);
        UnitCurrStat_GO.SetActive(true);
        showUnit.DualShow(curStatWeapon.statInt, curStatArmor.statInt);
        nameHeader.SetText(unitNameHeader);
    }

    public override void ShowTheBase()
    {
        showUnit.BaseShowDual();

        nameHeader.SetText(baseNameHeader);
        BaseCurrStat_GO.SetActive(true);
        UnitCurrStat_GO.SetActive(false);

        UpdateCurretBase();
    }

    public override void UpdateCurretBase()
    {
        Unit_Spawner us = theHub;
        BuildingHealth bh = theHub.GetComponent<BuildingHealth>();

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
