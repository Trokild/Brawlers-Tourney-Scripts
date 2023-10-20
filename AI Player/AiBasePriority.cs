using UnityEngine;

[CreateAssetMenu(fileName = "BasePriority", menuName = "Base Priority")]
public class AiBasePriority : ScriptableObject
{
    [Range(0, 4f)]
    public float PriorityMainBase;
    [Range(0, 4f)]
    public float PriorityRightBase;
    [Range(0, 4f)]
    public float PriorityLeftBase;

    public Vector3 GetPriorityVector()  // main, right, left
    {
        return new Vector3(PriorityMainBase, PriorityRightBase, PriorityLeftBase);
    }
}
