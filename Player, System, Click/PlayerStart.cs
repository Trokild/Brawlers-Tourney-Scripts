using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    public string myName;
    public bool st_isLocal;
    public bool isActivated;

    [Range(0, 3)]
    public int st_IdPlayer;

    [Range(0, 1)]
    public int st_Hero;

    public int st_TeamInt;

    [Range(1, 4)]
    public int st_PosInt;

    [Range(0, 7)]
    public int st_ColorInt;

}
