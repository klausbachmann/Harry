using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoboxController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, Time.deltaTime * -1, 0);
    }

    void OnTriggerEnter2D(Collider2D hit) {
        Debug.Log("BOX " + hit.name);
        if (hit.name.Equals("Harry")) {
            Debug.Log("INCREASE HEALTH");
            HarryController harry = GameObject.Find("Harry").GetComponent<HarryController>();

            harry.increaseHealth(25);
        }

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D hit) {
        Debug.Log("Box hitted " + hit.gameObject.name);
    }
}