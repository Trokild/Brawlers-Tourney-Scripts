using UnityEngine;

public class CurrentShop_Ctrl : MonoBehaviour
{
    Ui_Base uibase;
    private ShopStat shopStat;
    private CurrentStats curStat;

    private Unit_Spawner myBase;
    private Infantry_Spawner myInfBase;
    private Dualweild_Spawner myDualBase;
    private Archer_Spawner myArcBase;
    private Spearmen_Spawner mySpearBase;
    private Cavalry_Spawner myCavBase;
    private Thrower_Spawner myThrowerBase;

    #region SetUp
    private void Start()
    {
        uibase = GetComponent<Ui_Base>();
    }

    public void SetUpArcher(Archer_Spawner aspw, CurrentStats cs, ShopStat ss)
    {
        myArcBase = aspw;
        SetUpCurShopBase(cs, ss, aspw);
    }

    public void SetUpInfantry(Infantry_Spawner ispw, CurrentStats cs, ShopStat ss)
    {
        myInfBase = ispw;
        SetUpCurShopBase(cs, ss, ispw);
    }

    public void SetUpDualweild(Dualweild_Spawner dwpw, CurrentStats cs, ShopStat ss)
    {
        myDualBase = dwpw;
        SetUpCurShopBase(cs, ss, dwpw);
    }

    public void SetUpThrower(Thrower_Spawner thsp, CurrentStats cs, ShopStat ss)
    {
        myThrowerBase = thsp;
        SetUpCurShopBase(cs, ss, thsp);
    }

    public void SetUpSpearmen(Spearmen_Spawner sspw, CurrentStats cs, ShopStat ss)
    {
        mySpearBase = sspw;
        SetUpCurShopBase(cs, ss, sspw);
    }

    public void SetUpCavalry(Cavalry_Spawner cspw, CurrentStats cs, ShopStat ss)
    {
        myCavBase = cspw;
        SetUpCurShopBase(cs, ss, cspw);
    }

    void SetUpCurShopBase(CurrentStats c, ShopStat s, Unit_Spawner b)
    {
        curStat = c;
        shopStat = s;
        myBase = b;
    }
    #endregion

    public void _HideStat()
    {
        shopStat.HideStat();
    }

    #region Infantry
    public void _ShowStatWep(int w)
    {
        if (myInfBase.weaponPrice[w] <= 0)
        {
            shopStat.ShowWeaponStatUI(myInfBase.weaponArsenal[w], true);
        }
        else
        {
            shopStat.ShowWeaponStatUI(myInfBase.weaponArsenal[w], false);
        }
    }

    public void _ShowStatShield(int s)
    {
        if (myInfBase.shieldPrice[s] <= 0)
        {
            shopStat.ShowShieldStatUI(myInfBase.shieldStack[s], true);
        }
        else
        {
            shopStat.ShowShieldStatUI(myInfBase.shieldStack[s], false);
        }
    }
    #endregion

    #region Cavalry
    public void _ShowStatWepCav(int w)
    {

        if (myCavBase.weaponPrice[w] <= 0)
        {
            shopStat.ShowWeaponStatUI(myCavBase.weaponArsenal[w], true);
        }
        else
        {
            shopStat.ShowWeaponStatUI(myCavBase.weaponArsenal[w], false);
        }
    }

    public void _ShowStatShieldCav()
    {
        if (myCavBase.shieldPrice <= 0)
        {
            shopStat.ShowShieldStatUI(myCavBase.shieldStack, true);
        }
        else
        {
            shopStat.ShowShieldStatUI(myCavBase.shieldStack, false);
        }
    }
    #endregion

    #region Spear
    public void _ShowStatSpear(int w)
    {
        if (mySpearBase.spearPrice[w] <= 0)
        {
            shopStat.ShowSpearStatUI(mySpearBase.spearWalls[w], true);
        }
        else
        {
            shopStat.ShowSpearStatUI(mySpearBase.spearWalls[w], false);
        }
    }

    public void _ShowStatShieldSpear(int s)
    {
        if (mySpearBase.shieldPrice[s] <= 0)
        {
            shopStat.ShowShieldStatUI(mySpearBase.shieldStack[s], true);
        }
        else
        {
            shopStat.ShowShieldStatUI(mySpearBase.shieldStack[s], false);
        }
    }
    #endregion

    #region Archer
    public void _ShowStatArrow(int w)
    {
        if (myArcBase.arrowPrice[w] <= 0)
        {
            shopStat.ShowArrowStatUI(myArcBase.arrowRack[w], true);
        }
        else
        {
            shopStat.ShowArrowStatUI(myArcBase.arrowRack[w], false);
        }
    }

    public void _ShowStatBow(int w)
    {
        if (myArcBase.bowPrice[w] <= 0)
        {
            shopStat.ShowBowStatUI(myArcBase.bowHolder[w], true);
        }
        else
        {
            shopStat.ShowBowStatUI(myArcBase.bowHolder[w], false);
        }
    }
    #endregion

    #region Dualweilder
    public void _ShowStatDualWep(int w)
    {
        if (myDualBase.weaponPrice[w] <= 0)
        {
            shopStat.ShowDualStatUI(myDualBase.weaponArsenal[w], true);
        }
        else
        {
            shopStat.ShowDualStatUI(myDualBase.weaponArsenal[w], false);
        }
    }

    public void _ShowStatCharge(int c)
    {
        if (myDualBase.chargePrice[c] <= 0)
        {
            shopStat.ShowChargerStatUI(myDualBase.chargeStat[c], true);
        }
        else
        {
            shopStat.ShowChargerStatUI(myDualBase.chargeStat[c], false);
        }
    }
    #endregion

    #region Thrower
    public void _ShowStatRock(int r)
    {
        if(myThrowerBase.rockPrice[r] <= 0)
        {
            shopStat.ShowRockStatUI(myThrowerBase.rocks[r], true);
        }
        else
        {
            shopStat.ShowRockStatUI(myThrowerBase.rocks[r], false);
        }
    }

    public void _ShowStatWepThrow(int w)
    {
        if (myThrowerBase.handWepPrice[w] <= 0)
        {
            shopStat.ShowWeaponStatUI(myThrowerBase.handWeapon[w], true);
        }
        else
        {
            shopStat.ShowWeaponStatUI(myThrowerBase.handWeapon[w], false);
        }
    }

    public void _SHowStatGoogles()
    {
        if (myThrowerBase.googlesPrize <= 0)
        {
            shopStat.ShowGooglesStatUI(myThrowerBase.googels, true);
        }
        else
        {
            shopStat.ShowGooglesStatUI(myThrowerBase.googels, false);
        }
    }
    #endregion

    #region MyBase
    public void _ShowStatArm(int a)
    {
        if (myBase.armorPrice[a] <= 0)
        {
            shopStat.ShowArmorStatUI(myBase.armorWardrobe[a], true);
        }
        else
        {
            shopStat.ShowArmorStatUI(myBase.armorWardrobe[a], false);
        }
    }

    public void _ShowMoreUnitsStat(int b)
    {
        if (myBase.spawnPrice[b] <= 0)
        {
            shopStat.Base_ShowMoreUnits(myBase.spawnNum[b], myBase.spawnPrice[b], true);
        }
        else
        {
            shopStat.Base_ShowMoreUnits(myBase.spawnNum[b], myBase.spawnPrice[b], false);
        }
    }

    public void _ShowFasterUnitsStat(int b)
    {

        if (myBase.timePrice[b] <= 0)
        {
            shopStat.Base_ShowFasterUnits(myBase.timeDecrease[b], myBase.timePrice[b], true);
        }
        else
        {
            shopStat.Base_ShowFasterUnits(myBase.timeDecrease[b], myBase.timePrice[b], false);
        }
    }

    public void _ShowStrongerUnitsStat()
    {
        if (myBase.strongPrice <= 0)
        {
            shopStat.Base_ShowStrongerUnits(myBase.strongerInt, myBase.strongPrice, true);
        }
        else
        {
            shopStat.Base_ShowStrongerUnits(myBase.strongerInt, myBase.strongPrice, false);
        }
    }
    #endregion

    public void ShowTextInfo(string info)
    {
        Tooltip.ShowTooltip_Static(info);
    }


    public void HideTextInfo()
    {
        Tooltip.HideTooltip_Static();
    }
}
