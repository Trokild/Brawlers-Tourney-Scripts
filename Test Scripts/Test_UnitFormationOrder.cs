using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_UnitFormationOrder : MonoBehaviour
{
    public GameObject[] gunits;
    public List<GameObject> lunits;
    public bool listu;
    [SerializeField]
    private LayerMask clickAbleLayer;
    Camera cam;
    private Vector3 total;
    private Vector3 total2;
    public Transform clicMid;
    public int col;
    public float sideDist;
    public float radDist;
    private float offset;
    public enum Formation { NoFormation, KeepFormation, BoxFormation }
    public Formation curForm;

    void Start()
    {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit rayHit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            switch(curForm)
            {
                case Formation.BoxFormation:
                    if (!listu)
                    {
                        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, clickAbleLayer))
                        {
                            Vector3 dir2 = (rayHit.point - clicMid.position).normalized;
                            Vector3 dir = (clicMid.position - rayHit.point).normalized;
                            clicMid.position = rayHit.point;
                            clicMid.rotation = Quaternion.LookRotation(dir);

                            if (gunits.Length > 0)
                            {
                                if (gunits.Length < col + 1)
                                {
                                    col = gunits.Length - 1;
                                }

                                if (gunits.Length > 12)
                                {
                                    col = 5;
                                }

                                if (gunits.Length > 24)
                                {
                                    col = 7;
                                }

                                if (gunits.Length > 36)
                                {
                                    col = 9;
                                }

                                offset = ((sideDist * col) / 2) * -1;
                                bool first = false;
                                int curCol = 0;
                                for (int i = 0; i < gunits.Length; i++)
                                {
                                    gunits[i].transform.SetParent(clicMid);
                                    if (first)
                                    {
                                        if (curCol >= col)
                                        {
                                            total += new Vector3(0, 0, radDist);
                                            total.x = 0;
                                            curCol = 0;
                                        }
                                        else
                                        {
                                            curCol += 1;
                                            total += new Vector3(sideDist, 0, 0);
                                        }
                                    }
                                    else
                                    {
                                        first = true;
                                    }

                                    Vector3 startPos = new Vector3(total.x + offset, 0, total.z);
                                    //gunits[i].transform.position = (rayHit.point + startPos);
                                    gunits[i].transform.localPosition = startPos;
                                    gunits[i].transform.rotation = Quaternion.LookRotation(dir2);
                                    gunits[i].transform.parent = null;
                                }
                                total = Vector3.zero;
                            }
                        }
                    }
                    else
                    {
                        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, clickAbleLayer))
                        {
                            Vector3 dir2 = (rayHit.point - clicMid.position).normalized;
                            Vector3 dir = (clicMid.position - rayHit.point).normalized;
                            clicMid.position = rayHit.point;
                            clicMid.rotation = Quaternion.LookRotation(dir);

                            if (lunits.Count > 0)
                            {
                                if (lunits.Count < col + 1)
                                {
                                    col = gunits.Length - 1;
                                }

                                if (lunits.Count > 12)
                                {
                                    col = 5;
                                }

                                if (lunits.Count > 24)
                                {
                                    col = 7;
                                }

                                if (lunits.Count > 36)
                                {
                                    col = 9;
                                }

                                offset = ((sideDist * col) / 2) * -1;
                                bool first = false;
                                int curCol = 0;
                                for (int i = 0; i < lunits.Count; i++)
                                {
                                    lunits[i].transform.SetParent(clicMid);
                                    if (first)
                                    {
                                        if (curCol >= col)
                                        {
                                            total += new Vector3(0, 0, radDist);
                                            total.x = 0;
                                            curCol = 0;
                                        }
                                        else
                                        {
                                            curCol += 1;
                                            total += new Vector3(sideDist, 0, 0);
                                        }
                                    }
                                    else
                                    {
                                        first = true;
                                    }

                                    Vector3 startPos = new Vector3(total.x + offset, 0, total.z);
                                    //gunits[i].transform.position = (rayHit.point + startPos);
                                    lunits[i].transform.localPosition = startPos;
                                    lunits[i].transform.rotation = Quaternion.LookRotation(dir2);
                                    lunits[i].transform.parent = null;
                                }
                                total = Vector3.zero;
                            }
                        }
                    }
                    break;

                case Formation.NoFormation:
                    if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, clickAbleLayer))
                    {
                        for (int i = 0; i < gunits.Length; i++)
                        {
                            gunits[i].transform.position = rayHit.point;
                        }
                    }
                        break;

                case Formation.KeepFormation:
                    if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, clickAbleLayer))
                    {
                        if (gunits.Length > 0)
                        {
                            for (int i = 0; i < gunits.Length; i++)
                            {
                                total2 += gunits[i].transform.position;
                            }

                            Vector3 center = total2 / gunits.Length;

                            for (int i = 0; i < gunits.Length; i++)
                            {
                                Vector3 startPos = gunits[i].transform.position - center;
                                gunits[i].transform.position = rayHit.point + startPos;
                                total2 = Vector3.zero;
                            }
                        }
                    }
                    break;
            }


        }
    }
}
