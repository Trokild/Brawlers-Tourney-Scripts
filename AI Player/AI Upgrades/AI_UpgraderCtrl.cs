using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI_Controller))]
public class AI_UpgraderCtrl : MonoBehaviour
{
    public enum BaseType  {None, Infantry, Archery, Spearmen, Cavalry, DualOrc, SpearOrc, Thrower}
    [SerializeField] private enum AiRace {Human, Orc, None}
    private AiRace thisAiRace = AiRace.None;
    public BaseType MainBaseType;
    public BaseType RightBaseType;
    public BaseType LeftBaseType;

    [SerializeField] private float StartUpgrade;
    [SerializeField] private float RepeatUpgrade;

    [SerializeField] private AI_Controller AiCtrl;
    private Spawner_Manager spw_manager;
    private Player pl;

    [SerializeField] private Unit_Spawner MainBase;
    [SerializeField] private int mBaseUpgLvl = 0;
    private int mBaseMaxUpgLvl;
    private bool mBase;
    private bool build = false;
    [Range(0, 4f)]
    private float PriorityMain;

    [SerializeField] private Unit_Spawner RightBase;
    [SerializeField] private int rBaseUpgLvl = 0;
    private int rBaseMaxUpgLvl;
    private bool rBase = false;
    [Range(0, 4f)]
    private float PriorityRight;

    [SerializeField] private Unit_Spawner LeftBase;
    [SerializeField] private int lBaseUpgLvl = 0;
    private int lBaseMaxUpgLvl;
    private bool lBase = false;
    [Range(0, 4f)]
    private float PriorityLeft;
    [Space]
    [SerializeField] private AiBasePriority AgrroBasePrio;
    [SerializeField] private AiBasePriority DefBasePrio;
    [SerializeField] private AiBasePriority NeutrBasePrio;

    [Header("Human Upg Lists")]
    [SerializeField] private AI_ListUpgInfantry listInfUpg;
    [SerializeField] private AI_ListUpgArch listArchUpg;
    [SerializeField] private AI_ListUpgSpear listSpearUpg;
    [SerializeField] private AI_ListUpgCavalry listCavUpg;
    [Header("Orc Upg Lists")]
    [SerializeField] private AI_ListUpgDual listDualUpg;
    [SerializeField] private AI_ListUpgSpear listSpearOrcUpg;
    [SerializeField] private AI_ListUpgThrow listThrowerUpg;
    [Space]
    [SerializeField] private List<AI_Upg> mainBaseUpgrades;
    [SerializeField] private List<AI_Upg> rightBaseUpgrades;
    [SerializeField] private List<AI_Upg> leftBaseUpgrades;

    public void CanBuildMore()
    {
        if(rBase && lBase)
        {
            Debug.Log("Cant build more");
            return;
        }
        build = false;
    }

    public void SetUpAIUpg(Player p, Spawner_Manager sm)
    {
        pl = p;
        spw_manager = sm;
        SetUpUpgBase(sm.basesInManager[0], 0);

        switch(p.playerHero)
        {
            case Player.HeroPlayer.Werdoom:
                thisAiRace = AiRace.Human;
                break;
            case Player.HeroPlayer.Urka:
                thisAiRace = AiRace.Orc;
                break;
            default:
                Debug.LogError("No race choosen, didnt start upg");
                return;
        }
        switch(AiCtrl.currentAI)
        {
            case AI_Controller.AIType.Aggressor:
                if(AgrroBasePrio != null)
                {
                    SetBasePriotity(AgrroBasePrio.GetPriorityVector());
                }
                break;
            case AI_Controller.AIType.Defensive:
                if (DefBasePrio != null)
                {
                    SetBasePriotity(DefBasePrio.GetPriorityVector());
                }
                break;
            case AI_Controller.AIType.Netrual:
                if (NeutrBasePrio != null)
                {
                    SetBasePriotity(NeutrBasePrio.GetPriorityVector());
                }
                break;
        }
        InvokeRepeating("BaseUpgrade", StartUpgrade, RepeatUpgrade);
    }

    void SetBasePriotity(Vector3 v) //main, right, left
    {
        PriorityMain = v.x;
        PriorityRight = v.y;
        PriorityLeft = v.z;
    }

    public void SetUpUpgBase(Unit_Spawner us, int bas) // bas 0 main, 1 right, 2 left
    {
        switch(bas)
        {
            case 0:
                MainBase = us;
                mBase = true;
                break;
            case 1:
                RightBase = us;
                rBase = true;
                break;
            case 2:
                LeftBase = us;
                lBase = true;
                break;
            default: MainBase = us; Debug.LogError("Invalid bas"); break;
        }

        Infantry_Spawner infs = us as Infantry_Spawner;
        if (infs != null)
        {
            switch (bas)
            {
                case 0:
                    MainBaseType = BaseType.Infantry;
                    break;
                case 1:
                    RightBaseType = BaseType.Infantry;
                    break;
                case 2:
                    LeftBaseType = BaseType.Infantry;
                    break;
                default:Debug.LogError("Invalid bas"); break;
            }
        }
        Archer_Spawner archs = us as Archer_Spawner;
        if (archs != null)
        {
            switch (bas)
            {
                case 0:
                    MainBaseType = BaseType.Archery;
                    break;
                case 1:
                    RightBaseType = BaseType.Archery;
                    break;
                case 2:
                    LeftBaseType = BaseType.Archery;
                    break;
                default: Debug.LogError("Invalid bas"); break;
            }
        }
        Spearmen_Spawner spers = us as Spearmen_Spawner;
        if (spers != null)
        {
            switch (bas)
            {
                case 0:
                    MainBaseType = BaseType.Spearmen;
                    break;
                case 1:
                    RightBaseType = BaseType.Spearmen;
                    break;
                case 2:
                    LeftBaseType = BaseType.Spearmen;
                    break;
                default: Debug.LogError("Invalid bas"); break;
            }
        }
        Dualweild_Spawner duals = us as Dualweild_Spawner;
        if(duals != null)
        {
            switch (bas)
            {
                case 0:
                    MainBaseType = BaseType.DualOrc;
                    break;
                case 1:
                    RightBaseType = BaseType.DualOrc;
                    break;
                case 2:
                    LeftBaseType = BaseType.DualOrc;
                    break;
                default: Debug.LogError("Invalid bas"); break;
            }
        }
        Cavalry_Spawner cavs = us as Cavalry_Spawner;
        if (cavs != null)
        {
            switch (bas)
            {
                case 0:
                    MainBaseType = BaseType.Cavalry;
                    break;
                case 1:
                    RightBaseType = BaseType.Cavalry;
                    break;
                case 2:
                    LeftBaseType = BaseType.Cavalry;
                    break;
                default: Debug.LogError("Invalid bas"); break;
            }
        }

        switch (bas)
        {
            case 0:
                GetUpgListBase(MainBaseType, bas);
                break;
            case 1:
                GetUpgListBase(RightBaseType, bas);
                break;
            case 2:
                GetUpgListBase(LeftBaseType, bas);
                break;
            default: Debug.LogError("Invalid bas"); break;
        }
    }

    void GetUpgListBase(BaseType bas, int numBase) // 0 is main, 1 is Right, 2 is left
    {
        Debug.Log(bas + " base and num base: " + numBase);
        switch (bas)
        {
            case BaseType.Infantry:
                int wepTyp = Random.Range(1, 4);
                switch (AiCtrl.currentAI)
                {
                    case AI_Controller.AIType.Aggressor:

                        AI_UpgradeInf[] lstInfUpgA = listInfUpg.GetAggresiveList(wepTyp);
                        for (int i = 0; i < lstInfUpgA.Length; i++)
                        {
                            switch(numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstInfUpgA[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstInfUpgA[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstInfUpgA[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstInfUpgA[i]);
                                    break;
                            }

                        }
                        break;

                    case AI_Controller.AIType.Defensive:

                        AI_UpgradeInf[] lstInfUpgD = listInfUpg.GetDefensiveList(wepTyp);
                        for (int i = 0; i < lstInfUpgD.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstInfUpgD[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstInfUpgD[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstInfUpgD[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstInfUpgD[i]);
                                    break;
                            }
                        }
                        break;

                    case AI_Controller.AIType.Netrual:

                        AI_UpgradeInf[] lstInfUpgN = listInfUpg.GetNeturalList(wepTyp);
                        for (int i = 0; i < lstInfUpgN.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstInfUpgN[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstInfUpgN[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstInfUpgN[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstInfUpgN[i]);
                                    break;
                            }
                        }
                        break;
                }
                break;

            case BaseType.Archery:
                int archTyp = Random.Range(1, 4);
                switch (AiCtrl.currentAI)
                {
                    case AI_Controller.AIType.Aggressor:

                        AI_UpgradeArch[] lstArchUpgA = listArchUpg.GetAggresiveList(archTyp);
                        for (int i = 0; i < lstArchUpgA.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstArchUpgA[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstArchUpgA[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstArchUpgA[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstArchUpgA[i]);
                                    break;
                            }
                        }
                        break;

                    case AI_Controller.AIType.Defensive:

                        AI_UpgradeArch[] lstArchUpgD = listArchUpg.GetDefensiveList(archTyp);
                        for (int i = 0; i < lstArchUpgD.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstArchUpgD[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstArchUpgD[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstArchUpgD[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstArchUpgD[i]);
                                    break;
                            }
                        }
                        break;

                    case AI_Controller.AIType.Netrual:

                        AI_UpgradeArch[] lstArchUpgN = listArchUpg.GetNeturalList(archTyp);
                        for (int i = 0; i < lstArchUpgN.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstArchUpgN[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstArchUpgN[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstArchUpgN[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstArchUpgN[i]);
                                    break;
                            }
                        }
                        break;
                }
                break;

            case BaseType.Spearmen:
                int spearTyp = Random.Range(1, 4);
                switch (AiCtrl.currentAI)
                {
                    case AI_Controller.AIType.Aggressor:
                        AI_UpgradeSpear[] lstSpearUpgA;
                        switch (thisAiRace)
                        {
                            case AiRace.Human:
                                lstSpearUpgA = listSpearUpg.GetAggresiveList(spearTyp);
                                break;
                            case AiRace.Orc:
                                lstSpearUpgA = listSpearOrcUpg.GetAggresiveList(spearTyp);
                                break;
                            default:
                                Debug.LogError("NoRace");
                                return;
                        }
                         
                        for (int i = 0; i < lstSpearUpgA.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstSpearUpgA[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstSpearUpgA[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstSpearUpgA[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstSpearUpgA[i]);
                                    break;
                            }
                        }
                        break;

                    case AI_Controller.AIType.Defensive:
                        AI_UpgradeSpear[] lstSpearUpgD;
                        switch (thisAiRace)
                        {
                            case AiRace.Human:
                                lstSpearUpgD = listSpearUpg.GetDefensiveList(spearTyp);
                                break;
                            case AiRace.Orc:
                                lstSpearUpgD = listSpearOrcUpg.GetDefensiveList(spearTyp);
                                break;
                            default:
                                Debug.LogError("NoRace");
                                return;
                        }
                        for (int i = 0; i < lstSpearUpgD.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstSpearUpgD[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstSpearUpgD[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstSpearUpgD[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstSpearUpgD[i]);
                                    break;
                            }
                        }
                        break;

                    case AI_Controller.AIType.Netrual:
                        AI_UpgradeSpear[] lstSpearUpgN;
                        switch (thisAiRace)
                        {
                            case AiRace.Human:
                                lstSpearUpgN = listSpearUpg.GetNeturalList(spearTyp);
                                break;
                            case AiRace.Orc:
                                lstSpearUpgN = listSpearOrcUpg.GetNeturalList(spearTyp);
                                break;
                            default:
                                Debug.LogError("NoRace");
                                return;
                        }
                        for (int i = 0; i < lstSpearUpgN.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstSpearUpgN[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstSpearUpgN[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstSpearUpgN[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstSpearUpgN[i]);
                                    break;
                            }
                        }
                        break;
                }
                break;

            case BaseType.Cavalry:
                int cavTyp = Random.Range(1, 4);
                switch(AiCtrl.currentAI)
                {
                    case AI_Controller.AIType.Aggressor:
                        AI_UpgradeCav[] lstCavUpgA = listCavUpg.GetAggresiveList(cavTyp);
                        for (int i = 0; i < lstCavUpgA.Length; i++)
                        {
                            switch(numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstCavUpgA[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstCavUpgA[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstCavUpgA[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstCavUpgA[i]);
                                    break;
                            }
                        }
                        break;
                    case AI_Controller.AIType.Defensive:
                        AI_UpgradeCav[] lstCavUpgD = listCavUpg.GetDefensiveList(cavTyp);
                        for (int i = 0; i < lstCavUpgD.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstCavUpgD[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstCavUpgD[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstCavUpgD[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstCavUpgD[i]);
                                    break;
                            }
                        }
                        break;
                    case AI_Controller.AIType.Netrual:
                        AI_UpgradeCav[] lstCavUpgN = listCavUpg.GetNeturalList(cavTyp);
                        for (int i = 0; i < lstCavUpgN.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstCavUpgN[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstCavUpgN[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstCavUpgN[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstCavUpgN[i]);
                                    break;
                            }
                        }
                        break;
                }
                break;

            case BaseType.DualOrc:
                int dualTyp = Random.Range(1, 4);
                switch (AiCtrl.currentAI)
                {
                    case AI_Controller.AIType.Aggressor:

                        Ai_UpgradeDual[] lstDualUpgA = listDualUpg.GetAggresiveList(dualTyp);
                        for (int i = 0; i < lstDualUpgA.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstDualUpgA[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstDualUpgA[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstDualUpgA[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstDualUpgA[i]);
                                    break;
                            }
                        }
                        break;

                    case AI_Controller.AIType.Defensive:

                        Ai_UpgradeDual[] lstDualUpgD = listDualUpg.GetDefensiveList(dualTyp);
                        for (int i = 0; i < lstDualUpgD.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstDualUpgD[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstDualUpgD[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstDualUpgD[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstDualUpgD[i]);
                                    break;
                            }
                        }
                        break;

                    case AI_Controller.AIType.Netrual:

                        Ai_UpgradeDual[] lstDualUpgN = listDualUpg.GetNeturalList(dualTyp);
                        for (int i = 0; i < lstDualUpgN.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstDualUpgN[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstDualUpgN[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstDualUpgN[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstDualUpgN[i]);
                                    break;
                            }
                        }
                        break;
                }
                break;
            case BaseType.Thrower:
                int thrTyp = Random.Range(1, 4);
                switch (AiCtrl.currentAI)
                {
                    case AI_Controller.AIType.Aggressor:

                        Ai_UpgradeThrow[] lstThrUpgA = listThrowerUpg.GetAggresiveList(thrTyp);
                        
                        for (int i = 0; i < lstThrUpgA.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstThrUpgA[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstThrUpgA[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstThrUpgA[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstThrUpgA[i]);
                                    break;
                            }
                        }
                        break;

                    case AI_Controller.AIType.Defensive:

                        Ai_UpgradeThrow[] lstThrUpgD = listThrowerUpg.GetDefensiveList(thrTyp);
                        for (int i = 0; i < lstThrUpgD.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstThrUpgD[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstThrUpgD[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstThrUpgD[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstThrUpgD[i]);
                                    break;
                            }
                        }
                        break;

                    case AI_Controller.AIType.Netrual:

                        Ai_UpgradeThrow[] lstThrUpgN = listThrowerUpg.GetNeturalList(thrTyp);
                        for (int i = 0; i < lstThrUpgN.Length; i++)
                        {
                            switch (numBase)
                            {
                                case 0:
                                    mainBaseUpgrades.Add(lstThrUpgN[i]);
                                    break;
                                case 1:
                                    rightBaseUpgrades.Add(lstThrUpgN[i]);
                                    break;
                                case 2:
                                    leftBaseUpgrades.Add(lstThrUpgN[i]);
                                    break;
                                default:
                                    mainBaseUpgrades.Add(lstThrUpgN[i]);
                                    break;
                            }
                        }
                        break;
                }
                break;
        }
    }

    void MainBaseUpgrade()
    {
        if (mBaseUpgLvl >= mainBaseUpgrades.Count)
        {
            //Debug.LogError("Fully Upgraded MainBase, build new base");
            if (!build)
            {
                build = true;
                NewBaseAI();
            }
            mBase = false;
            return;
        }

        switch (MainBaseType)
        {
            case BaseType.Infantry:
                AI_UpgradeInf one = mainBaseUpgrades[mBaseUpgLvl] as AI_UpgradeInf;
                Infantry_Spawner bs = MainBase as Infantry_Spawner;
                if (one != null)
                {
                    if (one.Upgrade(bs, pl))
                    {
                        mBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("one == null");
                }
                break;

            case BaseType.Archery:
                AI_UpgradeArch arc = mainBaseUpgrades[mBaseUpgLvl] as AI_UpgradeArch;
                Archer_Spawner aas = MainBase as Archer_Spawner;
                if (arc != null)
                {
                    if (arc.Upgrade(aas, pl))
                    {
                        mBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("arc == null");
                }
                break;

            case BaseType.Spearmen:
                AI_UpgradeSpear spr = mainBaseUpgrades[mBaseUpgLvl] as AI_UpgradeSpear;
                Spearmen_Spawner ss = MainBase as Spearmen_Spawner;
                if (spr != null)
                {
                    if (spr.Upgrade(ss, pl))
                    {
                        mBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("spr == null");
                }
                break;

            case BaseType.Cavalry:
                AI_UpgradeCav cav = mainBaseUpgrades[mBaseUpgLvl] as AI_UpgradeCav;
                Cavalry_Spawner cs = MainBase as Cavalry_Spawner;
                if(cav != null)
                {
                    if(cav.Upgrade(cs, pl))
                    {
                        mBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("cav == null");
                }
                break;

            case BaseType.DualOrc:
                Ai_UpgradeDual dul = mainBaseUpgrades[mBaseUpgLvl] as Ai_UpgradeDual;
                Dualweild_Spawner ds = MainBase as Dualweild_Spawner;
                if (ds != null)
                {
                    if (dul.Upgrade(ds, pl))
                    {
                        mBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("spr == null");
                }
                break;

            case BaseType.Thrower:
                Ai_UpgradeThrow thr = mainBaseUpgrades[mBaseUpgLvl] as Ai_UpgradeThrow;
                Thrower_Spawner ts = MainBase as Thrower_Spawner;
                if (ts != null)
                {
                    if (thr.Upgrade(ts, pl))
                    {
                        mBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("spr == null");
                }
                break;
        }
    }

    void RightBaseUpgrade()
    {
        if (rBaseUpgLvl >= rightBaseUpgrades.Count)
        {
            Debug.LogError("Fully Upgraded MainBase, build new base");
            if (!build)
            {
                build = true;
                NewBaseAI();
            }
            rBase = false;
            return;
        }

        switch (RightBaseType)
        {
            case BaseType.Infantry:
                AI_UpgradeInf one = rightBaseUpgrades[rBaseUpgLvl] as AI_UpgradeInf;
                Infantry_Spawner bs = RightBase as Infantry_Spawner;
                if (one != null)
                {
                    if (one.Upgrade(bs, pl))
                    {
                        rBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("one == null");
                }
                break;

            case BaseType.Archery:
                AI_UpgradeArch arc = rightBaseUpgrades[rBaseUpgLvl] as AI_UpgradeArch;
                Archer_Spawner aas = RightBase as Archer_Spawner;
                if (arc != null)
                {
                    if (arc.Upgrade(aas, pl))
                    {
                        rBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("arc == null");
                }
                break;

            case BaseType.Spearmen:
                AI_UpgradeSpear spr = rightBaseUpgrades[rBaseUpgLvl] as AI_UpgradeSpear;
                Spearmen_Spawner ss = RightBase as Spearmen_Spawner;
                if (spr != null)
                {
                    if (spr.Upgrade(ss, pl))
                    {
                        rBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("spr == null");
                }
                break;

            case BaseType.Cavalry:
                AI_UpgradeCav cav = rightBaseUpgrades[rBaseUpgLvl] as AI_UpgradeCav;
                Cavalry_Spawner cs = RightBase as Cavalry_Spawner;
                if (cav != null)
                {
                    if (cav.Upgrade(cs, pl))
                    {
                        rBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("cav == null");
                }
                break;

            case BaseType.DualOrc:
                Ai_UpgradeDual dul = rightBaseUpgrades[rBaseUpgLvl] as Ai_UpgradeDual;
                Dualweild_Spawner ds = RightBase as Dualweild_Spawner;
                if (dul != null)
                {
                    if (dul.Upgrade(ds, pl))
                    {
                        rBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("spr == null");
                }
                break;
        }
    }

    void LeftBaseUpgrade()
    {
        if (lBaseUpgLvl >= leftBaseUpgrades.Count)
        {
            Debug.LogError("Fully Upgraded MainBase, build new base");
            lBase = false;
            if (!build)
            {
                build = true;
                NewBaseAI();
            }
            return;
        }

        switch (LeftBaseType)
        {
            case BaseType.Infantry:
                AI_UpgradeInf one = leftBaseUpgrades[lBaseUpgLvl] as AI_UpgradeInf;
                Infantry_Spawner bs = LeftBase as Infantry_Spawner;
                if (one != null)
                {
                    if (one.Upgrade(bs, pl))
                    {
                        lBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("one == null");
                }
                break;

            case BaseType.Archery:
                AI_UpgradeArch arc = leftBaseUpgrades[lBaseUpgLvl] as AI_UpgradeArch;
                Archer_Spawner aas = LeftBase as Archer_Spawner;
                if (arc != null)
                {
                    if (arc.Upgrade(aas, pl))
                    {
                        lBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("arc == null");
                }
                break;

            case BaseType.Spearmen:
                AI_UpgradeSpear spr = leftBaseUpgrades[lBaseUpgLvl] as AI_UpgradeSpear;
                Spearmen_Spawner ss = LeftBase as Spearmen_Spawner;
                if (spr != null)
                {
                    if (spr.Upgrade(ss, pl))
                    {
                        lBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("spr == null");
                }
                break;

            case BaseType.Cavalry:
                AI_UpgradeCav cav = leftBaseUpgrades[lBaseUpgLvl] as AI_UpgradeCav;
                Cavalry_Spawner cs = LeftBase as Cavalry_Spawner;
                if (cav != null)
                {
                    if (cav.Upgrade(cs, pl))
                    {
                        lBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("cav == null");
                }
                break;


            case BaseType.DualOrc:
                Ai_UpgradeDual dul = leftBaseUpgrades[lBaseUpgLvl] as Ai_UpgradeDual;
                Dualweild_Spawner ds = LeftBase as Dualweild_Spawner;
                if (dul != null)
                {
                    if (dul.Upgrade(ds, pl))
                    {
                        lBaseUpgLvl += 1;
                    }
                }
                else
                {
                    Debug.LogError("spr == null");
                }
                break;
        }
    }

    void BaseUpgrade()
    {
        if(ShouldBuildBase())
        {
            //Debug.LogError("Priotiry build new base "+ mBaseUpgLvl);
            if (!build)
            {
                build = true;
                NewBaseAI();
            }
            return;
        }
        int pri = PriorityBase();
        //Debug.Log(pri);
        switch (pri)
        {
            case 0:
                MainBaseUpgrade();
                break;
            case 1:
                RightBaseUpgrade();
                break;
            case 2:
                LeftBaseUpgrade();
                break;
            default:Debug.LogError(pri); break;
        }
    }

    int PriorityBase()
    {
        List<float> bases = new List<float>();

        int upg = mainBaseUpgrades.Count - mBaseUpgLvl;
        float pri = upg * PriorityMain;
        bases.Add(pri);

        int upg1 = rightBaseUpgrades.Count - rBaseUpgLvl;
        float pri1 = upg1 * PriorityRight;
        bases.Add(pri1);

        int upg2 = leftBaseUpgrades.Count - lBaseUpgLvl;
        float pri2 = upg2 * PriorityLeft;
        bases.Add(pri2);

        float maxf = 0;
        foreach (float t in bases)
        {
            if(t > maxf)
            {
                maxf = t;
            }
        }
        return bases.IndexOf(maxf);
    }

    bool ShouldBuildBase()
    {
        bool rBuildt = spw_manager.rightBaseBuildt;
        bool lBuildt = spw_manager.leftBaseBuildt;
        if (rBuildt == false || lBuildt  == false)
        {
            if(rBuildt)
            {
                //should I build Left?
                //Debug.Log("ShouldBuildLeft?");
                return ShouldBuildLeft();
            }
            else if(lBuildt)
            {
                //should I build right?
               // Debug.Log("ShouldBuildRight?");
                return ShouldBuildRight();
            }
            else // both are false, check who has highest pri to
            {
                //MAIN BASE UPG PRIO / PROGRESS
                float prioMain = (PriorityMain / 4f);
                float mainProgress = ((float)mBaseUpgLvl / (float)mainBaseUpgrades.Count);
                if(prioMain >= mainProgress)
                {
                    //Debug.Log("build Left, both false, proM:" + PriorityMain + " / 4 = " + prioMain);
                    //Debug.Log(mBaseUpgLvl + " / " + mainBaseUpgrades.Count + " = " + mainProgress);
                    return false;
                }

                if(PriorityRight > PriorityLeft)
                {
                    //build Right
                    //Debug.Log("build right, both false");
                    return true;
                }
                else
                {
                    //build Left
                    //Debug.Log("build Left, both false");
                    return true;
                }
            }
        }
        else
        {
            //Debug.Log("Both bases are buildt");
            return false; //
        }
    }

    bool ShouldBuildLeft()
    {
        float prioRight = (PriorityRight / 4f);
        float upgRightProgress = (rBaseUpgLvl / rightBaseUpgrades.Count);
        if (prioRight >= upgRightProgress)
        {
            //Debug.Log("Right base Upgrades are sill a priority, since Prio: " + prioRight + " is greater than progress: " + upgRightProgress);
            return false;
        }
        else
        {
            //Debug.Log("Can build Left base since prio: " + prioRight + " is less than progress: " + upgRightProgress);
            //Build Left Base
            return true;
        }
    }

    bool ShouldBuildRight()
    {
        float prioLeft = (PriorityLeft / 4f);
        float upgLeftProgress = (lBaseUpgLvl / leftBaseUpgrades.Count);
        if (prioLeft >= upgLeftProgress)
        {
            Debug.Log("Left base Upgrades are sill a priority, since Prio: " + prioLeft + " is greater than progress: " + upgLeftProgress);
            return false;
        }
        else
        {
            Debug.Log("Can build Right base since prio: " + prioLeft + " is less than progress: " + upgLeftProgress);
            //Build Right Base
            return true;
        }
    }

    public void NewBaseAI()
    {
        // 0 humanInf, 1 Archer, 2 HumanSpear, 3 Cavalry, 4 UrcDual, 5 UrkSpear, 6 UrkThrow
        if(!spw_manager.rightBaseBuildt || !spw_manager.leftBaseBuildt)
        {
            if (!spw_manager.isBuilding)
            {
                switch (thisAiRace)
                {
                    case AiRace.Human:

                        switch (AiCtrl.currentAI)
                        {
                            case AI_Controller.AIType.Aggressor:
                                if (!spw_manager.rightBaseBuildt)
                                {
                                    spw_manager.BuildNewBase(3);
                                }
                                else if(!spw_manager.leftBaseBuildt)
                                {
                                    spw_manager.BuildNewBase(0);
                                }
                                else
                                {
                                    CancelInvoke("BaseUpgrade");
                                }
                                break;
                            case AI_Controller.AIType.Defensive:
                                if (!spw_manager.rightBaseBuildt)
                                {
                                    spw_manager.BuildNewBase(2);
                                }
                                else if (!spw_manager.leftBaseBuildt)
                                {
                                    spw_manager.BuildNewBase(1);
                                }
                                else
                                {
                                    CancelInvoke("BaseUpgrade");
                                }
                                break;
                            case AI_Controller.AIType.Netrual:
                                if (!spw_manager.rightBaseBuildt)
                                {
                                    spw_manager.BuildNewBase(1);
                                }
                                else if (!spw_manager.leftBaseBuildt)
                                {
                                    spw_manager.BuildNewBase(2);
                                }
                                else
                                {
                                    CancelInvoke("BaseUpgrade");
                                }
                                break;
                        }

                        break;
                    case AiRace.Orc:
                        switch (AiCtrl.currentAI)
                        {
                            case AI_Controller.AIType.Aggressor:
                                if (!spw_manager.rightBaseBuildt)
                                {
                                    spw_manager.BuildNewBase(6);
                                }
                                else if (!spw_manager.leftBaseBuildt)
                                {
                                    spw_manager.BuildNewBase(5);
                                }
                                else
                                {
                                    CancelInvoke("BaseUpgrade");
                                }
                                break;
                            case AI_Controller.AIType.Defensive:
                                if (!spw_manager.rightBaseBuildt)
                                {
                                    spw_manager.BuildNewBase(5);
                                }
                                else if (!spw_manager.leftBaseBuildt)
                                {
                                    spw_manager.BuildNewBase(6);
                                }
                                else
                                {
                                    CancelInvoke("BaseUpgrade");
                                }
                                break;
                            default:
                                if (!spw_manager.rightBaseBuildt)
                                {
                                    spw_manager.BuildNewBase(5);
                                }
                                else if (!spw_manager.leftBaseBuildt)
                                {
                                    spw_manager.BuildNewBase(6);
                                }
                                else
                                {
                                    CancelInvoke("BaseUpgrade");
                                }
                                break;
                        }
                        
                        break;
                    case AiRace.None:                   
                        Debug.LogError("No AiRace chosen");
                        return;
                }
                build = false;
            }
        }
        else
        {
            Debug.Log("Buildt right bases");
        }
    }
}
