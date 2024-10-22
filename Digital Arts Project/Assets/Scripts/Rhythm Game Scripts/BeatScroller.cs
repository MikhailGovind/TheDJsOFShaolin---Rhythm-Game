using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted;

    public bool leftBeatScroller;
    public bool rightBeatScroller;

    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    void Update()
    {
        if (!hasStarted)
        {
            //if (Input.anyKeyDown)
            //{ 
            //    hasStarted = true;
            //}
        }
        else
        {
            if (leftBeatScroller)
            {
                transform.position -= new Vector3(-beatTempo * Time.deltaTime, 0f, 0f);

            }
            else
            {
                transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
            }
        }
    }
}
