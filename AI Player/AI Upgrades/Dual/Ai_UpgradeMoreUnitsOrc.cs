using UnityEngine;

[CreateAssetMenu(fileName = "More Units Orc", menuName = "AI Upgrades/Dual/More Units")]
public class Ai_UpgradeMoreUnitsOrc : Ai_UpgradeDual
{
    [Range(1, 3)]
    public int LevelUpgrade;

    public override bool Upgrade(Dualweild_Spawner spwn, Player ply)
    {
        if (ply.gold >= spwn.spawnPrice[LevelUpgrade - 1])
        {
            ply.gold -= spwn.spawnPrice[LevelUpgrade - 1];
            spwn.spawnPrice[LevelUpgrade - 1] = 0;
            switch (LevelUpgrade)
            {
                case 1:
                    spwn.MoreUnits_One();
                    return true;
                case 2:
                    spwn.MoreUnits_Two();
                    return true;
                case 3:
                    spwn.MoreUnits_Three();
                    return true;
                default:
                    Debug.LogError("Level Upgrade: " + LevelUpgrade + " is invalid");
                    return false;
            }
        }
        else
        {
            return false;
        }
    }
}
