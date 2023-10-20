using UnityEngine;
[CreateAssetMenu(fileName = "ArmorThr", menuName = "AI Upgrades/Thrower/Gear")]
public class Ai_UpgArmorThrow : Ai_UpgradeThrow
{
    [Range(1, 3)]
    public int LevelUpgrade;

    public override bool Upgrade(Thrower_Spawner thr_spwn, Player ply)
    {
        if (ply.gold >= thr_spwn.armorPrice[LevelUpgrade])
        {
            thr_spwn.armorLevel = LevelUpgrade;
            ply.gold -= thr_spwn.armorPrice[LevelUpgrade];
            thr_spwn.armorPrice[LevelUpgrade] = 0;
            return true;
        }
        else return false;
    }
}
