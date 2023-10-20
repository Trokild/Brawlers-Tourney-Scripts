using UnityEngine;

[CreateAssetMenu(fileName = "Meele", menuName = "AI Upgrades/Thrower/Meele")]
public class Ai_UpgThrowMeele : Ai_UpgradeThrow
{
    [Range(1, 2)]
    public int LevelUpgrade;

    public override bool Upgrade(Thrower_Spawner thr_spwn, Player ply)
    {
        if (ply.gold >= thr_spwn.handWepPrice[LevelUpgrade])
        {
            if (!thr_spwn.hasMeleeWeapon)
            {
                thr_spwn.hasMeleeWeapon = true;
            }

            thr_spwn.handWeaponLevel = LevelUpgrade;
            ply.gold -= thr_spwn.handWepPrice[LevelUpgrade];
            thr_spwn.handWepPrice[LevelUpgrade] = 0;
            return true;
        }
        else return false;
    }
}
