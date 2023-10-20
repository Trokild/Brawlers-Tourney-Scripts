using UnityEngine;

[CreateAssetMenu(fileName = "Spear", menuName = "AI Upgrades/Spearmen/Spear")]
public class AI_UpgWeaponSpear : AI_UpgradeSpear
{
    public enum TypeWeapon { Spear, Polearm }
    public TypeWeapon WeaponType;
    [Range(1, 2)]
    public int LevelUpgrade;
    [Range(1, 4)]
    public int WeaponInt;

    public override bool Upgrade(Spearmen_Spawner spr_spwn, Player spr_ply)
    {
        if (spr_ply.gold >= spr_spwn.spearPrice[WeaponInt])
        {
            spr_spwn.weaponLevel = WeaponInt;
            spr_ply.gold -= spr_spwn.spearPrice[WeaponInt];
            spr_spwn.spearPrice[WeaponInt] = 0;
            return true;
        }
        return false;
    }
}
