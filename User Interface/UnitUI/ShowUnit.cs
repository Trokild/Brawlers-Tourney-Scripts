using UnityEngine;

public class ShowUnit : MonoBehaviour
{
    private int teamColorShop; //set shop color for units
    public bool hasAssignedCol { get; private set; } = false;
    [Header("Human")]
    [SerializeField] private Unit_Appearance Infantry;
    [SerializeField] private Unit_Appearance Archer;
    [SerializeField] private Unit_Appearance Spearman;
    [SerializeField] private Apperance_Cav Cavalry;
    [SerializeField] private GameObject Unit;
    [SerializeField] private GameObject UnitAr;
    [SerializeField] private GameObject UnitSp;
    [SerializeField] private GameObject UnitCav;
    [SerializeField] private GameObject MyBase;
    [Header("Urk")]
    [SerializeField] private GameObject MyBaseDual;

    [SerializeField] private Apperance_DualWeild OrcDualweild;
    [SerializeField] private Unit_Appearance OrcSpear;
    [SerializeField] private Apperanc_Throw OrcThrower;

    [SerializeField] private GameObject UnitDual;
    [SerializeField] private GameObject UnitOrcSpear;
    [SerializeField] private GameObject UnitThrw;

    public void UiShopSetColorInt_Orc(int col)
    {
        if (hasAssignedCol)
        {
            return;
        }
        teamColorShop = col;
        OrcDualweild.ChangeUnitColorNoDot(col);
        OrcSpear.ChangeUnitColorNoDot(col);
        OrcThrower.ChangeUnitColorNoDot(col);
        hasAssignedCol = true;
    }

    public void UiShopSetColorInt_Human(int col)
    {
        if (hasAssignedCol)
        {
            return;
        }
        teamColorShop = col;
        Infantry.ChangeUnitColorNoDot(col);
        Archer.ChangeUnitColorNoDot(col);
        Spearman.ChangeUnitColorNoDot(col);
        Cavalry.ChangeUnitColorNoDot(col);
        hasAssignedCol = true;
    }

    #region Human
    public void InfantryWep(int w)
    {
        Infantry.WeaponInt(w);
    }

    public void SpearWep(int w)
    {
        Spearman.WeaponInt(w);
    }

    public void InfantryArm(int a)
    {
        Infantry.ArmorInt(a);
    }

    public void SpearArm(int a)
    {
        Spearman.ArmorInt(a);
    }

    public void InfantryArm(int a, int s)
    {
        Infantry.ArmorShield(a,s, false);
    }

    public void SpearArm(int a, int s)
    {
        Spearman.ArmorShield(a, s, false);
    }

    public void ArcherWep(int w) //bow
    {
        Archer.WeaponInt(w);
    }

    public void ArcherShi(int w) //arrow
    {
        Archer.ShieldInt(w);
    }

    public void ArcherArm(int a)
    {
        Archer.ArmorInt(a);
    }

    public void ArcherArmShi(int a, int s)
    {
        Archer.ArmorShield(a, s, false);
    }

    public void CavalryWep(int we)
    {
        Cavalry.WeaponInt(we);
    }

    public void CavalryShield()
    {
        Cavalry.ShieldInt(0);
    }

    public void CavalryArmor(int arm)
    {
        Cavalry.ArmorInt(arm);
    }

    public void CavalryHorse(int hrs)
    {
        Cavalry.ChangeHorseMat(hrs);
    }

    public void UnitShow(int w, int a, int s)
    {
        Unit.SetActive(true);
        MyBase.SetActive(false);
        UnitAr.SetActive(false);
        UnitSp.SetActive(false);
        UnitCav.SetActive(false);
        InfantryAllShow(w, a, s);
    }

    public void UnitShowStart(int w, int a) //no shield
    {
        Unit.SetActive(true);
        MyBase.SetActive(false);
        UnitAr.SetActive(false);
        UnitSp.SetActive(false);
        UnitCav.SetActive(false);
        Infantry.WeaponInt(w);
        Infantry.ArmorInt(a);
    }

    public void CavalryShow(int wep, int arm, int horse)
    {
        Unit.SetActive(false);
        MyBase.SetActive(false);
        UnitAr.SetActive(false);
        UnitSp.SetActive(false);
        UnitCav.SetActive(true);

        Cavalry.ArmorInt(arm);
        Cavalry.ChangeHorseMat(horse);
        Cavalry.WeaponInt(wep);
    }

    public void CavalryShowStart(int wep, int arm, int horse)
    {
        Unit.SetActive(false);
        MyBase.SetActive(false);
        UnitAr.SetActive(false);
        UnitSp.SetActive(false);
        UnitCav.SetActive(true);

        CavalryAllShow(wep, arm, horse);
    }

    public void SpearShow(int w, int a, int s)
    {
        Unit.SetActive(false);
        MyBase.SetActive(false);
        UnitAr.SetActive(false);
        UnitSp.SetActive(true);
        UnitCav.SetActive(false);
        SpearAllShow(w, a, s);
    }

    public void SpearShowStart(int w, int a) //no shield
    {
        Unit.SetActive(false);
        MyBase.SetActive(false);
        UnitAr.SetActive(false);
        UnitSp.SetActive(true);
        UnitCav.SetActive(false);
        Spearman.WeaponInt(w);
        Spearman.ArmorInt(a);
    }

    public void UnitShowArch(int armor, int bow, int arrow)
    {
        Unit.SetActive(false);
        MyBase.SetActive(false);
        UnitAr.SetActive(true);
        UnitSp.SetActive(false);
        UnitCav.SetActive(false);
        Archer.GetComponent<Animator>().SetInteger("State", 1);
        ArcherAllShow(armor, bow, arrow);
    }

    public void BaseShow()
    {
        Unit.SetActive(false);
        MyBase.SetActive(true);
        UnitAr.SetActive(false);
        UnitSp.SetActive(false);
        UnitCav.SetActive(false);
    }

    void InfantryAllShow(int wep, int arm, int shi)
    {
        Infantry.ArmorShield(arm, shi, false);
        Infantry.WeaponInt(wep);
    }

    void CavalryAllShow(int wep, int arm, int horse)
    {
        Cavalry.ArmorShield(arm, 0, false);
        Cavalry.ChangeHorseMat(horse);
        Cavalry.WeaponInt(wep);
    }

    void SpearAllShow(int wep, int arm, int shi)
    {
        Spearman.ArmorShield(arm, shi, false);
        Spearman.WeaponInt(wep);
    }

    void  ArcherAllShow(int arm, int bow, int aro)
    {
        Archer.ArmorShield(arm, aro, false);
        Archer.WeaponInt(bow);
    }
    #endregion

    public void Dual_Wep(int w)
    {
        OrcDualweild.WeaponInt(w);
    }

    public void Dual_Arm(int a)
    {
        OrcDualweild.ArmorDualInt(a);
    }

    public void Dual_Charge(int c)
    {
        OrcDualweild.ChargerHead(c);
    }

    void Dual_WepArm(int wep, int arm)
    {
        Dual_Wep(wep);
        Dual_Arm(arm);
    }

    public void OrcSpr_Wep(int w)
    {
        OrcSpear.WeaponInt(w);
    }

    public void OrcSpr_Arm(int a)
    {
        OrcSpear.ArmorInt(a);
    }

    public void OrcSpr_Shi(int s)
    {
        OrcSpear.ShieldInt(s);
    }

    public void Throw_Rock(int t)
    {
        OrcThrower.ShieldInt(t);
    }

    public void Throw_HandWep(int h)
    {
        OrcThrower.WeaponInt(h);
    }

    public void Throw_Armor(int a)
    {
        OrcThrower.ArmorInt(a);
    }

    public void Throw_Googles()
    {
        OrcThrower.GooglesHelm();
    }

    void SprOrcAllShow(int w, int a, int s)
    {
        OrcSpr_Wep(w); OrcSpr_Arm(a); OrcSpr_Shi(s);
    }

    void ThwOrcAllShow(int t, int h, int a)
    {
        Throw_Rock(t); Throw_HandWep(h); Throw_Armor(a);
    }

    public void DualShow(int w, int a)
    {
        UnitOrcSpear.SetActive(false);
        UnitThrw.SetActive(false);
        MyBaseDual.SetActive(false);

        UnitDual.SetActive(true);
        Dual_WepArm(w, a);
    }

    public void OrcSpearShow(int w, int a, int shi)
    {
        UnitDual.SetActive(false);
        UnitThrw.SetActive(false);
        MyBaseDual.SetActive(false);

        UnitOrcSpear.SetActive(true);
        SprOrcAllShow(w, a, shi);
    }

    public void OrcSpearShowNoShi(int w, int a)
    {
        UnitDual.SetActive(false);
        UnitThrw.SetActive(false);
        MyBaseDual.SetActive(false);

        UnitOrcSpear.SetActive(true);
        OrcSpr_Wep(w);
        OrcSpr_Arm(a);
    }

    public void OrcThrowShow(int t, int a)
    {
        UnitOrcSpear.SetActive(false);
        UnitDual.SetActive(false);
        MyBaseDual.SetActive(false);

        UnitThrw.SetActive(true);
        Throw_Rock(t); Throw_Armor(a);
    }

    public void OrcThrowAllShow(int t,int h, int a)
    {
        UnitOrcSpear.SetActive(false);
        UnitDual.SetActive(false);
        MyBaseDual.SetActive(false);

        UnitThrw.SetActive(true);
        ThwOrcAllShow(t, h, a);
    }

    public void BaseShowDual()
    {
        UnitOrcSpear.SetActive(false);
        UnitDual.SetActive(false);
        UnitThrw.SetActive(false);
        MyBaseDual.SetActive(true);
    }
}
