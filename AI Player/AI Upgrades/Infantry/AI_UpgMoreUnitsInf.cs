using UnityEngine;

[CreateAssetMenu(fileName = "More Inf", menuName = "AI Upgrades/Infantry/More Units")]
public class AI_UpgMoreUnitsInf : AI_UpgradeInf
{
    [Range(1, 3)]
    public int LevelUpgrade;

    public override bool Upgrade(Infantry_Spawner inf_spwn, Player inf_ply)
    {
        if (inf_ply.gold >= inf_spwn.spawnPrice[LevelUpgrade - 1])
        {
            inf_ply.gold -= inf_spwn.spawnPrice[LevelUpgrade - 1];
            inf_spwn.spawnPrice[LevelUpgrade -1] = 0;
            switch (LevelUpgrade)
            {
                case 1:
                    inf_spwn.MoreUnits_One();
                    return true;
                case 2:
                    inf_spwn.MoreUnits_Two();
                    return true;
                case 3:
                    inf_spwn.MoreUnits_Three();
                    return true;
                default:
                    Debug.LogError("Level Upgrade: " + LevelUpgrade + " is invalid");
                    return false;
            }
        }
        else
        {
            return false;
        }
    }
}
