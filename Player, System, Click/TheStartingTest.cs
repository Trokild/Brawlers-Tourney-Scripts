using System.Collections.Generic;
using UnityEngine;

public class TheStartingTest : MonoBehaviour
{
    public List<Player> players = new List<Player>();
    //public Player[] players;

    //public List<AiStart> Startplayers = new List<AiStart>();
    public PlayerStart[] Startplayers;
    public Player localPlayer;

    public Item item;


    private void Start()
    {
        //players = Object.FindObjectsOfType<Player>();
        GameObject go = GameObject.FindGameObjectWithTag("Contenders");
        PlayersSceneStart pss = go.GetComponent<PlayersSceneStart>();

        Startplayers = pss.pl;

        SyncPlayers();

        Invoke("StartGame", 1f); //Should not happend after x time, but when the game if finished setting all the refrences
    }

    void SyncPlayers()
    {
        for (int i = 0; i < Startplayers.Length; i++)
        {
            switch (Startplayers[i].st_Hero)
            {
                case 0:
                    players[i].playerHero = Player.HeroPlayer.Werdoom;
                    players[i].heroInt = 0;
                    players[i].baseInt = 0;
                    break;
                case 1:
                    players[i].playerHero = Player.HeroPlayer.Urka;
                    players[i].heroInt = 1;
                    players[i].baseInt = 4;
                    break;
                default:
                    Debug.LogError("Wrong st_Hero");
                    break;
            }

            if (Startplayers[i].st_isLocal)
            {
                localPlayer.teamPlayer = Startplayers[i].st_TeamInt;
                localPlayer.spawnId = Startplayers[i].st_PosInt;
                localPlayer.colorInt = Startplayers[i].st_ColorInt;
                localPlayer.NamePlayer = Startplayers[i].myName;
            }
            else
            {
                players[i].isPlay = Startplayers[i].isActivated;
                players[i].teamPlayer = Startplayers[i].st_TeamInt;
                players[i].spawnId = Startplayers[i].st_PosInt;
                players[i].colorInt = Startplayers[i].st_ColorInt;

                AiStart ais = Startplayers[i].GetComponent<AiStart>();
                AI_Controller plai = players[i].GetComponent<AI_Controller>();

                if (ais == null)
                {
                    Debug.Log("cant find aistart");
                }

                if (plai == null)
                {
                    Debug.Log("cant find AI");
                }

                int aisint = ais.st_Ai;

                switch (aisint)
                {
                    case 0:
                        plai.currentAI = AI_Controller.AIType.Netrual;
                        players[i].NamePlayer = "Netrual Computer";
                        break;
                    case 1:
                        plai.currentAI = AI_Controller.AIType.Aggressor;
                        players[i].NamePlayer = "Aggresive Computer";
                        break;
                    case 2:
                        plai.currentAI = AI_Controller.AIType.Defensive;
                        players[i].NamePlayer = "Defensive Computer";
                        break;
                }
            }
        }
    }

    public void StartGame()
    {
        foreach (Player ply in players)
        {    
            if(ply.isLocal)
            {
                Click clk = ply.clickScript as Click;
                if(clk != null)
                {
                    clk.StartLocalHost();
                }
                else
                {
                    Debug.LogError("went wrong here");
                }

                ply.PlayerInn();
            }
            else if(ply.isPlay)
            {
                ply.PlayerInn();
                ply.GetComponent<AI_Controller>().StartTheAI();
            }
        }
    }
}
