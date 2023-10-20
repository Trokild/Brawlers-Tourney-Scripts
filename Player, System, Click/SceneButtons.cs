using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SceneButtons : MonoBehaviour
{
    public GameObject MainMenuGo;
    public GameObject PlayGameMenu;
    public GameObject HowToMenu;
    public GameObject OptionsGo;
    public TextMeshProUGUI textHeader;
    public TextMeshProUGUI textHowTo;
    public Image imageHowTo;
    public Animator anim;
    public bool isSceneStartFade = false;
    public bool sceneLoading = false;
    private AudioSource audi;
    public AudioClip click;
    public AudioClip startGame;

    [TextArea(15, 20)]
    public string[] howToTxt;
    public Sprite[] howToPicture;
    private int ordHt = 0;

    private void Start()
    {
        if(isSceneStartFade)
        {
            SceneInn();
        }

        audi = GetComponent<AudioSource>();
    }

    public void TurnOnPlayGame()
    {
        PlayGameMenu.SetActive(true);
        audi.PlayOneShot(click);
        textHeader.SetText("Game Options");
    }

    public void TurnOnHowTo()
    {
        HowToMenu.SetActive(true);
        audi.PlayOneShot(click);
        textHeader.SetText("How To");
    }

    public void TurnOnOptions()
    {
        MainMenuGo.SetActive(false);
        OptionsGo.SetActive(true); 
        audi.PlayOneShot(click);
        textHeader.SetText("Options");
    }

    public void MainMenu()
    {
        MainMenuGo.SetActive(true);
        HowToMenu.SetActive(false);
        OptionsGo.SetActive(false);
        PlayGameMenu.SetActive(false);
        audi.PlayOneShot(click);
        textHeader.SetText("Main Menu");
    }

    public void NextHowTo(bool plus)
    {
        if(plus)
        {
            ordHt++;
        }
        else
        {
            ordHt--;
        }
        
        if (ordHt > 4)
        {
            ordHt = 0;
        }

        if(ordHt < 0)
        {
            ordHt = 4;
        }

        textHowTo.SetText(howToTxt[ordHt]);
        imageHowTo.sprite = howToPicture[ordHt];
        audi.PlayOneShot(click);
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();

        MainSystem.SystemUnitList.Clear();
        MainSystem.PlayerList.Clear();
        MainSystem.Spawner.Clear();
        MainSystem.sys_SelectableUnits.Clear();
        MainSystem.sys_Selected.Clear();
        MainSystem.sort_Selected.Clear();

        SceneManager.LoadScene(scene.name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator CoroutineLoadScene(string s)
    {
        yield return new WaitForSeconds(2f);

        MainSystem.SystemUnitList.Clear();
        MainSystem.PlayerList.Clear();
        MainSystem.Spawner.Clear();
        MainSystem.sys_SelectableUnits.Clear();
        MainSystem.sys_Selected.Clear();
        MainSystem.sort_Selected.Clear();

        SceneManager.LoadScene(s);
    }

    public void SceneInn()
    {
        anim.SetTrigger("FadeInn");
    }

    public void LoadScene(string s)
    {
        if (!sceneLoading)
        {
            sceneLoading = true;
            StartCoroutine(CoroutineLoadScene(s));
            audi.PlayOneShot(startGame);
        }
    }


}
