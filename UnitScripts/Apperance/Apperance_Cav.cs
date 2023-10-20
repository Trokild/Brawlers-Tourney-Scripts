using UnityEngine;

public class Apperance_Cav : Unit_Appearance
{
    public SkinnedMeshRenderer _MeshHorse;
    public Material[] horseMat;
    //public int ja = 5;

    public void ChangeHorseMat(int cc)
    {
        _MeshHorse.material = horseMat[cc];
        if(hif != null)
        {
            hif.GetTheMesh(_MeshHorse);
        }
    }

    public override void Start()
    {
        base.Start();
        //Debug.Log("Apperance_Cav");
    }
}
