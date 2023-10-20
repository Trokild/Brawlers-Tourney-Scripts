using UnityEngine;

[CreateAssetMenu(fileName = "New Horse Stat", menuName = "Unit Equiptment/Horse")]
public class StatHorse : StatEquiptment
{
    [Space]
    public int armor;
    public int stamina;
    public int strenght;
    public int healthB;
    [Space]
    public int attackSpeed;
    public Vector2 speed;
    public int chargeDamage;
}
