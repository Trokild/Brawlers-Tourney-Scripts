using UnityEngine;

[CreateAssetMenu(fileName = "Stonger Orc", menuName = "AI Upgrades/Dual/Stronger Orc")]
public class Ai_UpgradeStongerOrc : Ai_UpgradeDual
{
    public override bool Upgrade(Dualweild_Spawner spwn, Player ply)
    {
        if (ply.gold >= spwn.strongPrice)
        {
            if (spwn.isStrong)
            {
                Debug.LogError("Units already stong bruh");
                return false;
            }
            else
            {
                ply.gold -= spwn.strongPrice;
                spwn.strongPrice = 0;
                spwn.Stronger_Units();
                return true;
            }
        }
        else
        {
            return false;
        }
    }
}
