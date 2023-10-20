using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using FoW;

public class MiniMap : MonoBehaviour {

    public Click clickRef; // Set on Click.StartLocalHost()
    [SerializeField] private SelectBox sb;
    private bool isOver;
    private Camera mapCam;
    private New_Camera newCam;
    private FogOfWarMinimap FoW_mm;
    private Vector2Int reso_mm;
    private bool rdy = false;
    public bool isFoW;
    private RectTransform rt;
    private Vector2 test;
    [SerializeField] private RawImage CamRawImg;
    public Canvas canvas;

	public void StartMiniMap (Player p)
    {
        rt = GetComponent<RectTransform>();
        mapCam = GameObject.FindGameObjectWithTag("MiniMapCam").GetComponent<Camera>();
        newCam = Camera.main.gameObject.GetComponentInParent<New_Camera>();
        Debug.Log(canvas.scaleFactor);

        if (isFoW)
        {
            FoW_mm = GetComponent<FogOfWarMinimap>();
            if (FoW_mm != null)
            {
                reso_mm = FoW_mm.StartMiniMap_FoW(p);
                if(CamRawImg != null)
                {
                    CamRawImg.color = Color.white;
                }
                rdy = true;
            }
        }
        else
        {
            rdy = true;
        }
    }

    void Update ()
    {
        if(!rdy)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(1) && isOver)
            {
                RaycastHit rHi;
                Ray ryi = mapCam.ScreenPointToRay(new Vector3((Input.mousePosition.x) - (rt.position.x), (Input.mousePosition.y) - (rt.position.y), 0));
                //Ray ryi = mapCam.ScreenPointToRay(new Vector3((Input.mousePosition.x) - reso_mm.x, (Input.mousePosition.y) - reso_mm.y, 0));
                //Debug.Log("HIT " + rHi.point);

                if (Physics.Raycast(ryi, out rHi) && rHi.transform.tag == "Ground")
                {
                    if (Input.GetKey(KeyCode.LeftAlt))
                    {
                        clickRef.ClickMoveOrder(rHi.point, true);
                    }
                    else
                    {
                        clickRef.ClickMoveOrder(rHi.point, false);
                    }
                }
            }

            if (Input.GetMouseButtonDown(0) && isOver)
            {
                RaycastHit rHi;
                Ray ryi = mapCam.ScreenPointToRay(new Vector3((Input.mousePosition.x / canvas.scaleFactor) - rt.position.x, (Input.mousePosition.y / canvas.scaleFactor) - rt.position.y, 0));
                //Ray ryi = mapCam.ScreenPointToRay(new Vector3((Input.mousePosition.x), (Input.mousePosition.y), 0));
                //Ray ryi = mapCam.ScreenPointToRay(new Vector3((Input.mousePosition.x) - reso_mm.x, (Input.mousePosition.y) - reso_mm.y, 0));
                //if(Physics.Raycast(ryi, out rHi))
                //{
                //    Debug.Log("HIT " + rHi.point);
                //}
                
                if (Physics.Raycast(ryi, out rHi) && rHi.transform.tag == "Ground")
                {
                    newCam.SetDesPos(rHi.point);
                }
            }
        }
        else
        {
            isOver = false;
        }

    }

    public void EnterPointer()
    {
        isOver = true;
    }

    public void ExitPointer()
    {
        isOver = false;
    }
}

