using UnityEngine.UI;
using UnityEngine;

public class Ui_Base_InfantryShield : Ui_Base
{
    private Infantry_Spawner myInfBase;
    private CurrStat_HumanInf currStatHumanInf;

    [Header("::__:: Infantry ::__::")]
    [Space]
    public GameObject upgradeUi;
    public GameObject WeapUi;
    public GameObject ArmorUi;
    public GameObject BaseUi;
    public Image selectWep;
    public Image selectArm;
    public Image selectShi;

    public RectTransform[] WepBtnRect;
    public RectTransform[] ArmBtnRect;
    public RectTransform[] ShiBtnRect;

    [Header("::__:: Base ::__::")]
    [Space]
    public Image selectBase;
    public Image selectBase2;
    public Image selectBase3;
    public RectTransform[] BaseBtnRect;
    public RectTransform[] Base2BtnRect;
    public RectTransform Base3BtnRect;

    public override void GameStartUi()
    {
        Infantry_Spawner c = myBase as Infantry_Spawner;
        CurrStat_HumanInf cshi = curStat as CurrStat_HumanInf;
        currStatHumanInf = cshi;
        if (c != null)
        {
            myInfBase = c;

            if(currStatHumanInf != null)
            {
                currStatHumanInf.ConnectInfantry(c);
            }

            if (csc != null)
            {
                csc.SetUpInfantry(c, curStat, shopStat);
            }
            else
            {
                csc = GetComponent<CurrentShop_Ctrl>();
                csc.SetUpInfantry(c, curStat, shopStat);
            }

        }
        upgradeUi.SetActive(true);
        myBase.StartProducing();
        curStat.UpdateAllStats();
        Debug.Log("Infanrty_GameStart");
    }

    public void WepUpgUi()
    {
        if (!WeapUi.activeSelf)
        {
            WeapUi.SetActive(true);
            ArmorUi.SetActive(false);
            BaseUi.SetActive(false);
            adui.PlayOneShot(changeMenu);

            if (showBase)
            {
                curStat.ShowTheUnit();
                showBase = false;
            }
        }
        else
        {
            adui.PlayOneShot(changeMenu2);
        }
    }

    public override void ChangeWeapon(int wep)
    {
        canUpgrade = false;
        if (playerRef.gold >= myInfBase.weaponPrice[wep])
        {
            if (selectWep.enabled == false)
            {
                selectWep.enabled = true;
            }

            myInfBase.weaponLevel = wep;
            selectWep.rectTransform.anchoredPosition = WepBtnRect[wep].anchoredPosition;

            if (myInfBase.weaponPrice[wep] > 0)
            {
                playerRef.gold -= myInfBase.weaponPrice[wep];
                myInfBase.weaponPrice[wep] = 0;
                adui.PlayOneShot(wepbuy[wep]);
            }
            else
            {
                adui.PlayOneShot(equipt);
            }

            if (currStatHumanInf != null)
            {
                currStatHumanInf.UpdateCurrentStatWeapon(myInfBase.weaponArsenal[myInfBase.weaponLevel]);
            }
            shopStat.ShowWeaponClickUpdate();

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
            selectArm.rectTransform.anchoredPosition = ArmBtnRect[arm - 1].anchoredPosition;

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

            if (currStatHumanInf != null)
            {
                currStatHumanInf.UpdateCurrentStatArmor(myBase.armorWardrobe[arm]);
            }

            shopStat.ShowArmorClickUpdate();

            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }

    public void ChangeShield(int shi)
    {
        canUpgrade = false;
        if (playerRef.gold >= myInfBase.shieldPrice[shi])
        {
            myInfBase.shieldLevel = shi;
            myInfBase.hasShield = true;
            if (selectShi.enabled == false)
            {
                selectShi.enabled = true;
            }
            selectShi.rectTransform.anchoredPosition = ShiBtnRect[shi].anchoredPosition;

            if (myInfBase.shieldPrice[shi] > 0)
            {
                playerRef.gold -= myInfBase.shieldPrice[shi];
                myInfBase.shieldPrice[shi] = 0;
                adui.PlayOneShot(buyShield);
            }
            else
            {
                adui.PlayOneShot(equipt);
            }

            if (currStatHumanInf != null)
            {
                currStatHumanInf.UpdateCurrentStatShield(myInfBase.shieldStack[shi]);
            }
            shopStat.ShowArmorClickUpdate();

            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }

    public void ArmUpgUi()
    {
        if (!ArmorUi.activeSelf)
        {
            WeapUi.SetActive(false);
            ArmorUi.SetActive(true);
            BaseUi.SetActive(false);
            adui.PlayOneShot(changeMenu);

            if (showBase)
            {
                curStat.ShowTheUnit();
                showBase = false;
            }
        }
        else
        {
            adui.PlayOneShot(changeMenu2);
        }
    }

    #region BaseUpgrades
    public void BaseUpgUi()
    {
        if (!BaseUi.activeSelf)
        {
            WeapUi.SetActive(false);
            ArmorUi.SetActive(false);
            BaseUi.SetActive(true);
            adui.PlayOneShot(changeMenu);

            if (!showBase && currStatHumanInf != null)
            {
                currStatHumanInf.ShowTheBase();
                showBase = true;
            }
        }
        else
        {
            adui.PlayOneShot(changeMenu2);
        }
    }

    public override void MoreUnits(int spw)
    {
        canUpgrade = false;
        if (playerRef.gold >= myBase.spawnPrice[spw])
        {
            if (selectBase.enabled == false)
            {
                selectBase.enabled = true;
            }

            selectBase.rectTransform.anchoredPosition = BaseBtnRect[spw].anchoredPosition;

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

    public override void FasterSpawn(int sp)
    {
        canUpgrade = false;
        if (playerRef.gold >= myBase.timePrice[sp])
        {
            if (selectBase2.enabled == false)
            {
                selectBase2.enabled = true;
            }

            selectBase2.rectTransform.anchoredPosition = Base2BtnRect[sp].anchoredPosition;

            if (myBase.timePrice[sp] > 0)
            {
                playerRef.gold -= myBase.timePrice[sp];
                myBase.timePrice[sp] = 0;
                adui.PlayOneShot(buyMoreUnits);
            }

            switch (sp)
            {
                case 0:
                    myBase.FasterUnits_One();
                    break;

                case 1:
                    myBase.FasterUnits_Two();
                    break;
            }
            canUpgrade = true;
            curStat.UpdateCurretBase();
            shopStat.ShowBaseClickUpdate();
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
            if (selectBase3.enabled == false)
            {
                selectBase3.enabled = true;
            }

            selectBase3.rectTransform.anchoredPosition = Base3BtnRect.anchoredPosition;

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
    #endregion
}
