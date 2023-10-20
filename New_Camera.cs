using UnityEngine;

public class New_Camera : MonoBehaviour {

    [SerializeField] private GameConsol _GC;
    public Transform camTransform;
    public float panSpeed = 20f;
    private float privSpeed;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 2f;
    public float maxZoom;
    public float minZoom;
    public Vector2 panLimitWidth;
    public Vector2 panLimitHeight;

    private Vector3 velco = Vector3.zero;
    public float smooth;
    public float smoothScrol;
    public Vector3 DesPos;

    public Vector3 BasePos;
    public Transform HeroTrans;

    private float zoom2;
    private float newZoom;
    private float CurZoom;
    private float veloZ = 0.0f;
    
    private void Start()
    {
        privSpeed = panSpeed;
        DesPos = transform.position;
        zoom2 = camTransform.localPosition.z;
        CurZoom = zoom2;
    }

    void Update ()
    {
        //float zoom = Camera.main.fieldOfView;
        float zoom = camTransform.localPosition.z;
        if (!_GC.isMenu && !_GC.isOptions)
        {
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness && Input.mousePosition.y < Screen.height)
            {
                DesPos.z += panSpeed * 10 * Time.deltaTime;
            }

            if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness && Input.mousePosition.y > 0)
            {
                DesPos.z -= panSpeed * 10 * Time.deltaTime;
            }

            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness && Input.mousePosition.x < Screen.width)
            {
                DesPos.x += panSpeed * 10 * Time.deltaTime;
            }

            if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness && Input.mousePosition.x > 0)
            {
                DesPos.x -= panSpeed * 10 * Time.deltaTime;
            }

            //if (Input.GetKey("q"))
            //{
            //    transform.eulerAngles -= new Vector3(0,(panSpeed * 2 * Time.deltaTime),0);
            //}s

            //if (Input.GetKey("e"))
            //{
            //    transform.eulerAngles += new Vector3(0, (panSpeed * 2 * Time.deltaTime), 0);
            //}

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                panSpeed = privSpeed * 2;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                panSpeed = privSpeed;
            }

            //if(Input.GetKeyDown(KeyCode.B))
            //{
            //    SetDesPos_Base();
            //}

            if (Input.GetKey(KeyCode.Space) && HeroTrans != null)
            {
                SetDesPos_Hero();
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            zoom2 -= scroll * scrollSpeed * 10 * Time.deltaTime;

            newZoom = Mathf.SmoothDamp(CurZoom, zoom2, ref veloZ, smoothScrol);

            if (newZoom < minZoom)
            {
                newZoom = minZoom;
                zoom2 = minZoom;
            }
            if (newZoom > maxZoom)
            {
                newZoom = maxZoom;
                zoom2 = maxZoom;
            }

            CurZoom = newZoom;
            camTransform.localPosition = new Vector3(0, 0, CurZoom);
        }
        DesPos.x = Mathf.Clamp(DesPos.x, panLimitWidth.x, panLimitWidth.y);
        DesPos.z = Mathf.Clamp(DesPos.z, panLimitHeight.x, panLimitHeight.y);


        Vector3 Lookpoint = new Vector3(DesPos.x, DesPos.y, ((DesPos.z) + 50f));
        float sa = Terrain.activeTerrain.SampleHeight(Lookpoint);
        DesPos.y = 40f + sa;

        transform.position = Vector3.SmoothDamp(transform.position, DesPos, ref velco, smooth);
        //Camera.main.fieldOfView = zoom;
    }

    public void SetDesPos(Vector3 poss)
    {
        DesPos = new Vector3(poss.x, transform.position.y, ((poss.z) - 50));
    }

    public void SetDesPos_Base()
    {
        DesPos = new Vector3(BasePos.x, transform.position.y, ((BasePos.z) - 50));
    }

    public void SetDesPos_Hero()
    {
        float fo = Terrain.activeTerrain.SampleHeight(DesPos);
        DesPos = new Vector3(HeroTrans.position.x, (40f + fo), ((HeroTrans.position.z) - 50));
    }
}
