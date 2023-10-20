using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MapBorder : MonoBehaviour {

    [SerializeField]private Camera cam;
    public RectTransform canv;

    Mesh mesh;
    public Vector3[] vertices;
    float depth;

    public Vector3 upperLeftScreen;
    public Vector3 upperRightScreen;
    public Vector3 lowerLeftScreen;
    public Vector3 lowerRightScreen;

    public Vector3 upperLeft;
    public Vector3 upperRight;
    public Vector3 lowerLeft;
    public Vector3 lowerRight;

    void Start()
    {
        //cam = Camera.main;
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        SetPlane();
    }

    private void Update()
    {
        mesh.vertices = vertices;
        SetPlane();
    }
    // Update is called once per frame
    void SetPlane ()
    {
        depth = (transform.position.y - cam.transform.position.y);

        upperLeftScreen = new Vector3(0, canv.rect.height, 60);
        upperRightScreen = new Vector3(canv.rect.width, canv.rect.height, 60);
        lowerLeftScreen = new Vector3(0, 0, 60);
        lowerRightScreen = new Vector3(canv.rect.width, 0, 60);

        //Corner locations in world coordinates
        upperLeft = cam.ScreenToWorldPoint(upperLeftScreen);
        upperRight = cam.ScreenToWorldPoint(upperRightScreen);
        lowerLeft = cam.ScreenToWorldPoint(lowerLeftScreen);
        lowerRight = cam.ScreenToWorldPoint(lowerRightScreen);

        //upperLeft.y = upperRight.y = lowerLeft.y = lowerRight.y = ship.transform.position.y

        vertices[0] = upperLeft;
        vertices[1] = lowerRight;
        vertices[2] = lowerLeft;
        vertices[3] = upperRight;

        mesh.vertices = vertices;
        //mesh.vertices[0] = upperLeft;
        //mesh.vertices[1] = upperRight;
        //mesh.vertices[2] = lowerLeft;
        //mesh.vertices[3] = lowerRight;
    }

}
