using UnityEngine;

[CreateAssetMenu(fileName = "Stronger Inf", menuName = "AI Upgrades/Infantry/Stronger")]
public class AI_UpgStrongerInf : AI_UpgradeInf
{
    public override bool Upgrade(Infantry_Spawner inf_spwn, Player inf_ply)
    {
        if (inf_ply.gold >= inf_spwn.strongPrice)
        {
            if(inf_spwn.isStrong)
            {
                Debug.LogError("Units already stong bruh");
                return false;
            }
            else
            {
                inf_ply.gold -= inf_spwn.strongPrice;
                inf_spwn.strongPrice = 0;
                inf_spwn.Stronger_Units();
                return true;
            }
        }
        else
        {
            return false;
        }
    }
}
