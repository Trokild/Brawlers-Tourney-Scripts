using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_NewOutline : MonoBehaviour
{

    public void NewOuline()
    {
        Outline ot = GetComponent<Outline>();
        if (ot == null)
        {
            Outline outline = gameObject.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.green;
            outline.OutlineWidth = 1f;
        }
        else
        {
            Destroy(ot);
            Outline outline = gameObject.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.green;
            outline.OutlineWidth = 1f;
        }

    }
}
