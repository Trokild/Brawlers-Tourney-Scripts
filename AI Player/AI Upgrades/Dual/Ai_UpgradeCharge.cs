using UnityEngine;

[CreateAssetMenu(fileName = "Dual Charge", menuName = "AI Upgrades/Dual/Charge")]
public class Ai_UpgradeCharge : Ai_UpgradeDual
{
    [Range(1, 3)]
    public int LevelUpgrade;

    public override bool Upgrade(Dualweild_Spawner dual_spwn, Player ply)
    {
        if (ply.gold >= dual_spwn.chargePrice[LevelUpgrade])
        {
            if (!dual_spwn.canCharge)
            {
                dual_spwn.canCharge = true;
            }
            dual_spwn.chargeLvl = LevelUpgrade;
            ply.gold -= dual_spwn.chargePrice[LevelUpgrade];
            dual_spwn.chargePrice[LevelUpgrade] = 0;
            return true;
        }
        return false;
    }
}
