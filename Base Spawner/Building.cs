using System.Collections;
using System.Collections.Generic;
using FoW;
using UnityEngine;

// Parent of Unit_Spawner and Shop_Building
public class Building : MonoBehaviour
{
    public bool isAI;
    public bool isDestroyed = false;

    [Range(1, 3)]
    public int buildingNumber;
    public int buildingTeam = -1;
    public int buildingId = -1;
    public int buildingColInt { get; protected set; }

    [SerializeField] protected MeshRenderer[] MeshBuilding;
    public Material[] colorBuilding;
    [SerializeField] protected GameObject townHall;
    public float rangeFogVision = 25f;

    [HideInInspector]
    public Ui_Base BaseUi;
    public bool isUiOpen = false;

    [HideInInspector]
    public Outline ol;
    public BuildingHealth hlt;

    [HideInInspector]
    public ClickParent clickRef;
    [HideInInspector]
    public Player playerRef;
    protected GameConsol gc;

    protected virtual void Start()
    {
        if (ol == null)
        {
            ol = GetComponentInChildren<Outline>();
        }

        if (hlt == null)
        {
            hlt = GetComponent<BuildingHealth>();
        }
    }

    public virtual void DestroyedBuilding()
    {
        isDestroyed = true;

        if (BaseUi != null)
        {
            if (isUiOpen)
            {
                BaseUi.gameObject.SetActive(false);
                isUiOpen = false;
            }
        }
        Debug.Log("Building Destroyed");
    }

    public void SetColor(int colorInt)
    {
        buildingColInt = colorInt;
        if (!townHall.activeSelf)
        {
            townHall.SetActive(true);
        }
        SetBuildingColor(colorInt);

        if (!isAI)
        {
            FogOfWarUnit fogu = gameObject.AddComponent<FogOfWarUnit>();
            fogu.circleRadius = 25f;
            fogu.team = buildingTeam;
        }
    }

    void SetBuildingColor(int colorInt)
    {
        for (int i = 0; i < MeshBuilding.Length; i++)
        {
            MeshBuilding[i].material = colorBuilding[colorInt];
        }
    }

    public void SetGameConsol()
    {
        gc = GameObject.FindGameObjectWithTag("GameConsol").GetComponent<GameConsol>();
        if (gc == null)
        {
            Debug.LogError("could not find gameconsol");
        }
    }

    public virtual void TurnOnUi()
    {
        if (BaseUi != null)
        {
            if (!isUiOpen)
            {
                gc.OffUi();
                BaseUi.TurnOnUi();
                ol.OutlineColor = Color.green;
                isUiOpen = true;
            }
            else
            {
                BaseUi.TurnOffUi();
                ol.OutlineColor = Color.white;
                isUiOpen = false;
            }
        }
    }
}
