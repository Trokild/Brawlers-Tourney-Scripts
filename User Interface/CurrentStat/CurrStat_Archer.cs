using TMPro;
using UnityEngine;

public class CurrStat_Archer : CurrentStats
{
    private Archer_Spawner TheArch;
    //public StatArrow curArrow;
    //public StatBow curBow;

    public void ConnectArchery(Archer_Spawner aspw)
    {
        TheArch = aspw;
        currentBase = BaseType.Archery;
        ArcherCurrentStat();
    }

    void ArcherCurrentStat()
    {
        //GO_rangeTxt.SetActive(true);
        //GO_rangeTxtStatic.SetActive(true);

        //GO_ChargeTxt.SetActive(false);
        //GO_ChargeTxtStatic.SetActive(false);

        //GO_BonusCavStatic.SetActive(false);
        //GO_BonusCavTxt.SetActive(false);

        unitNameHeader = "Archer";
        baseNameHeader = "Archery";
        nameHeader.SetText(unitNameHeader);
    }

    public void ShowTheArc()
    {
        //BaseCurrStat_GO.SetActive(false);
        UnitCurrStat_GO.SetActive(true);
        //armor, bow, arrow)
        showUnit.UnitShowArch(curStatArmor.statInt, curBow.statInt, curArrow.statInt);
        nameHeader.SetText(unitNameHeader);
    }

    public void UpdateCurrentStatArrow(StatArrow aro)
    {
        showUnit.ArcherShi(aro.statInt);
        curArrow = aro;
        dmgTxt.SetText(aro.weaponDamage.ToString());
        armPercTxt.SetText(aro.armorPercing.ToString());
        int weg;

        weg = (curArrow.weight + curStatArmor.weight + curBow.weight);
        WeightTxt.SetText(weg.ToString());

        maxBarVal = weg + TheArch.unitStat_Arch.z;
        float val = (TheArch.unitStat_Arch.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UppdateCurrentStatBow(StatBow bow)
    {
        showUnit.ArcherWep(bow.statInt);
        curBow = bow;
        atkSpdTxt.SetText(bow.attackSpeed.ToString());
        rangeTxt.SetText(bow.range.ToString());
        int weg;

        weg = (curArrow.weight + curStatArmor.weight + curBow.weight);
        WeightTxt.SetText(weg.ToString());

        maxBarVal = weg + TheArch.unitStat_Arch.z;
        float val = (TheArch.unitStat_Arch.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateCurrentStatArmorArcher(StatArmor ara)
    {
        showUnit.ArcherArm(ara.statInt);
        curStatArmor = ara;
        int weg;

        ArmTxt.SetText(ara.armor.ToString());
        weg = (curArrow.weight + curStatArmor.weight + curBow.weight);
        WeightTxt.SetText(weg.ToString());

        maxBarVal = weg + TheArch.unitStat_Arch.z;
        float val = (TheArch.unitStat_Arch.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;
    }

    public void UpdateAllStats_Archer()
    {
        Debug.Log("UpdateAllStats Arc");
        dmgTxt.SetText(TheArch.arrowRack[TheArch.arrowLvl].weaponDamage.ToString());
        atkSpdTxt.SetText(TheArch.bowHolder[TheArch.bowLvl].attackSpeed.ToString());
        armPercTxt.SetText(TheArch.arrowRack[TheArch.arrowLvl].armorPercing.ToString());

        HealthPTxt.SetText(TheArch.unitStat_Arch.x.ToString());
        int wgt;

        ArmTxt.SetText(TheArch.armorWardrobe[TheArch.armorLevel].armor.ToString());
        wgt = (TheArch.bowHolder[TheArch.bowLvl].weight + TheArch.armorWardrobe[TheArch.armorLevel].weight + TheArch.arrowRack[TheArch.arrowLvl].weight);
        WeightTxt.SetText(wgt.ToString());

        StrengthTxt.SetText(TheArch.unitStat_Arch.z.ToString());

        curStatArmor = TheArch.armorWardrobe[TheArch.armorLevel];
        curArrow = TheArch.arrowRack[TheArch.arrowLvl];
        curBow = TheArch.bowHolder[TheArch.bowLvl];

        maxBarVal = wgt + TheArch.unitStat_Arch.z;
        float val = (TheArch.unitStat_Arch.z * 1f) / maxBarVal;
        Strenght_bar.fillAmount = val;

        if (!started)
        {
            int colInt = TheArch.playerRef.colorInt;
            //showUnit.SetTheColorArch(colInt);
            started = true;
        }
    }
}
