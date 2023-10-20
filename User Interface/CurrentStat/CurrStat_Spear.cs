using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrStat_Spear : CurrentStats
{
    private Spearmen_Spawner TheSpear;
    //public StatSpear curSpear;
    //public StatWeapon curStatWeapon;
    //public StatArmor curStatShield;

    public void ConnetctSpearGarrison(Spearmen_Spawner sspw)
    {
        TheSpear = sspw;
        currentBase = BaseType.Garrison;
        SpearCurrentStat();
    }

    void SpearCurrentStat()
    {
        unitNameHeader = "Spearmen";
        baseNameHeader = "Garrison";
        nameHeader.SetText(unitNameHeader);
    }

    public void ShowTheSpear()
    {
        //BaseCurrStat_GO.SetActive(false);
        UnitCurrStat_GO.SetActive(true);
        // weapon, armor, shield
        if (TheSpear.hasShield)
        {
            switch (raceCurrStats)
            {
                case RaceType.Human:
                    showUnit.SpearShow(curSpear.statInt, curStatArmor.statInt, curStatShield.statInt);
                    break;
                case RaceType.Orc:
                    showUnit.OrcSpearShow(curSpear.statInt, curStatArmor.statInt, curStatShield.statInt);
                    break;
            }
        }
        else
        {
            switch (raceCurrStats)
            {
                case RaceType.Human:
                    showUnit.SpearShowStart(curSpear.statInt, curStatArmor.statInt);
                    break;
                case RaceType.Orc:
                    showUnit.OrcSpearShowNoShi(curSpear.statInt, curStatArmor.statInt);
                    break;
            }

        }
        nameHeader.SetText(unitNameHeader);
    }

    public void UpdateCurrentStatSpear(StatSpear sw)
    {
        switch (raceCurrStats)
        {
            case RaceType.Human:
                showUnit.SpearWep(sw.statInt);
                break;
            case RaceType.Orc:
                showUnit.OrcSpr_Wep(sw.statInt);
                break;
        }
        curSpear = sw;
        dmgTxt.SetText(sw.weaponDamage.ToString());
        atkSpdTxt.SetText(sw.attackSpeed.ToString());
        armPercTxt.SetText(sw.armorPercing.ToString());
        int weg;

        if (TheSpear.hasShield)
        {
            weg = (curSpear.weight + curStatArmor.weight + curStatShield.weight);
            WeightTxt.SetText(weg.ToString());
        }
        else
        {
            weg = (curSpear.weight + curStatArmor.weight);
            WeightTxt.SetText(weg.ToString());
        }
        maxBarVal = weg + TheSpear.unitStat_spear.z;
        float val = (TheSpear.unitStat_spear.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateCurrentStatArmorSpear(StatArmor ar)
    {
        switch (raceCurrStats)
        {
            case RaceType.Human:
                showUnit.SpearArm(ar.statInt);
                break;
            case RaceType.Orc:
                showUnit.OrcSpr_Arm(ar.statInt);
                break;
        }

        curStatArmor = ar;
        int weg;

        if (TheSpear.hasShield)
        {
            ArmTxt.SetText("(+" + curStatShield.armor.ToString() + ") " + curStatArmor.armor.ToString());
            weg = (curSpear.weight + curStatArmor.weight + curStatShield.weight);
            WeightTxt.SetText(weg.ToString());
        }
        else
        {
            ArmTxt.SetText(ar.armor.ToString());
            weg = (curSpear.weight + curStatArmor.weight);
            WeightTxt.SetText(weg.ToString());
        }

        maxBarVal = weg + TheSpear.unitStat_spear.z;
        float val = (TheSpear.unitStat_spear.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateCurrentStatShieldSpear(StatArmor shi)
    {
        switch (raceCurrStats)
        {
            case RaceType.Human:
                showUnit.SpearArm(curStatArmor.statInt, shi.statInt);
                break;
            case RaceType.Orc:
                showUnit.OrcSpr_Shi(shi.statInt);
                break;
        }


        curStatShield = shi;

        ArmTxt.SetText("(+" + curStatShield.armor.ToString() + ") " + curStatArmor.armor.ToString());
        int weg = (curSpear.weight + curStatArmor.weight + curStatShield.weight);
        WeightTxt.SetText(weg.ToString());

        maxBarVal = weg + TheSpear.unitStat_spear.z;
        float val = (TheSpear.unitStat_spear.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateAllStatsSpear()
    {
        Debug.Log("UpdateAllStats spear");
        dmgTxt.SetText(TheSpear.spearWalls[TheSpear.weaponLevel].weaponDamage.ToString());
        atkSpdTxt.SetText(TheSpear.spearWalls[TheSpear.weaponLevel].attackSpeed.ToString());
        armPercTxt.SetText(TheSpear.spearWalls[TheSpear.weaponLevel].armorPercing.ToString());

        HealthPTxt.SetText(TheSpear.unitStat_spear.x.ToString());
        BonusCavTxt.SetText(TheSpear.spearWalls[TheSpear.weaponLevel].bonusVsCav.ToString());

        int wgt;
        if (!TheSpear.hasShield)
        {
            ArmTxt.SetText(TheSpear.armorWardrobe[TheSpear.armorLevel].armor.ToString());
            wgt = (TheSpear.spearWalls[TheSpear.weaponLevel].weight + TheSpear.armorWardrobe[TheSpear.armorLevel].weight);
            WeightTxt.SetText(wgt.ToString());
        }
        else
        {
            ArmTxt.SetText("(+" + TheSpear.shieldStack[TheSpear.shieldLevel].armor.ToString() + ") " + TheSpear.armorWardrobe[TheSpear.armorLevel].armor.ToString());
            wgt = (TheSpear.spearWalls[TheSpear.weaponLevel].weight + TheSpear.armorWardrobe[TheSpear.armorLevel].weight + TheSpear.armorWardrobe[TheSpear.armorLevel].weight);
            WeightTxt.SetText(wgt.ToString());
            curStatShield = TheSpear.shieldStack[TheSpear.shieldLevel];
        }

        StrengthTxt.SetText(TheSpear.unitStat_spear.z.ToString());

        curStatArmor = TheSpear.armorWardrobe[TheSpear.armorLevel];
        curSpear = TheSpear.spearWalls[TheSpear.weaponLevel];

        maxBarVal = wgt + TheSpear.unitStat_spear.z;
        float val = (TheSpear.unitStat_spear.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;

        if (!started)
        {
            int colInt = TheSpear.playerRef.colorInt;
            //showUnit.SetTheColorSpear(colInt);
            started = true;
        }
    }
}
