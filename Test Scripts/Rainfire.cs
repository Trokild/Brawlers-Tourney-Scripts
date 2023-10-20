using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainfire : MonoBehaviour {

    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisonEvent;

	// Use this for initialization
	void Start ()
    {
        part = GetComponent<ParticleSystem>();
        collisonEvent = new List<ParticleCollisionEvent>();
	}

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("HIT");
    }
}
