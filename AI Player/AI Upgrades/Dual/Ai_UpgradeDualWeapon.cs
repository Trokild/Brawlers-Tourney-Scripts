using UnityEngine;

[CreateAssetMenu(fileName = "Dual Weapons", menuName = "AI Upgrades/Dual/Weapons")]
public class Ai_UpgradeDualWeapon : Ai_UpgradeDual
{
    [Range(1, 3)]
    public int LevelUpgrade;

    public override bool Upgrade(Dualweild_Spawner dual_spwn, Player ply)
    {
        if (ply.gold >= dual_spwn.weaponPrice[LevelUpgrade])
        {
            dual_spwn.weaponLevel = LevelUpgrade;
            ply.gold -= dual_spwn.weaponPrice[LevelUpgrade];
            dual_spwn.weaponPrice[LevelUpgrade] = 0;
            return true;
        }
        return false;
    }
}
