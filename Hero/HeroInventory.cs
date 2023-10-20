using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInventory : MonoBehaviour
{
    public List<Item> inventoryHero = new List<Item>();
    public int[] StackItems; //private Set, public Get
    public bool[] EquiptItems; //private Set, public Get
    public float[] CooldownItems; //private Set, public Get
    private int maxBagSize;
    public Mask MaskSlot;
    public Weapon WeaponSlot;
    public Gear OffHandSlot;

    //private void Start()
    //{
    //    _hero = GetComponent<Hero>();
    //}

    private void Update()
    {
        for (int i = 0; i < CooldownItems.Length; i++)
        {
            if(CooldownItems[i] > 0)
            {
                CooldownItems[i] -= Time.deltaTime;
            }
            else
            {
                CooldownItems[i] = 0;
            }
        }
    }

    public void UseIventorySlot(int inv)
    {
        if (inv < inventoryHero.Count)
        {
            if (inventoryHero[inv] != null)
            {
                switch (inventoryHero[inv].thisItemIs)
                {
                    case Item.ItemType.Consumable:
                        Consumables cons = inventoryHero[inv] as Consumables;
                        if (cons != null)
                        {
                            if (CooldownItems[inv] <= 0)
                            {
                                StackItems[inv] -= 1;
                                cons.UseItem(gameObject);

                                if (StackItems[inv] <= 0)
                                {
                                    inventoryHero[inv] = null;
                                }
                                else
                                {
                                    CooldownItems[inv] = cons.cooldown;
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("This is not a consumable");
                        }
                        break;

                    case Item.ItemType.Gear:
                        Gear gr = inventoryHero[inv] as Gear;
                        if (gr != null)
                        {
                            if (CooldownItems[inv] <= 0)
                            {
                                switch(gr.thisGearIs)
                                {
                                    case Gear.GearType.Mask:
                                        Mask msk = gr as Mask;
                                        if(msk != null)
                                        {
                                            if (MaskSlot == null)
                                            {
                                                MaskSlot = msk;
                                                msk.UseItem(gameObject);
                                                CooldownItems[inv] = msk.cooldown;
                                                EquiptItems[inv] = true;
                                            }
                                            else if (MaskSlot == msk)
                                            {
                                                MaskSlot.UnEquipt(gameObject);
                                                MaskSlot = null;
                                                EquiptItems[inv] = false;
                                            }
                                            else
                                            {
                                                MaskSlot.UnEquipt(gameObject);
                                                MaskSlot = msk;
                                                MaskSlot.UseItem(gameObject);
                                                CooldownItems[inv] = msk.cooldown;
                                                EquiptItems[inv] = true;
                                                UnEquiptGear(inv, msk.thisGearIs);
                                            }
                                        }
                                        else
                                        {
                                            Debug.LogError("gr is not Mask.cs");
                                            return;
                                        }
                                        break;

                                    case Gear.GearType.Weapon:
                                        //Have to set fill weaponslot before UseItem()
                                        Weapon wep = gr as Weapon;
                                        if(wep != null)
                                        {
                                            if (WeaponSlot == null)
                                            {
                                                WeaponSlot = wep;
                                                wep.UseItem(gameObject);
                                                CooldownItems[inv] = wep.cooldown;
                                                EquiptItems[inv] = true;
                                            }
                                            else if (WeaponSlot == wep)
                                            {
                                                WeaponSlot.UnEquipt(gameObject);
                                                WeaponSlot = null;
                                                EquiptItems[inv] = false;
                                            }
                                            else
                                            {
                                                WeaponSlot.UnEquipt(gameObject);
                                                WeaponSlot = wep;
                                                WeaponSlot.UseItem(gameObject);
                                                CooldownItems[inv] = wep.cooldown;
                                                EquiptItems[inv] = true;
                                                UnEquiptGear(inv, wep.thisGearIs);
                                            }
                                        }
                                        else
                                        {
                                            Debug.LogError("gr is not Weapon.cs");
                                            return;
                                        }

                                        break;

                                    case Gear.GearType.OffHand:
                                        if (OffHandSlot == null)
                                        {
                                            OffHandSlot = gr;
                                            gr.UseItem(gameObject);
                                            CooldownItems[inv] = gr.cooldown;
                                            EquiptItems[inv] = true;
                                        }
                                        else if (OffHandSlot == gr)
                                        {
                                            OffHandSlot.UnEquipt(gameObject);
                                            OffHandSlot = null;
                                            EquiptItems[inv] = false;
                                        }
                                        else
                                        {
                                            OffHandSlot.UnEquipt(gameObject);
                                            OffHandSlot = gr;
                                            OffHandSlot.UseItem(gameObject);
                                            CooldownItems[inv] = gr.cooldown;
                                            EquiptItems[inv] = true;
                                            UnEquiptGear(inv, gr.thisGearIs);
                                        }
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("This is not a Armor / Gear");
                        }
                        break;

                    default:
                        if (CooldownItems[inv] <= 0)
                        {
                            inventoryHero[inv].UseItem(gameObject);
                            CooldownItems[inv] = inventoryHero[inv].cooldown;
                            Debug.Log("Use Inventory slot " + inv);
                        }
                        break;
                }
            }
            else
            {
                Debug.Log("inventoryHero[" + inv + "] is null");
            }
        }
        else
        {
            Debug.Log("Empty bagspot "+ inv );
        }
    }

    void UnEquiptGear(int m, Gear.GearType gerT)
    {
        for (int i = 0; i < inventoryHero.Count; i++)
        {
            if(i != m && inventoryHero[i] != null)
            {
                if (inventoryHero[i].thisItemIs == Item.ItemType.Gear)
                {
                    Gear gr = inventoryHero[i] as Gear;
                    if (gr != null)
                    {
                        if (gr.thisGearIs == gerT)
                        {
                            EquiptItems[i] = false;
                        }
                    }
                }
            }
        }
    }

    public void SetInventorySize(int inv)
    {
        maxBagSize = inv;
        if(StackItems.Length == 0)
        {
            StackItems = new int[maxBagSize];
        }
        else
        {
            int[] newArray = new int[maxBagSize];
            for (int i = 0; i < newArray.Length; i++)
            {
                if(i < StackItems.Length)
                {
                    newArray[i] = StackItems[i];
                }

                if (inventoryHero.Count > i)
                {
                    if (inventoryHero[i] != null)
                    {
                        Consumables conus = inventoryHero[i] as Consumables;
                        if (conus != null)
                        {
                            if (StackItems[i] > conus.MaxStack)
                            {
                                newArray[i] = conus.MaxStack;
                            }
                        }
                        else
                        {
                            newArray[i] = 0;
                        }
                    }
                    else
                    {
                        newArray[i] = 0;
                    }
                }
                else
                {
                    newArray[i] = 0;
                }
            }
            StackItems = newArray;
            CooldownItems = new float[maxBagSize];
            EquiptItems = new bool[maxBagSize];
        }

        if(inventoryHero.Count == 0)
        {
            inventoryHero = new List<Item>(new Item[maxBagSize]);
        }
        else if(inventoryHero.Count > maxBagSize)
        {
            for (int i = inventoryHero.Count - 1; i > maxBagSize; i--)
            {
                if (inventoryHero[i] == null)
                {
                    inventoryHero.RemoveAt(i);
                }
            }
            inventoryHero.RemoveRange(maxBagSize, inventoryHero.Count - maxBagSize);
        }
        else //Fill out list
        {
            int restInt = maxBagSize - inventoryHero.Count;
            for (int i = 0; i < restInt; i++)
            {
                inventoryHero.Add(null);
            }
        }
        #region Removes Null from list and trims it down to maxBagSize
        //for (int i = inventoryHero.Count - 1; i > -1; i--)
        //{
        //    if (inventoryHero[i] == null)
        //    {
        //        inventoryHero.RemoveAt(i);
        //    }
        //}

        //if (inventoryHero.Count > maxBagSize)
        //{
        //    inventoryHero.RemoveRange(maxBagSize, inventoryHero.Count - maxBagSize);
        //}
        #endregion
        EquiptWearable();
    }

    void EquiptWearable()
    {
        for (int i = 0; i < inventoryHero.Count; i++)
        {
            Item itm = inventoryHero[i];
            if(itm != null)
            {
                if (itm.thisItemIs == Item.ItemType.Gear)
                {
                    UseIventorySlot(i);
                }
            }
        }
    }

    public bool AddItemIventoryHero(Item itm)
    {
        if(itm.thisItemIs == Item.ItemType.Consumable)
        {
            Consumables consum = itm as Consumables;
            if(consum != null)
            {
                for (int i = 0; i < inventoryHero.Count; i++)
                {
                    if (consum == inventoryHero[i])
                    {
                        Debug.Log("AddItem Same type");
                        if (StackItems[i] < consum.MaxStack)
                        {
                            StackItems[i] += 1;
                            return true;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < inventoryHero.Count; i++)
        {
            if(inventoryHero[i] == null)
            {
                inventoryHero[i] = itm;
                return true;
            }
        }
        return false;
    }

    public bool AddItemInSlotHero(int slot, Item itm)
    {
        if (inventoryHero[slot] == null)
        {
            inventoryHero[slot] = itm;
            return true;
        }

        if (itm.thisItemIs == Item.ItemType.Consumable)
        {
            Consumables consum = itm as Consumables;
            if(consum == inventoryHero[slot] && consum.MaxStack > StackItems[slot])
            {
                StackItems[slot] += 1;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

}
