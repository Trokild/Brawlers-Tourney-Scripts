using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Ui_BaseManager : MonoBehaviour
{
    public Player playerBaseMan;
    public Ui_Base[] myBases;
    [SerializeField] private ShopStat _shopStat;
    [SerializeField] private ShowUnit _showUnit;

    public TextMeshProUGUI Gold_txt;
    private int myGoldInt;
    private Animator anim;
    [SerializeField] private GameObject[] BaseUi_prefab;

    private void Start()
    {
        anim = Gold_txt.gameObject.GetComponent<Animator>();
    }

    public void SetGold(int g)
    {
        myGoldInt = g;
    }

    private void Update()
    {
        if(playerBaseMan != null && Gold_txt != null)
        {
            Gold_txt.text = playerBaseMan.gold.ToString();

            if (myGoldInt != playerBaseMan.gold)
            {
                PopAnimGold();
                myGoldInt = playerBaseMan.gold;
            }
        }
    }

    public void PopAnimGold()
    {
        anim.SetTrigger("Pop");
        //Debug.Log("GoldAnim");
    }

    public bool isBaseUiOn()
    {
        for (int i = 0; i < myBases.Length; i++)
        {
            if(myBases[i] != null)
            {
                if (myBases[i].showUIbase)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void TurnOffBaseUI()
    {
        for (int i = 0; i < myBases.Length; i++)
        {
            if (myBases[i] != null)
            {
                Debug.Log(myBases[i].showUIbase);
                if (myBases[i].showUIbase)
                {
                    myBases[i].myBase.TurnOnUi();
                    myBases[i].csc._HideStat();
                }
            }
        }
    }

    public void NewCanvasBase(int b, int t)
    {
        GameObject newbaseui = Instantiate(BaseUi_prefab[t]);
        newbaseui.transform.SetParent(this.transform);
        RectTransform rt = newbaseui.GetComponent<RectTransform>();
        rt.anchoredPosition = Vector2.zero;
        //Debug.Log("TestNewCanvasBase", newbaseui);
        myBases[b] = newbaseui.GetComponent<Ui_Base>();
        myBases[b].ConnectShopShow(_shopStat, _showUnit);
    }
}
