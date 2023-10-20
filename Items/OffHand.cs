using UnityEngine;

[CreateAssetMenu(fileName = "New Off-Hand", menuName = "Item/Off-Hand")]
public class OffHand : Gear
{
    [Header("X Health Regen - Y Mana Regen")]
    public Vector2Int StatStick; //virtually a literal stat stick

    public override void UseItem(GameObject User)
    {
        thisGearIs = GearType.OffHand;
        base.UseItem(User);
        if (StatStick.x > 0)
        {
            HeroHealth heroHlt = User.GetComponent<HeroHealth>();
            heroHlt.reg_Health.AddModifier(StatStick.x);
        }

        if (StatStick.y > 0)
        {
            HeroMagic heroMag = User.GetComponent<HeroMagic>();
            heroMag.reg_Mana.AddModifier(StatStick.y);
        }
    }

    public override void UnEquipt(GameObject User)
    {
        base.UnEquipt(User);
        if (StatStick.x > 0)
        {
            HeroHealth heroHlt = User.GetComponent<HeroHealth>();
            heroHlt.reg_Health.RemoveModifier(StatStick.x);
        }

        if (StatStick.y > 0)
        {
            HeroMagic heroMag = User.GetComponent<HeroMagic>();
            heroMag.reg_Mana.RemoveModifier(StatStick.y);
        }
    }
}
