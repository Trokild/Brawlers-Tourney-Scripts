using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNoobCam : MonoBehaviour
{
    public float Speed = 100;
    public Transform cam;
    public float leftPos, rightPos, upPos, downPos;
    public static bool camScroll = true, camMove = true;
    public float panBorderThickness = 10f;
    public float minCam, maxCam;

    void Update()
    {
        if (camMove)
        {
            if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness && Input.mousePosition.y > 0)
            {
                transform.position += transform.forward * Time.deltaTime * Speed;
            }

            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness && Input.mousePosition.y < Screen.height)
            {
                transform.position -= transform.forward * Time.deltaTime * Speed;
            }

            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness && Input.mousePosition.x < Screen.width)
            {
                transform.position += transform.right * Time.deltaTime * Speed;
            }

            if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness && Input.mousePosition.x > 0)
            {
                transform.position -= transform.right * Time.deltaTime * Speed;
            }
        }
    }
}
