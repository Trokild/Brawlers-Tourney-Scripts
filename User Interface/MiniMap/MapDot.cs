using UnityEngine;

public class MapDot : MonoBehaviour
{
    //[SerializeField] private Material[] MapMaterialColors;
    //[SerializeField] private MeshRenderer MapBaseIcon;
    //public Transform MapBaseGo;

    [SerializeField] protected Material[] MapDotColors;
    [SerializeField] protected Material DeadMapDot;
    [SerializeField] protected MeshRenderer MapDotIcon;
    [SerializeField] protected Transform DotGo;
    protected Vector3 oldScale;
    protected int colInt;

    private void Start()
    {
        oldScale = DotGo.localScale;
        if (DotGo == null)
        {
            DotGo = this.transform;
        }

        if (MapDotIcon == null)
        {
            MapDotIcon = GetComponent<MeshRenderer>();
        }
    }

    public void SetColorDot(int colorInt)
    {
        colInt = colorInt;
        MapDotIcon.material = MapDotColors[colorInt];
        DotGo.rotation = Quaternion.Euler(0, 180, 0);
        ChangeDotColor(colorInt);
        if(MapDotIcon.enabled == false)
        {
            MapDotIcon.enabled = true;
        }
    }

    protected void ChangeDotColor(int c)
    {
        MapDotIcon.material = MapDotColors[c];
    }

    public void DotBack()
    {
        ChangeDotColor(colInt);
        DotGo.localScale = oldScale;
    }

    public virtual void DotDead()
    {
        DotGo.localScale = new Vector3(1.5f, 0, 1.5f);
        DotGo.rotation = Quaternion.Euler(0, 180, 0);
        DotGo.localPosition += new Vector3(0, 1, 0);
        MapDotIcon.material = DeadMapDot;
        Invoke("DeadDotFinish", 1f);
    }

    protected virtual void DeadDotFinish()
    {
        DotGo.localScale = new Vector3(1, 0, 1);
        DotGo.localPosition -= new Vector3(0, 1, 0);
    }
}
