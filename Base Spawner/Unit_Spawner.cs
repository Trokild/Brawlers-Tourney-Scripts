using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoW;

public class Unit_Spawner : Building {

    public bool StartTester = false;

    //[SerializeField] private Material[] MapMaterialColors;
    //[SerializeField] private MeshRenderer MapBaseIcon;
    //public Transform MapBaseGo;

    #region Upgrade and UI

    //public StatUnit startingStatsUnit;                                      //Remove
    //public Vector3Int unitStat; // x = health, y = stamina, z = strenght    //Remove
    public int Weight { get; protected set; }

    public int[] spawnNum;
    public int[] spawnPrice;

    public int[] timePrice;
    public float[] timeDecrease;

    public int strongerInt;
    public int strongPrice;
    public bool isStrong = false;

    public StatUnit startingStat;

    [Range(0, 4)]
    public int armorLevel;
    public StatArmor[] armorWardrobe;
    [HideInInspector]
    public int[] armorPrice;
    #endregion

    #region Spawner Var

    public bool isProducing;
    public int MaxNumUnits;
    public int NumUnits;
    public IsSomethingHere availableSpace;

    public Vector3[] Areas;
    public Transform areaSelector;
    public Transform Spawner;

    public int Colums;
    public int Rows;
    public int MaxAreas;

    public float spaceSide;
    public float spaceFront;

    public float timeStart;
    public float timeColum;
    public float timeRow;
    public float timeArea;

    protected int cur_Row;
    protected int cur_Area;

    private List<GameObject> UnitSpawned = new List<GameObject>();
    #endregion

    #region TimeTest
    //public float timeRec;
    //public bool tims = false;
    //public bool tims2 = false;
    //private void Update()
    //{
    //    if (tims)
    //    {
    //        timeRec += Time.deltaTime;

    //        if (timeRec >= 60f && !tims2)
    //        {
    //            Debug.Log("60 sec of production have passed " + NumUnits.ToString() + " units have been produced");
    //            tims2 = true;
    //        }

    //        if (NumUnits >= MaxNumUnits)
    //        {
    //            Debug.Log("End of Production, it has taken: " + timeRec.ToString() + " secs");
    //            tims = false;
    //        }
    //    }
    //}
    #endregion

    #region Start
    protected override void Start()
    {
        if (ol == null)
        {
            ol = GetComponentInChildren<Outline>();
        }

        if (hlt == null)
        {
            hlt = GetComponent<BuildingHealth>();
        }

        cur_Area = 0;
        cur_Row = 0;

        areaSelector.transform.localPosition = Areas[cur_Area];
        Spawner.localPosition = new Vector3(0, 0, 0);

        SetPrices();
        SetStartingStats();

        if (StartTester)
        { StartCoroutine(UnitProducer()); }
    }

    void SetBuildingColor(int colorInt)
    {
        for (int i = 0; i < MeshBuilding.Length; i++)
        {
            MeshBuilding[i].material = colorBuilding[colorInt];
        }
    }

    protected virtual void SetPrices()
    {
        Debug.Log(gameObject +" old Infantry set Prises");
    }

    protected virtual void SetStartingStats()
    {

    }

    public void StartProducing()
    {
        StartCoroutine(UnitProducer());
    }

    public virtual void MoreUnits_One()
    {
        MaxNumUnits = spawnNum[0]; //16 inf
        Rows = 2;
        MaxAreas = 2;

        Areas[0] = new Vector3(-11, 0, 14);
        Areas[1] = new Vector3(1, 0, 14);
    }

    public virtual void MoreUnits_Two()
    {
        MaxNumUnits = spawnNum[1];   //24inf
        Rows = 3;

        Areas[0] = new Vector3(-11, 0, 16);
        Areas[1] = new Vector3(1, 0, 16);
    }

    public virtual void MoreUnits_Three()
    {
        MaxNumUnits = spawnNum[2];   //36inf
        MaxAreas = 3;

        Areas[0] = new Vector3(-4, 0, 16);
        Areas[1] = new Vector3(8, 0, 16);
        Areas[2] = new Vector3(-16, 0, 16);
    }

    public void FasterUnits_One()
    {
        //timeColum = (timeColum * 0.90f);
        timeColum = (timeColum - ((timeDecrease[0] / 100) * timeColum));
        timeRow = (timeRow - ((timeDecrease[0] / 100) * timeRow));
        timeArea = (timeArea - ((timeDecrease[0] / 100) * timeArea));
    }

    public void FasterUnits_Two()
    {
        timeColum = (timeColum - ((timeDecrease[1] / 100) * timeColum));
        timeRow = (timeRow - ((timeDecrease[1] / 100) * timeRow));
        timeArea = (timeArea - ((timeDecrease[1] / 100) * timeArea));
    }

    public virtual void Stronger_Units()
    {

    }
    #endregion

    public override void DestroyedBuilding()
    {
        isDestroyed = true;
        isProducing = false;

        if (BaseUi != null)
        {
            if (isUiOpen)
            {
                BaseUi.gameObject.SetActive(false);
                isUiOpen = false;
            }
        }
        StopAllCoroutines();
        Debug.Log("Unit_Spawner Destroyed");
    }

    private IEnumerator UnitProducer()
    {
        yield return new WaitForSeconds(timeStart);
//                                                   tims = true;    //test
        if (NumUnits < MaxNumUnits && !isProducing)
        {
            isProducing = true;
        }
        else
        {
            if (NumUnits >= MaxNumUnits)
            {
                isProducing = false;   
                cur_Area = 0;
                cur_Row = 0;
                areaSelector.transform.localPosition = Areas[cur_Area];
                Spawner.localPosition = new Vector3(0, 0, 0);

                yield return new WaitForSeconds(timeStart * 4f);
                StartCoroutine(UnitProducer());
                yield break;
            }
        }

        if (isProducing)
        {
            Vector3 startpos = Spawner.localPosition;
            SpawnUnit();

            for (int b = 1; b < Colums; b = b + 1)
            {
                if (NumUnits >= MaxNumUnits)
                {
                    StartCoroutine(UnitProducer());
                    Spawner.localPosition = Vector3.zero;
                    yield break;
                }

                Spawner.localPosition += new Vector3(spaceSide, 0, 0);
                yield return new WaitForSeconds(timeColum);
                SpawnUnit();
            }

            Spawner.localPosition = new Vector3(startpos.x, 0, Spawner.localPosition.z);
            Spawner.localPosition -= new Vector3(0, 0, spaceFront);
            cur_Row += 1;

            if (cur_Row < Rows)
            {
                yield return new WaitForSeconds(timeRow);
                StartCoroutine(UnitProducer());
            }
            else
            {
                cur_Row = 0;
                cur_Area += 1;

                if (cur_Area >= MaxAreas)
                {
                    cur_Area = 0;
                }

                areaSelector.localPosition = Areas[cur_Area];
                Spawner.localPosition = new Vector3(0, 0, 0);

                yield return new WaitForSeconds(timeArea);
                StartCoroutine(UnitProducer());
            }
        }
    }

    protected virtual void SpawnUnit()
    {
        Debug.Log(gameObject + "old SpawnUnit");
    }

    int UnitsAlive()
    {
        for (int i = 0; i < UnitSpawned.Count; i++)
        {
            if(UnitSpawned[i] == null)
            {
                UnitSpawned.RemoveAt(i);
            }
        }

        int a = UnitSpawned.Count;
        return a;
    }
}
