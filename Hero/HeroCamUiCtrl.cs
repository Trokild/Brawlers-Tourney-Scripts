using UnityEngine.UI;
using UnityEngine;

public class HeroCamUiCtrl : MonoBehaviour
{
    [SerializeField] private Unit_Appearance[] unitApperance;
    [SerializeField] private Animator[] anim;
    [SerializeField] private RawImage camCanvas;
    [SerializeField] private Color camCanvasNormal;
    [SerializeField] private Color camCanvasGhost;
    //[SerializeField] private Material normalMaterial;
    [SerializeField] private Material[] ghostMaterial;
    [SerializeField] private GameObject[] heroEffects;
    [SerializeField] private GameObject[] hero;
    private int colorInt;
    private int heroInt;

    void Start()
    {

    }

    void GhostCamHero()
    {
        if(unitApperance != null && ghostMaterial != null)
        {
            for (int i = 0; i < unitApperance[heroInt]._MeshUnitBody.Length; i++)
            {
                unitApperance[heroInt]._MeshUnitBody[i].material = ghostMaterial[heroInt];
            }
        }

        if(heroEffects.Length > 0)
        {
            for (int i = 0; i < heroEffects.Length; i++)
            {
                heroEffects[i].SetActive(false);
            }
        }
    }

    void NormalCamHero()
    {
        for (int i = 0; i < unitApperance[heroInt]._MeshUnitBody.Length; i++)
        {
            //unitApperance._MeshUnitBody[i].material = normalMaterial;
            unitApperance[heroInt].ChangeUnitColor(colorInt);
        }

        for (int i = 0; i < heroEffects.Length; i++)
        {
            heroEffects[i].SetActive(true);
        }
    }

    public void DeathHeroCam()
    {
        anim[heroInt].SetTrigger("Hit");
        if(camCanvas != null)
        {
            camCanvas.color = camCanvasGhost;
        }

        GhostCamHero();
    }

    public void RespawnHeroCam()
    {
        anim[heroInt].SetTrigger("CastB");
        if (camCanvas != null)
        {
            camCanvas.color = camCanvasNormal;
        }
        NormalCamHero();
    }

    public void ColorHeroCam(int col, int h)
    {
        hero[h].SetActive(true);
        heroInt = h;
        unitApperance[heroInt].ChangeUnitColorNoDot(col);
        colorInt = col;

        if (camCanvas != null)
        {
            camCanvas.color = camCanvasNormal;
        }
    }
}
