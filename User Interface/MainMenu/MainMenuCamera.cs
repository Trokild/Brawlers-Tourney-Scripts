using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public float val1;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, val1 * Time.deltaTime, 0 );
    }

    public void SceneOut()
    {
        anim.SetTrigger("FadeOut");
    }
}
