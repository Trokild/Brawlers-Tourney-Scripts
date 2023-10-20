using UnityEngine;

public class SpellCtrl_SwipeCol : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCol;
    private Vector2Int teamId;
    private Vector2Int dmgAp;

    public void SetUpSwipeCol(Vector2Int timId, Vector2Int dp)
    {
        teamId = timId;
        dmgAp = dp;

        if(boxCol != null)
        {
            boxCol.enabled = true;
        }
        else
        {
            boxCol = GetComponent<BoxCollider>();
            boxCol.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);
        Health hl = other.GetComponent<Health>();
        if (hl != null)
        {
            if (hl.healthTeam != teamId.x)
            {
                hl.TakeDamage(dmgAp.x, dmgAp.y, 0, teamId.y);
            }
        }
    }
}
