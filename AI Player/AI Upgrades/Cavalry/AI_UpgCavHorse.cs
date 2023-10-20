using UnityEngine;

[CreateAssetMenu(fileName = "Horse", menuName = "AI Upgrades/Cavalry/Horse")]
public class AI_UpgCavHorse : AI_UpgradeCav
{
    [Range(1, 3)]
    public int horseInt;

    public override bool Upgrade(Cavalry_Spawner cav_spwn, Player cav_ply)
    {
        if (cav_ply.gold >= cav_spwn.cavPrice[horseInt])
        {
            cav_spwn.cavLvl = horseInt;
            cav_ply.gold -= cav_spwn.cavPrice[horseInt];
            cav_spwn.cavPrice[horseInt] = 0;
            return true;
        }
        return false;
    }
}
