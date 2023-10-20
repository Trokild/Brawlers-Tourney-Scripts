using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class BuyAbility_UI : MonoBehaviour
{
    [SerializeField] private Hero_UI heroUi;
    public Sprite[] LockAbleSprites;
    public List<Ability> AbilityList = new List<Ability>();

    [SerializeField] private GameObject AbilityOptions;
    [SerializeField] private GameObject[] OptionsBtn;
    private bool isOptionsOpen = false;
    [SerializeField] private RectTransform[] AbilityBtnRec;
    [SerializeField] private RectTransform rec;
    private int curBtn = 0;

    // Button trigger returns this bool to see if it is open
    // Its on the button_ability you change the lock/open book/closed book/ability sprite
    public bool OpenOptions(Button_Ability btn)
    {
        if(!isOptionsOpen)
        {
            isOptionsOpen = true;

            AbilityOptions.SetActive(isOptionsOpen);
            int avalibleAbi = AbilityList.Count;
            int whatBtn = btn.ButtonInt;
            curBtn = whatBtn;

            for (int i = 0; i < avalibleAbi; i++)
            {
                OptionsBtn[i].SetActive(true);
                OptionsBtn[i].GetComponent<Image>().sprite = AbilityList[i].Choose_AbilitySprite;
                //OptionsBtn[i].GetComponentInChildren<Text>().text = AbilityList[i].BookCost.ToString();
            }

            switch (avalibleAbi)
            {
                case 1:
                    rec.sizeDelta = new Vector2(rec.sizeDelta.x, 104.7f);
                    rec.anchoredPosition = new Vector2(AbilityBtnRec[whatBtn].anchoredPosition.x, 94.2f);
                    break;

                case 2:
                    rec.sizeDelta = new Vector2(rec.sizeDelta.x, 177.4f);
                    rec.anchoredPosition = new Vector2(AbilityBtnRec[whatBtn].anchoredPosition.x, 146.2f);
                    break;

                case 3:
                    rec.sizeDelta = new Vector2(rec.sizeDelta.x, 255.5f);
                    rec.anchoredPosition = new Vector2(AbilityBtnRec[whatBtn].anchoredPosition.x, 185.5f);
                    break;

                case 4:
                    rec.sizeDelta = new Vector2(rec.sizeDelta.x, 345);
                    rec.anchoredPosition = new Vector2(AbilityBtnRec[whatBtn].anchoredPosition.x, 230);
                    break;

                default:
                    Debug.LogError("Incorrect avalibleAbi:" + avalibleAbi);
                    break;
            }

            return true;
        }
        else
        {
            TurnOffSpellOptions();
            return false;
        }
    }

    void TurnOffSpellOptions()
    {
        isOptionsOpen = false;

        for (int i = 0; i < OptionsBtn.Length; i++)
        {
            OptionsBtn[i].SetActive(false);
        }
        AbilityOptions.SetActive(isOptionsOpen);
    }

    //The Buttons that pop up after you click on open book have this func
    public void BuyAbility(int at)
    {
        if (isOptionsOpen)
        {
            Debug.Log(AbilityList[at].name);
            heroUi.Hero_Magic.AbilityInitialize(at, curBtn);
            TurnOffSpellOptions();
        }

    }
}
