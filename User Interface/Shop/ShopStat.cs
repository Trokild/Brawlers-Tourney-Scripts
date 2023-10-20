using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ShopStat : MonoBehaviour
{
    private GraphicRaycaster _graphicRaycaster;
    PointerEventData _pointerEvent;
    private EventSystem _eventSystem;

    [SerializeField] private CurrentStats curStat;

    [SerializeField]
    private Color colNetrual;
    [SerializeField]
    private Color colWorse;
    [SerializeField]
    private Color colBetter;

    //DONT NEED THAT MANY; JUST HAVE THE ONE AND EDIT IT

    [Space]
    [Header("_Weapon_")]
    [SerializeField] private GameObject wepStatGo;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI dmgTxt;
    [SerializeField] private TextMeshProUGUI atkSpdTxt;
    [SerializeField] private TextMeshProUGUI armPercTxt;
    [SerializeField] private TextMeshProUGUI weightWepTxt;
    [SerializeField] private TextMeshProUGUI priceTxt;

    [Space]
    [Header("_Dual_")]
    [SerializeField] private GameObject dualStatGo;
    [SerializeField] private TextMeshProUGUI dualnameTxt;
    [SerializeField] private TextMeshProUGUI dualdmgTxt;
    [SerializeField] private TextMeshProUGUI dualatkSpdTxt;
    [SerializeField] private TextMeshProUGUI dualarmPercTxt;
    [SerializeField] private TextMeshProUGUI dualChargeTxt;
    [SerializeField] private TextMeshProUGUI dualweightWepTxt;
    [SerializeField] private TextMeshProUGUI dualpriceTxt;

    [Space]
    [Header("_Googles_")]
    [SerializeField] private GameObject googStatGo;
    [SerializeField] private TextMeshProUGUI googNameTxt;
    [SerializeField] private TextMeshProUGUI googRangeTxt;
    [SerializeField] private TextMeshProUGUI googArmorTxt;
    [SerializeField] private TextMeshProUGUI googWeightTxt;
    [SerializeField] private TextMeshProUGUI googpriceTxt;

    [Space]
    [Header("_Rock_")]
    [SerializeField] private GameObject rockStatGo;
    [SerializeField] private TextMeshProUGUI rocknameTxt;
    [SerializeField] private TextMeshProUGUI rockdmgTxt;
    [SerializeField] private TextMeshProUGUI rockatkSpdTxt;
    [SerializeField] private TextMeshProUGUI rockarmPercTxt;
    [SerializeField] private TextMeshProUGUI rockRangeTxt;

    [SerializeField] private TextMeshProUGUI rockweightWepTxt;
    [SerializeField] private TextMeshProUGUI rockpriceTxt;

    [Space]
    [Header("_Charge_")]
    [SerializeField] private GameObject chargeStatGo;
    [SerializeField] private TextMeshProUGUI chargeNameTxt;
    [SerializeField] private TextMeshProUGUI chargeDmgTxt;
    [SerializeField] private TextMeshProUGUI chargeStamCostTxt;
    [SerializeField] private TextMeshProUGUI chargePrizeTxt;

    [Space]
    [Header("_Armor & Shield_")]
    [SerializeField] private GameObject armStatGo;
    [SerializeField] private TextMeshProUGUI nameArmTxt;
    [SerializeField] private TextMeshProUGUI ArmTxt;
    [SerializeField] private TextMeshProUGUI HealthPTxt;
    [SerializeField] private TextMeshProUGUI weightArmTxt;
    [SerializeField] private TextMeshProUGUI priceArmTxt;

    [Space]
    [Header("_Arrow_")]
    [SerializeField] private GameObject arrStatGo;
    [SerializeField] private TextMeshProUGUI nameArrowTxt;
    [SerializeField] private TextMeshProUGUI dmgArrowTxt;
    [SerializeField] private TextMeshProUGUI armPercArrowTxt;
    [SerializeField] private TextMeshProUGUI weightArrowTxt;
    [SerializeField] private TextMeshProUGUI priceArrowTxt;

    [Space]
    [Header("_Bow_")]
    [SerializeField] private GameObject bowStatGo;
    [SerializeField] private TextMeshProUGUI nameBowTxt;
    [SerializeField] private TextMeshProUGUI atkSpdBowTxt;
    [SerializeField] private TextMeshProUGUI rangeBowTxt;
    [SerializeField] private TextMeshProUGUI weightBowTxt;
    [SerializeField] private TextMeshProUGUI priceBowTxt;

    [Space]
    [Header("_Spear_")]
    [SerializeField] private GameObject spearStatGo;
    [SerializeField] private TextMeshProUGUI nameSpearTxt;
    [SerializeField] private TextMeshProUGUI dmgSpearTxt;
    [SerializeField] private TextMeshProUGUI apSpearTxt;
    [SerializeField] private TextMeshProUGUI atkSpdSpearTxt;
    [SerializeField] private TextMeshProUGUI bonusCavTxt;
    [SerializeField] private TextMeshProUGUI weightSpearTxt;
    [SerializeField] private TextMeshProUGUI priceSpearTxt;

    [Space]
    [Header("_Base_")]
    [SerializeField] private GameObject baseStatGo;
    [SerializeField] private TextMeshProUGUI nameBaseTxt;
    [SerializeField] private TextMeshProUGUI _staticVarTxt;
    [SerializeField] private TextMeshProUGUI varStatTxt;
    [SerializeField] private TextMeshProUGUI infoTxt;
    [SerializeField] private TextMeshProUGUI priceBaseTxt;
    [Space]
    [SerializeField] private string[] infoText;
    private GameObject curActiveStatGo;

    private void Start()
    {
        _eventSystem = EventSystem.current;
        _graphicRaycaster = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GraphicRaycaster>();
    }

    public void SetCurrStat(CurrentStats cs)
    {
        curStat = cs;
    }

    #region Show Click Update
    public void ShowWeaponClickUpdate()
    {
        dmgTxt.color = colNetrual;
        atkSpdTxt.color = colNetrual;
        apSpearTxt.color = colNetrual;

        priceTxt.SetText("Bought");
    }

    public void ShowRockClickUpdate()
    {
        rockdmgTxt.color = colNetrual;
        rockatkSpdTxt.color = colNetrual;
        rockarmPercTxt.color = colNetrual;
        rockRangeTxt.color = colNetrual;

        rockpriceTxt.SetText("Bought");
    }

    public void ShowSpearClickUpdate()
    {
        dmgSpearTxt.color = colNetrual;
        atkSpdSpearTxt.color = colNetrual;
        armPercTxt.color = colNetrual;
        bonusCavTxt.color = colNetrual;

        priceSpearTxt.SetText("Bought");
    }

    public void ShowArmorClickUpdate()
    {
        ArmTxt.color = colNetrual;
        priceArmTxt.SetText("Bought");
    }

    public void ShowArrowClickUpdate()
    {
        dmgArrowTxt.color = colNetrual;
        armPercArrowTxt.color = colNetrual;
        priceArmTxt.SetText("Bought");
    }

    public void ShowBowClickUpdate()
    {
        rangeBowTxt.color = colNetrual;
        atkSpdBowTxt.color = colNetrual;
        priceArmTxt.SetText("Bought");
    }

    public void ShowChargeClickUpdate()
    {
        chargeDmgTxt.color = colNetrual;
        chargePrizeTxt.SetText("Bought");
    }

    public void ShowHorseClickUpdate()
    {
        Debug.Log("ShowHorseClickUpdate");
    }

    public void ShowBaseClickUpdate()
    {
        priceBaseTxt.SetText("Bought");
    }

    #endregion
    public void ShowWeaponStatUI(StatWeapon st, bool b)
    {
        wepStatGo.SetActive(true);
        curActiveStatGo = wepStatGo;
        nameTxt.SetText(st.name);
        dmgTxt.SetText(st.weaponDamage.ToString());

        if (st.weaponDamage > curStat.curStatWeapon.weaponDamage)
        {
            dmgTxt.color = colBetter;
        }
        else if (st.weaponDamage < curStat.curStatWeapon.weaponDamage)
        {
            dmgTxt.color = colWorse;
        }
        else if (st.weaponDamage == curStat.curStatWeapon.weaponDamage)
        {
            dmgTxt.color = colNetrual;
        }

        atkSpdTxt.SetText(st.attackSpeed.ToString());

        if (st.attackSpeed > curStat.curStatWeapon.attackSpeed)
        {
            atkSpdTxt.color = colBetter;
        }
        else if (st.attackSpeed < curStat.curStatWeapon.attackSpeed)
        {
            atkSpdTxt.color = colWorse;
        }
        else if (st.attackSpeed == curStat.curStatWeapon.attackSpeed)
        {
            atkSpdTxt.color = colNetrual;
        }

        armPercTxt.SetText(st.armorPercing.ToString());

        if (st.armorPercing > curStat.curStatWeapon.armorPercing)
        {
            armPercTxt.color = colBetter;
        }
        else if (st.armorPercing < curStat.curStatWeapon.armorPercing)
        {
            armPercTxt.color = colWorse;
        }
        else if (st.armorPercing == curStat.curStatWeapon.armorPercing)
        {
            armPercTxt.color = colNetrual;
        }

        weightWepTxt.SetText(st.weight.ToString());

        if (b)
        {
            priceTxt.SetText("Owned");
        }
        else
        {
            priceTxt.SetText(st.prize.ToString());
        }

        _pointerEvent = new PointerEventData(_eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(_pointerEvent, results);

        foreach (RaycastResult raysult in results)
        {
            Button bt = raysult.gameObject.GetComponent<Button>();
            if (bt != null)
            {
                bool ia = bt.interactable;
            }
        }
    }

    public void ShowRockStatUI(StatRock rk, bool b)
    {
        rockStatGo.SetActive(true);
        curActiveStatGo = rockStatGo;
        rocknameTxt.SetText(rk.name);

       rockdmgTxt.SetText(rk.weaponDamage.ToString());
        if (rk.weaponDamage > curStat.curRock.weaponDamage)
        {
            rockdmgTxt.color = colBetter;
        }
        else if (rk.weaponDamage < curStat.curRock.weaponDamage)
        {
            rockdmgTxt.color = colWorse;
        }
        else if (rk.weaponDamage == curStat.curRock.weaponDamage)
        {
            rockdmgTxt.color = colNetrual;
        }

        rockatkSpdTxt.SetText(rk.attackSpeed.ToString());
        if (rk.attackSpeed > curStat.curRock.attackSpeed)
        {
            rockatkSpdTxt.color = colBetter;
        }
        else if (rk.attackSpeed < curStat.curRock.attackSpeed)
        {
            rockatkSpdTxt.color = colWorse;
        }
        else if (rk.attackSpeed == curStat.curRock.attackSpeed)
        {
            rockatkSpdTxt.color = colNetrual;
        }

        rockarmPercTxt.SetText(rk.armorPercing.ToString());

        if (rk.armorPercing > curStat.curRock.armorPercing)
        {
            rockarmPercTxt.color = colBetter;
        }
        else if (rk.armorPercing < curStat.curRock.armorPercing)
        {
            rockarmPercTxt.color = colWorse;
        }
        else if (rk.armorPercing == curStat.curRock.armorPercing)
        {
            rockarmPercTxt.color = colNetrual;
        }

        rockRangeTxt.SetText(rk.range.ToString());
        if (rk.range > curStat.curRock.range)
        {
            rockRangeTxt.color = colBetter;
        }
        else if (rk.range < curStat.curRock.range)
        {
            rockRangeTxt.color = colWorse;
        }
        else if (rk.range == curStat.curRock.range)
        {
            rockRangeTxt.color = colNetrual;
        }

        rockweightWepTxt.SetText(rk.weight.ToString());
        if (b)
        {
            rockpriceTxt.SetText("Owned");
        }
        else
        {
            rockpriceTxt.SetText(rk.prize.ToString());
        }

        _pointerEvent = new PointerEventData(_eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(_pointerEvent, results);

        foreach (RaycastResult raysult in results)
        {
            Button bt = raysult.gameObject.GetComponent<Button>();
            if (bt != null)
            {
                bool ia = bt.interactable;
            }
        }
    }

    public void ShowGooglesStatUI(StatGoogels gl, bool b)
    {
        googStatGo.SetActive(true);
        googNameTxt.SetText(gl.name);
        curActiveStatGo = googStatGo;
        
        googArmorTxt.SetText("+" + gl.armorBonus.ToString());
        googArmorTxt.color = colBetter;

        googRangeTxt.SetText("+" + gl.rangeBonus.ToString());
        googRangeTxt.color = colBetter;

        googWeightTxt.SetText(gl.weight.ToString());
        if (b)
        {
            googpriceTxt.SetText("Owned");
        }
        else
        {
            googpriceTxt.SetText(gl.prize.ToString());
        }

        _pointerEvent = new PointerEventData(_eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(_pointerEvent, results);
    }

    public void ShowDualStatUI(StatWeapon st, bool b)
    {
        dualStatGo.SetActive(true);
        curActiveStatGo = dualStatGo;
        dualnameTxt.SetText(st.name);
        dualdmgTxt.SetText(st.weaponDamage.ToString());

        if (st.weaponDamage > curStat.curStatWeapon.weaponDamage)
        {
            dualdmgTxt.color = colBetter;
        }
        else if (st.weaponDamage < curStat.curStatWeapon.weaponDamage)
        {
            dualdmgTxt.color = colWorse;
        }
        else if (st.weaponDamage == curStat.curStatWeapon.weaponDamage)
        {
            dualdmgTxt.color = colNetrual;
        }

        dualatkSpdTxt.SetText(st.attackSpeed.ToString());

        if (st.attackSpeed > curStat.curStatWeapon.attackSpeed)
        {
            dualatkSpdTxt.color = colBetter;
        }
        else if (st.attackSpeed < curStat.curStatWeapon.attackSpeed)
        {
            dualatkSpdTxt.color = colWorse;
        }
        else if (st.attackSpeed == curStat.curStatWeapon.attackSpeed)
        {
            dualatkSpdTxt.color = colNetrual;
        }

        dualarmPercTxt.SetText(st.armorPercing.ToString());

        if (st.armorPercing > curStat.curStatWeapon.armorPercing)
        {
            dualarmPercTxt.color = colBetter;
        }
        else if (st.armorPercing < curStat.curStatWeapon.armorPercing)
        {
            dualarmPercTxt.color = colWorse;
        }
        else if (st.armorPercing == curStat.curStatWeapon.armorPercing)
        {
            dualarmPercTxt.color = colNetrual;
        }

        dualChargeTxt.SetText(st.chargeDamage.ToString());

        if (st.chargeDamage > curStat.curStatWeapon.chargeDamage)
        {
            dualChargeTxt.color = colBetter;
        }
        else if (st.chargeDamage < curStat.curStatWeapon.chargeDamage)
        {
            dualChargeTxt.color = colWorse;
        }
        else if (st.chargeDamage == curStat.curStatWeapon.chargeDamage)
        {
            dualChargeTxt.color = colNetrual;
        }

        dualweightWepTxt.SetText(st.weight.ToString());

        if (b)
        {
            dualpriceTxt.SetText("Owned");
        }
        else
        {
            dualpriceTxt.SetText(st.prize.ToString());
        }

        _pointerEvent = new PointerEventData(_eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(_pointerEvent, results);

        foreach (RaycastResult raysult in results)
        {
            Button bt = raysult.gameObject.GetComponent<Button>();
            if (bt != null)
            {
                bool ia = bt.interactable;
            }
        }
    }

    public void ShowChargerStatUI(StatCharge ch, bool b)
    {
        chargeStatGo.SetActive(true);
        curActiveStatGo = chargeStatGo;
        chargeNameTxt.SetText(ch.name);
        chargeDmgTxt.SetText(ch.ChargeDmg.ToString());
        chargeStamCostTxt.SetText(ch.StamCost.ToString());

        if (ch.ChargeDmg > curStat.curCharge.ChargeDmg)
        {
            chargeDmgTxt.color = colBetter;
        }
        else if (ch.ChargeDmg < curStat.curCharge.ChargeDmg)
        {
            chargeDmgTxt.color = colWorse;
        }
        else if (ch.ChargeDmg == curStat.curCharge.ChargeDmg)
        {
            chargeDmgTxt.color = colNetrual;
        }

        if (ch.StamCost > curStat.curCharge.StamCost)
        {
            chargeStamCostTxt.color = colWorse;
        }
        else if (ch.StamCost < curStat.curCharge.StamCost)
        {
            chargeStamCostTxt.color = colBetter;
        }
        else if (ch.StamCost == curStat.curCharge.StamCost)
        {
            chargeStamCostTxt.color = colNetrual;
        }

        if (b)
        {
            chargePrizeTxt.SetText("Owned");
        }
        else
        {
            chargePrizeTxt.SetText(ch.prize.ToString());
        }

        _pointerEvent = new PointerEventData(_eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(_pointerEvent, results);

        foreach (RaycastResult raysult in results)
        {
            Button bt = raysult.gameObject.GetComponent<Button>();
            if (bt != null)
            {
                bool ia = bt.interactable;
            }
        }
    }

    public void ShowSpearStatUI(StatSpear ss, bool b)
    {
        spearStatGo.SetActive(true);
        curActiveStatGo = spearStatGo;
        nameSpearTxt.SetText(ss.name);
        dmgSpearTxt.SetText(ss.weaponDamage.ToString());

        if (ss.weaponDamage > curStat.curSpear.weaponDamage)
        {
            dmgSpearTxt.color = colBetter;
        }
        else if (ss.weaponDamage < curStat.curSpear.weaponDamage)
        {
            dmgSpearTxt.color = colWorse;
        }
        else if (ss.weaponDamage == curStat.curSpear.weaponDamage)
        {
            dmgSpearTxt.color = colNetrual;
        }

        atkSpdSpearTxt.SetText(ss.attackSpeed.ToString());

        if (ss.attackSpeed > curStat.curSpear.attackSpeed)
        {
            atkSpdSpearTxt.color = colBetter;
        }
        else if (ss.attackSpeed < curStat.curSpear.attackSpeed)
        {
            atkSpdSpearTxt.color = colWorse;
        }
        else if (ss.attackSpeed == curStat.curSpear.attackSpeed)
        {
            atkSpdSpearTxt.color = colNetrual;
        }

        apSpearTxt.SetText(ss.armorPercing.ToString());

        if (ss.armorPercing > curStat.curSpear.armorPercing)
        {
            apSpearTxt.color = colBetter;
        }
        else if (ss.armorPercing < curStat.curSpear.armorPercing)
        {
            apSpearTxt.color = colWorse;
        }
        else if (ss.armorPercing == curStat.curSpear.armorPercing)
        {
            apSpearTxt.color = colNetrual;
        }

        bonusCavTxt.SetText(ss.armorPercing.ToString());

        if (ss.bonusVsCav > curStat.curSpear.bonusVsCav)
        {
            bonusCavTxt.color = colBetter;
        }
        else if (ss.bonusVsCav < curStat.curSpear.bonusVsCav)
        {
            bonusCavTxt.color = colWorse;
        }
        else if (ss.bonusVsCav == curStat.curSpear.bonusVsCav)
        {
            bonusCavTxt.color = colNetrual;
        }

        weightSpearTxt.SetText(ss.weight.ToString());

        if (b)
        {
            priceSpearTxt.SetText("Owned");
        }
        else
        {
            priceSpearTxt.SetText(ss.prize.ToString());
        }
    }

    public void ShowArmorStatUI(StatArmor sa, bool b)
    {
        armStatGo.SetActive(true);
        curActiveStatGo = armStatGo;
        nameArmTxt.SetText(sa.name.ToString());
        ArmTxt.SetText(sa.armor.ToString());

        if (sa.armor > curStat.curStatArmor.armor)
        {
            ArmTxt.color = colBetter;
        }
        else if (sa.armor < curStat.curStatArmor.armor)
        {
            ArmTxt.color = colWorse;
        }
        else if (sa.armor == curStat.curStatArmor.armor)
        {
            ArmTxt.color = colNetrual;
        }

        HealthPTxt.SetText(sa.healthBonus.ToString());
        weightArmTxt.SetText(sa.weight.ToString());

        if (b)
        {
            priceArmTxt.SetText("Owned");
        }
        else
        {
            priceArmTxt.SetText(sa.prize.ToString());
        }

        _pointerEvent = new PointerEventData(_eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(_pointerEvent, results);

        foreach (RaycastResult raysult in results)
        {
            Button bt = raysult.gameObject.GetComponent<Button>();
            if (bt != null)
            {
                bool ia = bt.interactable;
            }
        }
    }

    public void ShowShieldStatUI(StatArmor sa, bool b)
    {
        armStatGo.SetActive(true);
        curActiveStatGo = armStatGo;
        nameArmTxt.SetText(sa.name.ToString());
        ArmTxt.SetText(sa.armor.ToString());

        if(curStat.curStatShield !=null)
        {
            if (sa.armor > curStat.curStatShield.armor)
            {
                ArmTxt.color = colBetter;
            }
            else if (sa.armor < curStat.curStatShield.armor)
            {
                ArmTxt.color = colWorse;
            }
            else if (sa.armor == curStat.curStatShield.armor)
            {
                ArmTxt.color = colNetrual;
            }
        }
        else
        {
            ArmTxt.color = colBetter;
        }

        HealthPTxt.SetText(sa.healthBonus.ToString());
        weightArmTxt.SetText(sa.weight.ToString());

        if (b)
        {
            priceArmTxt.SetText("Owned");
        }
        else
        {
            priceArmTxt.SetText(sa.prize.ToString());
        }

        _pointerEvent = new PointerEventData(_eventSystem)
        {
            position = Input.mousePosition
        };
    }

    public void ShowArrowStatUI(StatArrow sa, bool b)
    {
        arrStatGo.SetActive(true);
        curActiveStatGo = arrStatGo;
        nameArrowTxt.SetText(sa.name);
        dmgArrowTxt.SetText(sa.weaponDamage.ToString());

        if (sa.weaponDamage > curStat.curArrow.weaponDamage)
        {
            dmgArrowTxt.color = colBetter;
        }
        else if (sa.weaponDamage < curStat.curArrow.weaponDamage)
        {
            dmgArrowTxt.color = colWorse;
        }
        else if (sa.weaponDamage == curStat.curArrow.weaponDamage)
        {
            dmgArrowTxt.color = colNetrual;
        }

        armPercArrowTxt.SetText(sa.armorPercing.ToString());

        if (sa.armorPercing > curStat.curArrow.armorPercing)
        {
            armPercArrowTxt.color = colBetter;
        }
        else if (sa.armorPercing < curStat.curArrow.armorPercing)
        {
            armPercArrowTxt.color = colWorse;
        }
        else if (sa.armorPercing == curStat.curArrow.armorPercing)
        {
            armPercArrowTxt.color = colNetrual;
        }

        weightArrowTxt.SetText(sa.weight.ToString());

        if (b)
        {
            priceArrowTxt.SetText("Owned");
        }
        else
        {
            priceArrowTxt.SetText(sa.prize.ToString());
        }
    }

    public void ShowBowStatUI(StatBow sb, bool b)
    {
        bowStatGo.SetActive(true);
        curActiveStatGo = bowStatGo;

        nameBowTxt.SetText(sb.name);
        atkSpdBowTxt.SetText(sb.attackSpeed.ToString());

        if (sb.attackSpeed > curStat.curBow.attackSpeed)
        {
            atkSpdBowTxt.color = colBetter;
        }
        else if (sb.attackSpeed < curStat.curBow.attackSpeed)
        {
            atkSpdBowTxt.color = colWorse;
        }
        else if (sb.attackSpeed == curStat.curBow.attackSpeed)
        {
            atkSpdBowTxt.color = colNetrual;
        }

        rangeBowTxt.SetText(sb.range.ToString());

        if (sb.range > curStat.curBow.range)
        {
            rangeBowTxt.color = colBetter;
        }
        else if (sb.range < curStat.curBow.range)
        {
            rangeBowTxt.color = colWorse;
        }
        else if (sb.range == curStat.curBow.range)
        {
            rangeBowTxt.color = colNetrual;
        }

        weightBowTxt.SetText(sb.weight.ToString());

        if (b)
        {
            priceBowTxt.SetText("Owned");
        }
        else
        {
            priceBowTxt.SetText(sb.prize.ToString());
        }
    }

    public void Base_ShowMoreUnits(int maxU, int price, bool b)
    {
        baseStatGo.SetActive(true);
        curActiveStatGo = baseStatGo;
        nameBaseTxt.SetText("More Units");
        _staticVarTxt.SetText("Max Units: ");
        varStatTxt.SetText(maxU.ToString());
        infoTxt.SetText(infoText[0]);

        if (b)
        {
            priceBaseTxt.SetText("Upgraded");
        }
        else
        {
            priceBaseTxt.SetText(price.ToString());
        }
    }

    public void Base_ShowFasterUnits(float spdPrc, int price, bool b)
    {
        baseStatGo.SetActive(true);
        curActiveStatGo = baseStatGo;
        nameBaseTxt.SetText("Faster Spawns");
        _staticVarTxt.SetText("Time Spawn: ");
        varStatTxt.SetText("-" + spdPrc.ToString() + "%");
        infoTxt.SetText(infoText[1]);

        if (b)
        {
            priceBaseTxt.SetText("Upgraded");
        }
        else
        {
            priceBaseTxt.SetText(price.ToString());
        }
    }

    public void Base_ShowStrongerUnits(int stg, int price, bool b)
    {
        baseStatGo.SetActive(true);
        curActiveStatGo = baseStatGo;
        nameBaseTxt.SetText("Stronger Soliders");
        _staticVarTxt.SetText("Stenght: ");
        varStatTxt.SetText("+"+stg.ToString());
        infoTxt.SetText(infoText[2]);

        if (b)
        {
            priceBaseTxt.SetText("Upgraded");
        }
        else
        {
            priceBaseTxt.SetText(price.ToString());
        }
    }

    public void HideWeaponStat()
    {
        wepStatGo.SetActive(false);
    }

    public void HideArmorStat()
    {
        armStatGo.SetActive(false);
    }

    public void HideArrowStat()
    {
        arrStatGo.SetActive(false);
    }

    public void HideBowStat()
    {
        bowStatGo.SetActive(false);
    }
    
    public void HideBaseStat()
    {
        baseStatGo.SetActive(false);
    }

    public void HideStat()
    {
        if(curActiveStatGo != null)
        {
            curActiveStatGo.SetActive(false);
            curActiveStatGo = null;
        }
    }
}
