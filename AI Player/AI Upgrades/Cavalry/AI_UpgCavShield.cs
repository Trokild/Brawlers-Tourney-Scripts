using UnityEngine;

[CreateAssetMenu(fileName = "ShieldCav", menuName = "AI Upgrades/Cavalry/Shield Cavalry")]
public class AI_UpgCavShield : AI_UpgradeCav
{
    [Range(0, 1)]
    public int ShieldInt;

    public override bool Upgrade(Cavalry_Spawner cav_spwn, Player cav_ply)
    {
        if(ShieldInt == 0)
        {
            cav_spwn.hasShield = false;
            return true;
        }

        if (cav_ply.gold >= cav_spwn.shieldPrice)
        {
            cav_spwn.hasShield = true;
            cav_ply.gold -= cav_spwn.shieldPrice;
            cav_spwn.shieldPrice = 0;
            return true;
        }
        return false;
    }
}
