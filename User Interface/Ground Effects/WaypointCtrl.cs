using UnityEngine;

public class WaypointCtrl : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem system;
    private ParticleSystem.MainModule psm;
    [SerializeField]
    private Transform wpTrans;

    [SerializeField]
    private Color moveColor;
    [SerializeField]
    private Color attackColor;

    void Start()
    {
        if(wpTrans == null)
        {
            wpTrans = transform;
        }
        if(system == null)
        {
            system = GetComponentInChildren<ParticleSystem>();
        }
        psm = system.main;
    }

    public void WayPoint(Vector3 pos, bool move)
    {
        wpTrans.position = pos;
        if (move)
        {
            psm.startColor = moveColor;
        }
        else
        {
            psm.startColor = attackColor;
        }
        DoEmit();
    }

    void DoEmit()
    {
        if(system.particleCount > 0)
        {
            system.Clear();
        }

        var emitParams = new ParticleSystem.EmitParams();
        system.Emit(emitParams, 4);
    }
}
