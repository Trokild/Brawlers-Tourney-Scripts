using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Item/Test")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public enum ItemType {Consumable, Gear, Armor }
    public ItemType thisItemIs;
    public float cooldown;

    public virtual void UseItem(GameObject User) //on equiptment use is Equipt/Unequipt
    {
        Debug.Log("Use item " + itemName);
    }
}
