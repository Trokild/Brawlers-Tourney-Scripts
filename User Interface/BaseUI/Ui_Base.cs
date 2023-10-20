using UnityEngine;
using UnityEngine.UI;

public class Ui_Base : MonoBehaviour
{
    public bool isTest = false;

    public Player playerRef;
    public Unit_Spawner myBase;

    [SerializeField] protected ShopStat shopStat;
    [SerializeField] protected CurrentStats curStat;

    public CurrentShop_Ctrl csc;
    public bool showBase { get; protected set; }
    public bool showUIbase { get; protected set; }

    protected bool canUpgrade = false;
    [Space]
    public GameObject startUi;

    [Space]
    public AudioClip equipt;
    public AudioClip[] wepbuy;
    public AudioClip cantBuy;
    public AudioClip buyArmor;
    public AudioClip buyShield;
    public AudioClip buyMoreUnits;


    public AudioClip changeMenu;
    public AudioClip changeMenu2;
    [SerializeField]protected AudioSource adui;

    private void Start()
    {
        adui = GetComponent<AudioSource>();
        csc = GetComponent<CurrentShop_Ctrl>();
        showUIbase = false;
        showBase = false;
    }

    public void ConnectShopShow(ShopStat ss, ShowUnit su)
    {
        shopStat = ss;
        curStat.ConnectShowUnit(su);
    }

    public virtual void TurnOnUi()
    {
        showUIbase = true; 
        startUi.SetActive(true);

        if (!isTest)
        {
            shopStat.SetCurrStat(curStat);
            curStat.ShowTheUnit();
            curStat.UpdateAllStats();
        }
    }

    public void TurnOffUi()
    {
        startUi.SetActive(false);
        showUIbase = false;
    }

    public virtual void GameStartUi()
    {
        Debug.Log("GameStartUi old");
    }

    public void CloseUi()
    {
        myBase.TurnOnUi();
    }

    public void PlaySound(int i)
    {
        adui.PlayOneShot(wepbuy[i]);
    }

    public virtual void MoreUnits(int spw)
    {
        Debug.Log("MoreUnits old");
    }

    public virtual void FasterSpawn(int sp)
    {
        Debug.Log("FasterSpawn old");
    }

    public void UnlockBtn(Button btn)
    {
        if(canUpgrade)
        {
            btn.interactable = true;
            canUpgrade = false;
        }     
    }

    public virtual void ChangeWeapon(int wep)
    {
        Debug.Log("old ChangeWeapon ui_base");
    }

    public virtual void ChangeArmor(int arm)
    {
        Debug.Log("old ChangeWeapon ui_base");
    }

}