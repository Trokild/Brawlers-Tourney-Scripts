using UnityEngine;

[CreateAssetMenu(fileName = "SpearArmor", menuName = "AI Upgrades/Spearmen/Armor")]
public class AI_UpgArmorSpear : AI_UpgradeSpear
{
    [Range(1, 3)]
    public int LevelUpgrade;

    public override bool Upgrade(Spearmen_Spawner spr_spwn, Player spr_ply)
    {
        if (spr_ply.gold >= spr_spwn.armorPrice[LevelUpgrade])
        {
            spr_spwn.armorLevel = LevelUpgrade;
            spr_ply.gold -= spr_spwn.armorPrice[LevelUpgrade];
            spr_spwn.armorPrice[LevelUpgrade] = 0;
            return true;
        }
        return false;
    }
}
