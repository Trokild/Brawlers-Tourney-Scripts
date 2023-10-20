using UnityEngine;

[CreateAssetMenu(fileName = "CavWeapon", menuName = "AI Upgrades/Cavalry/Weapons")]
public class AI_UpgCavWep : AI_UpgradeCav
{
    [Range(0, 3)]
    public int WeaponInt;

    public override bool Upgrade(Cavalry_Spawner cav_spwn, Player cav_ply)
    {
        if (cav_ply.gold >= cav_spwn.weaponPrice[WeaponInt])
        {
            cav_spwn.weaponLevel = WeaponInt;
            cav_ply.gold -= cav_spwn.weaponPrice[WeaponInt];
            cav_spwn.weaponPrice[WeaponInt] = 0;
            return true;
        }
        return false;
    }
}
