using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon")]
public class Weapon : Gear
{
    [Range(0, 100f)]//Affliction have proc chance or Weapon?
    public int Proc;
    public Buff_Affliction BuffWeapon;
    [Header("X Damage - Y Armor Percing - Z Attackspeed")]
    public Vector3Int StatWeapon; //Damage, AP, AttackSpeed

    public override void UseItem(GameObject User)
    {
        thisGearIs = GearType.Weapon;
        base.UseItem(User);
        Hero userUnit = User.GetComponent<Hero>();
        if(userUnit != null)
        {
            userUnit.attackDamage.AddModifier(StatWeapon.x);
            userUnit.armorPercing.AddModifier(StatWeapon.y);
            userUnit.attackSpeed.AddModifier(StatWeapon.z);
            userUnit.WeaponHero_Equipt();
        }
    }

    public override void UnEquipt(GameObject User)
    {
        base.UnEquipt(User);
        Hero userUnit = User.GetComponent<Hero>();
        if (userUnit != null)
        {
            userUnit.attackDamage.RemoveModifier(StatWeapon.x);
            userUnit.armorPercing.RemoveModifier(StatWeapon.y);
            userUnit.attackSpeed.RemoveModifier(StatWeapon.z);
            userUnit.WeaponHero_Unequipt();
        }
        
    }
}
