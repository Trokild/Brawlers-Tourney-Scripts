using UnityEngine;

[CreateAssetMenu(fileName = "BowArch", menuName = "AI Upgrades/Archer/Bow")]
public class AI_UpgBow : AI_UpgradeArch
{
    public enum TypeArrow { Longbow, Shortbow, Shieldbow }
    public TypeArrow BowType;
    [Range(1, 3)]
    public int WeaponInt;

    public override bool Upgrade(Archer_Spawner arc_spwn, Player arc_ply)
    {
        if (arc_ply.gold >= arc_spwn.bowPrice[WeaponInt])
        {
            arc_spwn.bowLvl = WeaponInt;
            arc_ply.gold -= arc_spwn.bowPrice[WeaponInt];
            arc_spwn.bowPrice[WeaponInt] = 0;
            return true;
        }
        return false;
    }
}
