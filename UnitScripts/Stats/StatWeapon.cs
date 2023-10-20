using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Stat", menuName = "Unit Equiptment/Weapon")]
public class StatWeapon : StatEquiptment
{
    public int weaponDamage;
    public int armorPercing;
    public int attackSpeed;

    public int chargeDamage; // just orc weapons?
}
