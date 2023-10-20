using TMPro;
using UnityEngine;

public class CurrStat_Cavalry : CurrentStats
{
    private Cavalry_Spawner TheCav;
    //public StatWeapon curStatWeapon;
    //public StatArmor curStatShield;
    // public StatHorse curHorse ??

    public void ConnetctCavalryStable(Cavalry_Spawner cspw)
    {
        TheCav = cspw;
        currentBase = BaseType.Stable;
        CavalryCurrentStat();
    }

    void CavalryCurrentStat()
    {
        unitNameHeader = "Cavalry";
        baseNameHeader = "Stable";
        nameHeader.SetText(unitNameHeader);
    }

    public void UpdateCurrentStatCavalry(StatWeapon sw)
    {
        showUnit.CavalryWep(sw.statInt);
        curStatWeapon = sw;
        dmgTxt.SetText(sw.weaponDamage.ToString());
        atkSpdTxt.SetText(sw.attackSpeed.ToString());
        armPercTxt.SetText(sw.armorPercing.ToString());
        int weg;

        if (TheCav.hasShield)
        {
            weg = (curStatWeapon.weight + curStatArmor.weight + curStatShield.weight);
            WeightTxt.SetText(weg.ToString());
        }
        else
        {
            weg = (curStatWeapon.weight + curStatArmor.weight);
            WeightTxt.SetText(weg.ToString());
        }
        maxBarVal = weg + TheCav.unitStat_cav.z;
        float val = (TheCav.unitStat_cav.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateCurrentStatArmorCavalry(StatArmor ar)
    {
        showUnit.CavalryArmor(ar.statInt);
        curStatArmor = ar;
        int weg;

        if (TheCav.hasShield)
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

        maxBarVal = weg + TheCav.unitStat_cav.z;
        float val = (TheCav.unitStat_cav.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateCurrentStatShieldCavalry(StatArmor shi)
    {
        showUnit.CavalryShield();
        curStatShield = shi;

        ArmTxt.SetText("(+" + curStatShield.armor.ToString() + ") " + curStatArmor.armor.ToString());
        int weg = (curStatWeapon.weight + curStatArmor.weight + curStatShield.weight);
        WeightTxt.SetText(weg.ToString());

        maxBarVal = weg + TheCav.unitStat_cav.z;
        float val = (TheCav.unitStat_cav.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateCurrentStatHorse(StatHorse hrs)
    {
        showUnit.CavalryHorse(hrs.statInt);
    }

    public void UpdateAllStats_Cavalry()
    {
        Debug.Log("UpdateAllStats Cav");
        dmgTxt.SetText(TheCav.weaponArsenal[TheCav.weaponLevel].weaponDamage.ToString());
        atkSpdTxt.SetText(TheCav.weaponArsenal[TheCav.weaponLevel].attackSpeed.ToString());
        armPercTxt.SetText(TheCav.weaponArsenal[TheCav.weaponLevel].armorPercing.ToString());

        HealthPTxt.SetText(TheCav.unitStat_cav.x.ToString());
        int charDmg = TheCav.chargeBonus_cav + TheCav.weaponArsenal[TheCav.weaponLevel].chargeDamage;
        ChargeTxt.SetText(charDmg.ToString());

        int wgt;
        if (!TheCav.hasShield)
        {
            ArmTxt.SetText(TheCav.armorWardrobe[TheCav.armorLevel].armor.ToString());
            wgt = (TheCav.weaponArsenal[TheCav.weaponLevel].weight + TheCav.armorWardrobe[TheCav.armorLevel].weight);
            WeightTxt.SetText(wgt.ToString());
        }
        else
        {
            ArmTxt.SetText("(+" + TheCav.shieldStack.armor.ToString() + ") " + TheCav.armorWardrobe[TheCav.armorLevel].armor.ToString());
            wgt = (TheCav.weaponArsenal[TheCav.weaponLevel].weight + TheCav.armorWardrobe[TheCav.armorLevel].weight + TheCav.armorWardrobe[TheCav.armorLevel].weight);
            WeightTxt.SetText(wgt.ToString());
            curStatShield = TheCav.shieldStack;
        }

        StrengthTxt.SetText(TheCav.unitStat_cav.z.ToString());

        curStatArmor = TheCav.armorWardrobe[TheCav.armorLevel];
        curStatWeapon = TheCav.weaponArsenal[TheCav.weaponLevel];

        maxBarVal = wgt + TheCav.unitStat_cav.z;
        float val = (TheCav.unitStat_cav.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;

        if (!started)
        {
            int colInt = TheCav.playerRef.colorInt;
            //showUnit.SetTheColor(colInt);
            started = true;
        }
    }

    public void ShowTheCav()
    {
        //BaseCurrStat_GO.SetActive(false);
        UnitCurrStat_GO.SetActive(true);
        // weapon, armor, shield
        if (TheCav.hasShield)
        {
            showUnit.CavalryShowStart(curStatWeapon.statInt, curStatArmor.statInt, TheCav.cavLvl);
        }
        else
        {
            showUnit.CavalryShow(curStatWeapon.statInt, curStatArmor.statInt, TheCav.cavLvl);
        }
        nameHeader.SetText(unitNameHeader);
    }
}
