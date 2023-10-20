using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerMainMenu : MonoBehaviour
{
    public PlayerStart localPlayer;
    public AiStart Ai1;
    public AiStart Ai2;
    public AiStart Ai3;

    [SerializeField] private AudioSource audi;
    [SerializeField] private MapStartUi mapUi;
    public AudioClip click;
    public AudioClip choose;

    public Color[] colors;

    public List<int> positions = new List<int>();
    public List<int> colorIntList = new List<int>();

    [Space]
    public List<GameObject> button_SW = new List<GameObject>();
    public List<GameObject> button_NW = new List<GameObject>();
    public List<GameObject> button_NE = new List<GameObject>();
    public List<GameObject> button_SE = new List<GameObject>();
    [Space]
    public List<GameObject> button_black = new List<GameObject>();
    public List<GameObject> button_blue = new List<GameObject>();
    public List<GameObject> button_brown = new List<GameObject>();
    public List<GameObject> button_green = new List<GameObject>();
    public List<GameObject> button_purple = new List<GameObject>();
    public List<GameObject> button_red = new List<GameObject>();
    public List<GameObject> button_tan = new List<GameObject>();
    public List<GameObject> button_white = new List<GameObject>();
    [Space]
    public GameObject _LocalPlayerSetup;
    public GameObject _AiSetup1;
    public GameObject _AiSetup2;
    public GameObject _AiSetup3;
    [Space]
    public GameObject go_NameInput;
    public GameObject go_0ChooseHero;
    [SerializeField] private TMP_InputField name_input;
    public GameObject go_0ChoosePos;
    public GameObject go_0ChooseTeam;
    public GameObject go_0ChooseColor;
    [Space]
    public GameObject go_1ChooseAi;
    public GameObject go_1ChooseHero;
    public GameObject go_1ChoosePos;
    public GameObject go_1ChooseTeam;
    public GameObject go_1ChooseColor;
    [Space]
    public GameObject go_2ChooseAi;
    public GameObject go_2ChooseHero;
    public GameObject go_2ChoosePos;
    public GameObject go_2ChooseTeam;
    public GameObject go_2ChooseColor;
    [Space]
    public GameObject go_3ChooseAi;
    public GameObject go_3ChooseHero;
    public GameObject go_3ChoosePos;
    public GameObject go_3ChooseTeam;
    public GameObject go_3ChooseColor;

    private void Start()
    {
        SetupLocalPlayer(localPlayer);
        SetupFirstAI(Ai1);

        if (!localPlayer.isActiveAndEnabled)
        {
            PlayersSceneStart pss = GameObject.FindGameObjectWithTag("Contenders").GetComponent<PlayersSceneStart>();
            if (pss != null)
            {
                localPlayer = pss.pl[0].GetComponent<LocalStart>();
                Ai1 = pss.pl[1].GetComponent<AiStart>();
                Ai2 = pss.pl[2].GetComponent<AiStart>();
                Ai3 = pss.pl[3].GetComponent<AiStart>();
            }
        }
    }

    public void SetupLocalPlayer(PlayerStart ls)
    {
        PlayerInfoMenu pim = _LocalPlayerSetup.GetComponent<PlayerInfoMenu>();
        if (pim == null)
        {
            Debug.LogError("no pim found");
            return;
        }

        pim.textPlayerName.SetText(ls.myName);
        switch(ls.st_Hero)
        {
            case 0:
                pim.textHero.SetText("Werdoom");
                break;
            case 1:
                pim.textHero.SetText("Urka");
                break;
            default:
                Debug.LogError("No such hero int");
                return;
        }

        positions.Remove(ls.st_PosInt);
        switch (ls.st_PosInt)
        {
            case 1:
                pim.textPositon.SetText("South West");
                foreach (GameObject gosw in button_SW)
                {
                    gosw.SetActive(false);
                }

                break;

            case 2:
                pim.textPositon.SetText("North West");

                foreach (GameObject gosw in button_NW)
                {
                    gosw.SetActive(false);
                }

                break;

            case 3:
                pim.textPositon.SetText("North East");

                foreach (GameObject gosw in button_NE)
                {
                    gosw.SetActive(false);
                }

                break;

            case 4:
                pim.textPositon.SetText("South East");

                foreach (GameObject gosw in button_SE)
                {
                    gosw.SetActive(false);
                }

                break;
        }

        pim.textTeam.SetText(ls.st_TeamInt.ToString());

        if (ls.st_ColorInt >= 0 && ls.st_ColorInt < 9)
        {
            pim.theColor.color = colors[ls.st_ColorInt];
        }
        else
        {
            Debug.LogError("wrong ls.stcolorint");
        }

        colorIntList.Remove(ls.st_ColorInt);
        switch (ls.st_ColorInt)
        {
            case 0:

                foreach (GameObject gosw in button_black)
                {
                    gosw.SetActive(false);
                }

                break;

            case 1:

                foreach (GameObject gosw in button_blue)
                {
                    gosw.SetActive(false);
                }

                break;

            case 2:

                foreach (GameObject gosw in button_brown)
                {
                    gosw.SetActive(false);
                }

                break;

            case 3:

                foreach (GameObject gosw in button_green)
                {
                    gosw.SetActive(false);
                }

                break;

            case 4:

                foreach (GameObject gosw in button_purple)
                {
                    gosw.SetActive(false);
                }

                break;

            case 5:

                foreach (GameObject gosw in button_red)
                {
                    gosw.SetActive(false);
                }

                break;

            case 6:

                foreach (GameObject gosw in button_tan)
                {
                    gosw.SetActive(false);
                }

                break;

            case 7:

                foreach (GameObject gosw in button_white)
                {
                    gosw.SetActive(false);
                }

                break;
        }
        mapUi.SetPlayerMap((ls.st_PosInt - 1), ls.st_ColorInt);
    }

    public void SetupFirstAI(AiStart ls)
    {
        PlayerInfoMenu pim = _AiSetup1.GetComponent<PlayerInfoMenu>();
        if (pim == null)
        {
            Debug.LogError("no pim found");
            return;
        }

        switch (ls.st_Hero)
        {
            case 0:
                pim.textHero.SetText("Werdoom");
                break;
            case 1:
                pim.textHero.SetText("Urka");
                break;
            default:
                Debug.LogError("No such hero int");
                return;
        }

        switch (ls.AiBehav)
        {
            case 0:
                Ai1.NeturalAi();
                pim.textPlayerName.SetText("Neutral AI");
                break;

            case 1:
                Ai1.AggresiveAi();
                pim.textPlayerName.SetText("Aggresive AI");
                break;

            case 2:
                Ai1.DefensiveAi();
                pim.textPlayerName.SetText("Defensive AI");
                break;

            default:
                Debug.LogError("Wrong AI");
                break;
        }

        positions.Remove(ls.st_PosInt);
        switch (ls.st_PosInt)
        {
            case 1:
                pim.textPositon.SetText("South West");

                foreach (GameObject gosw in button_SW)
                {
                    gosw.SetActive(false);
                }

                break;

            case 2:
                pim.textPositon.SetText("North West");

                foreach (GameObject gosw in button_NW)
                {
                    gosw.SetActive(false);
                }

                break;

            case 3:
                pim.textPositon.SetText("North East");

                foreach (GameObject gosw in button_NE)
                {
                    gosw.SetActive(false);
                }

                break;

            case 4:
                pim.textPositon.SetText("South East");

                foreach (GameObject gosw in button_SE)
                {
                    gosw.SetActive(false);
                }

                break;
        }

        pim.textTeam.SetText(ls.st_TeamInt.ToString());

        if (ls.st_ColorInt >= 0 && ls.st_ColorInt < 9)
        {
            pim.theColor.color = colors[ls.st_ColorInt];
        }
        else
        {
            Debug.LogError("wrong ls.stcolorint");
        }

        colorIntList.Remove(ls.st_ColorInt);
        switch (ls.st_ColorInt)
        {
            case 0:

                foreach (GameObject gosw in button_black)
                {
                    gosw.SetActive(false);
                }

                break;

            case 1:

                foreach (GameObject gosw in button_blue)
                {
                    gosw.SetActive(false);
                }

                break;

            case 2:

                foreach (GameObject gosw in button_brown)
                {
                    gosw.SetActive(false);
                }

                break;

            case 3:

                foreach (GameObject gosw in button_green)
                {
                    gosw.SetActive(false);
                }

                break;

            case 4:

                foreach (GameObject gosw in button_purple)
                {
                    gosw.SetActive(false);
                }

                break;

            case 5:

                foreach (GameObject gosw in button_red)
                {
                    gosw.SetActive(false);
                }

                break;

            case 6:

                foreach (GameObject gosw in button_tan)
                {
                    gosw.SetActive(false);
                }

                break;

            case 7:

                foreach (GameObject gosw in button_white)
                {
                    gosw.SetActive(false);
                }

                break;
        }
        mapUi.SetPlayerMap((ls.st_PosInt - 1), ls.st_ColorInt);
    }

    public void SetupNewPlayerAI_2()
    {
        if (!Ai2.isActivated)
        {
            Ai2.isActivated = true;
            PlayerInfoMenu pim = _AiSetup2.GetComponent<PlayerInfoMenu>();
            if (pim == null)
            {
                Debug.LogError("no pim found");
                return;
            }

            switch (Ai2.st_Hero)
            {
                case 0:
                    pim.textHero.SetText("Werdoom");
                    break;
                case 1:
                    pim.textHero.SetText("Urka");
                    break;
                default:
                    Debug.LogError("No such hero int");
                    return;
            }

            switch (Ai2.AiBehav)
            {
                case 0:
                    Ai2.NeturalAi();
                    pim.textPlayerName.SetText("Neutral AI");
                    break;

                case 1:
                    Ai2.AggresiveAi();
                    pim.textPlayerName.SetText("Aggresive AI");
                    break;

                case 2:
                    Ai2.DefensiveAi();
                    pim.textPlayerName.SetText("Defensive AI");
                    break;

                default:
                    Debug.LogError("Wrong AI");
                    break;
            }

            Ai2.st_PosInt = positions[0];
            positions.Remove(Ai2.st_PosInt);
            switch (Ai2.st_PosInt)
            {
                case 1:
                    pim.textPositon.SetText("South West");

                    foreach (GameObject gosw in button_SW)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 2:
                    pim.textPositon.SetText("North West");

                    foreach (GameObject gosw in button_NW)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 3:
                    pim.textPositon.SetText("North East");

                    foreach (GameObject gosw in button_NE)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 4:
                    pim.textPositon.SetText("South East");

                    foreach (GameObject gosw in button_SE)
                    {
                        gosw.SetActive(false);
                    }

                    break;
            }

            pim.textTeam.SetText(Ai2.st_TeamInt.ToString());

            Ai2.st_ColorInt = colorIntList[0];
            colorIntList.Remove(Ai2.st_ColorInt);

            if (Ai2.st_ColorInt >= 0 && Ai2.st_ColorInt < 9)
            {
                pim.theColor.color = colors[Ai2.st_ColorInt];
            }
            else
            {
                Debug.LogError("wrong ls.stcolorint");
            }

            switch (Ai2.st_ColorInt)
            {
                case 0:

                    foreach (GameObject gosw in button_black)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 1:

                    foreach (GameObject gosw in button_blue)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 2:

                    foreach (GameObject gosw in button_brown)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 3:

                    foreach (GameObject gosw in button_green)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 4:

                    foreach (GameObject gosw in button_purple)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 5:

                    foreach (GameObject gosw in button_red)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 6:

                    foreach (GameObject gosw in button_tan)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 7:

                    foreach (GameObject gosw in button_white)
                    {
                        gosw.SetActive(false);
                    }

                    break;
            }
            audi.PlayOneShot(choose);
            mapUi.SetPlayerMap((Ai2.st_PosInt - 1), Ai2.st_ColorInt);
        }
        else
        {
            OpenAi_2();
            audi.PlayOneShot(click);
        }

    }

    public void SetupNewPlayerAI_3()
    {
        if (!Ai3.isActivated)
        {
            Ai3.isActivated = true;
            PlayerInfoMenu pim = _AiSetup3.GetComponent<PlayerInfoMenu>();
            if (pim == null)
            {
                Debug.LogError("no pim found");
                return;
            }

            switch (Ai3.st_Hero)
            {
                case 0:
                    pim.textHero.SetText("Werdoom");
                    break;
                case 1:
                    pim.textHero.SetText("Urka");
                    break;
                default:
                    Debug.LogError("No such hero int");
                    return;
            }

            switch (Ai3.AiBehav)
            {
                case 0:
                    Ai3.NeturalAi();
                    pim.textPlayerName.SetText("Neutral AI");
                    break;

                case 1:
                    Ai3.AggresiveAi();
                    pim.textPlayerName.SetText("Aggresive AI");
                    break;

                case 2:
                    Ai3.DefensiveAi();
                    pim.textPlayerName.SetText("Defensive AI");
                    break;

                default:
                    Debug.LogError("Wrong AI");
                    break;
            }

            Ai3.st_PosInt = positions[0];
            positions.Remove(Ai3.st_PosInt);
            switch (Ai3.st_PosInt)
            {
                case 1:
                    pim.textPositon.SetText("South West");

                    foreach (GameObject gosw in button_SW)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 2:
                    pim.textPositon.SetText("North West");

                    foreach (GameObject gosw in button_NW)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 3:
                    pim.textPositon.SetText("North East");

                    foreach (GameObject gosw in button_NE)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 4:
                    pim.textPositon.SetText("South East");

                    foreach (GameObject gosw in button_SE)
                    {
                        gosw.SetActive(false);
                    }

                    break;
            }

            pim.textTeam.SetText(Ai3.st_TeamInt.ToString());

            Ai3.st_ColorInt = colorIntList[0];
            colorIntList.Remove(Ai3.st_ColorInt);

            if (Ai3.st_ColorInt >= 0 && Ai3.st_ColorInt < 9)
            {
                pim.theColor.color = colors[Ai3.st_ColorInt];
            }
            else
            {
                Debug.LogError("wrong ls.stcolorint");
            }

            switch (Ai3.st_ColorInt)
            {
                case 0:

                    foreach (GameObject gosw in button_black)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 1:

                    foreach (GameObject gosw in button_blue)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 2:

                    foreach (GameObject gosw in button_brown)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 3:

                    foreach (GameObject gosw in button_green)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 4:

                    foreach (GameObject gosw in button_purple)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 5:

                    foreach (GameObject gosw in button_red)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 6:

                    foreach (GameObject gosw in button_tan)
                    {
                        gosw.SetActive(false);
                    }

                    break;

                case 7:

                    foreach (GameObject gosw in button_white)
                    {
                        gosw.SetActive(false);
                    }

                    break;
            }
            mapUi.SetPlayerMap((Ai3.st_PosInt - 1), Ai3.st_ColorInt);
            audi.PlayOneShot(choose);
        }
        else
        {
            OpenAi_3();
            audi.PlayOneShot(click);
        }
    }

    #region OPEN / CLOSE
    public void OpenHero(int ply)
    {
        switch (ply)
        {
            case 0:
                go_0ChooseHero.SetActive(true);
                break;
            case 1:
                if(Ai1.isActivated)
                {
                    go_1ChooseHero.SetActive(true);
                }
                break;
            case 2:
                if (Ai2.isActivated)
                {
                    go_2ChooseHero.SetActive(true);
                }
                break;
            case 3:
                if (Ai3.isActivated)
                {
                    go_3ChooseHero.SetActive(true);
                }
                break;
            default:
                Debug.LogError("Wrong ply");
                return;
        }
        audi.PlayOneShot(click);
    }

    public void CloseHero(int ply)
    {
        switch (ply)
        {
            case 0:
                go_0ChooseHero.SetActive(false);
                break;
            case 1:
                go_1ChooseHero.SetActive(false);
                break;
            case 2:
                go_2ChooseHero.SetActive(false);
                break;
            case 3:
                go_3ChooseHero.SetActive(false);
                break;
            default:
                Debug.LogError("Wrong ply");
                break;
        }
    }

    #region LOCAL
    public void OpenNameInput()
    {
        go_NameInput.SetActive(true);
        name_input.ActivateInputField();
    }

    public void CloseNameInput()
    {
        go_NameInput.SetActive(false);
        PlayerInfoMenu pim = _LocalPlayerSetup.GetComponent<PlayerInfoMenu>();
        string newName = name_input.text;
        pim.textPlayerName.text = newName;
        localPlayer.myName = newName;
    }

    public void OpenPos_0()
    {
        if(positions.Count < 1)
        {
            return;
        }
        go_0ChoosePos.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void ClosePos_0()
    {
        go_0ChoosePos.SetActive(false);
    }

    public void OpenTeam_0()
    {
        go_0ChooseTeam.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void CloseTeam_0()
    {
        go_0ChooseTeam.SetActive(false);
    }

    public void OpenColor_0()
    {
        go_0ChooseColor.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void CloseColor_0()
    {
        go_0ChooseColor.SetActive(false);
    }

    #endregion

    #region AI ONE
    public void OpenAi_1()
    {
        go_1ChooseAi.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void CloseAi_1()
    {
        go_1ChooseAi.SetActive(false);
    }

    public void OpenPos_1()
    {
        if (positions.Count < 1 || !Ai1.isActivated)
        {
            return;
        }
        go_1ChoosePos.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void Closepos_1()
    {
        go_1ChoosePos.SetActive(false);
    }

    public void OpenTeam_1()
    {
        if (!Ai1.isActivated)
        {
            return;
        }
        go_1ChooseTeam.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void CloseTeam_1()
    {
        go_1ChooseTeam.SetActive(false);
    }

    public void OpenColor_1()
    {
        if (!Ai1.isActivated)
        {
            return;
        }
        go_1ChooseColor.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void CloseColor_1()
    {
        go_1ChooseColor.SetActive(false);
    }
    #endregion

    #region AI TWO
    public void OpenAi_2()
    {
        go_2ChooseAi.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void CloseAi_2()
    {
        go_2ChooseAi.SetActive(false);
    }

    public void OpenPos_2()
    {
        if (positions.Count < 1 || !Ai2.isActivated)
        {
            return;
        }
        go_2ChoosePos.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void Closepos_2()
    {
        go_2ChoosePos.SetActive(false);
    }

    public void OpenTeam_2()
    {
        if(!Ai2.isActivated)
        {
            return;
        }

        go_2ChooseTeam.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void CloseTeam_2()
    {
        go_2ChooseTeam.SetActive(false);
    }

    public void OpenColor_2()
    {
        if (!Ai2.isActivated)
        {
            return;
        }
        go_2ChooseColor.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void CloseColor_2()
    {
        go_2ChooseColor.SetActive(false);
    }

    public void HoverNewPlayer_2()
    {
        if (!Ai2.isActivated)
        {
            PlayerInfoMenu pim = _AiSetup2.GetComponent<PlayerInfoMenu>();
            pim.textPlayerName.SetText("Add Player");
        }
    }

    public void HoverExitNewPlayer_2()
    {
        if (!Ai2.isActivated)
        {
            PlayerInfoMenu pim = _AiSetup2.GetComponent<PlayerInfoMenu>();
            pim.textPlayerName.SetText("Open");
        }
    }

    #endregion

    #region AI THREE
    public void OpenAi_3()
    {
        go_3ChooseAi.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void CloseAi_3()
    {
        go_3ChooseAi.SetActive(false);
        audi.PlayOneShot(click);
    }

    public void OpenPos_3()
    {
        if (positions.Count < 1 || !Ai3.isActivated)
        {
            return;
        }
        go_3ChoosePos.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void Closepos_3()
    {
        go_3ChoosePos.SetActive(false);
        audi.PlayOneShot(click);
    }

    public void OpenTeam_3()
    {
        if (!Ai3.isActivated)
        {
            return;
        }
        go_3ChooseTeam.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void CloseTeam_3()
    {
        go_3ChooseTeam.SetActive(false);
        audi.PlayOneShot(click);
    }

    public void OpenColor_3()
    {
        if (!Ai3.isActivated)
        {
            return;
        }
        go_3ChooseColor.SetActive(true);
        audi.PlayOneShot(click);
    }

    public void CloseColor_3()
    {
        go_3ChooseColor.SetActive(false);
        audi.PlayOneShot(click);
    }

    public void HoverNewPlayer_3()
    {
        if (!Ai3.isActivated)
        {
            PlayerInfoMenu pim = _AiSetup3.GetComponent<PlayerInfoMenu>();
            pim.textPlayerName.SetText("Add Player");
        }
    }

    public void HoverExitNewPlayer_3()
    {
        if (!Ai3.isActivated)
        {
            PlayerInfoMenu pim = _AiSetup3.GetComponent<PlayerInfoMenu>();
            pim.textPlayerName.SetText("Open");
        }
    }
    #endregion

    #endregion

    public void Hero_Werdoom(int ply)
    {
        PlayerInfoMenu pim;
        switch (ply)
        {
            case 0:
                pim = _LocalPlayerSetup.GetComponent<PlayerInfoMenu>();
                localPlayer.st_Hero = 0;
                break;
            case 1:
                pim = _AiSetup1.GetComponent<PlayerInfoMenu>();
                Ai1.st_Hero = 0;
                break;
            case 2:
                pim = _AiSetup2.GetComponent<PlayerInfoMenu>();
                Ai2.st_Hero = 0;
                break;
            case 3:
                pim = _AiSetup3.GetComponent<PlayerInfoMenu>();
                Ai3.st_Hero = 0;
                break;
            default:
                Debug.LogError("Wrong ply Werdoom");
                return;
        }

        if(pim != null)
        {
            pim.textHero.SetText("Werdoom");
        }
        CloseHero(ply);
    }

    public void Hero_Urka(int ply)
    {
        PlayerInfoMenu pim;
        switch (ply)
        {
            case 0:
                pim = _LocalPlayerSetup.GetComponent<PlayerInfoMenu>();
                localPlayer.st_Hero = 1;
                break;
            case 1:
                pim = _AiSetup1.GetComponent<PlayerInfoMenu>();
                Ai1.st_Hero = 1;
                break;
            case 2:
                pim = _AiSetup2.GetComponent<PlayerInfoMenu>();
                Ai2.st_Hero = 1;
                break;
            case 3:
                pim = _AiSetup3.GetComponent<PlayerInfoMenu>();
                Ai3.st_Hero = 1;
                break;
            default:
                Debug.LogError("Wrong ply Urka");
                return;
        }
        if (pim != null)
        {
            pim.textHero.SetText("Urka");
        }
        CloseHero(ply);
    }

    public void Behaveior_Ai_1(int b)
    {
        switch(b)
        {
            case 0:
                Ai1.NeturalAi();
                _AiSetup1.GetComponent<PlayerInfoMenu>().textPlayerName.SetText("Neutral AI");
                CloseAi_1();
                break;

            case 1:
                Ai1.AggresiveAi();
                _AiSetup1.GetComponent<PlayerInfoMenu>().textPlayerName.SetText("Aggresive AI");
                CloseAi_1();
                break;

            case 2:
                 Ai1.DefensiveAi();
                _AiSetup1.GetComponent<PlayerInfoMenu>().textPlayerName.SetText("Defensive AI");
                CloseAi_1();
                break;

            default:
                Debug.LogError("Wrong AI");
                break;
        }
        audi.PlayOneShot(choose);
    }

    public void Behaveior_Ai_2(int b)
    {
        switch (b)
        {
            case 0:
                Ai2.NeturalAi();
                _AiSetup2.GetComponent<PlayerInfoMenu>().textPlayerName.SetText("Neutral AI");
                CloseAi_2();
                break;

            case 1:
                Ai2.AggresiveAi();
                _AiSetup2.GetComponent<PlayerInfoMenu>().textPlayerName.SetText("Aggresive AI");
                CloseAi_2();
                break;

            case 2:
                Ai2.DefensiveAi();
                _AiSetup2.GetComponent<PlayerInfoMenu>().textPlayerName.SetText("Defensive AI");
                CloseAi_2();
                break;

            default:
                Debug.LogError("Wrong AI");
                break;
        }
        audi.PlayOneShot(choose);
    }

    public void Behaveior_Ai_3(int b)
    {
        switch (b)
        {
            case 0:
                Ai3.NeturalAi();
                _AiSetup3.GetComponent<PlayerInfoMenu>().textPlayerName.SetText("Neutral AI");
                CloseAi_3();
                break;

            case 1:
                Ai3.AggresiveAi();
                _AiSetup3.GetComponent<PlayerInfoMenu>().textPlayerName.SetText("Aggresive AI");
                CloseAi_3();
                break;

            case 2:
                Ai3.DefensiveAi();
                _AiSetup3.GetComponent<PlayerInfoMenu>().textPlayerName.SetText("Defensive AI");
                CloseAi_3();
                break;

            default:
                Debug.LogError("Wrong AI");
                break;
        }
        audi.PlayOneShot(choose);
    }

    public void Position_Local_0(int b)
    {
        int op = localPlayer.st_PosInt;
        if (op == b)
        {
            return;
        }

        if (b < 5 && b > 0)
        {
            localPlayer.st_PosInt = b;
        }
        else
        {
            Debug.LogError("Wrong Pos Int");
            return;
        }

        positions.Remove(b);
        switch (b)
        {
            case 1:
                localPlayer.st_PosInt = 1;
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().textPositon.SetText("South West");
                foreach (GameObject gosw in button_SW)
                {
                    gosw.SetActive(false);
                }
                ClosePos_0();
                break;

            case 2:
                localPlayer.st_PosInt = 2;
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().textPositon.SetText("North West");
                foreach (GameObject gosw in button_NW)
                {
                    gosw.SetActive(false);
                }
                ClosePos_0();
                break;

            case 3:
                localPlayer.st_PosInt = 3;
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().textPositon.SetText("North East");
                foreach (GameObject gosw in button_NE)
                {
                    gosw.SetActive(false);
                }
                ClosePos_0();
                break;

            case 4:
                localPlayer.st_PosInt = 4;
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().textPositon.SetText("South East");
                foreach (GameObject gosw in button_SE)
                {
                    gosw.SetActive(false);
                }
                ClosePos_0();
                break;

            default:
                Debug.LogError("Wrong Pos Int Switch");
                break;
        }

        mapUi.ChangePosMap((op -1), (b -1), localPlayer.st_ColorInt);
        audi.PlayOneShot(choose);
        TurnOnPositionButtons(op);
    }

    public void Position_Ai_1(int b)
    {
        int op = Ai1.st_PosInt;
        if (op == b)
        {
            return;
        }

        if (b < 5 && b > 0)
        {
            Ai1.st_PosInt = b;
        }
        else
        {
            Debug.LogError("Wrong Pos Int");
            return;
        }

        positions.Remove(b);
        switch (b)
        { 
            case 1:
                Ai1.st_PosInt = 1;
                _AiSetup1.GetComponent<PlayerInfoMenu>().textPositon.SetText("South West");
                foreach (GameObject gosw in button_SW)
                {
                    gosw.SetActive(false);
                }
                Closepos_1();
                break;

            case 2:
                Ai1.st_PosInt = 2;
                _AiSetup1.GetComponent<PlayerInfoMenu>().textPositon.SetText("North West");
                foreach (GameObject gosw in button_NW)
                {
                    gosw.SetActive(false);
                }
                Closepos_1();
                break;

            case 3:
                Ai1.st_PosInt = 3;
                _AiSetup1.GetComponent<PlayerInfoMenu>().textPositon.SetText("North East");
                foreach (GameObject gosw in button_NE)
                {
                    gosw.SetActive(false);
                }
                Closepos_1();
                break;

            case 4:
                Ai1.st_PosInt = 4;
                _AiSetup1.GetComponent<PlayerInfoMenu>().textPositon.SetText("South East");
                foreach (GameObject gosw in button_SE)
                {
                    gosw.SetActive(false);
                }
                Closepos_1();
                break;

            default:
                Debug.LogError("Wrong Pos Int Switch");
                break;
        }

        mapUi.ChangePosMap((op - 1), (b - 1), Ai1.st_ColorInt);
        audi.PlayOneShot(choose);
        TurnOnPositionButtons(op);
    }

    public void Position_Ai_2(int b)
    {
        int op = Ai2.st_PosInt;
        if (op == b)
        {
            return;
        }

        if (b < 5 && b > 0)
        {
            Ai2.st_PosInt = b;
        }
        else
        {
            Debug.LogError("Wrong Pos Int");
            return;
        }

        positions.Remove(b);
        switch (b)
        {
            case 1:
                Ai2.st_PosInt = 1;
                _AiSetup2.GetComponent<PlayerInfoMenu>().textPositon.SetText("South West");
                foreach (GameObject gosw in button_SW)
                {
                    gosw.SetActive(false);
                }
                Closepos_2();
                break;

            case 2:
                Ai2.st_PosInt = 2;
                _AiSetup2.GetComponent<PlayerInfoMenu>().textPositon.SetText("North West");
                foreach (GameObject gosw in button_NW)
                {
                    gosw.SetActive(false);
                }
                Closepos_2();
                break;

            case 3:
                Ai2.st_PosInt = 3;
                _AiSetup2.GetComponent<PlayerInfoMenu>().textPositon.SetText("North East");
                foreach (GameObject gosw in button_NE)
                {
                    gosw.SetActive(false);
                }
                Closepos_2();
                break;

            case 4:
                Ai2.st_PosInt = 4;
                _AiSetup2.GetComponent<PlayerInfoMenu>().textPositon.SetText("South East");
                foreach (GameObject gosw in button_SE)
                {
                    gosw.SetActive(false);
                }
                Closepos_2();
                break;

            default:
                Debug.LogError("Wrong Pos Int Switch");
                break;
        }

        mapUi.ChangePosMap((op - 1), (b - 1), Ai2.st_ColorInt);
        audi.PlayOneShot(choose);
        TurnOnPositionButtons(op);
    }

    public void Position_Ai_3(int b)
    {
        int op = Ai3.st_PosInt;
        if (op == b)
        {
            return;
        }

        if (b < 5 && b > 0)
        {
            Ai3.st_PosInt = b;
        }
        else
        {
            Debug.LogError("Wrong Pos Int");
            return;
        }

        positions.Remove(b);
        switch (b)
        {
            case 1:
                Ai3.st_PosInt = 1;
                _AiSetup3.GetComponent<PlayerInfoMenu>().textPositon.SetText("South West");
                foreach (GameObject gosw in button_SW)
                {
                    gosw.SetActive(false);
                }
                Closepos_3();
                break;

            case 2:
                Ai3.st_PosInt = 2;
                _AiSetup3.GetComponent<PlayerInfoMenu>().textPositon.SetText("North West");
                foreach (GameObject gosw in button_NW)
                {
                    gosw.SetActive(false);
                }
                Closepos_3();
                break;

            case 3:
                Ai3.st_PosInt = 3;
                _AiSetup3.GetComponent<PlayerInfoMenu>().textPositon.SetText("North East");
                foreach (GameObject gosw in button_NE)
                {
                    gosw.SetActive(false);
                }
                Closepos_3();
                break;

            case 4:
                Ai3.st_PosInt = 4;
                _AiSetup3.GetComponent<PlayerInfoMenu>().textPositon.SetText("South East");
                foreach (GameObject gosw in button_SE)
                {
                    gosw.SetActive(false);
                }
                Closepos_3();
                break;

            default:
                Debug.LogError("Wrong Pos Int Switch");
                break;
        }

        mapUi.ChangePosMap((op - 1), (b - 1), Ai3.st_ColorInt);
        audi.PlayOneShot(choose);
        TurnOnPositionButtons(op);
    }

    public void Team_Ai_1(int b)
    {
        switch (b)
        {
            case 0:
                _AiSetup1.GetComponent<PlayerInfoMenu>().textTeam.SetText("1");
                Ai1.st_TeamInt = 1;
                CloseTeam_1();
                break;

            case 1:
                _AiSetup1.GetComponent<PlayerInfoMenu>().textTeam.SetText("2");
                Ai1.st_TeamInt = 2;
                CloseTeam_1();
                break;

            case 2:
                _AiSetup1.GetComponent<PlayerInfoMenu>().textTeam.SetText("3");
                Ai1.st_TeamInt = 3;
                CloseTeam_1();
                break;

            case 3:
                _AiSetup1.GetComponent<PlayerInfoMenu>().textTeam.SetText("4");
                Ai1.st_TeamInt = 4;
                CloseTeam_1();
                break;

            default:
                Debug.LogError("Wrong Team int");
                break;
        }
        audi.PlayOneShot(choose);
    }

    public void Team_Ai_2(int b)
    {
        switch (b)
        {
            case 0:
                _AiSetup2.GetComponent<PlayerInfoMenu>().textTeam.SetText("1");
                Ai2.st_TeamInt = 1;
                CloseTeam_2();
                break;

            case 1:
                _AiSetup2.GetComponent<PlayerInfoMenu>().textTeam.SetText("2");
                Ai2.st_TeamInt = 2;
                CloseTeam_2();
                break;

            case 2:
                _AiSetup2.GetComponent<PlayerInfoMenu>().textTeam.SetText("3");
                Ai2.st_TeamInt = 3;
                CloseTeam_2();
                break;

            case 3:
                _AiSetup2.GetComponent<PlayerInfoMenu>().textTeam.SetText("4");
                Ai2.st_TeamInt = 4;
                CloseTeam_2();
                break;

            default:
                Debug.LogError("Wrong Team int");
                break;
        }
        audi.PlayOneShot(choose);
    }

    public void Team_Ai_3(int b)
    {
        switch (b)
        {
            case 0:
                _AiSetup3.GetComponent<PlayerInfoMenu>().textTeam.SetText("1");
                Ai3.st_TeamInt = 1;
                CloseTeam_3();
                break;

            case 1:
                _AiSetup3.GetComponent<PlayerInfoMenu>().textTeam.SetText("2");
                Ai3.st_TeamInt = 2;
                CloseTeam_3();
                break;

            case 2:
                _AiSetup3.GetComponent<PlayerInfoMenu>().textTeam.SetText("3");
                Ai3.st_TeamInt = 3;
                CloseTeam_3();
                break;

            case 3:
                _AiSetup3.GetComponent<PlayerInfoMenu>().textTeam.SetText("4");
                Ai3.st_TeamInt = 4;
                CloseTeam_3();
                break;

            default:
                Debug.LogError("Wrong Team int");
                break;
        }
        audi.PlayOneShot(choose);
    }

    public void Team_Local_0(int b)
    {
        switch (b)
        {
            case 0:
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().textTeam.SetText("1");
                localPlayer.st_TeamInt = 1;
                CloseTeam_0();
                break;

            case 1:
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().textTeam.SetText("2");
                localPlayer.st_TeamInt = 2;
                CloseTeam_0();
                break;

            case 2:
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().textTeam.SetText("3");
                localPlayer.st_TeamInt = 3;
                CloseTeam_0();
                break;

            case 3:
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().textTeam.SetText("4");
                localPlayer.st_TeamInt = 4;
                CloseTeam_0();
                break;

            default:
                Debug.LogError("Wrong Team int");
                break;
        }
        audi.PlayOneShot(choose);
    }

    public void Color_Ai_1(int b)
    {
        int cl = Ai1.st_ColorInt;

        if (Ai1.st_ColorInt == b)
        {
            return;
        }

        colorIntList.Remove(b);
        switch (b)
        {
            case 0:
                Ai1.st_ColorInt = 0;
                _AiSetup1.GetComponent<PlayerInfoMenu>().theColor.color = colors[0];

                foreach (GameObject gosw in button_black)
                {
                    gosw.SetActive(false);
                }

                CloseColor_1();
                break;

            case 1:
                Ai1.st_ColorInt = 1;
                _AiSetup1.GetComponent<PlayerInfoMenu>().theColor.color = colors[1];

                foreach (GameObject gosw in button_blue)
                {
                    gosw.SetActive(false);
                }

                CloseColor_1();
                break;

            case 2:
                Ai1.st_ColorInt = 2;
                _AiSetup1.GetComponent<PlayerInfoMenu>().theColor.color = colors[2];

                foreach (GameObject gosw in button_brown)
                {
                    gosw.SetActive(false);
                }

                CloseColor_1();
                break;

            case 3:
                Ai1.st_ColorInt = 3;
                _AiSetup1.GetComponent<PlayerInfoMenu>().theColor.color = colors[3];

                foreach (GameObject gosw in button_green)
                {
                    gosw.SetActive(false);
                }

                CloseColor_1();
                break;

            case 4:
                Ai1.st_ColorInt = 4;
                _AiSetup1.GetComponent<PlayerInfoMenu>().theColor.color = colors[4];

                foreach (GameObject gosw in button_purple)
                {
                    gosw.SetActive(false);
                }

                CloseColor_1();
                break;

            case 5:
                Ai1.st_ColorInt = 5;
                _AiSetup1.GetComponent<PlayerInfoMenu>().theColor.color = colors[5];

                foreach (GameObject gosw in button_red)
                {
                    gosw.SetActive(false);
                }

                CloseColor_1();
                break;

            case 6:
                Ai1.st_ColorInt = 6;
                _AiSetup1.GetComponent<PlayerInfoMenu>().theColor.color = colors[6];

                foreach (GameObject gosw in button_tan)
                {
                    gosw.SetActive(false);
                }

                CloseColor_1();
                break;

            case 7:
                Ai1.st_ColorInt = 7;
                _AiSetup1.GetComponent<PlayerInfoMenu>().theColor.color = colors[7];

                foreach (GameObject gosw in button_white)
                {
                    gosw.SetActive(false);
                }

                CloseColor_1();
                break;



            default:
                Debug.LogError("Wrong color int");
                break;
        }
        mapUi.NewColMap((Ai1.st_PosInt - 1), Ai1.st_ColorInt);
        audi.PlayOneShot(choose);
        TurnOnColorButtons(cl);
    }

    public void Color_Ai_2(int b)
    {
        int cl = Ai2.st_ColorInt;

        if (Ai2.st_ColorInt == b)
        {
            return;
        }

        colorIntList.Remove(b);
        switch (b)
        {
            case 0:
                Ai2.st_ColorInt = 0;
                _AiSetup2.GetComponent<PlayerInfoMenu>().theColor.color = colors[0];

                foreach (GameObject gosw in button_black)
                {
                    gosw.SetActive(false);
                }

                CloseColor_2();
                break;

            case 1:
                Ai2.st_ColorInt = 1;
                _AiSetup2.GetComponent<PlayerInfoMenu>().theColor.color = colors[1];

                foreach (GameObject gosw in button_blue)
                {
                    gosw.SetActive(false);
                }

                CloseColor_2();
                break;

            case 2:
                Ai2.st_ColorInt = 2;
                _AiSetup2.GetComponent<PlayerInfoMenu>().theColor.color = colors[2];

                foreach (GameObject gosw in button_brown)
                {
                    gosw.SetActive(false);
                }

                CloseColor_2();
                break;

            case 3:
                Ai2.st_ColorInt = 3;
                _AiSetup2.GetComponent<PlayerInfoMenu>().theColor.color = colors[3];

                foreach (GameObject gosw in button_green)
                {
                    gosw.SetActive(false);
                }

                CloseColor_2();
                break;

            case 4:
                Ai2.st_ColorInt = 4;
                _AiSetup2.GetComponent<PlayerInfoMenu>().theColor.color = colors[4];

                foreach (GameObject gosw in button_purple)
                {
                    gosw.SetActive(false);
                }

                CloseColor_2();
                break;

            case 5:
                Ai2.st_ColorInt = 5;
                _AiSetup2.GetComponent<PlayerInfoMenu>().theColor.color = colors[5];

                foreach (GameObject gosw in button_red)
                {
                    gosw.SetActive(false);
                }

                CloseColor_2();
                break;

            case 6:
                Ai2.st_ColorInt = 6;
                _AiSetup2.GetComponent<PlayerInfoMenu>().theColor.color = colors[6];

                foreach (GameObject gosw in button_tan)
                {
                    gosw.SetActive(false);
                }

                CloseColor_2();
                break;

            case 7:
                Ai2.st_ColorInt = 7;
                _AiSetup2.GetComponent<PlayerInfoMenu>().theColor.color = colors[7];

                foreach (GameObject gosw in button_white)
                {
                    gosw.SetActive(false);
                }

                CloseColor_2();
                break;



            default:
                Debug.LogError("Wrong color int");
                break;
        }

        mapUi.NewColMap((Ai2.st_PosInt - 1), Ai2.st_ColorInt);
        audi.PlayOneShot(choose);
        TurnOnColorButtons(cl);
    }

    public void Color_Ai_3(int b)
    {
        int cl = Ai3.st_ColorInt;

        if (Ai3.st_ColorInt == b)
        {
            return;
        }

        colorIntList.Remove(b);
        switch (b)
        {
            case 0:
                Ai3.st_ColorInt = 0;
                _AiSetup3.GetComponent<PlayerInfoMenu>().theColor.color = colors[0];

                foreach (GameObject gosw in button_black)
                {
                    gosw.SetActive(false);
                }

                CloseColor_3();
                break;

            case 1:
                Ai3.st_ColorInt = 1;
                _AiSetup3.GetComponent<PlayerInfoMenu>().theColor.color = colors[1];

                foreach (GameObject gosw in button_blue)
                {
                    gosw.SetActive(false);
                }

                CloseColor_3();
                break;

            case 2:
                Ai3.st_ColorInt = 2;
                _AiSetup3.GetComponent<PlayerInfoMenu>().theColor.color = colors[2];

                foreach (GameObject gosw in button_brown)
                {
                    gosw.SetActive(false);
                }

                CloseColor_3();
                break;

            case 3:
                Ai3.st_ColorInt = 3;
                _AiSetup3.GetComponent<PlayerInfoMenu>().theColor.color = colors[3];

                foreach (GameObject gosw in button_green)
                {
                    gosw.SetActive(false);
                }

                CloseColor_3();
                break;

            case 4:
                Ai3.st_ColorInt = 4;
                _AiSetup3.GetComponent<PlayerInfoMenu>().theColor.color = colors[4];

                foreach (GameObject gosw in button_purple)
                {
                    gosw.SetActive(false);
                }

                CloseColor_3();
                break;

            case 5:
                Ai3.st_ColorInt = 5;
                _AiSetup3.GetComponent<PlayerInfoMenu>().theColor.color = colors[5];

                foreach (GameObject gosw in button_red)
                {
                    gosw.SetActive(false);
                }

                CloseColor_3();
                break;

            case 6:
                Ai3.st_ColorInt = 6;
                _AiSetup3.GetComponent<PlayerInfoMenu>().theColor.color = colors[6];

                foreach (GameObject gosw in button_tan)
                {
                    gosw.SetActive(false);
                }

                CloseColor_3();
                break;

            case 7:
                Ai3.st_ColorInt = 7;
                _AiSetup3.GetComponent<PlayerInfoMenu>().theColor.color = colors[7];

                foreach (GameObject gosw in button_white)
                {
                    gosw.SetActive(false);
                }

                CloseColor_3();
                break;



            default:
                Debug.LogError("Wrong color int");
                break;
        }

        mapUi.NewColMap((Ai3.st_PosInt - 1), Ai3.st_ColorInt);
        audi.PlayOneShot(choose);
        TurnOnColorButtons(cl);
    }

    public void Color_Local_0(int b)
    {
        int cl = localPlayer.st_ColorInt;

        if (localPlayer.st_ColorInt == b)
        {
            return;
        }

        colorIntList.Remove(b);
        switch (b)
        {
            case 0:
                localPlayer.st_ColorInt = 0;
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().theColor.color = colors[0];

                foreach (GameObject gosw in button_black)
                {
                    gosw.SetActive(false);
                }

                CloseColor_0();
                break;

            case 1:
                localPlayer.st_ColorInt = 1;
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().theColor.color = colors[1];

                foreach (GameObject gosw in button_blue)
                {
                    gosw.SetActive(false);
                }

                CloseColor_0();
                break;

            case 2:
                localPlayer.st_ColorInt = 2;
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().theColor.color = colors[2];

                foreach (GameObject gosw in button_brown)
                {
                    gosw.SetActive(false);
                }

                CloseColor_0();
                break;

            case 3:
                localPlayer.st_ColorInt = 3;
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().theColor.color = colors[3];

                foreach (GameObject gosw in button_green)
                {
                    gosw.SetActive(false);
                }

                CloseColor_0();
                break;

            case 4:
                localPlayer.st_ColorInt = 4;
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().theColor.color = colors[4];

                foreach (GameObject gosw in button_purple)
                {
                    gosw.SetActive(false);
                }

                CloseColor_0();
                break;

            case 5:
                localPlayer.st_ColorInt = 5;
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().theColor.color = colors[5];

                foreach (GameObject gosw in button_red)
                {
                    gosw.SetActive(false);
                }

                CloseColor_0();
                break;

            case 6:
                localPlayer.st_ColorInt = 6;
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().theColor.color = colors[6];

                foreach (GameObject gosw in button_tan)
                {
                    gosw.SetActive(false);
                }

                CloseColor_0();
                break;

            case 7:
                localPlayer.st_ColorInt = 7;
                _LocalPlayerSetup.GetComponent<PlayerInfoMenu>().theColor.color = colors[7];

                foreach (GameObject gosw in button_white)
                {
                    gosw.SetActive(false);
                }

                CloseColor_0();
                break;



            default:
                Debug.LogError("Wrong color int");
                break;
        }
        mapUi.NewColMap((localPlayer.st_PosInt - 1), localPlayer.st_ColorInt);
        audi.PlayOneShot(choose);
        TurnOnColorButtons(cl);
    }

    public void TurnOnColorButtons(int a)
    {
        colorIntList.Add(a);
        switch (a)
        {
            case 0:
                foreach (GameObject gosw in button_black)
                {
                    gosw.SetActive(true);
                }
                break;

            case 1:
                foreach (GameObject gosw in button_blue)
                {
                    gosw.SetActive(true);
                }
                break;

            case 2:
                foreach (GameObject gosw in button_brown)
                {
                    gosw.SetActive(true);
                }
                break;

            case 3:
                foreach (GameObject gosw in button_green)
                {
                    gosw.SetActive(true);
                }
                break;

            case 4:
                foreach (GameObject gosw in button_purple)
                {
                    gosw.SetActive(true);
                }
                break;

            case 5:
                foreach (GameObject gosw in button_red)
                {
                    gosw.SetActive(true);
                }
                break;

            case 6:
                foreach (GameObject gosw in button_tan)
                {
                    gosw.SetActive(true);
                }
                break;

            case 7:
                foreach (GameObject gosw in button_white)
                {
                    gosw.SetActive(true);
                }
                break;

            default:
                Debug.LogError("Wrong color int");
                break;
        }
    }

    public void TurnOnPositionButtons(int a)
    {
        positions.Add(a);

        switch(a)
        {
            case 1:
                foreach (GameObject gosw in button_SW)
                {
                    gosw.SetActive(true);
                }
                break;

            case 2:
                foreach (GameObject gosw in button_NW)
                {
                    gosw.SetActive(true);
                }
                break;

            case 3:
                foreach (GameObject gosw in button_NE)
                {
                    gosw.SetActive(true);
                }
                break;

            case 4:
                foreach (GameObject gosw in button_SE)
                {
                    gosw.SetActive(true);
                }
                break;
        }
    }

    public void RemoveAI_2()
    {
        if (Ai2.isActivated)
        {
            int op = Ai2.st_PosInt;
            int cl = Ai2.st_ColorInt;

            TurnOnPositionButtons(op);
            TurnOnColorButtons(cl);

            Ai2.isActivated = false;
            PlayerInfoMenu pim = _AiSetup2.GetComponent<PlayerInfoMenu>();
            pim.textPlayerName.SetText("Open");
            pim.textPositon.SetText(" ");
            pim.textTeam.SetText(" ");
            pim.textHero.SetText(" ");
            pim.theColor.color = Color.black;
            CloseAi_2();
        }
    }

    public void RemoveAI_3()
    {
        if (Ai3.isActivated)
        {
            int op = Ai3.st_PosInt;
            int cl = Ai3.st_ColorInt;

            TurnOnPositionButtons(op);
            TurnOnColorButtons(cl);

            Ai3.isActivated = false;
            PlayerInfoMenu pim = _AiSetup3.GetComponent<PlayerInfoMenu>();
            pim.textPlayerName.SetText("Open");
            pim.textPositon.SetText(" ");
            pim.textHero.SetText(" ");
            pim.textTeam.SetText(" ");
            pim.theColor.color = Color.black;
            CloseAi_3();
        }
    }
}
