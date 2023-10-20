[System.Serializable]
public class BuffClass 
{
    public enum BuffType { Armor, AttackSpeed, Damage, ArmorPercing, Speed }
    public BuffType Buff;
    public int BuffAmount;

    public BuffClass(int am, BuffType bt) //
    {
        BuffAmount = am;
        Buff = bt;
    }
}
