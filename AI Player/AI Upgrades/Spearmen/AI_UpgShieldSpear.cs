using UnityEngine;

[CreateAssetMenu(fileName = "ShieldSpear", menuName = "AI Upgrades/Spearmen/Shield")]
public class AI_UpgShieldSpear : AI_UpgradeSpear
{
    [Range(1, 4)]
    public int LevelUpgrade;

    public override bool Upgrade(Spearmen_Spawner spr_spwn, Player spr_ply)
    {
        if (spr_ply.gold >= spr_spwn.shieldPrice[LevelUpgrade])
        {
            spr_spwn.shieldLevel = LevelUpgrade;
            spr_ply.gold -= spr_spwn.shieldPrice[LevelUpgrade];
            spr_spwn.shieldPrice[LevelUpgrade] = 0;
            return true;
        }
        return false;
    }
}
