using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Upg", menuName = "AI Upgrades/Infantry/Weapons")]
public class AI_UpgWeaponInf : AI_UpgradeInf
{
    public enum TypeWeapon {Sword, Axe, Mace}
    public TypeWeapon WeaponType;
    [Range(1, 2)]
    public int LevelUpgrade;
    [Range(1, 6)]
    public int WeaponInt;

    public override bool Upgrade(Infantry_Spawner inf_spwn, Player inf_ply)
    {
        if (inf_ply.gold >= inf_spwn.weaponPrice[WeaponInt])
        {
            inf_spwn.weaponLevel = WeaponInt;
            inf_ply.gold -= inf_spwn.weaponPrice[WeaponInt];
            inf_spwn.weaponPrice[WeaponInt] = 0;
            return true;
        }
        return false;
    }
}
