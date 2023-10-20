using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private HeroInventory hero_Inventory;
    private int maxListSizeUi;
    public Button_Items[] ItemButtons;
    public Sprite BackgroundNormal;
    public Sprite BackgroundHighlight;
    public Image[] MaskItemCooldown;
    public TextMeshProUGUI[] StackTxt;
    [SerializeField] private Sprite emptyInventorySprite;
    public Item testItem;
    private bool activeUI = false;
    [Space]
    [SerializeField] private Hero_StatsUI _heroStatUI;
    [SerializeField] private Hero_UI _heroUI;

    private void Update()
    {
        if (activeUI)
        {
            for (int i = 0; i < hero_Inventory.CooldownItems.Length; i++)
            {
                if(hero_Inventory.CooldownItems[i] > 0)
                {
                    MaskItemCooldown[i].fillAmount = (hero_Inventory.CooldownItems[i] / hero_Inventory.inventoryHero[i].cooldown);
                }
                else if(MaskItemCooldown[i].fillAmount != 0)
                {
                    MaskItemCooldown[i].fillAmount = 0;
                }
            }
        }
    }

    public void StartInventory(HeroInventory hi)
    {
        int maxS = ItemButtons.Length;
        int txtS = StackTxt.Length;
        if(maxS != txtS)
        {
            Debug.LogError("maxS != txtS");
        }
        SetSizeList(maxS);
        hero_Inventory = hi;
        hero_Inventory.SetInventorySize(maxS);
        UpdateInventoryUI();
        UpdateStatUIWeapon();
        UpdateStatUIOffHand();
        if (MaskItemCooldown.Length == hero_Inventory.CooldownItems.Length)
        {
            activeUI = true;
            for (int i = 0; i < MaskItemCooldown.Length; i++)
            {
                MaskItemCooldown[i].fillAmount = 0;
            }
        }
        else
        {
            activeUI = false;
            Debug.LogError("MaskItemCooldown.Length =/= hero_Inventory.CooldownItems.Length");
        }

    }

    void SetSizeList(int i)
    {
        maxListSizeUi = i;
    }

    void UpdateInventoryUI()
    {
        for (int i = 0; i < hero_Inventory.inventoryHero.Count; i++)
        {
            if(i < ItemButtons.Length)
            {
                Item itm = hero_Inventory.inventoryHero[i];
                if (ItemButtons[i].ItemImage != null)
                {
                    if(itm != null)
                    {
                        ItemButtons[i].ItemImage.sprite = itm.itemSprite;
                    }
                    else
                    {
                        ItemButtons[i].ItemImage.sprite = emptyInventorySprite;
                    }
                }

                if(hero_Inventory.StackItems[i] > 0)
                {
                    StackTxt[i].SetText(hero_Inventory.StackItems[i].ToString());
                }
                else
                {
                    StackTxt[i].SetText(" ");
                }

                if(hero_Inventory.EquiptItems[i] == true)
                {
                    ItemButtons[i].BackGroundImage.sprite = BackgroundHighlight;
                }
                else
                {
                    ItemButtons[i].BackGroundImage.sprite = BackgroundNormal;
                }
            }
            else
            {
                return;
            }
        }
    }

    public void ButtonInventory(int b)
    {
        if(hero_Inventory == null)
        {
            Debug.LogError("Cant find hero_Inventory");
            return;
        }

        if(b >= maxListSizeUi || b < 0)
        {
            Debug.LogError("wrong btn int " + b);
            return;
        }
        else if(hero_Inventory.inventoryHero.Count > b)
        {

            if(hero_Inventory.inventoryHero[b] != null)
            {
                hero_Inventory.UseIventorySlot(b);
                UpdateInventoryUI();
                UpdateStatUIWeapon();
                UpdateStatUIOffHand();
            }
            else
            {
                Debug.Log("Empty Inventory slot " + b);
            }
        }
        else
        {
            Debug.Log(hero_Inventory.inventoryHero.Count + " is greater than " + b);
        }
    }

    public void AddItemInSlot(int slot, Item itm)
    {
        if(hero_Inventory.AddItemInSlotHero(slot, itm))
        {
            UpdateInventoryUI();
        }
        else
        {
            Debug.Log("Slot space " + slot + " is full");
        }
    }

    public void AddItemUI(Item itm)
    {
        if(hero_Inventory.AddItemIventoryHero(itm))
        {
            UpdateInventoryUI();
        }
        else
        {
            Debug.Log("cant add item");
        }
    }

    public void TestAddItem(int s)
    {
        AddItemInSlot(s ,testItem);
    }

    void UpdateStatUIWeapon()
    {
        if (_heroStatUI != null && _heroUI != null)
        {
            Hero ero = _heroUI.Hero_Hero;
            //Damage
            if (ero.attackDamage.GetModifiersValue() == 0)
            {
                _heroStatUI.NormalText_Damage(ero.attackDamage.GetValue());
            }
            else
            {
                _heroStatUI.GreenText_Damage(ero.attackDamage.GetValue());
            }
            // AP
            if (ero.armorPercing.GetModifiersValue() == 0)
            {
                _heroStatUI.NormalText_AP(ero.armorPercing.GetValue());
            }
            else
            {
                _heroStatUI.GreenText_AP(ero.armorPercing.GetValue());
            }
            // Attack Speed
            if (ero.attackSpeed.GetModifiersValue() == 0)
            {
                _heroStatUI.NormalText_AttackSpd(ero.attackSpeed.GetValue());
            }
            else
            {
                _heroStatUI.GreenText_AttackSpd(ero.attackSpeed.GetValue());
            }
        }
    }

    void UpdateStatUIOffHand()
    {
        if (_heroStatUI != null && _heroUI != null)
        {
            HeroHealth eroHlt = _heroUI.Hero_Health;
            HeroMagic eroMana = _heroUI.Hero_Magic;
            //Health Regen
            if (eroHlt.reg_Health.GetModifiersValue() == 0)
            {
                _heroStatUI.NormalText_HealthRegen(eroHlt.reg_Health.GetValue());
            }
            else
            { 
                _heroStatUI.GreenText_HealthRegen(eroHlt.reg_Health.GetValue());
            }
        
            // Mana Regen
            if (eroMana.reg_Mana.GetModifiersValue() == 0)
            {
                _heroStatUI.NormalText_ManaRegen(eroMana.reg_Mana.GetValue());
            }
            else
            {
                _heroStatUI.GreenText_ManaRegen(eroMana.reg_Mana.GetValue());
            }

        }
    }
}
