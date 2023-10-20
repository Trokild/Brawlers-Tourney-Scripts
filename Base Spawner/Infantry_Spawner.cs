using UnityEngine;
using FoW;

public class Infantry_Spawner : Unit_Spawner
{
    public GameObject UnitInfantry;
    public Vector3Int unitStat_inf; // x = health, y = stamina, z = strenght

    [Range(0, 6)]
    public int weaponLevel;
    public StatWeapon[] weaponArsenal;
    [HideInInspector]
    public int[] weaponPrice;

    [Range(0, 3)]
    public int shieldLevel;
    public StatArmor[] shieldStack;
    [HideInInspector]
    public int[] shieldPrice;
    public bool hasShield = false;

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

        shieldPrice = new int[shieldStack.Length];
        for (int i = 0; i < shieldStack.Length; i++)
        {
            shieldPrice[i] = shieldStack[i].prize;
        }

        Debug.Log(gameObject + " new Infantry set Prises");
    }

    protected override void SetStartingStats()
    {
        unitStat_inf.x = startingStat.start_Health;
        unitStat_inf.y = startingStat.start_Stamina;
        unitStat_inf.z = startingStat.start_Strenght;
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
        Unit_Appearance apperance = gameObjectUnit.GetComponentInChildren<Unit_Appearance>();
        if (!isAI)
        {
            FogOfWarUnit fogu = gameObjectUnit.AddComponent<FogOfWarUnit>();
            fogu.circleRadius = rangeFogVision;
            fogu.team = buildingTeam;
        }

        spawnedInf.mother = this;
        NumUnits += 1;

        if (hasShield)
        {
            spawnedHealth.SetBaseHealthShield(unitStat_inf.x, armorWardrobe[armorLevel], shieldStack[shieldLevel]);
            Weight = (weaponArsenal[weaponLevel].weight + armorWardrobe[armorLevel].weight + shieldStack[shieldLevel].weight);
            apperance.ArmorWeaponShield(armorLevel, weaponLevel, shieldLevel);
            spawnedHealth.xpReward += 5;
        }
        else
        {
            spawnedHealth.SetBaseHealth(unitStat_inf.x, armorWardrobe[armorLevel]);
            Weight = (weaponArsenal[weaponLevel].weight + armorWardrobe[armorLevel].weight);
            apperance.ArmorWeapon(armorLevel, weaponLevel);
        }

        if(isStrong)
        {
            spawnedHealth.xpReward += 10;
            if(hasShield)
            {
                apperance.Bigger(10, weaponLevel);
            }
            else
            {
                apperance.BiggerShield(10, weaponLevel, shieldLevel);
            }
        }
        Vector3Int ssw = new Vector3Int(unitStat_inf.y, unitStat_inf.z, Weight);

        spawnedInf.SetUpStatsUnit(weaponArsenal[weaponLevel], ssw, 0);

        spawnedHealth.TeamId(buildingTeam, buildingId, buildingColInt);
    }

    public override void Stronger_Units()
    {
        if (isStrong == false)
        {
            unitStat_inf.z = (strongerInt + unitStat_inf.z);      
            isStrong = true;
        }
    }
}
