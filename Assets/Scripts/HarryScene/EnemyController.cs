using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int health = 100;
    public int damage = 35;
    public int score = 100;
    public GameObject Blood;
    private bool isVisible = false;
    private Vector2 targetPosition;
    public GameObject Bomb;
    public bool bombThrowed = false;
    public float enemySpeed = 2;
    public int makeDamageToHarry = 5;
    private float time;
    private float enemyFireRate = 2;
    private float offset;


    // Use this for initialization
    void Start()
    {
        // Gegner darf erst nach einer bestimmten Zeit schiessen
        offset = time + 2;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (isVisible)
        {
            // Gegner bewegen
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * enemySpeed);

            


            // Gegner ist Bombenwerfer
            if (health > 100 && !bombThrowed)
            {
                Instantiate(Bomb, transform.position, Quaternion.identity);
                bombThrowed = true;
            }

            if (time > offset)
            {
                RaycastHit2D hit2d;
                hit2d = Physics2D.Raycast(transform.position, Vector2.left, 15f);

                if (hit2d.collider != null)
                {
                    Debug.Log(hit2d.collider.name);
                    HarryController harry = GameObject.Find("Harry").GetComponent<HarryController>();

                    harry.damage(makeDamageToHarry);
                    offset = time + enemyFireRate;
                }
            }
        }
    }


    public void endAnimation()
    {
        Debug.Log("End animation");
    }


    public float hit()
    {
        health -= damage;
        GameObject blood = Instantiate(Blood, transform.position, Quaternion.identity);
        Destroy(blood, 1f);
        if (health <= 0)
        {
            Destroy(gameObject);
            return score;
        }

        return 0;

    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        Debug.Log(hit.gameObject.name);

    }

    void OnBecameInvisible()
    {
        isVisible = false;
        Debug.Log(gameObject.name + " is invisible");
    }

    void OnBecameVisible()
    {
        isVisible = true;
        targetPosition = new Vector2(transform.position.x - 4, transform.position.y);
        Debug.Log(gameObject.name + " is visible");


    }
}
