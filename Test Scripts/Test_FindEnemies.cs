using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_FindEnemies : MonoBehaviour {

    public float secLook;
    private float secLeft;

    public float radius;
    public GameObject nearestEnemie;
    public float shortestDist = Mathf.Infinity;

    [SerializeField]
    private LayerMask clickAbleLayer;

    public bool canSearch;
	// Use this for initialization
	void Start ()
    {
        secLeft = secLook;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(canSearch)
        {
            secLeft -= Time.deltaTime;
            if(secLeft <= 0)
            {
                FindEnemies2();
                secLeft = secLook;
            }
        }
	}

    void FindEnemies2()
    {
        Debug.Log("Search fuck");
        nearestEnemie = null;
        shortestDist = Mathf.Infinity;

        Collider[] hitColi = Physics.OverlapSphere(transform.position, radius, clickAbleLayer);
        if(hitColi.Length > 0)
        {
            int i = 0;
            while (i < hitColi.Length)
            {
                GameObject ene = hitColi[i].gameObject;
                if (ene != null & ene != this.gameObject)
                {
                    float distance = Vector3.Distance(transform.position, ene.transform.position);

                    if (distance < shortestDist)
                    {
                        shortestDist = distance;
                        nearestEnemie = ene;
                    }
                }
                i++;
            }
        }
        else
        {
            Debug.Log("No fucks");
        }

    }
}
