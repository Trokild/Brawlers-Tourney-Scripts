using UnityEngine;

[CreateAssetMenu(fileName = "Dual Gear", menuName = "AI Upgrades/Dual/Gear")]
public class Ai_UpgradeDualGear : Ai_UpgradeDual
{
    [Range(1, 3)]
    public int LevelUpgrade;

    public override bool Upgrade(Dualweild_Spawner dual_spwn, Player ply)
    {
        if (ply.gold >= dual_spwn.armorPrice[LevelUpgrade])
        {
            dual_spwn.armorLevel = LevelUpgrade;
            ply.gold -= dual_spwn.armorPrice[LevelUpgrade];
            dual_spwn.armorPrice[LevelUpgrade] = 0;
            return true;
        }
        else return false;
    }
}
