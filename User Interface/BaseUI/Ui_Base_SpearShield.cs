using UnityEngine.UI;
using UnityEngine;

public class Ui_Base_SpearShield : Ui_Base
{
    private Spearmen_Spawner mySprBase;
    private CurrStat_Spear cspear;
    [Header("::__:: Spear ::__::")]
    [Space]
    public GameObject UpgradeUi;
    public GameObject WepUi;
    public GameObject ArmorUi;
    public Image selectsprW;
    public Image selectArmor;
    public Image selectShiSpr;
    public RectTransform[] WepBtnRect;
    public RectTransform[] ArmBtnRect;
    public RectTransform[] ShiBtnRect;
    [Space]
    public bool canNoArmor = false;

    public override void GameStartUi()
    {
        Spear_GameStart();
    }

    public void Spear_GameStart()
    {
        Spearmen_Spawner c = myBase as Spearmen_Spawner;
        cspear = curStat as CurrStat_Spear;
        if (c != null)
        {
            mySprBase = c;
            if(cspear != null)
            {
                cspear.ConnetctSpearGarrison(c);
            }

            csc.SetUpSpearmen(c, curStat, shopStat);
        }
        UpgradeUi.SetActive(true);
        cspear.UpdateAllStatsSpear();
        myBase.StartProducing();
        
        Debug.Log("Spearmen_GameStart");
    }

    public void SprWepUpgUi()
    {
        WepUi.SetActive(true);
        ArmorUi.SetActive(false);
    }

    public void SprArmUpgUi()
    {
        WepUi.SetActive(false);
        ArmorUi.SetActive(true);
    }

    public override void TurnOnUi()
    {
        showUIbase = true;
        startUi.SetActive(true);
        shopStat.SetCurrStat(curStat);

        if (cspear != null)
        {
            cspear.ShowTheSpear();
            cspear.UpdateAllStatsSpear();
        }
    }

    public void Change_Spear(int spr)
    {
        canUpgrade = false;
        if (playerRef.gold >= mySprBase.spearPrice[spr])
        {
            if (selectsprW.enabled == false)
            {
                selectsprW.enabled = true;
            }

            mySprBase.weaponLevel = spr;
            selectsprW.rectTransform.anchoredPosition = WepBtnRect[spr].anchoredPosition;

            if (mySprBase.spearPrice[spr] > 0)
            {
                playerRef.gold -= mySprBase.spearPrice[spr];
                mySprBase.spearPrice[spr] = 0;
                adui.PlayOneShot(wepbuy[spr]);
            }
            else
            {
                adui.PlayOneShot(equipt);
            }

            //curStat.UpdateCurrentWeaponStat(wep, myBase.weaponStat[wep]);
            if (cspear != null)
            {
                cspear.UpdateCurrentStatSpear(mySprBase.spearWalls[mySprBase.weaponLevel]);
            }
            shopStat.ShowSpearClickUpdate();

            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }

    public void Change_ArmorSpear(int arm)
    {
        canUpgrade = false;
        if (playerRef.gold >= myBase.armorPrice[arm])
        {
            myBase.armorLevel = arm;
            if (selectArmor.enabled == false)
            {
                selectArmor.enabled = true;
            }

            if (canNoArmor)
            {
                selectArmor.rectTransform.anchoredPosition = ArmBtnRect[arm].anchoredPosition;
            }
            else
            {
                selectArmor.rectTransform.anchoredPosition = ArmBtnRect[arm - 1].anchoredPosition;
            }

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
            if (cspear != null)
            {
                cspear.UpdateCurrentStatArmorSpear(myBase.armorWardrobe[arm]);
            }
            shopStat.ShowArmorClickUpdate();

            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }

    public void Change_ShieldSpear(int shi) // SHOULD WORK FOR INF, SPEAR AND CAV.. but hey man
    {                                       // if it works it works :))
        canUpgrade = false;
        if (playerRef.gold >= mySprBase.shieldPrice[shi])
        {
            mySprBase.shieldLevel = shi;
            mySprBase.hasShield = true;
            if (selectShiSpr.enabled == false)
            {
                selectShiSpr.enabled = true;
            }
            selectShiSpr.rectTransform.anchoredPosition = ShiBtnRect[shi].anchoredPosition;

            if (mySprBase.shieldPrice[shi] > 0)
            {
                playerRef.gold -= mySprBase.shieldPrice[shi];
                mySprBase.shieldPrice[shi] = 0;
                adui.PlayOneShot(buyShield);
            }
            else
            {
                adui.PlayOneShot(equipt);
            }

            if (cspear != null)
            {
                cspear.UpdateCurrentStatShieldSpear(mySprBase.shieldStack[shi]);
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
