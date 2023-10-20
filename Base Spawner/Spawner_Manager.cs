using FoW;
using UnityEngine;

public class Spawner_Manager : MonoBehaviour {

    public bool isHeroSpawning;

    public int spawnablePlace;
    public int aliveBases;
    public GameObject[] BaseTypes;
    //public Vector3Int BasePrice;
    public bool rightBaseBuildt { get; private set; }
    public bool leftBaseBuildt { get; private set; }
    public bool isBuilding { get; private set; }
    public Unit_Spawner[] basesInManager;
    public GameObject[] heroTypes;
    public Player smPlayerRef;
    public Transform heroSpawn;
    public Transform[] baseSpawnPos;
    private bool gameStart = false;
    private Vector3Int baseTypeVec;
    private Ui_BaseManager ubm;
    private AI_UpgraderCtrl aiUpgCtrl;
    [SerializeField] private BuildBuilding[] buildingFoundation;
    [SerializeField] private BuildingHealth[] Towers;

    private int team_sm;
    private int id_sm;
    private int colr_sm;
    private int typ_sm;

    private void Awake()
    {
        MainSystem.Spawner.Add(this);
    }

    public void SetUpStartingBase(int team, int id, int color, int type, Player pl)
    {
        smPlayerRef = pl;
        pl.clickScript.spawnManager = this;
        GameObject startBase = Instantiate(BaseTypes[type], baseSpawnPos[0].position, baseSpawnPos[0].rotation);
        baseTypeVec.x = type;
        Unit_Spawner us = startBase.GetComponent<Unit_Spawner>();
        startBase.transform.parent = this.gameObject.transform;
        basesInManager[0] = us;
        us.clickRef = pl.clickScript;
        us.playerRef = pl;
        us.hlt.TeamId(team, id, color); //Need to change
        aliveBases = 1;

        team_sm = team;
        id_sm = id;
        colr_sm = color;
        typ_sm = type;

        if (isHeroSpawning)
        {
            SpawnHero(pl.heroInt, team, id, color);
        }

        if (pl.isLocal && !pl.isComputer)
        {
            for (int i = 0; i < buildingFoundation.Length; i++)
            {
                buildingFoundation[i].SetLocal(pl.heroInt);
                if (team == MainSystem.LocalTeamInt)
                {
                    FogOfWarUnit fogu = buildingFoundation[i].gameObject.AddComponent<FogOfWarUnit>();
                    fogu.circleRadius = 35f;
                    fogu.team = team;
                }
            }
        }
        else
        {      
            for (int i = 0; i < buildingFoundation.Length; i++)
            {
                buildingFoundation[i].SetAi();
            }
            aiUpgCtrl = pl.GetComponent<AI_UpgraderCtrl>();
        }
        // Tower needs its own setup, takes currently from BuildingHealth, Building health got logic involving spawner
        if(Towers.Length > 0)
        {
            for (int i = 0; i < Towers.Length; i++)
            {
                if(Towers[i] != null)
                {
                    Towers[i].gameObject.SetActive(true);
                    Towers[i].TeamId(team, id, color);
                    if(team == MainSystem.LocalTeamInt)
                    {
                        FogOfWarUnit fogu = Towers[i].gameObject.AddComponent<FogOfWarUnit>();
                        fogu.circleRadius = 35f;
                        fogu.team = team;
                    }
                }
            }
        }
    }

    public void SetUpBase(int baseType, bool isR)
    {
        if (isR)
        {
            SetUpRightBase(team_sm, id_sm, colr_sm, baseType);
        }
        else
        {
            SetUpLeftBase(team_sm, id_sm, colr_sm, baseType);
        }

        if(smPlayerRef.isComputer && !smPlayerRef.isLocal)
        {
            aiUpgCtrl.CanBuildMore();
        }
    }

    public void SetUpRightBase(int team, int id, int color, int type)
    {
            GameObject startBase = (GameObject)Instantiate(BaseTypes[type], baseSpawnPos[1].position, baseSpawnPos[1].rotation);
            baseTypeVec.y = type;
            Unit_Spawner us = startBase.GetComponent<Unit_Spawner>();
            startBase.transform.parent = this.gameObject.transform;
            basesInManager[1] = us;
            us.clickRef = smPlayerRef.clickScript;
            us.playerRef = smPlayerRef;
            us.hlt.TeamId(team, id, color); //Need to change
            rightBaseBuildt = true;
            isBuilding = false;
            aliveBases += 1;

        if (smPlayerRef.isLocal)
        {
            ConnectBaseUi(1, type);
        }
        else
        {
            us.StartProducing();
            aiUpgCtrl.SetUpUpgBase(us, 1);
        }
    }

    public void SetUpLeftBase(int team, int id, int color, int type)
    {
        GameObject startBase = (GameObject)Instantiate(BaseTypes[type], baseSpawnPos[2].position, baseSpawnPos[2].rotation);
        baseTypeVec.z = type;
        Unit_Spawner us = startBase.GetComponent<Unit_Spawner>();
        startBase.transform.parent = this.gameObject.transform;
        basesInManager[2] = us;
        us.clickRef = smPlayerRef.clickScript;
        us.playerRef = smPlayerRef;
        us.hlt.TeamId(team, id, color); //Need to change
        leftBaseBuildt = true;
        isBuilding = false;
        aliveBases += 1;

        if (smPlayerRef.isLocal)
        {
            ConnectBaseUi(2, type);
        }
        else
        {
            us.StartProducing();
            aiUpgCtrl.SetUpUpgBase(us, 2);
        }
    }

    public void ConnectBaseUi_Start()
    {
        ubm = GameObject.FindGameObjectWithTag("BaseUiManager").GetComponent<Ui_BaseManager>();
        ubm.playerBaseMan = smPlayerRef;
        ubm.SetGold(smPlayerRef.gold);
        ubm.PopAnimGold();

        ubm.NewCanvasBase(0, baseTypeVec.x);

        ubm.myBases[0].myBase = basesInManager[0];
        ubm.myBases[0].playerRef = smPlayerRef;
        basesInManager[0].BaseUi = ubm.myBases[0];

        ubm.myBases[0].GameStartUi();
        gameStart = true;

        New_Camera newCam = Camera.main.GetComponentInParent<New_Camera>();
        Transform transBase = gameObject.transform;
        newCam.BasePos = transBase.position;
        newCam.SetDesPos_Base();
        
    }

    public void ConnectBaseUi(int bas, int typ)
    {
        ubm.NewCanvasBase(bas, typ);

        ubm.myBases[bas].myBase = basesInManager[bas];
        ubm.myBases[bas].playerRef = smPlayerRef;
        basesInManager[bas].BaseUi = ubm.myBases[bas];

        ubm.myBases[bas].GameStartUi();
    }

    public void SpawnHero(int typ, int team, int id, int col)
    {
        GameObject gameObjectHero = (GameObject)Instantiate(heroTypes[typ], heroSpawn.position, heroSpawn.rotation);
        HeroHealth spawnedHealth = gameObjectHero.GetComponent<HeroHealth>();
        Unit_Appearance apperance = gameObjectHero.GetComponentInChildren<Unit_Appearance>();
        Hero spawnedHero = gameObjectHero.GetComponent<Hero>();
        if (team == MainSystem.LocalTeamInt)
        {
            FogOfWarUnit fogu = gameObjectHero.AddComponent<FogOfWarUnit>();
            fogu.circleRadius = 35f;
            fogu.team = team;
        }
        smPlayerRef.Go_PlayerHero = gameObjectHero;

        apperance.HeroOutline();
        apperance.NoShield();
        apperance.NoShoulders();
        apperance.WeaponInt(0);
        spawnedHealth.TeamId(team, id, col);
    }

    public void BuildNewBase(int baseT)
    {
        if(buildingFoundation.Length > 0)
        {
            if (!rightBaseBuildt)
            {
                if (smPlayerRef.gold >= buildingFoundation[1].BasePrizes[baseT])
                {
                    smPlayerRef.gold -= buildingFoundation[1].BasePrizes[baseT];
                    buildingFoundation[1].StartBuilding(baseT);
                    isBuilding = true;
                    Debug.Log("Buildt right");
                }
                else
                {
                    Debug.Log("Can't afford new right base");
                }
            }
            else if(!leftBaseBuildt)
            {
                if (smPlayerRef.gold >= buildingFoundation[0].BasePrizes[baseT])
                {
                    smPlayerRef.gold -= buildingFoundation[0].BasePrizes[baseT];
                    buildingFoundation[0].StartBuilding(baseT);
                    isBuilding = true;
                    Debug.Log("Buildt left");
                }
                else
                {
                    Debug.Log("Can't afford new right base");
                }
            }
        }
        else
        {
            Debug.LogError("Got No Base To build");
        }
    }

    public bool BaseDestroyed()
    {
        aliveBases -= 1;
        if (aliveBases <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
