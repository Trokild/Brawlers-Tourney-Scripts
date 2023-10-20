using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Upg", menuName = "AI Upgrades/Infantry/Shield")]
public class AI_UpgShieldInf : AI_UpgradeInf
{
    [Range(1, 3)]
    public int LevelUpgrade;

    public override bool Upgrade(Infantry_Spawner inf_spwn, Player inf_ply)
    {
        if (inf_ply.gold >= inf_spwn.shieldPrice[LevelUpgrade])
        {
            inf_spwn.shieldLevel = LevelUpgrade;
            inf_ply.gold -= inf_spwn.shieldPrice[LevelUpgrade];
            inf_spwn.shieldPrice[LevelUpgrade] = 0;

            if(inf_spwn.shieldLevel > 0)
            {
                inf_spwn.hasShield = true;
            }
            else
            {
                inf_spwn.hasShield = false;
            }

            return true;
        }
        return false;
    }
}