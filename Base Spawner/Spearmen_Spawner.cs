using UnityEngine;
using FoW;

public class Spearmen_Spawner : Unit_Spawner
{
    public GameObject UnitSpearmen;

    public Vector3Int unitStat_spear; // x = health, y = stamina, z = strenght
    public int bonusVcav;

    public int weaponLevel;
    public StatSpear[] spearWalls;
    [HideInInspector]
    public int[] spearPrice;

    [Range(0, 4)]
    public int shieldLevel;
    public StatArmor[] shieldStack;
    [HideInInspector]
    public int[] shieldPrice;
    public bool hasShield = false;

    protected override void SetPrices()
    {
        spearPrice = new int[spearWalls.Length];
        for (int i = 0; i < spearWalls.Length; i++)
        {
            spearPrice[i] = spearWalls[i].prize;
        }

        armorPrice = new int[armorWardrobe.Length];
        for (int i = 0; i < armorWardrobe.Length; i++)
        {
            armorPrice[i] = armorWardrobe[i].prize;
        }

        shieldPrice = new int[shieldStack.Length];
        for (int i = 0; i < shieldStack.Length; i++)
        {
            shieldPrice[i] = shieldStack[i].prize;
        }
    }

    protected override void SetStartingStats()
    {
        unitStat_spear.x = startingStat.start_Health;
        unitStat_spear.y = startingStat.start_Stamina;
        unitStat_spear.z = startingStat.start_Strenght;
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
                SpawnSpearman();
            }
        }
    }

    void SpawnSpearman()
    {
        GameObject gameObjectUnit = (GameObject)Instantiate(UnitSpearmen, Spawner.position, transform.rotation);
        Spearman spawnedSpr = gameObjectUnit.GetComponent<Spearman>();
        UnitHealth spawnedHealth = gameObjectUnit.GetComponent<UnitHealth>();
        Unit_Appearance apperance = gameObjectUnit.GetComponentInChildren<Unit_Appearance>();
        if (!isAI)
        {
            FogOfWarUnit fogu = gameObjectUnit.AddComponent<FogOfWarUnit>();
            fogu.circleRadius = rangeFogVision;
            fogu.team = buildingTeam;
        }

        spawnedSpr.mother = this;
        NumUnits += 1;

        if (hasShield)
        {
            spawnedHealth.SetBaseHealthShield(unitStat_spear.x, armorWardrobe[armorLevel], shieldStack[shieldLevel]);
            Weight = (spearWalls[weaponLevel].weight + armorWardrobe[armorLevel].weight + shieldStack[shieldLevel].weight);
            apperance.ArmorWeaponShield(armorLevel, weaponLevel, shieldLevel);
        }
        else
        {
            spawnedHealth.SetBaseHealth(unitStat_spear.x, armorWardrobe[armorLevel]);
            Weight = (spearWalls[weaponLevel].weight + armorWardrobe[armorLevel].weight);
            apperance.ArmorWeapon(armorLevel, weaponLevel);
        }

        if (isStrong)
        {
            if (hasShield)
            {
                apperance.Bigger(10, weaponLevel);
            }
            else
            {
                apperance.BiggerShield(10, weaponLevel, shieldLevel);
            }
        }
        Vector3Int ssw = new Vector3Int(unitStat_spear.y, unitStat_spear.z, Weight);

        spawnedSpr.SetUpStatsUnit(spearWalls[weaponLevel], ssw);

        spawnedHealth.TeamId(buildingTeam, buildingId, buildingColInt);
    }

    public override void Stronger_Units()
    {
        if (isStrong == false)
        {
            unitStat_spear.z = (strongerInt + unitStat_spear.z);
            isStrong = true;
        }
    }

    public override void TurnOnUi() // can be on Unit_Spawner??
    {
        if (BaseUi != null)
        {
            if (!isUiOpen)
            {
                BaseUi.TurnOnUi();
                ol.OutlineColor = Color.green;
                isUiOpen = true;
            }
            else
            {
                BaseUi.TurnOffUi();
                ol.OutlineColor = Color.white;
                isUiOpen = false;
            }
        }
    }
}
