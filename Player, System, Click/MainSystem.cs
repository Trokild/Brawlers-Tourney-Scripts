using System.Collections.Generic;
using UnityEngine;
using FoW;

public class MainSystem
{
    public static List<GameObject> SystemUnitList = new List<GameObject>();
    public static List<Player> PlayerList = new List<Player>();
    public static Player LocalPlayerMainSystem;
    public static GameConsol gameConsol;
    private static bool GameOn = false; //LocalPlayer Start the game bool
    
    public static List<Spawner_Manager> Spawner = new List<Spawner_Manager>();

    public static List<Unit> sys_SelectableUnits = new List<Unit>();
    public static List<Unit> sys_Selected = new List<Unit>();
    public static List<Unit> sort_Selected = new List<Unit>();
    public static int LocalTeamInt;

    private static void TheGameIsOn()
    {
        GameOn = true;
        Debug.Log("GameOn");
        if(gameConsol == null)
        {
            gameConsol = GameObject.FindGameObjectWithTag("GameConsol").GetComponent<GameConsol>();
        }
    }

    private static void TheGameIsOff()
    {
        GameOn = true;
        Debug.Log("GameOff");
    }

    public static bool IsGameOn()
    {
        if(GameOn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void NewGameMainSystem()
    {
        SystemUnitList.Clear();
        PlayerList.Clear();
        Spawner.Clear();
        sys_SelectableUnits.Clear();
        sys_Selected.Clear();
        sort_Selected.Clear();
        LocalPlayerMainSystem = null;
        gameConsol = GameObject.FindGameObjectWithTag("GameConsol").GetComponent<GameConsol>();
    }       

    public static void AddUnitList(Unit unit)
    {
        SystemUnitList.Add(unit.gameObject);

        foreach (Player play in PlayerList)
        {
            if(unit.idUnit == play.idPlayer)
            {
                //play.clickScript.selectableObjects.Add(unit);
                unit.clickRef = play.clickScript;
                unit.health.clickRef = play.clickScript;

                if (play.isLocal)
                {
                    sys_SelectableUnits.Add(unit);
                }
                else
                {
                    AI_Controller ai = play.GetComponent<AI_Controller>();
                    ai.AiUnitsBegin.Add(unit);
                    unit.selectionOutline.OutlineColor = Color.red;
                }
            }
            
            if(unit.unitTeam != play.teamPlayer)
            {
                play.clickScript.EnemyObjects.Add(unit.gameObject);
            }
        }
    }

    public static void AddHeroList(Unit hero)
    {
        AddUnitList(hero);

        foreach (Player play in PlayerList)
        {
            if (hero.idUnit == play.idPlayer)
            {
                HeroMagic hm = hero.GetComponent<HeroMagic>();
                if (play.isLocal)
                {
                    Hero_UI hui = GameObject.FindGameObjectWithTag("HeroUI").GetComponent<Hero_UI>();
                    if(hui != null)
                    {
                        hui.SetHeroUi(hero.gameObject, play.colorInt, play.heroInt);
                    }
                    else
                    {
                        Debug.LogError("Can't find heroUI");
                    }
              
                    if (hm != null)
                    {
                        hm.SetHeroLocal(play);
                        hm.NewAbility(0);
                    }
                    else
                    {
                        Debug.LogError("Can't find HeroMagic");
                    }

                    // ALL ABILITIES INITIALIZED
                    //foreach (Ability abi in hm.HeroAbilities)
                    //{
                    //    abi.Initialize(hero.gameObject);
                    //    hui.AbilityButtons[hm.HeroAbilities.IndexOf(abi)].Initialize(abi);
                    //}
                }
                else
                {
                    //need diffrent spell initialize
                    hm.NewAbility(0);
                    hm.StartAiSpell();
                    AI_Controller ai = play.GetComponent<AI_Controller>();
                    //ai.AiUnitsBegin.Add(hero);
                    hero.selectionOutline.OutlineColor = Color.red;
                }

            }
        }
    }

    public static void RespawnHero(Unit hero)
    {
        SystemUnitList.Add(hero.gameObject);

        foreach (Player play in PlayerList)
        {
            if (hero.idUnit == play.idPlayer)
            {
                if (play.isLocal)
                {
                    sys_SelectableUnits.Add(hero);
                    Hero_UI hui = GameObject.FindGameObjectWithTag("HeroUI").GetComponent<Hero_UI>();
                    hui.HeroUiRespawn();
                }
                else
                {
                    AI_Controller ai = play.GetComponent<AI_Controller>();
                    ai.AiUnitsBegin.Add(hero);
                }
            }

            if (hero.unitTeam != play.teamPlayer)
            {
                play.clickScript.EnemyObjects.Add(hero.gameObject);
            }
        }
    }

    public static void AddBuildingList(Health health)
    {
        SystemUnitList.Add(health.gameObject);

        foreach (Player play in PlayerList)
        {
            if (health.healthTeam != play.teamPlayer)
            {
                play.clickScript.EnemyObjects.Add(health.gameObject);
            }
        }
    }

    public static void RemoveBuildingList(Health health)
    {
        SystemUnitList.Remove(health.gameObject);

        foreach (Player play in PlayerList)
        {
            if (health.healthTeam != play.teamPlayer)
            {
                play.clickScript.EnemyObjects.Remove(health.gameObject);
            }
        }
    }

    public static void SortSelected()
    {
        if(sys_Selected.Count > 0)
        {
            sort_Selected.Clear();
            foreach (Unit un in sys_Selected)
            {
                if (un.currentUnitType == Unit.UnitType.Hero)
                {
                    sort_Selected.Add(un);
                }
            }

            foreach (Unit un in sys_Selected)
            {
                if (un.currentUnitType == Unit.UnitType.Infantry)
                {
                    sort_Selected.Add(un);
                }
            }

            foreach (Unit un in sys_Selected)
            {
                if (un.currentUnitType == Unit.UnitType.Spearman)
                {
                    sort_Selected.Add(un);
                }
            }

            foreach (Unit un in sys_Selected)
            {
                if (un.currentUnitType == Unit.UnitType.Archer)
                {
                    sort_Selected.Add(un);
                }
            }

            foreach (Unit un in sys_Selected)
            {
                if (un.currentUnitType == Unit.UnitType.Cavalry)
                {
                    sort_Selected.Add(un);
                }
            }
        }
    }

    public static void RemoveUnitList(Unit unit)
    {
        SystemUnitList.Remove(unit.gameObject);

        foreach (Player play in PlayerList)
        {
            if (unit.idUnit == play.idPlayer)
            {
                sys_SelectableUnits.Remove(unit);
                if(unit.currentlySelected)
                {
                    sys_Selected.Remove(unit);
                    SortSelected();
                    if(play.isLocal)
                    {
                        Click clk = play.clickScript as Click;
                        if(clk != null)
                        {
                            clk.uiu.RemoveUnitToUI(unit.gameObject);
                        }
                        else
                        {
                            Debug.LogError("Cant fin the click");
                        }
                    }
                }

                if (play.isComputer)
                {
                    AI_Controller ai = play.GetComponent<AI_Controller>();
                    ai.RemoveUnit(unit);
                }
            }

            if (unit.unitTeam != play.teamPlayer)
            {
                play.clickScript.EnemyObjects.Remove(unit.gameObject);
            }
        }
    }

    public static void RemoveHeroList(Unit unit)
    {
        SystemUnitList.Remove(unit.gameObject);

        foreach (Player play in PlayerList)
        {
            if (unit.idUnit == play.idPlayer)
            {
                sys_SelectableUnits.Remove(unit);
                if (unit.currentlySelected)
                {
                    sys_Selected.Remove(unit);
                    SortSelected();
                    if (play.isLocal)
                    {
                        Click clk = play.clickScript as Click;
                        if (clk != null)
                        {
                            clk.uiu.RemoveUnitToUI(unit.gameObject);
                        }
                        else
                        {
                            Debug.LogError("Cant fin the click");
                        }
                    }
                }

                if (play.isComputer)
                {
                    AI_Controller ai = play.GetComponent<AI_Controller>();
                    ai.RemoveUnit(unit);
                }

                if (play.isLocal)
                {
                    //disable UI
                    Hero_UI hui = GameObject.FindGameObjectWithTag("HeroUI").GetComponent<Hero_UI>();
                    hui.HeroUiDeath();
                }

                if(gameConsol != null)
                {
                    gameConsol.SetHeroDeath(play.NamePlayer, play.colorInt);
                }
                else
                {
                    Debug.LogError("No gameConsol");
                    gameConsol = GameObject.FindGameObjectWithTag("GameConsol").GetComponent<GameConsol>();
                }
            }

            if (unit.unitTeam != play.teamPlayer)
            {
                play.clickScript.EnemyObjects.Remove(unit.gameObject);
            }
        }
    }

    public static void AddPlayerList(Player player)
    {
        PlayerList.Add(player);
        if(player.clickScript.isLocalHost && player.isLocal)
        {
            LocalPlayerMainSystem = player;
            TheGameIsOn();
            player.GetComponent<FogOfWarTeam>().ChangeTeamFog(player.teamPlayer);
            LocalTeamInt = player.teamPlayer;
        }
        foreach(Spawner_Manager usm in Spawner)
        {
            if(usm.spawnablePlace == player.spawnId)
            {
                if (player.clickScript.isLocalHost)
                {
                    usm.SetUpStartingBase(player.teamPlayer, player.idPlayer, player.colorInt, player.baseInt, player);
                    usm.ConnectBaseUi_Start();
                }
                else //if AI? what if !localHost but Client
                {
                    usm.SetUpStartingBase(player.teamPlayer, player.idPlayer, player.colorInt, player.baseInt, player);
                    player.clickScript.spawnManager = usm;
                    player.GetComponent<AI_Controller>().SetIam(usm.spawnablePlace);  //this part might become a problem
                    player.GetComponent<AI_Controller>().WhatBaseDoIhave(usm.basesInManager[0]);  //when adding multiple bases
                }
            }
        }
    }

    public static void PlayerDefeated(int pd, int murId)
    {
        Debug.Log("PlayerDefeated MainSystem");
        int alive = 0;
        int teamMur = 0;
        int teamAliveMur = 0;

        foreach (Player play in PlayerList)
        {
            if(play.isComputer)
            {
                Debug.Log("play.isComputer");
                play.GetComponent<AI_Controller>().NewOpponent(pd);
            }

            if(!play.isDefeated)
            {
                alive += 1;
            }

            if(play.idPlayer == murId)
            {
                teamMur = play.teamPlayer;
            }
        }

        Debug.Log(alive + " IS ALIVE");

        if (alive == 1)
        {
            if(LocalPlayerMainSystem != null)
            {
                if(!LocalPlayerMainSystem.isDefeated)
                {
                    if (GameOn)
                    {
                        if(gameConsol != null)
                        {
                            gameConsol.LocalPlayerWon();       
                            TheGameIsOff();
                        }
                        else
                        {
                            Debug.LogError("No GameConsol");
                            gameConsol = GameObject.FindGameObjectWithTag("GameConsol").GetComponent<GameConsol>();
                        }
                    }
                    else
                    {
                        Debug.Log(GameOn);
                    }

                    return;
                }
                else
                {
                    foreach (Player play in PlayerList)
                    {
                        if (!play.isDefeated)
                        {
                            if (IsGameOn())
                            {
                                if(gameConsol != null)
                                {
                                    if (play.teamPlayer == LocalPlayerMainSystem.teamPlayer)
                                    {
                                        gameConsol.LocalPlayerWon();
                                    }
                                    else
                                    {
                                        gameConsol.LocalPlayerDead();
                                    }
                                    gameConsol.TeamWon(play.teamPlayer);
                                    TheGameIsOff();
                                }
                                else
                                {
                                    Debug.LogError("No GameConsol");
                                    gameConsol = GameObject.FindGameObjectWithTag("GameConsol").GetComponent<GameConsol>();
                                }
                            }
                            return;
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("LocalPlayerMainSystem == null");
            }
        }
        else if(alive > 1)
        {
            if(teamMur == 0)
            {
                Debug.LogError(murId + "murID with team: " + teamMur);
            }

            foreach (Player play in PlayerList)
            {
                if(play.teamPlayer == teamMur && !play.isDefeated)
                {
                    teamAliveMur += 1;
                }
            }

            if(teamAliveMur == alive)
            {
                Debug.Log(alive + " is alive and " + teamAliveMur + " of them is one of us");
                Debug.Log("Team " + teamMur +" won the game");

                LocalPlayerMainSystem.PlayerTeamWon(teamMur);
                TheGameIsOff();
            }
            else
            {
                Debug.Log(alive + " is alive and " + teamAliveMur + " of them is one of us");
                int left = alive - teamAliveMur;
                Debug.Log( "Team " + teamMur + " still got " + left + " to kill!");
            }
        }
    }
}
