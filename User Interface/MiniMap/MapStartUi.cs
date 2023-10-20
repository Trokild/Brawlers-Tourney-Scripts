using UnityEngine.UI;
using UnityEngine;

public class MapStartUi : MonoBehaviour
{
    [SerializeField] private Color[] startCol;
    [SerializeField] private Image[] basImg;
    [SerializeField] private Image mapImage;
    
    public void SetPlayerMap(int pos, int col)
    {
        basImg[pos].enabled = true;
        basImg[pos].color = startCol[col];
    }

    public void ChangePosMap(int oldP, int newP, int col)
    {
        basImg[oldP].enabled = false;
        basImg[newP].enabled = true;
        basImg[newP].color = startCol[col];
    }

    public void NewColMap(int pos, int col)
    {
        basImg[pos].color = startCol[col];
    }

    public void RemovePlayerMap(int pos)
    {
        basImg[pos].enabled = false;
    }
}
