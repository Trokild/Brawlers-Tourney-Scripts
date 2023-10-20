using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotator : MonoBehaviour {

    ParticleSystem ps;
    public Transform body;
	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ps.startRotation = body.transform.eulerAngles.y * Mathf.Deg2Rad;
	}
}
