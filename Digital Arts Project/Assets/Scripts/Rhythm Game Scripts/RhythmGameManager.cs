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

    [Header("Crossfader")]
    public Slider sldrCrossfader;
    public KeyCode leftCrossFaderKey;
    public KeyCode rightCrossFaderKey;

    public Slider sldrCrossfaderGuide;
    public Image imgCrossfaderHandle;

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

        sldrCrossfader.value = 0.1f;
        sldrCrossfaderGuide.value = 0.5f;
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

        if (Input.GetKeyDown(leftCrossFaderKey))
        {
            sldrCrossfader.value = sldrCrossfader.value - 0.1f;
        }


        if (Input.GetKeyDown(rightCrossFaderKey))
        {
            sldrCrossfader.value = sldrCrossfader.value + 0.1f;
        }

        if (sldrCrossfader.value == sldrCrossfaderGuide.value)
        {
            imgCrossfaderHandle.color = new Color (39, 106, 63, 255);
        }
        else
        {
            imgCrossfaderHandle.color = new Vector4(200, 17, 0, 255);
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
