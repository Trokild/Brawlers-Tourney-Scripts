using UnityEngine.UI;
using UnityEngine;

public class Ui_Base_Thrower : Ui_Base
{
    private Thrower_Spawner myThrowSpawn;
    private CurrStat_Throw cst;
    [Header("::__:: Throw & Hand Weapon::__::")]
    [Space]
    public GameObject UpgradeUi;
    [Space]
    public GameObject WeaponsUi;
    public Image selectdRock;
    public Image selectdWep;
    public RectTransform[] RockBtnRect;
    public RectTransform[] WepBtnRect;
    [Space]
    [Header("::__:: Armor ::__::")]
    public GameObject ArmorUi;
    public Image selectdArmor;
    public Image selectdGoogles;
    public RectTransform[] ArmorBtnRect;

    public override void GameStartUi()
    {
        Thrower_Spawner t = myBase as Thrower_Spawner;
        if(t != null)
        {
            myThrowSpawn = t;
            if(cst != null)
            {
                cst.ConnectThrowerBoulder(t);
            }

            if(csc != null)
            {
                csc.SetUpThrower(t, curStat, shopStat);
            }
            else
            {
                csc = GetComponent<CurrentShop_Ctrl>();
                if(csc != null)
                {
                    csc.SetUpThrower(t, curStat, shopStat);
                }
                else
                {
                    Debug.LogError("Cant find CurrentShop_Ctrl", gameObject);
                }
            }
        }
        UpgradeUi.SetActive(true);
        myBase.StartProducing();
        cst.UpdateAllStatsThrower();
    }

    public void WepUpgUi()
    {
        WeaponsUi.SetActive(true);
        ArmorUi.SetActive(false);
    }

    public void ArmUpgUi()
    {
        WeaponsUi.SetActive(false);
        ArmorUi.SetActive(true);
    }

    public override void TurnOnUi()
    {
        showUIbase = true;
        startUi.SetActive(true);
        shopStat.SetCurrStat(curStat);

        if (cst != null)
        {
            cst.ShowTheThrower();
            cst.UpdateAllStatsThrower();
        }
    }

    public void Change_Stone(int rock)
    {
        canUpgrade = false;
        if (playerRef.gold >= myThrowSpawn.rockPrice[rock])
        {
            if (selectdRock.enabled == false)
            {
                selectdRock.enabled = true;
            }

            myThrowSpawn.rockLevel = rock;
            selectdRock.rectTransform.anchoredPosition = RockBtnRect[rock].anchoredPosition;

            if (myThrowSpawn.rockPrice[rock] > 0)
            {
                playerRef.gold -= myThrowSpawn.rockPrice[rock];
                myThrowSpawn.rockPrice[rock] = 0;
                //adui.PlayOneShot(wepbuy[spr]);
            }
            else
            {
                adui.PlayOneShot(equipt);
            }

            if (cst != null)
            {
                cst.UpdateCurrentRock(myThrowSpawn.rocks[rock]);
            }
            shopStat.ShowRockClickUpdate();

            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }

    public override void ChangeWeapon(int wep)
    {
        canUpgrade = false;
        if (playerRef.gold >= myThrowSpawn.handWepPrice[wep])
        {
            if (selectdWep.enabled == false)
            {
                selectdWep.enabled = true;
            }

            if (!myThrowSpawn.hasMeleeWeapon)
            {
                myThrowSpawn.hasMeleeWeapon = true;
            }

            myThrowSpawn.rockLevel = wep;
            selectdWep.rectTransform.anchoredPosition = WepBtnRect[wep].anchoredPosition;

            if (myThrowSpawn.handWepPrice[wep] > 0)
            {
                playerRef.gold -= myThrowSpawn.handWepPrice[wep];
                myThrowSpawn.handWepPrice[wep] = 0;
                //adui.PlayOneShot(wepbuy[spr]);
            }
            else
            {
                adui.PlayOneShot(equipt);
            }

            if (cst != null)
            {
                cst.UpdateCurrentHandWeap(myThrowSpawn.handWeapon[wep]);
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
            if (selectdArmor.enabled == false)
            {
                selectdArmor.enabled = true;
            }
            selectdArmor.rectTransform.anchoredPosition = ArmorBtnRect[arm].anchoredPosition;

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

            if (cst != null)
            {
                cst.UpdateCurrentThrowArmor(myBase.armorWardrobe[arm]);
            }

            shopStat.ShowArmorClickUpdate();

            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }

    public void ChangeGoogles()
    {
        canUpgrade = false;
        if (playerRef.gold >= myThrowSpawn.googlesPrize)
        {
            myThrowSpawn.hasGoogels = true;
            if (selectdGoogles.enabled == false)
            {
                selectdGoogles.enabled = true;
            }

            if (myThrowSpawn.googlesPrize > 0)
            {
                playerRef.gold -= myThrowSpawn.googlesPrize;
                myThrowSpawn.googlesPrize = 0;
                adui.PlayOneShot(buyArmor);
            }
            else
            {
                adui.PlayOneShot(equipt);
            }

            if (cst != null)
            {
                cst.UpdateCurrentGoogles(myThrowSpawn.googels);
            }
            //shopStat.ShowArmorClickUpdate();

            canUpgrade = true;
        }
        else
        {
            adui.PlayOneShot(cantBuy);
        }
    }
}
