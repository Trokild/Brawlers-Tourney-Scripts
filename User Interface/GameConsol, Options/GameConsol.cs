using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameConsol : MonoBehaviour
{
    public TextMeshProUGUI txtConsol;
    [Space]
    public bool isCDTxt = false;
    public float CDtxt;
    public float _cdtxt;
    [Space]
    public GameObject youLostGO;
    public GameObject youWonGO;
    public GameObject BackgroundGO;
    public GameObject MenuGO;
    public GameObject OptionsGO;
    public Animator anim;
    public Animator animBlackness;
    public AudioSource audi;
    public AudioClip winSound;
    public AudioClip looseSound;
    public AudioClip click;
    public AudioClip warning;
    public bool isSceneStartFade = false;
    public bool isMenu = false;
    public bool isOptions = false;
    [SerializeField] private Ui_BaseManager uibm;
    [SerializeField] private UserInterface_Units uiu;

    private void Start()
    {
        if (isSceneStartFade)
        {
            SceneInn();
        }
        if(uibm == null)
        {
            uibm = GameObject.FindGameObjectWithTag("BaseUiManager").GetComponent<Ui_BaseManager>();
        }
        _cdtxt = CDtxt;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffUiMenus();
        }

        if(isCDTxt)
        {
            if(_cdtxt > 0)
            {
                _cdtxt -= Time.deltaTime;
            }
            else
            {
                isCDTxt = false;
                txtConsol.alpha = 0;
                _cdtxt = CDtxt;
                ClearConsol();
            }            
        }
    }

    public void OffUiMenus()
    {
        bool isBaseOn;
        isBaseOn = uibm.isBaseUiOn();
        //Debug.Log(isBaseOn);
        if (isBaseOn)
        {
            uibm.TurnOffBaseUI();
        }
        else
        {
            if (MainSystem.sys_Selected.Count > 0)
            {
                ClearSelection();
            }
            else
            {
                MenuOn();
            }
        }
    }

    public void OffUi()
    {
        bool isBaseOn;
        isBaseOn = uibm.isBaseUiOn();
        //Debug.Log(isBaseOn);
        if (isBaseOn)
        {
            uibm.TurnOffBaseUI();
        }
    }

    void ClearSelection()
    {
        foreach (Unit un in MainSystem.sys_Selected)
        {
            if (un != null)
            {
                un.ClickMe();
            }
        }
        MainSystem.sort_Selected.Clear();
        MainSystem.sys_Selected.Clear();
        uiu.ClearUnitToUI();
    }

    public void MenuOn()
    {
        if(!isMenu)
        {
            MenuGO.SetActive(true);
            if(isOptions)
            {
                OptionsGO.SetActive(false);
                isOptions = false;
            }
            isMenu = true;
        }
        else
        {
            MenuGO.SetActive(false);
            isMenu = false;
        }
        audi.PlayOneShot(click);
    }

    public void OptionsOn()
    {
        if (!isOptions)
        {
            OptionsGO.SetActive(true);
            if(isMenu)
            {
                MenuGO.SetActive(false);
                isMenu = false;
            }
            isOptions = true;
        }
        else
        {
            OptionsGO.SetActive(false);
            isOptions = false;
        }
        audi.PlayOneShot(click);
    }

    public void ResumeFromMenu()
    {
        OptionsGO.SetActive(false);
        isOptions = false;
        MenuGO.SetActive(false);
        isMenu = false;
        audi.PlayOneShot(click);
    }

    #region Consol Text
    void ClearConsol()
    {
        if (txtConsol.text.Length > 1)
        {
            txtConsol.text = null;
        }
    }

    public void SetDefeatedTxt(string plyr, int col)
    {
        string color = ColorString(col);
        string oldTxt = txtConsol.text;

        if (oldTxt.Length > 1)
        {
            txtConsol.SetText(oldTxt + "\n" + "<color=" + color + ">" + plyr + " </color> has been defeated");
        }
        else
        {
            txtConsol.SetText("<color=" + color + ">" + plyr + " </color> has been defeated");
        }
    }

    public void SetLocalDefeatedTxt(int idPl)
    {
        string oldTxt = txtConsol.text;

        if (oldTxt.Length > 1)
        {
            txtConsol.SetText(oldTxt + "\n" +  "You has been defeated");
        }
        else
        {
            txtConsol.SetText("You has been defeated");
        }
    }

    public void SetBaseDestroyed(string plyr, int col)
    {
        string color = ColorString(col);
        string oldTxt = txtConsol.text;

        if (oldTxt.Length > 1)
        {
            txtConsol.SetText(oldTxt + "\n" + "<color=" + color + ">" + plyr + " </color> base was destroyed");
        }
        else
        {
            txtConsol.SetText("<color=" + color + ">" + plyr + " </color> base was destroyed");
        }
    }

    public void SetHeroDeath(string plyName, int col)
    {
        isCDTxt = true;
        _cdtxt = CDtxt;
        string color = ColorString(col);
        txtConsol.alpha = 255;
        string oldTxt = txtConsol.text;
        if (oldTxt != null && oldTxt.Length > 1)
        {
            txtConsol.SetText(oldTxt + "\n" + "<color=" + color + ">" + plyName + " </color> Hero has been slain");
        }
        else
        {
            txtConsol.SetText("<color=" + color + ">" + plyName + " </color> Hero has been slain");
        }
    }

    string ColorString(int colInt)
    {
        string color;
        switch (colInt)
        {
            case 0:
                color = "#494747";
                break;
            case 1:
                color = "#1643E0";
                break;
            case 2:
                color = "#9A5619";
                break;
            case 3:
                color = "green";
                break;
            case 4:
                color = "#AC3EC5";
                break;
            case 5:
                color = "red";
                break;
            case 6:
                color = "yellow";
                break;
            case 7:
                color = "white";
                break;
            default: color = "white"; break;
        }
        return color;
    }

    public void LocalPlayerDead()
    {
        anim.SetTrigger("Loose");
        Invoke("SoundPlayerDead", 1f);
    }

    public void LocalPlayerWon()
    {
        anim.SetTrigger("Win");
        Invoke("SoundPlayerWon", 1f);
    }

    public void TeamWon(int t) //Check if there is solo or team
    {
        txtConsol.SetText("Team " + t.ToString() + " has won the game");
    }

    public void CreepTheConsol()
    {
        Invoke("CTC", 60f);
    }

    void CTC()
    {
        txtConsol.SetText("... Still playing huh?");
    }
    #endregion

    public void SoundPlayerDead()
    {
        audi.PlayOneShot(looseSound);
    }

    public void SoundPlayerWon()
    {
        audi.PlayOneShot(winSound);
    }

    public void SoundBaseAttacked()
    {
        audi.PlayOneShot(warning);
    }

    public void SceneInn()
    {
        animBlackness.SetTrigger("FadeInn");
    }

    public void ReloadScene()
    {
        StartCoroutine(RS());
        audi.PlayOneShot(click);
        animBlackness.SetTrigger("FadeOut");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LMM());
        audi.PlayOneShot(click);
        animBlackness.SetTrigger("FadeOut");
    }

    private IEnumerator RS()
    {
        yield return new WaitForSeconds(2f);

        Scene scene = SceneManager.GetActiveScene();
        MainSystem.NewGameMainSystem();
        SceneManager.LoadScene(scene.name);
    }

    private IEnumerator LMM()
    {
        yield return new WaitForSeconds(2f);

        MainSystem.NewGameMainSystem();
        PlayersSceneStart pss = GameObject.FindGameObjectWithTag("Contenders").GetComponent<PlayersSceneStart>();
        pss.sceneLoading = false;
        for (int i = 0; i < pss.pl.Length; i++)
        {
            if (i > 1)
            {
                pss.pl[i].isActivated = false;
            }
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void WatchGame()
    {
        anim.enabled = false;
        BackgroundGO.SetActive(false);
        youWonGO.SetActive(false);
        youLostGO.SetActive(false);
        audi.PlayOneShot(click); 

    }

}
