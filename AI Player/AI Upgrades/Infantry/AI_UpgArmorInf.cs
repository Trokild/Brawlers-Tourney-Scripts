using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Upg", menuName = "AI Upgrades/Infantry/Armor Infantry")]
public class AI_UpgArmorInf : AI_UpgradeInf
{
    [Range(1, 4)]
    public int LevelUpgrade;

    public override bool Upgrade(Infantry_Spawner inf_spwn, Player inf_ply)
    {
        if(inf_ply.gold >= inf_spwn.armorPrice[LevelUpgrade])
        {
            inf_spwn.armorLevel = LevelUpgrade;
            inf_ply.gold -= inf_spwn.armorPrice[LevelUpgrade];
            inf_spwn.armorPrice[LevelUpgrade] = 0;
            return true;
        }
        return false;
    }
}
