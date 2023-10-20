using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour {

    public enum AIType { Netrual, Aggressor, Defensive}
    public AIType currentAI;

    [SerializeField] private ClickParent clikAi;
    private Infantry_Spawner us;
    [SerializeField] private Player ply;
    [SerializeField] private AI_UpgraderCtrl AiUpg;

    [SerializeField] private int Iam;
    [SerializeField] private int myEnemieIs;
    public Transform[] mapArrayPos;

    public List<Unit> AiUnitsBegin;
    public List<Unit> AiUnitsOrder;
    public List<Unit> AiUnitsOrderTwo;
    public List<Unit> AiUnitsOrderEnd;
    public List<Unit> AiUnitsOrderDefend;

    public enum Formation { NoFormation, KeepFormation, BoxFormation }
    public Formation curForm;
    private int col;
    [SerializeField] private float sideDist;
    [SerializeField] private float radDist;
    private float offset;
    private Vector3 total;
    private Vector3 total2;

    public float startTime;
    public float startAttack;
    public float OrderAttack;
    public float attackTimer;

    private int upgInt = 0;
    private int wepTyp; //0 = sword, 1 = axe, 2 = mace
    private bool upgWep = false;

    public void StartTheAI()
    {
        Invoke("StartAi", startTime);
    }

    void StartAi()
    {
        myEnemieIs = FetchMeAWorthyOpponent();

        clikAi.spawnManager.basesInManager[0].StartProducing();
        AiUpg.SetUpAIUpg(ply, clikAi.spawnManager);
        switch (currentAI)
        {
            case AIType.Netrual:
                Invoke("StartAttackNetrual", startAttack + 20f);
                break;

            case AIType.Aggressor:
                Invoke("StartAttackAggresive", startAttack);
                break;

            case AIType.Defensive:
                Invoke("StartAttackNetrual", startAttack + 60f);
                break;
        }
    }

    void StartAttackNetrual()
    {
        switch (curForm)
        {
            case Formation.BoxFormation:
                if (AiUnitsBegin.Count > 0)
                {
                    if (AiUnitsBegin.Count < 12)
                    {
                        col = 4;
                    }
                    if (AiUnitsBegin.Count >= 12)
                    {
                        col = 5;
                    }
                    if (AiUnitsBegin.Count > 24)
                    {
                        col = 7;
                    }
                    if (AiUnitsBegin.Count > 36)
                    {
                        col = 9;
                    }

                    offset = ((sideDist * col) / 2) * -1;
                    bool first = false;
                    int curCol = 0;

                    for (int i = 0; i < AiUnitsBegin.Count; i++)
                    {
                        if (first)
                        {
                            if (curCol >= col)
                            {
                                total2 += new Vector3(0, 0, radDist);
                                total2.x = 0;
                                curCol = 0;
                            }
                            else
                            {
                                curCol += 1;
                                total2 += new Vector3(sideDist, 0, 0);
                            }
                        }
                        else
                        {
                            first = true;
                        }

                        Vector3 startPos = new Vector3(total2.x + offset, 0, total2.z);

                        if (AiUnitsBegin[i] != null && AiUnitsBegin[i].idUnit == clikAi.playerClick.idPlayer)
                        {
                            AiUnitsBegin[i].AttackMoveOrder(mapArrayPos[4].position + startPos);
                            AiUnitsOrder.Add(AiUnitsBegin[i]);       
                        }
                    }
                    total2 = Vector3.zero;
                }
                break;

            case Formation.KeepFormation:
                foreach (Unit un in AiUnitsBegin)
                {
                    if (un != null)
                    {
                        total += un.transform.position;
                    }
                }

                Vector3 center = total / AiUnitsBegin.Count;

                foreach (Unit un2 in AiUnitsBegin)
                {
                    if (un2 != null && un2.idUnit == clikAi.playerClick.idPlayer)
                    {
                        Vector3 startPos = un2.transform.position - center;
                        un2.AttackMoveOrder(mapArrayPos[4].position + startPos);
                        AiUnitsOrder.Add(un2);
                        total = Vector3.zero;
                    }
                }
                break;
        }

        int randNum = Random.Range(1, 4);
        switch(randNum)
        {
            case 1:
                Invoke("OrderGroupLeft", OrderAttack);
                break;
            case 2:
                Invoke("OrderGroupRight", OrderAttack);
                break;
            case 3:
                Invoke("OrderGroupStormBase", OrderAttack);
                break;
            default:
                Invoke("OrderGroupStormBase", OrderAttack);
                break;
        }
        AiUnitsBegin.Clear();
    }

    void StartAttackAggresive()
    {
        int myE = (myEnemieIs - 1);
        foreach (Unit un in AiUnitsBegin)
        {
            if (un != null)
            {
                total += un.transform.position;
            }
        }
        Vector3 center = total / AiUnitsBegin.Count;

        switch (curForm)
        {
            case Formation.BoxFormation:
                Vector3 dir2 = (mapArrayPos[myE].position - center).normalized;
                Vector3 dir = (center - mapArrayPos[myE].position).normalized;

                if (AiUnitsBegin.Count > 0)
                {
                    if (AiUnitsBegin.Count < 12)
                    {
                        col = 4;
                    }
                    if (AiUnitsBegin.Count >= 12)
                    {
                        col = 5;
                    }
                    if (AiUnitsBegin.Count > 24)
                    {
                        col = 7;
                    }
                    if (AiUnitsBegin.Count > 36)
                    {
                        col = 9;
                    }

                    offset = ((sideDist * col) / 2) * -1;
                    bool first = false;
                    int curCol = 0;
                     
                    for (int i = 0; i < AiUnitsBegin.Count; i++)
                    {
                        if (first)
                        {
                            if (curCol >= col)
                            {
                                total2 += new Vector3(0, 0, radDist);
                                total2.x = 0;
                                curCol = 0;
                            }
                            else
                            {
                                curCol += 1;
                                total2 += new Vector3(sideDist, 0, 0);
                            }
                        }
                        else
                        {
                            first = true;
                        }

                        Vector3 startPos = new Vector3(total2.x + offset, 0, total2.z);

                        if (AiUnitsBegin[i] != null && AiUnitsBegin[i].idUnit == clikAi.playerClick.idPlayer)
                        {
                            AiUnitsBegin[i].AttackMoveOrder(mapArrayPos[myE].position + startPos);
                            AiUnitsOrderEnd.Add(AiUnitsBegin[i]);
                            total2 = Vector3.zero;
                        }
                    }
                }
                    break;

            case Formation.KeepFormation:
                foreach (Unit un2 in AiUnitsBegin)
                {
                    if (un2 != null && un2.idUnit == clikAi.playerClick.idPlayer)
                    {
                        Vector3 startPos = un2.transform.position - center;
                        un2.AttackMoveOrder(mapArrayPos[myE].position + startPos);
                        AiUnitsOrderEnd.Add(un2);
                        total = Vector3.zero;
                    }
                }
                break;
        }

        AiUnitsBegin.Clear();
        Invoke("StartAttackAggresive", startAttack);
    }
    
    #region Orders
    void OrderGroupLeft()
    {
        foreach (Unit un in AiUnitsOrder)
        {
            if (un != null)
            {
                total += un.transform.position;
            }
            else
            {
                AiUnitsOrder.Remove(un);
            }
        }
        Vector3 center = total / AiUnitsOrder.Count;

        switch (curForm)
        {
            case Formation.BoxFormation:
                if (AiUnitsOrder.Count > 0)
                {
                    if (AiUnitsOrder.Count < 12)
                    {
                        col = 4;
                    }
                    if (AiUnitsOrder.Count >= 12)
                    {
                        col = 5;
                    }
                    if (AiUnitsOrder.Count > 24)
                    {
                        col = 7;
                    }
                    if (AiUnitsOrder.Count > 36)
                    {
                        col = 9;
                    }

                    offset = ((sideDist * col) / 2) * -1;
                    bool first = false;
                    int curCol = 0;

                    for (int i = 0; i < AiUnitsOrder.Count; i++)
                    {
                        if (first)
                        {
                            if (curCol >= col)
                            {
                                total2 += new Vector3(0, 0, radDist);
                                total2.x = 0;
                                curCol = 0;
                            }
                            else
                            {
                                curCol += 1;
                                total2 += new Vector3(sideDist, 0, 0);
                            }
                        }
                        else
                        {
                            first = true;
                        }
                        Vector3 startPos = new Vector3(total2.x + offset, 0, total2.z);

                        if (AiUnitsOrder[i] != null && AiUnitsOrder[i].idUnit == clikAi.playerClick.idPlayer)
                        {
                            switch (myEnemieIs)
                            {
                                case 0:
                                    Debug.LogError("No Enemies, you have either won or bugged out");
                                    break;
                                case 1:
                                    AiUnitsOrder[i].AttackMoveOrder(mapArrayPos[6].position + startPos);
                                    break;
                                case 2:
                                    AiUnitsOrder[i].AttackMoveOrder(mapArrayPos[8].position + startPos);
                                    break;
                                case 3:
                                    AiUnitsOrder[i].AttackMoveOrder(mapArrayPos[10].position + startPos);
                                    break;
                                case 4:
                                    AiUnitsOrder[i].AttackMoveOrder(mapArrayPos[12].position + startPos);
                                    break;
                                default:
                                    break;
                            }
                            AiUnitsOrderTwo.Add(AiUnitsOrder[i]);
                            total2 = Vector3.zero;
                        }
                    }
                }
                break;

            case Formation.KeepFormation:
                foreach (Unit un2 in AiUnitsOrder)
                {
                    if (un2 != null && un2.idUnit == clikAi.playerClick.idPlayer)
                    {
                        Vector3 startPos = un2.transform.position - center;

                        switch (myEnemieIs)
                        {
                            case 0:
                                //No enemies
                                Debug.LogError("No Enemies, you have either won or bugged out");
                                break;
                            case 1:
                                un2.AttackMoveOrder(mapArrayPos[6].position + startPos);
                                break;
                            case 2:
                                un2.AttackMoveOrder(mapArrayPos[8].position + startPos);
                                break;
                            case 3:
                                un2.AttackMoveOrder(mapArrayPos[10].position + startPos);
                                break;
                            case 4:
                                un2.AttackMoveOrder(mapArrayPos[12].position + startPos);
                                break;
                            default:
                                break;
                        }
                        AiUnitsOrderTwo.Add(un2);
                        total = Vector3.zero;
                    }
                }
                break;
        }

        AiUnitsOrder.Clear();
        Invoke("OrderGroupBase", OrderAttack);
        Invoke("StartAttackNetrual", attackTimer);
    }

    void OrderGroupRight()
    {
        foreach (Unit un in AiUnitsOrder)
        {
            if (un != null)
            {
                total += un.transform.position;
            }
            else
            {
                AiUnitsOrder.Remove(un);
            }
        }
        Vector3 center = total / AiUnitsOrder.Count;

        switch(curForm)
        {
            case Formation.KeepFormation:
                foreach (Unit un2 in AiUnitsOrder)
                {
                    if (un2 != null && un2.idUnit == clikAi.playerClick.idPlayer)
                    {
                        Vector3 startPos = un2.transform.position - center;
                        switch (myEnemieIs)
                        {
                            case 0:
                                Debug.LogError("No Enemies, you have either won or bugged out");
                                break;
                            case 1:
                                un2.AttackMoveOrder(mapArrayPos[5].position + startPos);
                                break;
                            case 2:
                                un2.AttackMoveOrder(mapArrayPos[7].position + startPos);
                                break;
                            case 3:
                                un2.AttackMoveOrder(mapArrayPos[9].position + startPos);
                                break;
                            case 4:
                                un2.AttackMoveOrder(mapArrayPos[11].position + startPos);
                                break;
                            default:
                                break;
                        }
                        AiUnitsOrderTwo.Add(un2);
                        total = Vector3.zero;
                    }
                }
                break;

            case Formation.BoxFormation:
                if (AiUnitsOrder.Count > 0)
                {
                    if (AiUnitsOrder.Count < 12)
                    {
                        col = 4;
                    }
                    if (AiUnitsOrder.Count >= 12)
                    {
                        col = 5;
                    }
                    if (AiUnitsOrder.Count > 24)
                    {
                        col = 7;
                    }
                    if (AiUnitsOrder.Count > 36)
                    {
                        col = 9;
                    }

                    offset = ((sideDist * col) / 2) * -1;
                    bool first = false;
                    int curCol = 0;

                    for (int i = 0; i < AiUnitsOrder.Count; i++)
                    {
                        if (first)
                        {
                            if (curCol >= col)
                            {
                                total2 += new Vector3(0, 0, radDist);
                                total2.x = 0;
                                curCol = 0;
                            }
                            else
                            {
                                curCol += 1;
                                total2 += new Vector3(sideDist, 0, 0);
                            }
                        }
                        else
                        {
                            first = true;
                        }

                        Vector3 startPos = new Vector3(total2.x + offset, 0, total2.z);
                        if (AiUnitsOrder[i] != null && AiUnitsOrder[i].idUnit == clikAi.playerClick.idPlayer)
                        {
                            switch (myEnemieIs)
                            {
                                case 0:
                                    Debug.LogError("No Enemies, you have either won or bugged out");
                                    break;
                                case 1:
                                    AiUnitsOrder[i].AttackMoveOrder(mapArrayPos[5].position + startPos);
                                    break;
                                case 2:
                                    AiUnitsOrder[i].AttackMoveOrder(mapArrayPos[7].position + startPos);
                                    break;
                                case 3:
                                    AiUnitsOrder[i].AttackMoveOrder(mapArrayPos[9].position + startPos);
                                    break;
                                case 4:
                                    AiUnitsOrder[i].AttackMoveOrder(mapArrayPos[11].position + startPos);
                                    break;
                                default:
                                    break;
                            }
                            AiUnitsOrderTwo.Add(AiUnitsOrder[i]);
                            total2 = Vector3.zero;
                        }
                    }
                }
                break;
        }

        AiUnitsOrder.Clear();
        Invoke("OrderGroupBase", OrderAttack);
        Invoke("StartAttackNetrual", attackTimer);
    }

    void OrderGroupStormBase()
    {
        foreach (Unit un in AiUnitsOrder)
        {
            if (un != null)
            {
                total += un.transform.position;
            }
            else
            {
                AiUnitsOrder.Remove(un);
            }
        }

        Vector3 center = total / AiUnitsOrder.Count;

        switch (curForm)
        {
            case Formation.KeepFormation:
                foreach (Unit un2 in AiUnitsOrder)
                {
                    if (un2 != null && un2.idUnit == clikAi.playerClick.idPlayer)
                    {
                        Vector3 startPos = un2.transform.position - center;
                        //un2.AttackMoveOrder(PointBase.position + startPos);
                        switch (myEnemieIs)
                        {
                            case 0:
                                //No enemies
                                Debug.LogError("No Enemies, you have either won or bugged out");
                                break;
                            case 1:
                                //Attack the base1 (from the sides), goes for the rest also
                                un2.AttackMoveOrder(mapArrayPos[0].position + startPos);
                                break;
                            case 2:
                                un2.AttackMoveOrder(mapArrayPos[1].position + startPos);
                                break;
                            case 3:
                                un2.AttackMoveOrder(mapArrayPos[2].position + startPos);
                                break;
                            case 4:
                                un2.AttackMoveOrder(mapArrayPos[3].position + startPos);
                                break;
                            default:
                                break;
                        }
                        total = Vector3.zero;
                        AiUnitsOrderEnd.Add(un2);
                    }
                }
                break;

            case Formation.BoxFormation:
                if (AiUnitsOrder.Count > 0)
                {
                    if (AiUnitsOrder.Count < 12)
                    {
                        col = 4;
                    }
                    if (AiUnitsOrder.Count >= 12)
                    {
                        col = 5;
                    }
                    if (AiUnitsOrder.Count > 24)
                    {
                        col = 7;
                    }
                    if (AiUnitsOrder.Count > 36)
                    {
                        col = 9;
                    }

                    offset = ((sideDist * col) / 2) * -1;
                    bool first = false;
                    int curCol = 0;

                    for (int i = 0; i < AiUnitsOrder.Count; i++)
                    {
                        if (first)
                        {
                            if (curCol >= col)
                            {
                                total2 += new Vector3(0, 0, radDist);
                                total2.x = 0;
                                curCol = 0;
                            }
                            else
                            {
                                curCol += 1;
                                total2 += new Vector3(sideDist, 0, 0);
                            }
                        }
                        else
                        {
                            first = true;
                        }

                        Vector3 startPos = new Vector3(total2.x + offset, 0, total2.z);

                        if (AiUnitsOrder[i] != null && AiUnitsOrder[i].idUnit == clikAi.playerClick.idPlayer)
                        {
                            switch (myEnemieIs)
                            {
                                case 0:
                                    Debug.LogError("No Enemies, you have either won or bugged out");
                                    break;
                                case 1:
                                    AiUnitsOrder[i].AttackMoveOrder(mapArrayPos[0].position + startPos);
                                    break;
                                case 2:
                                    AiUnitsOrder[i].AttackMoveOrder(mapArrayPos[1].position + startPos);
                                    break;
                                case 3:
                                    AiUnitsOrder[i].AttackMoveOrder(mapArrayPos[2].position + startPos);
                                    break;
                                case 4:
                                    AiUnitsOrder[i].AttackMoveOrder(mapArrayPos[3].position + startPos);
                                    break;
                                default:
                                    break;
                            }
                            AiUnitsOrderEnd.Add(AiUnitsOrder[i]);
                            total2 = Vector3.zero;
                        }
                    }
                }
                break;
        }

        Invoke("StartAttackNetrual", attackTimer);
        AiUnitsOrder.Clear();
    }

    void OrderGroupBase()
    {
        foreach (Unit un in AiUnitsOrderTwo)
        {
            if (un != null)
            {
                total += un.transform.position;
            }
            else
            {
                AiUnitsOrderTwo.Remove(un);
            }
        }

        Vector3 center = total / AiUnitsOrderTwo.Count;

        switch (curForm)
        {
            case Formation.KeepFormation:
                foreach (Unit un2 in AiUnitsOrderTwo)
                {
                    if (un2 != null && un2.idUnit == clikAi.playerClick.idPlayer)
                    {
                        Vector3 startPos = un2.transform.position - center;
                        //un2.AttackMoveOrder(PointBase.position + startPos);
                        switch (myEnemieIs)
                        {
                            case 0:
                                Debug.LogError("No Enemies, you have either won or bugged out");
                                break;
                            case 1:
                                un2.AttackMoveOrder(mapArrayPos[0].position + startPos);
                                break;
                            case 2:
                                un2.AttackMoveOrder(mapArrayPos[1].position + startPos);
                                break;
                            case 3:
                                un2.AttackMoveOrder(mapArrayPos[2].position + startPos);
                                break;
                            case 4:
                                un2.AttackMoveOrder(mapArrayPos[3].position + startPos);
                                break;
                            default:
                                break;
                        }
                        total = Vector3.zero;
                        AiUnitsOrderEnd.Add(un2);
                    }
                }
                break;

            case Formation.BoxFormation:
                if (AiUnitsOrderTwo.Count > 0)
                {
                    if (AiUnitsOrderTwo.Count < 12)
                    {
                        col = 4;
                    }
                    if (AiUnitsOrderTwo.Count >= 12)
                    {
                        col = 5;
                    }
                    if (AiUnitsOrderTwo.Count > 24)
                    {
                        col = 7;
                    }
                    if (AiUnitsOrderTwo.Count > 36)
                    {
                        col = 9;
                    }

                    offset = ((sideDist * col) / 2) * -1;
                    bool first = false;
                    int curCol = 0;

                    for (int i = 0; i < AiUnitsOrderTwo.Count; i++)
                    {
                        if (first)
                        {
                            if (curCol >= col)
                            {
                                total2 += new Vector3(0, 0, radDist);
                                total2.x = 0;
                                curCol = 0;
                            }
                            else
                            {
                                curCol += 1;
                                total2 += new Vector3(sideDist, 0, 0);
                            }
                        }
                        else
                        {
                            first = true;
                        }

                        Vector3 startPos = new Vector3(total2.x + offset, 0, total2.z);

                        if (AiUnitsOrderTwo[i] != null && AiUnitsOrderTwo[i].idUnit == clikAi.playerClick.idPlayer)
                        {
                            switch (myEnemieIs)
                            {
                                case 0:
                                    Debug.LogError("No Enemies, you have either won or bugged out");
                                    break;
                                case 1:
                                    AiUnitsOrderTwo[i].AttackMoveOrder(mapArrayPos[0].position + startPos);
                                    break;
                                case 2:
                                    AiUnitsOrderTwo[i].AttackMoveOrder(mapArrayPos[1].position + startPos);
                                    break;
                                case 3:
                                    AiUnitsOrderTwo[i].AttackMoveOrder(mapArrayPos[2].position + startPos);
                                    break;
                                case 4:
                                    AiUnitsOrderTwo[i].AttackMoveOrder(mapArrayPos[3].position + startPos);
                                    break;
                                default:
                                    break;
                            }
                            AiUnitsOrderEnd.Add(AiUnitsOrderTwo[i]);
                            total2 = Vector3.zero;
                        }
                    }
                }
                break;
        }


        AiUnitsOrderTwo.Clear();
    }
    #endregion

    #region Search
    public void SetIam(int a)
    {
        Iam = a;
    }

    public void WhatBaseDoIhave(Unit_Spawner usa)
    {
        Infantry_Spawner infs = usa as Infantry_Spawner;
        if (infs != null)
        {
            us = infs;
        }
    }

    int HowManyPlayers()
    {
        return MainSystem.PlayerList.Count;
    }

    bool IsHeAlive(Player ply)
    {
        if (!ply.isDefeated)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    int FetchMeAWorthyOpponent()
    {
        List<Player> wortyOpponent = new List<Player>();
        foreach (Player play in MainSystem.PlayerList)
        {
            if (play.idPlayer != ply.idPlayer && play.teamPlayer != ply.teamPlayer)
            {
                if (IsHeAlive(play))
                {
                    wortyOpponent.Add(play);
                }
                else
                {
                    Debug.Log("..Dammit hes dead");
                }
            }
        }

        if (wortyOpponent.Count > 0)
        {
            if (wortyOpponent.Count > 1)
            {
                int randNum = Random.Range(0, (wortyOpponent.Count));
                if (wortyOpponent[randNum] != null)
                {
                    return wortyOpponent[randNum].spawnId;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (wortyOpponent[0] != null)
                {
                    return wortyOpponent[0].spawnId;
                }
                else
                {
                    return 0;
                }
            }
        }
        else
        {
            Debug.LogError("There is no one..");
            return 0;
        }
    }

    public void NewOpponent(int dp)
    {
        Debug.Log("NewOpponent " + dp);
        if (dp == Iam)
        {
            //Base Is destroyed, remaining units on field does something??
            return;
        }

        if (dp == myEnemieIs)
        {
            myEnemieIs = FetchMeAWorthyOpponent();
            Debug.Log("My new enemie is " + myEnemieIs);

            // if(AiUnitsOrder.count > 0)
            foreach (Unit u in AiUnitsOrder)
            {
                if (u != null && u.idUnit == clikAi.playerClick.idPlayer)
                {
                    switch (myEnemieIs)
                    {
                        case 1:
                            u.AttackMoveOrder(mapArrayPos[0].position);
                            break;
                        case 2:
                            u.AttackMoveOrder(mapArrayPos[1].position);
                            break;
                        case 3:
                            u.AttackMoveOrder(mapArrayPos[2].position);
                            break;
                        case 4:
                            u.AttackMoveOrder(mapArrayPos[3].position);
                            break;
                        default:

                            break;
                    }
                }
            }

            foreach (Unit u2 in AiUnitsOrderTwo)
            {
                if (u2 != null && u2.idUnit == clikAi.playerClick.idPlayer)
                {
                    switch (myEnemieIs)
                    {
                        case 1:
                            u2.AttackMoveOrder(mapArrayPos[0].position);
                            break;
                        case 2:
                            u2.AttackMoveOrder(mapArrayPos[1].position);
                            break;
                        case 3:
                            u2.AttackMoveOrder(mapArrayPos[2].position);
                            break;
                        case 4:
                            u2.AttackMoveOrder(mapArrayPos[3].position);
                            break;
                        default:

                            break;
                    }
                }
            }

            foreach (Unit u2 in AiUnitsOrderEnd)
            {
                if (u2 != null && u2.idUnit == clikAi.playerClick.idPlayer)
                {
                    switch (myEnemieIs)
                    {
                        case 1:
                            u2.AttackMoveOrder(mapArrayPos[0].position);
                            break;
                        case 2:
                            u2.AttackMoveOrder(mapArrayPos[1].position);
                            break;
                        case 3:
                            u2.AttackMoveOrder(mapArrayPos[2].position);
                            break;
                        case 4:
                            u2.AttackMoveOrder(mapArrayPos[3].position);
                            break;
                        default:

                            break;
                    }
                }
            }
        }
        else
        {
            //keep doing whatever
            return;
        }
    }

    public void RemoveUnit(Unit un)
    {
        foreach (Unit u in AiUnitsBegin)
        {
            if (u == un)
            {
                AiUnitsBegin.Remove(un);
                return;
            }
        }

        foreach (Unit u in AiUnitsOrder)
        {
            if (u == un)
            {
                AiUnitsOrder.Remove(un);
                return;
            }
        }

        foreach (Unit u in AiUnitsOrderTwo)
        {
            if (u == un)
            {
                AiUnitsOrderTwo.Remove(un);
                return;
            }
        }

        foreach (Unit u in AiUnitsOrderEnd)
        {
            if (u == un)
            {
                AiUnitsOrderEnd.Remove(un);
                return;
            }
        }
    }
    #endregion

    public void DefendBase()
    {
        if(AiUnitsBegin.Count < 4)
        {
            SendBackTroops();
        }
    }

    void SendBackTroops()
    {
        if (AiUnitsOrder.Count > 4)
        {
            if (AiUnitsOrder.Count > 0)
            {
                if (AiUnitsOrder.Count < 12)
                {
                    col = 4;
                }
                if (AiUnitsOrder.Count >= 12)
                {
                    col = 5;
                }
                if (AiUnitsOrder.Count > 24)
                {
                    col = 7;
                }
                if (AiUnitsOrder.Count > 36)
                {
                    col = 9;
                }

                offset = ((sideDist * col) / 2) * -1;
                bool first = false;
                int curCol = 0;

                for (int i = 0; i < AiUnitsOrder.Count; i++)
                {
                    if (first)
                    {
                        if (curCol >= col)
                        {
                            total2 += new Vector3(0, 0, radDist);
                            total2.x = 0;
                            curCol = 0;
                        }
                        else
                        {
                            curCol += 1;
                            total2 += new Vector3(sideDist, 0, 0);
                        }
                    }
                    else
                    {
                        first = true;
                    }

                    Vector3 startPos = new Vector3(total2.x + offset, 0, total2.z);

                    if (AiUnitsOrder[i] != null && AiUnitsOrder[i].idUnit == clikAi.playerClick.idPlayer)
                    {
                        if (AiUnitsOrder[i].currentstate != Unit.UnitState.Attack)
                        {
                            switch (currentAI)
                            {
                                case AIType.Netrual:
                                    AiUnitsOrder[i].MoveOrder(mapArrayPos[Iam - 1].position + startPos);
                                    break;
                                case AIType.Aggressor:
                                    AiUnitsOrder[i].AttackMoveOrder(mapArrayPos[Iam - 1].position + startPos);
                                    break;
                                case AIType.Defensive:
                                    AiUnitsOrder[i].MoveOrder(mapArrayPos[Iam - 1].position + startPos);
                                    break;
                            }
                            AiUnitsOrderDefend.Add(AiUnitsOrder[i]);
                            total = Vector3.zero;
                        }
                    }
                }
                //send troops from middle
            }
            AiUnitsOrder.Clear();
        }

        if (AiUnitsOrderTwo.Count > 0)
        {
            if (AiUnitsOrderTwo.Count > 0)
            {
                if (AiUnitsOrderTwo.Count < 12)
                {
                    col = 4;
                }
                if (AiUnitsOrderTwo.Count >= 12)
                {
                    col = 5;
                }
                if (AiUnitsOrderTwo.Count > 24)
                {
                    col = 7;
                }
                if (AiUnitsOrderTwo.Count > 36)
                {
                    col = 9;
                }

                offset = ((sideDist * col) / 2) * -1;
                bool first = false;
                int curCol = 0;

                for (int i = 0; i < AiUnitsOrderTwo.Count; i++)
                {
                    if (first)
                    {
                        if (curCol >= col)
                        {
                            total2 += new Vector3(0, 0, radDist);
                            total2.x = 0;
                            curCol = 0;
                        }
                        else
                        {
                            curCol += 1;
                            total2 += new Vector3(sideDist, 0, 0);
                        }
                    }
                    else
                    {
                        first = true;
                    }

                    Vector3 startPos = new Vector3(total2.x + offset, 0, total2.z);

                    if (AiUnitsOrderTwo[i] != null && AiUnitsOrderTwo[i].idUnit == clikAi.playerClick.idPlayer)
                    {
                        if (AiUnitsOrderTwo[i].currentstate != Unit.UnitState.Attack)
                        {
                            switch (currentAI)
                            {
                                case AIType.Netrual:
                                    AiUnitsOrderTwo[i].MoveOrder(mapArrayPos[Iam - 1].position + startPos);
                                    break;
                                case AIType.Aggressor:
                                    AiUnitsOrderTwo[i].AttackMoveOrder(mapArrayPos[Iam - 1].position + startPos);
                                    break;
                                case AIType.Defensive:
                                    AiUnitsOrderTwo[i].MoveOrder(mapArrayPos[Iam - 1].position + startPos);
                                    break;
                            }
                            AiUnitsOrderDefend.Add(AiUnitsOrderTwo[i]);
                            total = Vector3.zero;
                        }
                    }
                }
            }
            AiUnitsOrderTwo.Clear();
        }

        if(AiUnitsOrderDefend.Count > 0)
        {
            Invoke("AttackMoveHomeBase", 20f);
        }
    }

    void AttackMoveHomeBase()
    {
        Debug.LogError("AttackMoveHomeBase");
        if(AiUnitsOrderDefend.Count < 1)
        {
            return;
        }

        foreach (Unit un in AiUnitsOrderDefend)
        {
            if (un != null)
            {
                total += un.transform.position;
            }
        }
        Vector3 center = total / AiUnitsOrderDefend.Count;


        foreach (Unit un2 in AiUnitsOrderDefend)
        {
            if (un2 != null && un2.idUnit == clikAi.playerClick.idPlayer)
            {
                Vector3 startPos = un2.transform.position - center;
                un2.AttackMoveOrder(mapArrayPos[Iam - 1].position + startPos);
                AiUnitsBegin.Add(un2);
                total = Vector3.zero;
            }
        }
        AiUnitsOrderDefend.Clear();
    }
}
