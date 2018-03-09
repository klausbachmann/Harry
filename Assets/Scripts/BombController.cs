using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombController : MonoBehaviour {

    public GameObject explosion;
    private float timer;
    public float bombTimer = 2;
    private GameObject harry;

    // Use this for initialization
    void Start () {
        harry = GameObject.Find("Harry");

    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        GetComponent<Rigidbody2D>().AddForce( new Vector2(-1, 0) );

        if (timer > bombTimer) {
            Debug.Log(Vector2.Distance(transform.position, harry.transform.position));
            int damage = 80 / Mathf.FloorToInt(Vector2.Distance(transform.position, harry.transform.position));

            harry.GetComponent<HarryController>().damage(damage);
            Destroy(gameObject);
        	Instantiate(explosion, transform.position, Quaternion.identity);
		}
    }

    void OnCollisionEnter2D(Collision2D hit) {
        Debug.Log("BOMB COLLISSION");
        
    }
}
