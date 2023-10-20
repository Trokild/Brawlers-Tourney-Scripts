using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class NameShower : MonoBehaviour {

    private Text tekt;
	// Use this for initialization
	void Start () {
        tekt = GetComponent<Text>();
	}
	
    public void TextName(string txt)
    {
        if(tekt != null)
        {
            tekt.text = txt;
        }
    }
}
