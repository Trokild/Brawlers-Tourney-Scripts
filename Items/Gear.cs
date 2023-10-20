using UnityEngine;

[CreateAssetMenu(fileName = "New Gear", menuName = "Item/Gear")]
public class Gear : Item
{
    public enum GearType {Mask, Weapon, OffHand} //Mask is Shoulders, OffHand Is Shield; 
    public GearType thisGearIs;

    public override void UseItem(GameObject User)
    {
        Unit_Appearance apprnc = User.GetComponentInChildren<Unit_Appearance>();
        switch (thisGearIs)
        {
            case GearType.Mask:
                apprnc.ShoulderA();
                break;
            case GearType.Weapon:
                apprnc.WeaponInt(1);
                break;
            case GearType.OffHand:
                apprnc.ShieldInt(0);
                break;
        }
    }

    public virtual void UnEquipt(GameObject User)
    {
        Unit_Appearance apprnc = User.GetComponentInChildren<Unit_Appearance>();
        switch (thisGearIs)
        {
            case GearType.Mask:
                apprnc.NoShoulders();
                break;
            case GearType.Weapon:
                apprnc.WeaponInt(0);
                break;
            case GearType.OffHand:
                apprnc.NoShield();
                break;
        }
    }
}
