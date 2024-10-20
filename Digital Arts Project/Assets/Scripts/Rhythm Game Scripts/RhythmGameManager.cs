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
    public KeyCode scratchKeyCode;

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

    public int currentCrossfaderMultiplier;
    public float crossfaderMultiplierTracker;
    public int[] crossfaderMultiplierThresholds;
    public Text txtCrossfaderMultiplier;

    public float outOfZone;

    void Start()
    {
        instance = this;

        music.pitch = 1;

        scorePerNote = 100;
        scorePerGoodNote = 125;
        scorePerPerfectNote = 150;
        currentScore = 0;
        txtScore.text = "Score: " + currentScore;
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;

        sldrCrossfader.value = 0.1f;
        sldrCrossfaderGuide.value = 0.5f;

        currentCrossfaderMultiplier = 1;
    }

    void Update()
    {
        #region Start music

        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                beatScroller.hasStarted = true;

                music.Play();
            }
        }

        #endregion

        #region Crossfader

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
            outOfZone = 0f; 

            if (currentCrossfaderMultiplier - 1 < crossfaderMultiplierThresholds.Length)
            {
                crossfaderMultiplierTracker += Time.deltaTime;

                if (crossfaderMultiplierThresholds[currentCrossfaderMultiplier - 1] <= crossfaderMultiplierTracker)
                {
                    crossfaderMultiplierTracker = 0;
                    currentCrossfaderMultiplier++;
                }
            }

            txtCrossfaderMultiplier.text = "x" + currentCrossfaderMultiplier;

            imgCrossfaderHandle.color = new Color(39, 106, 63, 255);
        }
        else
        {
            outOfZone += Time.deltaTime;

            if (outOfZone >= 2f)
            {
                outOfZone = 0f;
                currentCrossfaderMultiplier = 1;
                crossfaderMultiplierTracker = 0;
            }

            txtCrossfaderMultiplier.text = "x" + currentCrossfaderMultiplier;

            imgCrossfaderHandle.color = new Vector4(200, 17, 0, 255);
        }

        #endregion

        #region Scratch

        if (Input.GetKeyDown(scratchKeyCode))
        {
            music.pitch = -1;
        }

        if (Input.GetKeyUp(scratchKeyCode))
        {
            music.pitch = 1;
        }

        #endregion
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

        CameraShake.Shake(duration:0.1f, strength:0.1f);

        txtMultiplier.text = "x" + currentMultiplier;

        //currentScore += scorePerNote * currentMultiplier;
        txtScore.text = "Score: " + currentScore;
    }

    public void normalHit()
    {
        currentScore += scorePerNote * currentMultiplier * currentCrossfaderMultiplier;
        noteHit();

        normalNoteHits++;
    }

    public void goodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier * currentCrossfaderMultiplier;
        noteHit();

        goodNoteHits++;
    }

    public void perfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier * currentCrossfaderMultiplier;
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
