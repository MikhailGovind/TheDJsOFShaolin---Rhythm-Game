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
    public int scorePerGoodNote;
    public int scorePerPerfectNote;
    public Text txtScore;

    [Header("Multiplier")]
    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;
    public Text txtMultiplier;

    [Header("Results")]
    public float totalNotes;
    public float normalNoteHits;
    public float goodNoteHits;
    public float perfectNoteHits;
    public float missedNotes;

    void Start()
    {
        instance = this;
        scorePerNote = 100;
        scorePerGoodNote = 125;
        scorePerPerfectNote = 150;
        currentScore = 0;
        txtScore.text = "Score: " + currentScore;
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
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

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        txtMultiplier.text = "x" + currentMultiplier;

        //currentScore += scorePerNote * currentMultiplier;
        txtScore.text = "Score: " + currentScore;
    }

    public void normalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        noteHit();

        normalNoteHits++;
    }

    public void goodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        noteHit();

        goodNoteHits++;
    }

    public void perfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        noteHit();

        perfectNoteHits++;
    }

    public void noteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        txtMultiplier.text = "x" + currentMultiplier;

        missedNotes++;
    }
}
