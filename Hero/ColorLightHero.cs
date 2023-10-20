using UnityEngine;

public class ColorLightHero : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private Color[] colors;

    public void LightColorChange(int col)
    {
        if(_light != null)
        {
            _light.color = colors[col];
        }
    }
}
