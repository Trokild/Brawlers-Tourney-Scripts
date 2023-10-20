using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Hero_UI : MonoBehaviour
{
    [SerializeField] private Transform Hero_Trans;
    [SerializeField] public Hero Hero_Hero;
    [SerializeField] public HeroHealth Hero_Health;
    [SerializeField] private HeroExperiance Hero_Xp;
    [SerializeField] private HeroCamUiCtrl HeroCam;
    [SerializeField] private BuyAbility_UI heroBuyAbility;
    [SerializeField] private Hero_StatsUI statUI;
    [SerializeField] private InventoryUI inventoryUi;
    [SerializeField] private HeroInventory Hero_Inventory;
    public HeroMagic Hero_Magic;
    [Space]
    public Button_Ability[] AbilityButtons;
    [Space]
    [SerializeField] private Color fullColor;
    [SerializeField] private Color midColor;
    [SerializeField] private Color lowColor;
    [SerializeField] private Color respawnColor;
    [SerializeField] private Color[] CooldownBtnCol;
    [Space]
    [SerializeField] private Image[] BtnImages;
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image ManaBar;
    [SerializeField] private Image DamageBar;

    private bool started = false;
    public float smoothTime;
    public float Delay;
    private float pDelay;
    private bool canDamp;
    private float val;
    private float valMana;
    private float currentDmg;
    private float currentHlt;
    private float currentMana;
    private bool isDead = false;

    [Space]
    [SerializeField] private TextMeshProUGUI lvlTxt;
    [SerializeField] private Image XpBar;
    private int curLvl = 1;
    private float curXpCalc;

    void Update()
    {
        if(!started)
        {
            return;   
        }
        curXpCalc = (Hero_Xp.cur_Xp * 1f) / Hero_Xp.xpToNextLvl;
        XpBar.fillAmount = curXpCalc;

        if(curLvl != Hero_Xp.level_Hero)
        {
            SetNewLvlTxt(Hero_Xp.level_Hero);
            curLvl = Hero_Xp.level_Hero;
            UpdateStatsUi();
        }

        if (!isDead)
        {
            currentHlt = (Hero_Health.Cur_Health * 1f) / Hero_Health.max_Health;
            if (val != currentHlt)
            {
                HeroUI_Healthbar();
            }

            currentMana = (Hero_Magic.Cur_Mana * 1f) / Hero_Magic.max_Mana;
            if (valMana != currentMana)
            {
                HeroUI_ManaBar();
            }

            currentDmg = DamageBar.fillAmount;
            if (DamageBar.fillAmount != HealthBar.fillAmount && canDamp == false)
            {
                pDelay -= Time.deltaTime;
                if (pDelay <= 0)
                {
                    canDamp = true;
                }
            }

            if (canDamp)
            {
                DamageBar.fillAmount = Mathf.Lerp(currentDmg, val, smoothTime);
                float dist = DamageBar.fillAmount - HealthBar.fillAmount;
                if (dist < 0.001)
                {
                    DamageBar.fillAmount = HealthBar.fillAmount;
                    canDamp = false;
                    pDelay = Delay;
                }
            }
        }
        else
        {
            currentHlt = Hero_Health.rt / Hero_Health.respawnTime;
            HealthBar.fillAmount = currentHlt;
        }
    }

    public void SetHeroUi(GameObject hero, int col, int heroT)
    {
        Hero_Trans = hero.transform;
        Hero_Hero = hero.GetComponent<Hero>();
        Hero_Health = hero.GetComponent<HeroHealth>();
        Hero_Health.SetLocalHero(this, statUI);
        Hero_Magic = hero.GetComponent<HeroMagic>();
        Hero_Xp = hero.GetComponent<HeroExperiance>();
        Hero_Inventory = hero.GetComponent<HeroInventory>();
        if(inventoryUi != null)
        {
            if(Hero_Inventory != null)
            {
                inventoryUi.StartInventory(Hero_Inventory);
            }
            else
            {
                Debug.LogError("Cant find Hero_Inventory");
            }
        }
        else
        {
            Debug.LogError("Cant find inventoryUi");
        }
        heroBuyAbility.AbilityList = Hero_Magic.HeroAbilities;
        SetBtnCol(heroT);
        HeroCam.ColorHeroCam(col, heroT);
        SetAllBtnInt();
        UpdateStatsUi();

        started = true;
    }

    void UpdateStatsUi()
    {
        if(Hero_Hero != null)
        {
            Vector3Int atk = new Vector3Int(Hero_Hero.attackDamage.GetValue(), Hero_Hero.attackSpeed.GetValue(), Hero_Hero.armorPercing.GetValue());
            statUI.SetStat_Attack(atk);

        }

        if (Hero_Health != null)
        {
            Vector2Int def = new Vector2Int(Hero_Health.armor.GetValue(), Hero_Health.reg_Health.GetValue());
            statUI.SetStat_Defense(def);
        }

        if (Hero_Magic != null)
        {
            statUI.SetStat_Magic(Hero_Magic.reg_Mana.GetValue());
        }
    }

    void SetBtnCol(int colHeroT)
    {
        for (int i = 0; i < BtnImages.Length; i++)
        {
            BtnImages[i].color = CooldownBtnCol[colHeroT];
        }
    }

    public void HeroUI_Healthbar()
    {
        val = currentHlt;
        HealthBar.fillAmount = val;

        if (val >= 0.5f)
        {
            float CorrectedValG = ((val - 0.5f)) / (1 - 0.5f);
            HealthBar.color = Color.Lerp(midColor, fullColor, CorrectedValG);
        }
        else if (val < 0.8f && val >= 0.1)
        {
            float CorrectedValY = ((val - 0.1f)) / (0.5f - 0.1f);
            HealthBar.color = Color.Lerp(lowColor, midColor, CorrectedValY);
        }
        else if (val < 0.1f)
        {
            HealthBar.color = lowColor;
        }
    }

    public void HeroUI_ManaBar()
    {
        valMana = currentMana;
        ManaBar.fillAmount = valMana;
    }

    void SetAllBtnInt()
    {
        for (int i = 0; i < AbilityButtons.Length; i++)
        {
            AbilityButtons[i].SetBtnInt(i);
        }
    }

    public void HeroUiDeath()
    {
        Invoke("DelayDeathBool", 3f);

        if (HeroCam != null)
        {
            HeroCam.DeathHeroCam();
        }

        for (int i = 0; i < AbilityButtons.Length; i++)
        {
            AbilityButtons[i].SetButtonActive(false);
        }
    }

    void DelayDeathBool()
    {
        isDead = true;
        HealthBar.color = respawnColor;
    }

    public void HeroUiRespawn()
    {
        isDead = false;
        if (HeroCam != null)
        {
            HeroCam.RespawnHeroCam();
        }

        for (int i = 0; i < AbilityButtons.Length; i++)
        { 
            switch (AbilityButtons[i].curButtonState)
            {
                case Button_Ability.ButtonState.Active:
                    AbilityButtons[i].SetButtonActive(true);
                    break;
                case Button_Ability.ButtonState.Unlocked:
                    AbilityButtons[i].SetButtonActive(true);
                    break;
            }
        }
    }

    public void SetNewLvlTxt(int lvl)
    {
        lvlTxt.text = lvl.ToString();
    }
}
