using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find("SmallExplosionEffect").GetComponent<ParticleSystem>().Play();
		GameObject.Find("Light").GetComponent<ParticleSystem>().Play();
		GameObject.Find("Embers").GetComponent<ParticleSystem>().Play();
		GameObject.Find("Fire").GetComponent<ParticleSystem>().Play();

        Destroy(gameObject, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {

    }
}
