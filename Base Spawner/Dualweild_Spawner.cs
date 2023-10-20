using UnityEngine;
using FoW;

public class Dualweild_Spawner : Unit_Spawner
{
    public GameObject UnitInfantry;
    public Vector3Int unitStat_inf; // x = health, y = stamina, z = strenght
    public bool canCharge = false;
    [Range(0, 2)]
    public int chargeLvl = 0;
    [HideInInspector]
    public int[] chargePrice;
    public StatCharge[] chargeStat; //just for orcs ??

    [Range(0, 6)]
    public int weaponLevel;
    public StatWeapon[] weaponArsenal;
    [HideInInspector]
    public int[] weaponPrice;

    protected override void Start()
    {
        base.Start();
    }

    protected override void SetPrices()
    {
        weaponPrice = new int[weaponArsenal.Length];
        for (int i = 0; i < weaponArsenal.Length; i++)
        {
            weaponPrice[i] = weaponArsenal[i].prize;
        }

        armorPrice = new int[armorWardrobe.Length];
        for (int i = 0; i < armorWardrobe.Length; i++)
        {
            armorPrice[i] = armorWardrobe[i].prize;
        }

        chargePrice = new int[chargeStat.Length];
        for (int i = 0; i < chargeStat.Length; i++)
        {
            chargePrice[i] = chargeStat[i].prize;
        }

        Debug.Log(gameObject + " new Infantry set Prises");
    }

    protected override void SpawnUnit()
    {
        bool isHere = availableSpace.Somethinghere;
        if (!isHere && isProducing)
        {
            if (NumUnits >= MaxNumUnits)
            {
                isProducing = false;
                cur_Area = 0;
                cur_Row = 0;
                areaSelector.transform.localPosition = Areas[cur_Area];
                Spawner.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                SpawnInfantry();
            }
        }
    }

    void SpawnInfantry()
    {
        GameObject gameObjectUnit = (GameObject)Instantiate(UnitInfantry, Spawner.position, transform.rotation);
        Infantry spawnedInf = gameObjectUnit.GetComponent<Infantry>();
        UnitHealth spawnedHealth = gameObjectUnit.GetComponent<UnitHealth>();
        Apperance_DualWeild apperance = gameObjectUnit.GetComponentInChildren<Apperance_DualWeild>();
        if (!isAI)
        {
            FogOfWarUnit fogu = gameObjectUnit.AddComponent<FogOfWarUnit>();
            fogu.circleRadius = rangeFogVision;
            fogu.team = buildingTeam;
        }

        spawnedInf.mother = this;
        NumUnits += 1;

        spawnedInf.superCharger = canCharge;
        if(canCharge)
        {
            apperance.DualArmorWepCharge(armorLevel, weaponLevel, chargeLvl);
        }
        else
        {
            apperance.DualArmorWep(armorLevel, weaponLevel);
        }

        spawnedHealth.SetBaseHealth(unitStat_inf.x, armorWardrobe[armorLevel]);
        Weight = (weaponArsenal[weaponLevel].weight + armorWardrobe[armorLevel].weight);


        if (isStrong)
        {
            spawnedHealth.xpReward += 10;
            apperance.Bigger(10, weaponLevel);
        }

        Vector3Int ssw = new Vector3Int(unitStat_inf.y, unitStat_inf.z, Weight);
        spawnedInf.SetUpStatsUnit(weaponArsenal[weaponLevel], ssw, chargeStat[chargeLvl].ChargeDmg);

        spawnedHealth.TeamId(buildingTeam, buildingId, buildingColInt);
    }

    protected override void SetStartingStats()
    {
        unitStat_inf.x = startingStat.start_Health;
        unitStat_inf.y = startingStat.start_Stamina;
        unitStat_inf.z = startingStat.start_Strenght;
    }

    public override void Stronger_Units()
    {
        if (isStrong == false)
        {
            unitStat_inf.z = (strongerInt + unitStat_inf.z);
            isStrong = true;
        }
    }

    public override void MoreUnits_One()
    {
        MaxNumUnits = spawnNum[0]; //10 inf
        Colums = 5;

        //Rows = 2;
        //MaxAreas = 2;
        Areas[0] = new Vector3(-5, 0, 16);
        //Areas[1] = new Vector3(1, 0, 14);
    }

    public override void MoreUnits_Two()
    {
        MaxNumUnits = spawnNum[1];   //12 inf
        Colums = 4;
        Rows = 3;
    }

    public override void MoreUnits_Three()
    {
        MaxNumUnits = spawnNum[2];   //15 inf
        Colums = 5;
    }
}

