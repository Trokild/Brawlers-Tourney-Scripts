using UnityEngine.UI;
using UnityEngine;

public class Button_Ability : MonoBehaviour
{
    public int ButtonInt;
    public bool HasAbility { get; private set; }
    //public GameObject goHero; // heroUi.Heroref
    public Button Btn;
    public enum ButtonState {Active, Locked, Unlocked, Open}
    public ButtonState curButtonState;

    public float AbilityCooldown;
    public Image DarkMask;
    public Image thisImage;

    private float duration;
    private float readyTime;
    private float timeLeft;

    private bool isReady;
    private bool isCooldownSetRdy = false;
    private bool isInitialized = false;
    private int whatBtn;

    public Hero_UI heroUI;
    public BuyAbility_UI buyAbili;

    private void Start()
    {
        HasAbility = false;
        curButtonState = ButtonState.Locked;
    }

    private void Update()
    {
        if (isInitialized)
        {
            bool coolDownComplete = (Time.time > readyTime);
            isReady = coolDownComplete;
            if (coolDownComplete)
            {
                if (!isCooldownSetRdy)
                {
                    AbilityReadyCD();
                }
                else
                {
                    AbilityReady();
                }

            }
            else
            {
                Cooldown();
            }
        }
    }

    public void Initialize(Ability SelectedAbility)
    {
        AbilityCooldown = SelectedAbility.AbilityCooldown;
        HasAbility = true;
        curButtonState = ButtonState.Active;

        thisImage.sprite = SelectedAbility.AbilitySprite;
        thisImage.enabled = true;
        duration = SelectedAbility.AbilityCooldown;
        AbilityReady();

        Ability_PassiveBuff abiPassive = SelectedAbility as Ability_PassiveBuff;
        if (abiPassive != null)
        {
            Btn.interactable = false;
        }
        else
        {
            isInitialized = true;
        }
    }

    public void SetBtnInt(int i)
    {
        whatBtn = i;
    }

    private void AbilityReadyCD()
    {
        DarkMask.enabled = false;
        //Btn.interactable = true;
        isCooldownSetRdy = true;
    }

    private void AbilityReady()
    {
        if (heroUI.Hero_Magic.Cur_Mana >= heroUI.Hero_Magic.ActiveAbilities[whatBtn].ManaCost)
        {
            Btn.interactable = true;
        }
        else
        {
            Btn.interactable = false;
        }
    }

    public void ButtonTriggerd()
    {
        if(!HasAbility)
        {
            if(buyAbili.OpenOptions(this))
            {
                thisImage.sprite = buyAbili.LockAbleSprites[1];
                curButtonState = ButtonState.Open;
            }
            else
            {
                thisImage.sprite = buyAbili.LockAbleSprites[0];
            }
        }
        else if(isReady)
        {
            if (heroUI.Hero_Magic.Cur_Mana >= heroUI.Hero_Magic.ActiveAbilities[whatBtn].ManaCost)
            {
                heroUI.Hero_Magic.ActiveSpells[whatBtn].UseSpell(whatBtn);
            }
        }
    }

    public void StartCooldown()
    {
        readyTime = duration + Time.time;
        timeLeft = duration;
        DarkMask.enabled = true;
        Btn.interactable = false;
        isCooldownSetRdy = false;
    }

    private void Cooldown()
    {
        timeLeft -= Time.deltaTime;
        DarkMask.fillAmount = (timeLeft / duration);
    }

    public void SetButtonActive(bool act)
    {
        Btn.interactable = act;

        if (act)
        {
            if (!HasAbility)
            {
                thisImage.sprite = buyAbili.LockAbleSprites[0];
                curButtonState = ButtonState.Unlocked;
            }
        }
    }

    //public void SetRefrence(GameObject _holder)
    //{
    //    goHero = _holder;
    //}
}
