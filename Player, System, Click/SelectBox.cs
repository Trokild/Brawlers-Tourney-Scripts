using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectBox : MonoBehaviour {

    [SerializeField]
	private RectTransform selectSquareImage;

    private GraphicRaycaster _graphicRaycaster;
    private EventSystem _eventSystem;
    PointerEventData _pofloaterEvent;

    Vector3 startPos;
	Vector3 endPos;


    void Start () 
	{
        //selectSquareImage = GetComponent<RectTransform>();
        selectSquareImage.gameObject.SetActive (false);
        _eventSystem = EventSystem.current;
        _graphicRaycaster = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GraphicRaycaster>();
    }
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetMouseButtonDown (0)) 
		{
			RaycastHit hit;

			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity)) 
			{
				startPos = hit.point;
			}

            if (!selectSquareImage.gameObject.activeInHierarchy && !_eventSystem.IsPointerOverGameObject())
            {
                selectSquareImage.gameObject.SetActive(true);
            }
        }	

		if (Input.GetMouseButtonUp (0)) 
		{
			selectSquareImage.gameObject.SetActive (false);
		}

		if (Input.GetMouseButton (0)) 
		{
			endPos = Input.mousePosition;

			Vector3 squareStart = Camera.main.WorldToScreenPoint (startPos);
			squareStart.z = 0f;

			Vector3 centre = (squareStart + endPos) / 2f;

			selectSquareImage.position = centre;

			float sizeX = Mathf.Abs (squareStart.x - endPos.x);
			float sizeY = Mathf.Abs (squareStart.y - endPos.y);

			selectSquareImage.sizeDelta = new Vector2 (sizeX, sizeY);
		}

    }

}
