using UnityEngine;

[CreateAssetMenu(fileName = "Googles", menuName = "AI Upgrades/Thrower/Googles")]
public class Ai_UpgGoogles : Ai_UpgradeThrow
{
    public override bool Upgrade(Thrower_Spawner thr_spwn, Player ply)
    {
        if (ply.gold >= thr_spwn.googlesPrize && !thr_spwn.hasGoogels)
        {
            thr_spwn.hasGoogels = true;
            ply.gold -= thr_spwn.googlesPrize;
            thr_spwn.googlesPrize = 0;
            return true;
        }
        else return false;
    }
}
