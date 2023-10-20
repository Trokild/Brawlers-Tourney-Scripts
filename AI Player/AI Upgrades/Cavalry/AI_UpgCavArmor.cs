using UnityEngine;

[CreateAssetMenu(fileName = "ArmorRider", menuName = "AI Upgrades/Cavalry/Armor Rider")]
public class AI_UpgCavArmor : AI_UpgradeCav
{
    [Range(1, 3)]
    public int ArmorInt;

    public override bool Upgrade(Cavalry_Spawner cav_spwn, Player cav_ply)
    {
        if (cav_ply.gold >= cav_spwn.armorPrice[ArmorInt])
        {
            cav_spwn.armorLevel = ArmorInt;
            cav_ply.gold -= cav_spwn.armorPrice[ArmorInt];
            cav_spwn.armorPrice[ArmorInt] = 0;
            return true;
        }
        return false; ;
    }
}
