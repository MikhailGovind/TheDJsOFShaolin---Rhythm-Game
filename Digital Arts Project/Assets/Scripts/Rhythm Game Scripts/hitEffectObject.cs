using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitEffectObject : MonoBehaviour
{
    public float objectLifetime;

    void Start()
    {
        objectLifetime = 1f;
    }

    void Update()
    {
        Destroy(gameObject, objectLifetime);
    }
}
