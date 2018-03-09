using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace burglar
{
    public class MobilityController : MonoBehaviour
    {

        public List<Vector3> waypoint;
        public List<BurglarAction> mBurglarAction;

        public GameObject simulator;

        // Use this for initialization
        void Start()
        {
            waypoint = new List<Vector3>();
            mBurglarAction = new List<BurglarAction>();
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(mBurglarAction.Count);
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
                waypoint.Add(transform.position);
                mBurglarAction.Add(new BurglarAction(Actions.move, transform.position));
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                waypoint.Add(transform.position);
                mBurglarAction.Add(new BurglarAction(Actions.move, transform.position));
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
                waypoint.Add(transform.position);
                mBurglarAction.Add(new BurglarAction(Actions.move, transform.position));
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
                waypoint.Add(transform.position);
                mBurglarAction.Add(new BurglarAction(Actions.move, transform.position));
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                mBurglarAction.Add(new BurglarAction(Actions.wait, 3));
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                mBurglarAction.Add(new BurglarAction(Actions.open, 10));
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(simulator, new Vector3(0, -4, 0), Quaternion.identity);
            }



        }
    }
}