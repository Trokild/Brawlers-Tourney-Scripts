using UnityEngine;

[CreateAssetMenu(fileName = "Stone", menuName = "AI Upgrades/Thrower/Stone")]
public class Ai_UpgStone : Ai_UpgradeThrow
{
    [Range(1, 3)]
    public int LevelUpgrade;

    public override bool Upgrade(Thrower_Spawner thr_spwn, Player ply)
    {
        if (ply.gold >= thr_spwn.rockPrice[LevelUpgrade])
        {
            thr_spwn.rockLevel = LevelUpgrade;
            ply.gold -= thr_spwn.rockPrice[LevelUpgrade];
            thr_spwn.rockPrice[LevelUpgrade] = 0;
            return true;
        }
        else return false;
    }
}
