using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int health = 100;
    public int damage = 35;
    public int score = 100;
    public GameObject Blood;
    private bool isVisible = false;
    private Vector2 targetPosition;
    public GameObject Bomb;
    public bool bombThrowed = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isVisible)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * 2);
            //transform.Translate(-5 * Time.deltaTime, 0, 0);
            if (health > 100 && !bombThrowed)
            {
                Instantiate(Bomb, transform.position, Quaternion.identity);
                bombThrowed = true;
            }
        }
    }

    public void endAnimation() {
        Debug.Log("End animation");
    }

    public float hit() {
        health -= damage;
        GameObject blood = Instantiate(Blood, transform.position, Quaternion.identity);
        Destroy(blood, 1f);
        if (health <= 0) {
            Destroy(gameObject);
            return score;
        }

        return 0;


    }

    void OnCollisionEnter2D(Collision2D hit) {
        Debug.Log(hit.gameObject.name);
		
    }

    void OnBecameInvisible() {
        isVisible = false;
        Debug.Log(gameObject.name + " is invisible");
    }

	void OnBecameVisible() {
        isVisible = true;
        targetPosition = new Vector2(transform.position.x - 4, transform.position.y);
        Debug.Log(gameObject.name + " is visible");
        
        
    }
}
