using UnityEngine;
using UnityEngine.UI;

public class Ui_Base_CavHuman : Ui_Base
{
    private Cavalry_Spawner myCavBase;
    private CurrStat_Cavalry currCav;
    [Header("::__:: Cavalry ::__::")]
    [Space]
    public GameObject UpgradeUi;
    public GameObject WepUi;
    public GameObject ArmUi;
    public Image selectcavW;
    public Image selectcavArm;
    public Image selectcavShi;
    public Image selectcavHor;

    public RectTransform[] WepBtnRect;
    public RectTransform[] ArmBtnRect;
    public RectTransform ShiBtnRect;
    public RectTransform[] HorBtnRect;

    public override void GameStartUi()
    {
        Cav_GameStart();
    }

    public void Cav_GameStart()
    {
        Cavalry_Spawner c = myBase as Cavalry_Spawner;
        currCav = curStat as CurrStat_Cavalry;
        if (c != null)
        {
            myCavBase = c;
            if(currCav != null)
            {
                currCav.ConnetctCavalryStable(c);
            }

            csc.SetUpCavalry(c, curStat, shopStat);

            UpgradeUi.SetActive(true);

            myBase.StartProducing();
            currCav.UpdateAllStats_Cavalry();
            Debug.Log("Cavalry_GameStart");
        }
    }

    public override void TurnOnUi()
    {
        showUIbase = true;
        startUi.SetActive(true);
        shopStat.SetCurrStat(curStat);

        if (currCav != null)
        {
            currCav.ShowTheCav();
            currCav.UpdateAllStats_Cavalry();
        }
    }

    public void Change_WeaponCav(int wep)
    {
        canUpgrade = false;
        if (playerRef.gold >= myCavBase.weaponPrice[wep])
        {
            if (selectcavW.enabled == false)
            {
                selectcavW.enabled = true;
            }

            myCavBase.weaponLevel = wep;
            selectcavW.rectTransform.anchoredPosition = WepBtnRect[wep].anchoredPosition;

            if (myCavBase.weaponPrice[wep] > 0)
            {
                playerRef.gold -= myCavBase.weaponPrice[wep];
                myCavBase.weaponPrice[wep] = 0;
                adui.PlayOneShot(wepbuy[wep]);
            }
            else
            {
                adui.PlayOneShot(equipt);
            }
            if (currCav != null)
            {
                currCav.UpdateCurrentStatCavalry(myCavBase.weaponArsenal[myCavBase.weaponLevel]);
            }
            //shopStat.ShowWeaponClickUpdate();
            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }

    public void Change_ArmorCav(int arm)
    {
        canUpgrade = false;
        if (playerRef.gold >= myBase.armorPrice[arm])
        {
            myBase.armorLevel = arm;
            if (selectcavArm.enabled == false)
            {
                selectcavArm.enabled = true;
            }
            selectcavArm.rectTransform.anchoredPosition = ArmBtnRect[arm - 1].anchoredPosition;
            playerRef.gold -= myBase.armorPrice[arm];
            myBase.armorPrice[arm] = 0;
            canUpgrade = true;
        }

        canUpgrade = false;
        if (playerRef.gold >= myBase.armorPrice[arm])
        {
            myBase.armorLevel = arm;
            if (selectcavArm.enabled == false)
            {
                selectcavArm.enabled = true;
            }
            selectcavArm.rectTransform.anchoredPosition = ArmBtnRect[arm - 1].anchoredPosition;

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
            if (currCav != null)
            {
                currCav.UpdateCurrentStatArmorCavalry(myCavBase.armorWardrobe[arm]);
            }

            shopStat.ShowArmorClickUpdate();

            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }

    public void Change_ShieldCav() // SHOULD WORK FOR INF, SPEAR AND CAV
    {
        canUpgrade = false;
        if (playerRef.gold >= myCavBase.shieldPrice)
        {
            myCavBase.hasShield = true;
            if (selectcavShi.enabled == false)
            {
                selectcavShi.enabled = true;
            }
            selectcavShi.rectTransform.anchoredPosition = ShiBtnRect.anchoredPosition;

            if (myCavBase.shieldPrice > 0)
            {
                playerRef.gold -= myCavBase.shieldPrice;
                myCavBase.shieldPrice = 0;
                adui.PlayOneShot(buyShield);
            }
            else
            {
                adui.PlayOneShot(equipt);
            }
            if (currCav != null)
            {
                currCav.UpdateCurrentStatShieldCavalry(myCavBase.shieldStack);
            }

            shopStat.ShowArmorClickUpdate();

            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }

    public void Change_Horse(int hor)
    {
        canUpgrade = false;
        if (playerRef.gold >= myCavBase.cavPrice[hor])
        {
            myCavBase.cavLvl = hor;
            if (selectcavHor.enabled == false)
            {
                selectcavHor.enabled = true;
            }
            selectcavHor.rectTransform.anchoredPosition = HorBtnRect[hor - 1].anchoredPosition;
            playerRef.gold -= myCavBase.cavPrice[hor];
            myCavBase.cavPrice[hor] = 0;
            if (currCav != null)
            {
                currCav.UpdateCurrentStatHorse(myCavBase.horses[hor]);
            }

            shopStat.ShowHorseClickUpdate();
            canUpgrade = true;
        }
    }

    public void Cav_WepUpgUi()
    {
        WepUi.SetActive(true);
        ArmUi.SetActive(false);
    }

    public void Cav_ArmUpgUi()
    {
        WepUi.SetActive(false);
        ArmUi.SetActive(true);
    }

    public void Cav_HorUpgUi()
    {
        WepUi.SetActive(false);
        ArmUi.SetActive(false);
    }
}
