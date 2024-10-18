using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmGameManager : MonoBehaviour
{
    [Header("Misc")]
    public AudioSource music;
    public bool startPlaying;
    public BeatScroller beatScroller;

    public static RhythmGameManager instance;

    [Header("Score")]
    public int currentScore;
    public int scorePerNote;
    public Text txtScore;

    [Header("Multiplier")]
    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;
    public Text txtMultiplier;

    void Start()
    {
        instance = this;
        scorePerNote = 100;
        currentScore = 0;
        txtScore.text = "Score: " + currentScore;
        currentMultiplier = 1;
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

        //if (currentMultiplier - 1 < multiplierThresholds.Length)
        //{
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        //}

        txtMultiplier.text = "x" + currentMultiplier;

        currentScore += scorePerNote * currentMultiplier;
        txtScore.text = "Score: " + currentScore;
    }

    public void noteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        txtMultiplier.text = "x" + currentMultiplier;
    }
}
