using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Text tooltipTxt;
    private RectTransform tooltipRect;
    [SerializeField]
    private RectTransform backgroundRect;
    private Image img;

    private RectTransform rect;

    private void Awake()
    {
        instance = this;
        img = backgroundRect.GetComponent<Image>();
        rect = gameObject.GetComponent<RectTransform>();
        tooltipRect = tooltipTxt.gameObject.GetComponent<RectTransform>();
        //ShowTooltip("akljsflks flkajfkjs jklasjfkl ");
        //HideTooltip();
    }

    private void Update()
    {
       rect.position = Input.mousePosition;
    }

    private void ShowTooltip(string tooltipString)
    {
        tooltipTxt.text = tooltipString;
        float textPadding = 4f;
        Vector2 backgroundSize = new Vector2(tooltipTxt.preferredWidth + textPadding * 4f, tooltipTxt.preferredHeight + textPadding * 4f);
        tooltipRect.sizeDelta = backgroundSize;
        backgroundRect.sizeDelta = new Vector2(backgroundSize.x +4, backgroundSize.y);
        
        //gameObject.SetActive(true);
        Show();
    }

    private void HideTooltip()
    {
        tooltipTxt.enabled = false;
        img.enabled = false;
    }

    private void Show()
    {
        tooltipTxt.enabled = true;
        img.enabled = true;
    }

    public static void ShowTooltip_Static(string tooltipString)
    {
        if(instance != null)
        {
            instance.ShowTooltip(tooltipString);
        }
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }
}
