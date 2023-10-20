using UnityEngine;

public class Player : MonoBehaviour {

    public string NamePlayer;
    public bool isLocal;
    public bool isComputer;
    public bool isDefeated;
    public bool isPlay;

    public enum HeroPlayer {Werdoom, Urka}
    public HeroPlayer playerHero;
    public GameObject Go_PlayerHero;
    private HeroExperiance Xp_PlayerHero;
    public int teamPlayer;
    public int heroInt;
    public int idPlayer;
    public int spawnId;
    public int colorInt;
    public int baseInt;

    public int gold;
    public int book;

    public ClickParent clickScript;
    private GameConsol GC;

    void Start()
    {
        if (isLocal)
        {
            clickScript = GetComponent<Click>();
            if(clickScript != null)
            {
                clickScript.playerClick = this;
            }
        }
        GC = GameObject.FindGameObjectWithTag("GameConsol").GetComponent<GameConsol>();
    }

    public void PlayerInn()
    {
        MainSystem.AddPlayerList(this);
    }

    public void PlayerOut(int mur)
    {
        int a = idPlayer + 1;
        if (clickScript.spawnManager.BaseDestroyed())
        {
            isDefeated = true;
            if (isLocal)
            {
                Debug.Log("you lost");
                int teamMates = 0;
                foreach (Player play in MainSystem.PlayerList)
                {
                    if (play != this && play.teamPlayer == teamPlayer)
                    {
                        teamMates += 1;
                    }
                }

                Debug.Log("You have " + teamMates + " Left");

                if (teamMates < 1)
                {
                    GC.LocalPlayerDead();
                }
                GC.SetLocalDefeatedTxt(idPlayer);
            }
            else
            {
                GC.SetDefeatedTxt(NamePlayer, colorInt);
            }
            MainSystem.PlayerDefeated(a, mur);
        }
        else
        {
            GC.SetBaseDestroyed(NamePlayer, colorInt);
        }
       
    }

    public void PlayerTeamWon(int team)
    {
        if (MainSystem.IsGameOn())
        {
            GC.TeamWon(team);
            if (isLocal && teamPlayer == team)
            {
                GC.LocalPlayerWon();
            }
            else
            {
                GC.CreepTheConsol();
                GC.LocalPlayerDead();
            }
        }
    }

    public void PlayerXp(int xp, Vector3 victomPos)
    {
        //PlayerXP
        if(Go_PlayerHero != null)
        {
            if (Xp_PlayerHero == null)
            {
                Xp_PlayerHero = Go_PlayerHero.GetComponent<HeroExperiance>();
            }

            float dist = Vector3.Distance(Go_PlayerHero.transform.position, victomPos);
            float xpDistReq = Xp_PlayerHero.xpDist;

            if(dist < xpDistReq)
            {
                Xp_PlayerHero.GainXp(xp);
            }
        }
        else
        {
            return;
        }
    }
}
