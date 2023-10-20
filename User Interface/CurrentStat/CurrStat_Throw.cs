using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrStat_Throw : CurrentStats
{
    private Thrower_Spawner TheThrow;
    //public StatRock curRock;
    //public StatGoogels curGoogle;
    //public StatWeapon curStatWeapon;

    public void ConnectThrowerBoulder(Thrower_Spawner tspw)
    {
        TheThrow = tspw;
        currentBase = BaseType.Boulder;
        ThrowerCurrentStat();
    }

    public void UpdateCurrentRock(StatRock rk)
    {
        curRock = rk;
        showUnit.Throw_Rock(rk.statInt);

        int weg;
        if (TheThrow.hasMeleeWeapon)
        {
            dmgTxt.SetText(curStatWeapon.weaponDamage.ToString() + ", " + rk.weaponDamage.ToString());
            atkSpdTxt.SetText(curStatWeapon.attackSpeed.ToString() + ", " + rk.attackSpeed.ToString());
            armPercTxt.SetText(curStatWeapon.armorPercing.ToString() + ", " + rk.armorPercing.ToString());

            weg = (curRock.weight + curStatArmor.weight + curStatWeapon.weight);
        }
        else
        {
            dmgTxt.SetText(rk.weaponDamage.ToString());
            atkSpdTxt.SetText(rk.attackSpeed.ToString());
            armPercTxt.SetText(rk.armorPercing.ToString());

            weg = (curRock.weight + curStatArmor.weight);
        }

        if (TheThrow.hasGoogels)
        {
            weg = (weg + TheThrow.googels.weight);
            rangeTxt.SetText("(+" + TheThrow.googels.rangeBonus + ") " + rk.range);
        }
        else
        {
            rangeTxt.SetText(rk.range.ToString());
        }

        WeightTxt.SetText(weg.ToString());

        maxBarVal = weg + TheThrow.unitStat_Throw.z;
        float val = (TheThrow.unitStat_Throw.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateCurrentGoogles(StatGoogels gls)
    {
        curGoogle = gls;
        rangeTxt.SetText("(+" + gls.rangeBonus + ") " + curRock.range);
        ArmTxt.SetText("(+" + gls.armorBonus + ") " + curStatArmor.armor);
        showUnit.Throw_Googles();

        int weg;
        if (TheThrow.hasMeleeWeapon)
        {
            weg = (curRock.weight + curStatArmor.weight + curStatWeapon.weight + gls.weight);
        }
        else
        {
            weg = (curRock.weight + curStatArmor.weight + gls.weight);
        }

        WeightTxt.SetText(weg.ToString());

        maxBarVal = weg + TheThrow.unitStat_Throw.z;
        float val = (TheThrow.unitStat_Throw.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateCurrentThrowArmor(StatArmor ar)
    {
        showUnit.Throw_Armor(ar.statInt);
        curStatArmor = ar;

        int weg;

        if (TheThrow.hasMeleeWeapon)
        {
            weg = (curRock.weight + curStatArmor.weight + curStatWeapon.weight);
        }
        else
        {
            weg = (curRock.weight + curStatArmor.weight);
        }

        if (TheThrow.hasGoogels)
        {
            weg = (weg + TheThrow.googels.weight);
            ArmTxt.SetText("(+" + TheThrow.googels.armorBonus + ") " + ar.armor.ToString());
        }
        else
        {
            ArmTxt.SetText(ar.armor.ToString());
        }

        WeightTxt.SetText(weg.ToString());

        maxBarVal = weg + TheThrow.unitStat_Throw.z;
        float val = (TheThrow.unitStat_Throw.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateCurrentHandWeap(StatWeapon wep)
    {
        showUnit.Throw_HandWep(wep.statInt);
        curStatWeapon = wep;
        dmgTxt.SetText(wep.weaponDamage.ToString() + ", " + curRock.weaponDamage.ToString());
        atkSpdTxt.SetText(wep.attackSpeed.ToString() + ", " + curRock.attackSpeed.ToString());
        armPercTxt.SetText(wep.armorPercing.ToString() + ", " + curRock.armorPercing.ToString());

        int weg;
        weg = (wep.weight + curStatArmor.weight + curRock.weight);
        if (TheThrow.hasGoogels)
        {
            weg = (weg + TheThrow.googels.weight);
        }
        WeightTxt.SetText(weg.ToString());

        maxBarVal = weg + TheThrow.unitStat_Throw.z;
        float val = (TheThrow.unitStat_Throw.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateAllStatsThrower()
    {
        Debug.Log("UpdateAllStats Thrower");
        HealthPTxt.SetText(TheThrow.unitStat_Throw.x.ToString());
        curStatArmor = TheThrow.armorWardrobe[TheThrow.armorLevel];
        curStatWeapon = TheThrow.handWeapon[TheThrow.handWeaponLevel];
        curRock = TheThrow.rocks[TheThrow.rockLevel];

        int wgt;

        if (TheThrow.hasMeleeWeapon)
        {
            dmgTxt.SetText(TheThrow.handWeapon[TheThrow.handWeaponLevel].weaponDamage.ToString() + ", " + TheThrow.rocks[TheThrow.rockLevel].weaponDamage.ToString());
            atkSpdTxt.SetText(TheThrow.handWeapon[TheThrow.handWeaponLevel].attackSpeed.ToString() + ", " + TheThrow.rocks[TheThrow.rockLevel].attackSpeed.ToString());
            armPercTxt.SetText(TheThrow.handWeapon[TheThrow.handWeaponLevel].armorPercing.ToString() + ", " + TheThrow.rocks[TheThrow.rockLevel].armorPercing.ToString());

            wgt = (TheThrow.handWeapon[TheThrow.handWeaponLevel].weight + TheThrow.armorWardrobe[TheThrow.armorLevel].weight + TheThrow.rocks[TheThrow.rockLevel].weight);
        }
        else
        {
            dmgTxt.SetText(TheThrow.rocks[TheThrow.rockLevel].weaponDamage.ToString());
            atkSpdTxt.SetText(TheThrow.rocks[TheThrow.rockLevel].attackSpeed.ToString());
            armPercTxt.SetText(TheThrow.rocks[TheThrow.rockLevel].armorPercing.ToString());

            wgt = (TheThrow.armorWardrobe[TheThrow.armorLevel].weight + TheThrow.rocks[TheThrow.rockLevel].weight);
        }

        if (TheThrow.hasGoogels)
        {
            wgt = (wgt + TheThrow.googels.weight);
            curGoogle = TheThrow.googels;
            rangeTxt.SetText("(+" + TheThrow.googels.rangeBonus + ") " + curRock.range);
            ArmTxt.SetText("(+" + TheThrow.googels.armorBonus + ") " + TheThrow.armorWardrobe[TheThrow.armorLevel].armor.ToString());
        }
        else
        {
            rangeTxt.SetText(curRock.range.ToString());
            ArmTxt.SetText(TheThrow.armorWardrobe[TheThrow.armorLevel].armor.ToString());
        }

        WeightTxt.SetText(wgt.ToString());

        StrengthTxt.SetText(TheThrow.unitStat_Throw.z.ToString());

        maxBarVal = wgt + TheThrow.unitStat_Throw.z;
        float val = (TheThrow.unitStat_Throw.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;

        if (!started)
        {
            int colInt = TheThrow.playerRef.colorInt;
            //showUnit.SetTheColor(colInt);
            started = true;
        }
    }

    void ThrowerCurrentStat()
    {
        unitNameHeader = "Stone Urk";
        baseNameHeader = "Rock Pile";

        nameHeader.SetText(unitNameHeader);
    }

    public void ShowTheThrower()
    {
        //BaseCurrStat_GO.SetActive(false);
        UnitCurrStat_GO.SetActive(true);

        if (TheThrow.hasMeleeWeapon)
        {
            showUnit.OrcThrowAllShow(curRock.statInt, curStatWeapon.statInt, curStatArmor.statInt);
        }
        else
        {
            showUnit.OrcThrowShow(curRock.statInt, curStatArmor.statInt);
        }
    }
}
