using UnityEngine;

[CreateAssetMenu(fileName = "ArmorArch", menuName = "AI Upgrades/Archer/Armor")]
public class AI_UpgArmorArch : AI_UpgradeArch
{
    [Range(1, 2)]
    public int LevelUpgrade;

    public override bool Upgrade(Archer_Spawner arc_spwn, Player arc_ply)
    {
        if (arc_ply.gold >= arc_spwn.armorPrice[LevelUpgrade])
        {
            arc_spwn.armorLevel = LevelUpgrade;
            arc_ply.gold -= arc_spwn.armorPrice[LevelUpgrade];
            arc_spwn.armorPrice[LevelUpgrade] = 0;
            return true;
        }
        return false;
    }
}
