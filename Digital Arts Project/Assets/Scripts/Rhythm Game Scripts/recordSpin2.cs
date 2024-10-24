using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recordSpin2 : MonoBehaviour
{
    public Vector3 rotation;
    public float speed;

    public bool goSpin;
    public bool onScratch;

    void Update()
    {
        if (goSpin)
        {
            transform.Rotate(rotation * speed * Time.deltaTime);
        }

        if (onScratch)
        {
            transform.Rotate(-rotation * speed * Time.deltaTime);
        }
    }
}
