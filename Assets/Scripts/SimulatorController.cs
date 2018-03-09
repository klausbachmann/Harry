using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace burglar
{
    public class SimulatorController : MonoBehaviour
    {


        private MobilityController m;
        public float speed = 2;
        private int current = 0;
        public float time = 0;
        public float offset = 0;
        public bool waiting = false;


        public Text text;

        // Use this for initialization
        void Start()
        {
            text = GameObject.Find("Text").GetComponent<Text>();
            m = GameObject.Find("Sphere").GetComponent<MobilityController>();
        }

        void Update() {
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            time = time + Time.deltaTime;
            float step = speed * Time.deltaTime;

            if (time > offset)
            {
                if (current < m.mBurglarAction.Count)
                {
                    if (m.mBurglarAction[current].mAction == Actions.move)
                    {
                        if (m.mBurglarAction[current].mMovePosition != transform.position)
                        {
                            text.text = current + " BEWEGEN";
                            transform.position = Vector3.MoveTowards(transform.position, m.mBurglarAction[current].mMovePosition, step);
                        }
                        else
                        {
                            text.text = current + "Ziel erreicht";
                            current++;
                        }
                    }

                    if (m.mBurglarAction[current].mAction == Actions.wait)
                    {
                        offset = time + m.mBurglarAction[current].mWaitSeconds;
                        current++;
                    }

                    if (m.mBurglarAction[current].mAction == Actions.open) {
                        offset = time + m.mBurglarAction[current].mWaitSeconds;
                        current++;
                    }
                }
            }
        }
    }
}