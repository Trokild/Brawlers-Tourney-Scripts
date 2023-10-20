using UnityEngine;

[CreateAssetMenu(fileName = "New Mask", menuName = "Item/Mask")]
public class Mask : Gear
{
    public Affliction MaskBuff;
    public override void UseItem(GameObject User)
    {
        thisGearIs = GearType.Mask;
        base.UseItem(User);
        Spell_PassiveBuff spellBuff = User.GetComponent<Spell_PassiveBuff>();
        if(spellBuff == null)
        {
            Spell_PassiveBuff _spellBuff = User.AddComponent<Spell_PassiveBuff>();
            _spellBuff.SetUpPassive(MaskBuff);
        }
        else
        {
        
            spellBuff.SetUpPassive(MaskBuff);
        }
    }

    public override void UnEquipt(GameObject User)
    {
        base.UnEquipt(User);
        Spell_PassiveBuff spellBuff = User.GetComponent<Spell_PassiveBuff>();
        if (spellBuff == null)
        {
            Spell_PassiveBuff _spellBuff = User.AddComponent<Spell_PassiveBuff>();
            _spellBuff.RemovePassiveBuff(MaskBuff);
        }
        else
        {

            spellBuff.RemovePassiveBuff(MaskBuff);
        }
    }
}
