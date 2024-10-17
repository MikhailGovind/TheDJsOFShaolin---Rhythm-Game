using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGameManager : MonoBehaviour
{
    public AudioSource music;
    public bool startPlaying;
    public BeatScroller beatScroller;

    public static RhythmGameManager instance;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                beatScroller.hasStarted = true; 

                music.Play();
            }
        }
    }

    public void noteHit()
    {
        Debug.Log("Hit on time");
    }

    public void noteMissed()
    {
        Debug.Log("Missed Note");
    }
}
