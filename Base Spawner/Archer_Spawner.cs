using FoW;
using UnityEngine;

public class Archer_Spawner : Unit_Spawner
{
    public GameObject UnitArcher;

    public Vector3Int unitStat_Arch; // x = health, y = stamina, z = strenght

    [Range(0, 3)]
    public int arrowLvl;
    public StatArrow[] arrowRack;
    //[HideInInspector]
    public int[] arrowPrice;

    [Range(0, 3)]
    public int bowLvl;
    public StatBow[] bowHolder;
    //[HideInInspector]
    public int[] bowPrice;

    protected override void Start()
    {
        base.Start();
    }

    protected override void SetPrices()
    {
        arrowPrice = new int[arrowRack.Length];
        for (int i = 0; i < arrowRack.Length; i++)
        {
            arrowPrice[i] = arrowRack[i].prize;
        }

        armorPrice = new int[armorWardrobe.Length];
        for (int i = 0; i < armorWardrobe.Length; i++)
        {
            armorPrice[i] = armorWardrobe[i].prize;
        }

        bowPrice = new int[bowHolder.Length];
        for (int i = 0; i < bowHolder.Length; i++)
        {
            bowPrice[i] = bowHolder[i].prize;
        }

        Debug.Log(gameObject + " new archer set Prises");
    }

    protected override void SetStartingStats()
    {
        unitStat_Arch.x = startingStat.start_Health;
        unitStat_Arch.y = startingStat.start_Stamina;
        unitStat_Arch.z = startingStat.start_Strenght;
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
                SpawnArcher();
            }
        }
    }

    void SpawnArcher()
    {
        GameObject gameObjectUnit = (GameObject)Instantiate(UnitArcher, Spawner.position, transform.rotation);
        Archer spawnedArcher = gameObjectUnit.GetComponent<Archer>();
        Health spawnedHealth = gameObjectUnit.GetComponent<Health>();
        Unit_Appearance apperance = gameObjectUnit.GetComponentInChildren<Unit_Appearance>();
        ArrowProjectile arrowArcher = gameObjectUnit.GetComponentInChildren<ArrowProjectile>();
        if (!isAI)
        {
            FogOfWarUnit fogu = gameObjectUnit.AddComponent<FogOfWarUnit>();
            fogu.circleRadius = rangeFogVision;
            fogu.team = buildingTeam;
        }

        spawnedArcher.mother = this;
        NumUnits += 1;

        spawnedHealth.SetBaseHealth(unitStat_Arch.x, armorWardrobe[armorLevel]);
        Weight = (arrowRack[arrowLvl].weight + bowHolder[bowLvl].weight + armorWardrobe[armorLevel].weight);


        Vector3Int ssw = new Vector3Int(unitStat_Arch.y, unitStat_Arch.z, Weight);
        spawnedArcher.SetUpStatsUnit(arrowRack[arrowLvl], bowHolder[arrowLvl], ssw);
        apperance.ArmorWeaponShield(armorLevel, bowLvl, arrowLvl);

        arrowArcher.apLvl = arrowLvl;

        spawnedHealth.TeamId(buildingTeam, buildingId, buildingColInt);
    }

    //public override void TurnOnUi()
    //{
    //    if (BaseUi != null)
    //    {
    //        if (!isUiOpen)
    //        {
    //            BaseUi.TurnOnUi_Archer();
    //            ol.OutlineColor = Color.green;
    //            isUiOpen = true;
    //        }
    //        else
    //        {
    //            BaseUi.TurnOffUi();
    //            ol.OutlineColor = Color.white;
    //            isUiOpen = false;
    //        }
    //    }
    //}
}
