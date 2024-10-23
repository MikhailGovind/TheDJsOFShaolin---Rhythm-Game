using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recordSpin : MonoBehaviour
{
    public Vector3 rotation;
    public float speed;

    public bool goSpin;

    void Update()
    {
        if (goSpin)
        {
            transform.Rotate(rotation * speed * Time.deltaTime);
        }
    }
}
