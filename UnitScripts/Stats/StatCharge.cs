using UnityEngine;

[CreateAssetMenu(fileName = "New Charge Stat", menuName = "Charge Stat")]
public class StatCharge : ScriptableObject
{
    public new string name;
    [Range(0, 3)]
    public int ChargeInt;
    public int ChargeDmg;
    public int StamCost;
    public int prize;
}
