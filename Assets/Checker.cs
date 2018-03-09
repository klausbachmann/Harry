using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{

    public float moveTime = 1;
    public float offset = 1;
    private float time = 0;
    public float speed = 1;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("setTime", 1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;


        if (time >= offset)
        {
            move();
            offset += speed;
        }
    }

    void move()
    {
        transform.position = new Vector2(transform.position.x - 0.05f, transform.position.y);
    }

    void setTime()
    {
        if (speed > 0.1f)
        {
            speed -= 0.1f;
        }


    }

    void OnTriggerEnter(Collider c)
    {
        Debug.Log(c.name);
    }
}
