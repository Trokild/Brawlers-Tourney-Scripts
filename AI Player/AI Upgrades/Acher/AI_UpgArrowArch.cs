using UnityEngine;

[CreateAssetMenu(fileName = "ArrowArch", menuName = "AI Upgrades/Archer/Arrow")]
public class AI_UpgArrowArch : AI_UpgradeArch
{
    public enum TypeArrow { Bolt, Slice, Flame }
    public TypeArrow ArrowType;
    [Range(1, 3)]
    public int WeaponInt;

    public override bool Upgrade(Archer_Spawner arc_spwn, Player arc_ply)
    {
        if (arc_ply.gold >= arc_spwn.arrowPrice[WeaponInt])
        {
            arc_spwn.arrowLvl = WeaponInt;
            arc_ply.gold -= arc_spwn.arrowPrice[WeaponInt];
            arc_spwn.arrowPrice[WeaponInt] = 0;
            return true;
        }
        return false;
    }
}
