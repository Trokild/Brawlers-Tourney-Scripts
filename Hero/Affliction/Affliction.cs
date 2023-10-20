using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "Affliction/Passive Buff")]
public class Affliction : ScriptableObject
{
    public Sprite AfflitionSprite;
    public bool OnSelf;
    public bool DeBuff;
    public List<BuffClass> BuffAfflition = new List<BuffClass>();
}
