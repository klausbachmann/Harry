using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public GameObject bullet;

    public float nextFire = 2;
    public float fireRate = 1;
    public float time;

    private bool even;

    // Use this for initialization
    void Start () {
        GameObject cube = GameObject.Find("Mauer2");
        if (cube != null)
        {
            Transform[] all = cube.GetComponentsInChildren<Transform>();
            foreach (Transform child in all)
            {
                string s = child.name.Replace("Cube", "Mauer2");
                child.name = s;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        even = Mathf.FloorToInt(time) % 2 == 0;
        Debug.Log("TIME:" + Time.time + " DT: " + time);

        //transform.Translate(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 pos = new Vector3(Mathf.Clamp(transform.position.x + Input.GetAxisRaw("Horizontal")*Time.deltaTime*5, -9, 9), 
			transform.position.y, 0);

		
        transform.position = pos;
        

        if (Input.GetButton("Fire1")) {
            if (time > nextFire)
            {
                GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
                nextFire = time + fireRate;
            }
        }

    }
}
