using FoW;
using UnityEngine;

public class Thrower_Spawner : Unit_Spawner
{
    public GameObject UnitThrower;
    public Vector3Int unitStat_Throw;

    public int handWeaponLevel;
    public bool hasMeleeWeapon;
    public StatWeapon[] handWeapon;
    [HideInInspector]
    public int[] handWepPrice;

    public int rockLevel;
    public StatRock[] rocks;
    [HideInInspector]
    public int[] rockPrice;

    public bool hasGoogels = false;
    public StatGoogels googels;
    public int googlesPrize;

    protected override void Start()
    {
        base.Start();
    }

    protected override void SetPrices()
    {
        handWepPrice = new int[handWeapon.Length];
        for (int i = 0; i < handWeapon.Length; i++)
        {
            handWepPrice[i] = handWeapon[i].prize;
        }

        armorPrice = new int[armorWardrobe.Length];
        for (int i = 0; i < armorWardrobe.Length; i++)
        {
            armorPrice[i] = armorWardrobe[i].prize;
        }

        rockPrice = new int[rocks.Length];
        for (int i = 0; i < rocks.Length; i++)
        {
            rockPrice[i] = rocks[i].prize;
        }

        googlesPrize = googels.prize;
    } 

    protected override void SetStartingStats()
    {
        unitStat_Throw.x = startingStat.start_Health;
        unitStat_Throw.y = startingStat.start_Stamina;
        unitStat_Throw.z = startingStat.start_Strenght;
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
                SpawnThrower();
            }
        }
    }

    void SpawnThrower()
    {
        GameObject gameObjectUnit = (GameObject)Instantiate(UnitThrower, Spawner.position, transform.rotation);
        Thrower spawnedThr = gameObjectUnit.GetComponent<Thrower>();
        UnitHealth spawnedHealth = gameObjectUnit.GetComponent<UnitHealth>();
        Apperanc_Throw apperance = gameObjectUnit.GetComponentInChildren<Apperanc_Throw>();
        if (!isAI)
        {
            FogOfWarUnit fogu = gameObjectUnit.AddComponent<FogOfWarUnit>();
            fogu.circleRadius = rangeFogVision;
            fogu.team = buildingTeam;
        }

        spawnedThr.mother = this;
        NumUnits += 1;

        spawnedThr.SetUpStatsThrow(rocks[rockLevel]);
        if (hasGoogels)
        {
            apperance.GooglesHelm();
            spawnedHealth.armor.AddModifier(googels.armorBonus);
            spawnedThr.AttackRange += googels.rangeBonus;
            Weight = googels.weight;
        }
        else
        {
            apperance.NormalHelm(armorLevel);
            Weight = 0;
        }

        if (hasMeleeWeapon) // has one hand weapon
        {
            Weight += (handWeapon[handWeaponLevel].weight + armorWardrobe[armorLevel].weight + rocks[rockLevel].weight);
            apperance.ArmorWeaponShield(armorLevel, handWeaponLevel, rockLevel);
            spawnedThr.SetUpStatsMelee(handWeapon[handWeaponLevel]);
        }
        else
        {

            Weight += (rocks[rockLevel].weight + armorWardrobe[armorLevel].weight);
            apperance.ArmorShield(armorLevel, rockLevel, true);
        }
        spawnedHealth.SetBaseHealth(unitStat_Throw.x, armorWardrobe[armorLevel]);

        Vector3Int ssw = new Vector3Int(unitStat_Throw.y, unitStat_Throw.z, Weight);
        spawnedThr.SetUpStats(ssw, hasMeleeWeapon);

        spawnedHealth.TeamId(buildingTeam, buildingId, buildingColInt);
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
