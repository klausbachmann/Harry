using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour {

    public float speed;
    public float damage = 2;
    public GameObject explosion;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, Time.deltaTime * speed, 0);
	}

    void OnCollisionEnter(Collision hit) {
        Debug.Log(hit.gameObject.name);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider c) {
        Debug.Log(c.name);
        GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);
        GameObject.Find(c.name).GetComponent<MeshCollider>().isTrigger = false;
        GameObject.Find(c.name).GetComponent<Rigidbody>().useGravity = true;
        //GameObject.Find(c.name).GetComponent<Rigidbody>().AddForce(new Vector3(2, 4, 0), ForceMode.Impulse);
        GameObject.Find(c.name).GetComponent<Rigidbody>().velocity = new Vector3(0, damage, -5);
       // Destroy(e, 2.5f);
    }
}
