using UnityEngine;

[CreateAssetMenu(fileName = "Faster Inf", menuName = "AI Upgrades/Infantry/Faster")]
public class AI_UpgFasterInf : AI_UpgradeInf
{
    [Range(1, 2)]
    public int LevelUpgrade;

    public override bool Upgrade(Infantry_Spawner inf_spwn, Player inf_ply)
    {
        if (inf_ply.gold >= inf_spwn.timePrice[LevelUpgrade - 1])
        {
            inf_ply.gold -= inf_spwn.timePrice[LevelUpgrade - 1];
            inf_spwn.timePrice[LevelUpgrade -1] = 0;

            switch (LevelUpgrade)
            {
                case 1:
                    inf_spwn.FasterUnits_One();
                    return true;
                case 2:
                    inf_spwn.FasterUnits_Two();
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
