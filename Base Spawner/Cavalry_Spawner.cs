using FoW;
using UnityEngine;

public class Cavalry_Spawner : Unit_Spawner
{
    public GameObject UnitCavalry;

    public Vector3Int unitStat_cav; // x = health, y = stamina, z = strenght
    public int chargeBonus_cav;

    [Range(0, 6)]
    public int weaponLevel;
    public StatWeapon[] weaponArsenal;
    [HideInInspector]
    public int[] weaponPrice;

    public StatArmor shieldStack;
    [HideInInspector]
    public int shieldPrice;
    public bool hasShield = false;

    public int cavLvl;
    public StatHorse[] horses;
    [HideInInspector]
    public int[] cavPrice;

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

        cavPrice = new int[horses.Length];
        for (int i = 0; i < horses.Length; i++)
        {
            cavPrice[i] = horses[i].prize;
        }

        shieldPrice = shieldStack.prize;
    }

    protected override void SetStartingStats()
    {
        unitStat_cav.x = startingStat.start_Health;
        unitStat_cav.y = startingStat.start_Stamina;
        unitStat_cav.z = startingStat.start_Strenght;
        chargeBonus_cav = 5;

        // ++ horse
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
                SpawnCavalry();
            }
        }
    }

    void SpawnCavalry()
    {
        GameObject gameObjectUnit = (GameObject)Instantiate(UnitCavalry, Spawner.position, transform.rotation);
        Cavalry spawnedCav = gameObjectUnit.GetComponent<Cavalry>();
        UnitHealth spawnedHealth = gameObjectUnit.GetComponent<UnitHealth>();
        Apperance_Cav apperance = gameObjectUnit.GetComponentInChildren<Apperance_Cav>();
        if (!isAI)
        {
            FogOfWarUnit fogu = gameObjectUnit.AddComponent<FogOfWarUnit>();
            fogu.circleRadius = rangeFogVision;
            fogu.team = buildingTeam;
        }

        spawnedCav.mother = this;
        NumUnits += 1;

        if (hasShield)
        {
            spawnedHealth.shielded = true;
            spawnedHealth.SetBaseHealthShield((unitStat_cav.x + horses[cavLvl].healthB), armorWardrobe[armorLevel], shieldStack);
            apperance.ArmorWeaponShield(armorLevel, weaponLevel, 0);  
        }
        else
        {
            spawnedHealth.SetBaseHealth((unitStat_cav.x + horses[cavLvl].healthB), armorWardrobe[armorLevel]);
            apperance.ArmorWeapon(armorLevel, weaponLevel);
        }

        spawnedHealth.armor.AddBaseValue(horses[cavLvl].armor);
        spawnedCav.attackSpeed.AddBaseValue(horses[cavLvl].attackSpeed);
        spawnedCav.SetSpeed(horses[cavLvl].speed);
        apperance.ChangeHorseMat(horses[cavLvl].statInt);
        Vector3Int ssw = new Vector3Int((unitStat_cav.y + horses[cavLvl].stamina), (unitStat_cav.z + horses[cavLvl].strenght), (Weight + horses[cavLvl].weight));
        spawnedCav.SetUpStatsUnit((weaponArsenal[weaponLevel]), ssw, (chargeBonus_cav + horses[cavLvl].chargeDamage));

        spawnedHealth.TeamId(buildingTeam, buildingId, buildingColInt);
    }
}
