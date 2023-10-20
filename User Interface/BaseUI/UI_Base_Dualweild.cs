using UnityEngine;
using UnityEngine.UI;

public class UI_Base_Dualweild : Ui_Base
{
    private Dualweild_Spawner myDualBase;
    private CurrStat_Dual csd;
    public GameObject upgradeUi;

    [Header("::__:: Dual Weild ::__::")]
    [Space]
    public GameObject WepArmUi;

    public Image selectWep;
    public Image selectArm;

    public RectTransform[] WepBtnRect;
    public RectTransform[] ArmBtnRect;

    [Header("::__:: Tribe ::__::")]
    [Space]
    public GameObject TribUi;
    public Image selectCharge;
    public Image selectStronger;
    public RectTransform[] ChaBtnRect;


    [Header("::__:: Base ::__::")]
    [Space]
    public GameObject BaseUi;

    public Image selectMoreUnits;
    public RectTransform[] MorBtnRect;

    public override void GameStartUi()
    {
        Dualweild_Spawner c = myBase as Dualweild_Spawner;
        if (c != null)
        {
            myDualBase = c;
            if(csd != null)
            {
                csd.ConnectDual(c);
            }
            csc.SetUpDualweild(c, curStat, shopStat);
        }

        myBase.StartProducing();
        csd.UpdateAllStatDualWeild();
    }

    public override void ChangeWeapon(int wep)
    {
        canUpgrade = false;
        if (playerRef.gold >= myDualBase.weaponPrice[wep])
        {
            if (selectWep.enabled == false)
            {
                selectWep.enabled = true;
            }

            myDualBase.weaponLevel = wep;
            selectWep.rectTransform.anchoredPosition = WepBtnRect[wep].anchoredPosition;

            if (myDualBase.weaponPrice[wep] > 0)
            {
                playerRef.gold -= myDualBase.weaponPrice[wep];
                myDualBase.weaponPrice[wep] = 0;
                adui.PlayOneShot(wepbuy[wep]);
            }
            else
            {
                adui.PlayOneShot(equipt);
            }

            if (csd != null)
            {
                csd.UpdateCurrentDualWeapon(myDualBase.weaponArsenal[myDualBase.weaponLevel]);
            }

            shopStat.ShowWeaponClickUpdate();

            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }

    public void ChangeCharge(int cha)
    {
        canUpgrade = false;
        if (playerRef.gold >= myDualBase.chargePrice[cha])
        {
            if (selectCharge.enabled == false)
            {
                selectCharge.enabled = true;
            }
            myDualBase.canCharge = true;
            myDualBase.chargeLvl = cha;
            selectCharge.rectTransform.anchoredPosition = ChaBtnRect[cha - 1].anchoredPosition;

            if (myDualBase.chargePrice[cha] > 0)
            {
                playerRef.gold -= myDualBase.chargePrice[cha];
                myDualBase.chargePrice[cha] = 0;
                adui.PlayOneShot(wepbuy[cha]);
            }
            else
            {
                adui.PlayOneShot(equipt);
            }

            if (csd != null)
            {
                csd.UpdateCurrentCharge(myDualBase.chargeStat[myDualBase.chargeLvl]);
            }
            shopStat.ShowChargeClickUpdate();

            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }

    public override void ChangeArmor(int arm)
    {
        canUpgrade = false;
        if (playerRef.gold >= myBase.armorPrice[arm])
        {
            myBase.armorLevel = arm;
            if (selectArm.enabled == false)
            {
                selectArm.enabled = true;
            }
            selectArm.rectTransform.anchoredPosition = ArmBtnRect[arm].anchoredPosition;

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

            if (csd != null)
            {
                csd.UpdateCurrentDualArmor(myBase.armorWardrobe[arm]);
            }
            shopStat.ShowArmorClickUpdate();

            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }

    public override void MoreUnits(int spw)
    {
        canUpgrade = false;
        if (playerRef.gold >= myBase.spawnPrice[spw])
        {
            if (selectMoreUnits.enabled == false)
            {
                selectMoreUnits.enabled = true;
            }

            selectMoreUnits.rectTransform.anchoredPosition = MorBtnRect[spw].anchoredPosition;

            if (myBase.spawnPrice[spw] > 0)
            {
                playerRef.gold -= myBase.spawnPrice[spw];
                myBase.spawnPrice[spw] = 0;

                adui.PlayOneShot(buyMoreUnits);
            }

            switch (spw)
            {
                case 0:
                    myBase.MoreUnits_One();
                    break;

                case 1:
                    myBase.MoreUnits_Two();
                    break;

                case 2:
                    myBase.MoreUnits_Three();
                    break;
            }
            curStat.UpdateCurretBase();
            shopStat.ShowBaseClickUpdate();
            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }

    public void StrongerUnits()
    {
        canUpgrade = false;
        if (playerRef.gold >= myBase.strongPrice)
        {
            if (selectStronger.enabled == false)
            {
                selectStronger.enabled = true;
            }

            if (myBase.strongPrice > 0)
            {
                playerRef.gold -= myBase.strongPrice;
                myBase.strongPrice = 0;
                adui.PlayOneShot(buyMoreUnits);
            }
            myBase.Stronger_Units();
            curStat.UpdateCurretBase();
            shopStat.ShowBaseClickUpdate();
            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }

    public void Swap_Gear()
    {
        if (!WepArmUi.activeSelf)
        {
            WepArmUi.SetActive(true);
            TribUi.SetActive(false);
            BaseUi.SetActive(false);
            adui.PlayOneShot(changeMenu);

            if (showBase && csd != null)
            {
                csd.ShowTheDual();
                showBase = false;
            }
        }
        else
        {
            adui.PlayOneShot(changeMenu2);
        }
    }

    public void Swap_Trib()
    {
        if (!TribUi.activeSelf)
        {
            WepArmUi.SetActive(false);
            TribUi.SetActive(true);
            BaseUi.SetActive(false);
            adui.PlayOneShot(changeMenu);

            if (showBase && csd != null)
            {
                csd.ShowTheDual();
                showBase = false;
            }
        }
        else
        {
            adui.PlayOneShot(changeMenu2);
        }
    }

    public void Swap_Base()
    {
        if (!BaseUi.activeSelf)
        {
            WepArmUi.SetActive(false);
            TribUi.SetActive(false);
            BaseUi.SetActive(true);
            adui.PlayOneShot(changeMenu);

            if (!showBase)
            {
                curStat.ShowTheBase();
                showBase = true;
            }
        }
        else
        {
            adui.PlayOneShot(changeMenu2);
        }
    }

    public override void TurnOnUi()
    {
        showUIbase = true;
        startUi.SetActive(true);
        shopStat.SetCurrStat(curStat);

        if (csd != null)
        {
            csd.ShowTheDual();
            csd.UpdateAllStatDualWeild();
        }
    }
}
