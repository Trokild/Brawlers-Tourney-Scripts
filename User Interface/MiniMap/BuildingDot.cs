using System.Collections;
using UnityEngine;

public class BuildingDot : MapDot
{
    [SerializeField] private BuildingHealth bh;

    public void MapWarningDot()
    {
        StartCoroutine(MapVisuall());
    }


    private IEnumerator MapVisuall()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
        {
            GameObject.FindGameObjectWithTag("GameConsol").GetComponent<GameConsol>().SoundBaseAttacked();
        }

        for (int i = 0; i < 4; i++)
        {
            DotGo.localScale = new Vector3(2, 0, 2);
            ChangeDotColor(5);
            yield return new WaitForSeconds(0.5f);

            DotGo.localScale = new Vector3(1.5f, 0, 1.5f);
            ChangeDotColor(7);
            yield return new WaitForSeconds(0.5f);
        }
        ChangeDotColor(colInt);
        bh.isWarning = false;
    }

    public override void DotDead()
    {
        DotGo.localScale += new Vector3(2f, 0, 2f);
        DotGo.localPosition += new Vector3(0, 5f, 0);
        DotGo.parent = null;

        MapDotIcon.material = DeadMapDot;
        Invoke("DeadDotFinish", 3f);
    }

    protected override void DeadDotFinish()
    {
        DotGo.localScale = oldScale;
        DotGo.localPosition -= new Vector3(0, 1, 0);
    }
}
