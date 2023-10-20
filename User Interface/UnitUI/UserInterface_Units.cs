using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UserInterface_Units : MonoBehaviour {

    public Sprite[] unitType_Sprite;
    public GameObject[] UiGo;
    public Image[] UiUnite_HB;
    public Image[] UiUnite_Type;
    public Image[] UiUnite_Stamina;

    [SerializeField]
    private Color fullColor;
    [SerializeField]
    private Color midColor;
    [SerializeField]
    private Color lowColor;
    [Space]
    [SerializeField]
    private Color restedColor;
    [SerializeField]
    private Color tierdColor;

    public  List<Unit> ss = new List<Unit>();   //System Selected
    public  List<Unit> sss = new List<Unit>();  //System Selected Sorted

    private void Update()
    {
        ss = MainSystem.sys_Selected;
        sss = MainSystem.sort_Selected;

        if(MainSystem.sys_Selected.Count > 0) 
        {
            foreach(Unit uni in MainSystem.sort_Selected) // Change sort_Selected list to be <Health> not <Unit> to
            {                                             // remove GetComponemt<Health>()
                if (uni != null)                          // Also make it update every time a unit takes dmg, not on update
                {
                    bool isDead = false;

                    int MH = uni.GetComponent<Health>().max_Health;
                    int CH = uni.GetComponent<Health>().Cur_Health;
                    bool t = uni.tierd;

                    float val;
                    val = (CH * 1f) / MH;

                    UiUnite_HB[MainSystem.sort_Selected.IndexOf(uni)].fillAmount = val;

                    if (val >= 0.7f)
                    {
                        float CorrectedValG = ((val - 0.7f)) / (1 - 0.7f);
                        UiUnite_HB[MainSystem.sort_Selected.IndexOf(uni)].color = Color.Lerp(midColor, fullColor, CorrectedValG);
                    }
                    else if (val < 0.8f && val >= 0.1)
                    {
                        float CorrectedValY = ((val - 0.2f)) / (0.7f - 0.2f);
                        UiUnite_HB[MainSystem.sort_Selected.IndexOf(uni)].color = Color.Lerp(lowColor, midColor, CorrectedValY);
                    }
                    else if (val < 0.2f)
                    {
                        UiUnite_HB[MainSystem.sort_Selected.IndexOf(uni)].color = lowColor;
                    }

                    if(t)
                    {
                        UiUnite_Stamina[MainSystem.sort_Selected.IndexOf(uni)].color = tierdColor;
                    }
                    else
                    {
                        UiUnite_Stamina[MainSystem.sort_Selected.IndexOf(uni)].color = restedColor;
                    }

                    if (CH <= 0 && isDead == false || uni == null)
                    {
                        isDead = true;
                    }
                }
            }
        }
    }

    public void AddUnitToUI(GameObject UI)
    {
        Unit un = UI.GetComponent<Unit>();
        if (un != null)
        {
            int sa = MainSystem.sys_Selected.Count;
            int sy = MainSystem.sys_Selected.IndexOf(un);

            UiGo[sy].SetActive(true);

            foreach (Unit unn in MainSystem.sys_Selected)
            {
                int io = MainSystem.sort_Selected.IndexOf(unn);

                switch (unn.currentUnitType)
                {
                    case Unit.UnitType.Infantry:
                        UiUnite_Type[io].sprite = unitType_Sprite[0];
                        break;
                    case Unit.UnitType.Archer:
                        UiUnite_Type[io].sprite = unitType_Sprite[1];
                        break;
                    case Unit.UnitType.Cavalry:
                        UiUnite_Type[io].sprite = unitType_Sprite[2];
                        break;
                    case Unit.UnitType.Spearman:
                        UiUnite_Type[io].sprite = unitType_Sprite[3];
                        break;
                    case Unit.UnitType.Hero:
                        UiUnite_Type[io].sprite = unitType_Sprite[4];
                        break;
                }
            }
        }
    }

    public void AddSingleUnitToUI(GameObject UI)
    {
        Unit un = UI.GetComponent<Unit>();
        if (un != null)
        {
            int sa = MainSystem.sys_Selected.Count;
            int sy = MainSystem.sys_Selected.IndexOf(un);
            Debug.Log("count: " + sa + "  Index: " + sy);

            UiGo[sy].SetActive(true);

            switch (un.currentUnitType)
            {
                case Unit.UnitType.Infantry:
                    UiUnite_Type[sy].sprite = unitType_Sprite[0];
                    break;
                case Unit.UnitType.Archer:
                    UiUnite_Type[sy].sprite = unitType_Sprite[1];
                    break;
                case Unit.UnitType.Cavalry:
                    UiUnite_Type[sy].sprite = unitType_Sprite[2];
                    break;
                case Unit.UnitType.Spearman:
                    UiUnite_Type[sy].sprite = unitType_Sprite[3];
                    break;
                case Unit.UnitType.Hero:
                    UiUnite_Type[sy].sprite = unitType_Sprite[4];
                    break;
            }
        }
    }

    public void UpdateUnitUi()
    {
        foreach(Unit un in MainSystem.sort_Selected)
        {
            int io = MainSystem.sort_Selected.IndexOf(un);

            UiGo[io].SetActive(true);

            switch (un.currentUnitType)
            {
                case Unit.UnitType.Infantry:
                    UiUnite_Type[io].sprite = unitType_Sprite[0];
                    break;
                case Unit.UnitType.Archer:
                    UiUnite_Type[io].sprite = unitType_Sprite[1];
                    break;
                case Unit.UnitType.Cavalry:
                    UiUnite_Type[io].sprite = unitType_Sprite[2];
                    break;
                case Unit.UnitType.Spearman:
                    UiUnite_Type[io].sprite = unitType_Sprite[3];
                    break;
                case Unit.UnitType.Hero:
                    UiUnite_Type[io].sprite = unitType_Sprite[4];
                    break;
            }
        }
    }

    public void RemoveUnitToUI(GameObject UI)
    {
        Unit un = UI.GetComponent<Unit>();
        if (un != null)
        {
            //Debug.Log(MainSystem.sys_Selected.Count);
            UiGo[MainSystem.sys_Selected.Count].SetActive(false);
        }

        foreach (Unit unn in MainSystem.sort_Selected)
        {
            int io = MainSystem.sort_Selected.IndexOf(unn);

            switch(unn.currentUnitType)
            {
                case Unit.UnitType.Infantry:
                    UiUnite_Type[io].sprite = unitType_Sprite[0];
                    break;
                case Unit.UnitType.Archer:
                    UiUnite_Type[io].sprite = unitType_Sprite[1];
                    break;
                case Unit.UnitType.Cavalry:
                    UiUnite_Type[io].sprite = unitType_Sprite[2];
                    break;
                case Unit.UnitType.Spearman:
                    UiUnite_Type[io].sprite = unitType_Sprite[3];
                    break;
                case Unit.UnitType.Hero:
                    UiUnite_Type[io].sprite = unitType_Sprite[4];
                    break;
            }
        }
    }

    public void ClearUnitToUI()
    {
        for (int i = 0; i < UiGo.Length; i++)
        {
            UiGo[i].SetActive(false);
        }

    }

    public GameObject CheckImageInList(Image imga)
    {
        for (int i = UiUnite_Type.Length - 1; i >= 0; i--)
        {
            if (imga == UiUnite_Type[i])
            {
                //Debug.Log(sss[i].gameObject);
                //return UiUnite_HB[i].gameObject;
                return sss[i].gameObject;
            }
        }
        return null;
    }
}
