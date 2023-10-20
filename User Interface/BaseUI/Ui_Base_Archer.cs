using UnityEngine.UI;
using UnityEngine;

public class Ui_Base_Archer : Ui_Base
{
    private Archer_Spawner myArcBase;
    private CurrStat_Archer csa;
    [Header("::__:: Archer ::__::")]
    [Space]
    public GameObject UpgradeUi;
    public GameObject ArrowUi;
    public GameObject BowUi;
    public GameObject ArmUi;
    public Image selectBow;
    public Image selectAro;
    public Image selectArcArmor;
    public RectTransform[] ArrowBtnRect;
    public RectTransform[] ArmorBtnRect;
    public RectTransform[] BowBtnRect;

    public override void GameStartUi()
    {
        ArcherGameStart();
    }

    void ArcherGameStart()
    {
        csa = curStat as CurrStat_Archer;
        Archer_Spawner c = myBase as Archer_Spawner;
        
        if (c != null)
        {
            myArcBase = c;
            if(csa != null)
            {
                csa.ConnectArchery(c);
            }

            if(csc != null)
            {
                csc.SetUpArcher(c, curStat, shopStat);
            }
            else
            {
                csc = GetComponent<CurrentShop_Ctrl>();
                csc.SetUpArcher(c, curStat, shopStat);
            }

            UpgradeUi.SetActive(true);
            myBase.StartProducing();
            csa.UpdateAllStats_Archer();
            Debug.Log("Archer_GameStart");
        }
    }

    public override void TurnOnUi()
    {
        showUIbase = true;
        startUi.SetActive(true);
        shopStat.SetCurrStat(curStat);

        if (csa != null)
        {
            csa.ShowTheArc();
            csa.UpdateAllStats_Archer();
        }
    }

    public void AroUpgUi()
    {
        ArrowUi.SetActive(true);
        BowUi.SetActive(false);
        ArmUi.SetActive(false);
    }

    public void BowUpgUi()
    {
        ArrowUi.SetActive(false);
        BowUi.SetActive(true);
        ArmUi.SetActive(false);
    }

    public void ArmUpgUi()
    {
        ArrowUi.SetActive(false);
        BowUi.SetActive(false);
        ArmUi.SetActive(true);
    }

    public void Change_Arrow(int arr)
    {
        canUpgrade = false;

        if (playerRef.gold >= myArcBase.arrowPrice[arr])
        {
            myArcBase.arrowLvl = arr;

            selectAro.rectTransform.anchoredPosition = ArrowBtnRect[arr].anchoredPosition;

            playerRef.gold -= myArcBase.arrowPrice[arr];
            myArcBase.arrowPrice[arr] = 0;

            if (csa != null)
            {
                csa.UpdateCurrentStatArrow(myArcBase.arrowRack[myArcBase.arrowLvl]);
            }

            shopStat.ShowArrowClickUpdate();

            canUpgrade = true;
        }
    }

    public void Change_Bow(int bow)
    {
        canUpgrade = false;
        if (playerRef.gold >= myArcBase.bowPrice[bow])
        {
            myArcBase.bowLvl = bow;

            selectBow.rectTransform.anchoredPosition = BowBtnRect[bow].anchoredPosition;
            playerRef.gold -= myArcBase.bowPrice[bow];
            myArcBase.bowPrice[bow] = 0;

            if (csa != null)
            {
                csa.UppdateCurrentStatBow(myArcBase.bowHolder[myArcBase.bowLvl]);
            }

            shopStat.ShowBowClickUpdate();

            canUpgrade = true;
        }
    }

    public void Change_ArmorArcher(int arm)
    {
        canUpgrade = false;
        if (playerRef.gold >= myBase.armorPrice[arm])
        {
            myBase.armorLevel = arm;
            if (selectArcArmor.enabled == false)
            {
                selectArcArmor.enabled = true;
            }
            selectArcArmor.rectTransform.anchoredPosition = ArmorBtnRect[arm - 1].anchoredPosition;

            if (myBase.armorPrice[arm] > 0)
            {
                playerRef.gold -= myBase.armorPrice[arm];
                myBase.armorPrice[arm] = 0;
                adui.PlayOneShot(buyArmor);
            }
            else
            {
                adui.PlayOneShot(equipt);
            }

            if (csa != null)
            {
                csa.UpdateCurrentStatArmorArcher(myBase.armorWardrobe[arm]);
            }

            shopStat.ShowArmorClickUpdate();

            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }
}
