using UnityEngine;

public class Consumables : Item
{
    public int MaxStack;
    // Start is called before the first frame update
    public override void UseItem(GameObject User)
    {
        Debug.Log("Use Consumable " + itemName);
    }
}
