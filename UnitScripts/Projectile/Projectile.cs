using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int apLvl = 0;
    public GameObject Target;
    public GameObject father;

    protected bool hasReachedTrg;
    protected bool noTrg = false;

    protected Vector3 savPos;
    protected Vector3 previousPosition;
    protected Vector3 currentMovementDirection;

    protected void Start()
    {
        savPos = transform.localPosition;
    }

    public virtual void FireProjectile(GameObject tar, float time, float height)
    {
        Debug.Log("FireProjectile");
    }

    public virtual void FireProjectileStraight(GameObject tar, float time)
    {
        Debug.Log("FireProjectileStraight");
    }

    protected virtual void ResPos()
    {
        transform.parent = father.transform;
        transform.localPosition = savPos;
    }
}
