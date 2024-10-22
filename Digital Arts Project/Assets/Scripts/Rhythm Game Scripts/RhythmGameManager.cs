using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmGameManager : MonoBehaviour
{
    [Header("Misc")]
    public AudioSource music;
    public bool startPlaying;
    public BeatScroller leftBeatScroller;
    public BeatScroller rightBeatScroller;
    public KeyCode scratchKeyCode;

    public static RhythmGameManager instance;

    [Header("Score")]
    public int currentScore;
    public int scorePerNote;
    public int scorePerGoodNote;
    public int scorePerPerfectNote;
    public Text txtScore;

    public int backgroundScore;

    [Header("Multiplier")]
    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;
    public Text txtMultiplier;

    public GameObject multiplierUpEffect, multiplierDownEffect;

    public bool multiplierEnum;

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
    public float crossfaderValue;
    public float crossfaderGuideValue;

    public Image imgCrossfaderHandle;
    public Sprite spriteCrossfaderHandleCorrect;
    public Sprite spriteCrossfaderHandle;

    public Image imgCrossfaderGuide;

    public int currentCrossfaderMultiplier;
    public float crossfaderMultiplierTracker;
    public int[] crossfaderMultiplierThresholds;
    public Text txtCrossfaderMultiplier;

    public float outOfZone;
    public bool crossfaderEnum;

    void Start()
    {
        instance = this;

        music.pitch = 1;

        scorePerNote = 100;
        scorePerGoodNote = 125;
        scorePerPerfectNote = 150;
        currentScore = 0;
        backgroundScore = 0;
        txtScore.text = currentScore.ToString();

        currentMultiplier = 1;
        multiplierEnum = true;

        totalNotes = FindObjectsOfType<NoteObject>().Length;

        sldrCrossfader.value = 1f;
        sldrCrossfaderGuide.value = 5f;

        currentCrossfaderMultiplier = 1;

        crossfaderEnum = true;
    }

    void Update()
    {
        #region Start music

        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                leftBeatScroller.hasStarted = true;
                rightBeatScroller.hasStarted = true;

                music.Play();
            }
        }

        #endregion

        #region Score
        
        if (currentScore >= 100)
        {
            if (backgroundScore >= 1000)
            {
                backgroundScore = 0;
                StartCoroutine(scoreTextUp());
            }
        }

        #endregion

        #region Crossfader

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

                    StartCoroutine(crossfaderTextUp());
                }
            }

            txtCrossfaderMultiplier.text = "x" + currentCrossfaderMultiplier;

            imgCrossfaderHandle.sprite = spriteCrossfaderHandleCorrect;
            imgCrossfaderGuide.color = Color.cyan;

            crossfaderEnum = false;
        }
        else
        {
            outOfZone += Time.deltaTime;

            if (outOfZone >= 2f)
            {
                outOfZone = 0f;
                currentCrossfaderMultiplier = 1;
                crossfaderMultiplierTracker = 0;

                if (!crossfaderEnum)
                {
                    StartCoroutine(crossfaderTextDown());
                }
            }

            txtCrossfaderMultiplier.text = "x" + currentCrossfaderMultiplier;
            imgCrossfaderHandle.sprite = spriteCrossfaderHandle;

            imgCrossfaderGuide.color = Color.grey;
        }

        if (Input.GetKeyDown(leftCrossFaderKey))
        {
            sldrCrossfader.value = sldrCrossfader.value - 1f;
        }


        if (Input.GetKeyDown(rightCrossFaderKey))
        {
            sldrCrossfader.value = sldrCrossfader.value + 1f;
        }

        if (sldrCrossfader.value >= 10f)
        {
            sldrCrossfader.value = 10f;
        }

        crossfaderValue = sldrCrossfader.value;
        crossfaderGuideValue = sldrCrossfaderGuide.value;

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

                multiplierEnum = false;

                StartCoroutine(multiplierTextUp());
            }
        }

        CameraShake.Shake(duration:0.1f, strength:0.1f);

        txtMultiplier.text = "x" + currentMultiplier;

        //currentScore += scorePerNote * currentMultiplier;
        txtScore.text = currentScore.ToString();
    }

    public IEnumerator scoreTextUp()
    {
        CameraShake.Shake(duration: 0.2f, strength: 0.2f);

        txtScore.fontSize = 200;
        txtScore.color = Color.cyan;

        Instantiate(multiplierUpEffect, txtScore.transform.position, txtScore.transform.rotation);

        yield return new WaitForSeconds(0.5f);

        txtScore.fontSize = 150;
        txtScore.color = Color.white;
    }

    public IEnumerator multiplierTextUp()
    {
        CameraShake.Shake(duration: 0.2f, strength: 0.2f);

        txtMultiplier.fontSize = 200;
        txtMultiplier.color = Color.cyan;

        Instantiate(multiplierUpEffect, txtMultiplier.transform.position, txtMultiplier.transform.rotation);

        yield return new WaitForSeconds(0.5f);

        txtMultiplier.fontSize = 150;
        txtMultiplier.color = Color.white;
    }

    public IEnumerator multiplierTextDown()
    {
        CameraShake.Shake(duration: 0.3f, strength: 0.3f);

        txtMultiplier.fontSize = 200;
        txtMultiplier.color = Color.red;
        Instantiate(multiplierDownEffect, txtMultiplier.transform.position, txtMultiplier.transform.rotation);

        yield return new WaitForSeconds(0.5f);

        txtMultiplier.fontSize = 150;
        txtMultiplier.color = Color.white;
    }

    public IEnumerator crossfaderTextUp()
    {
        CameraShake.Shake(duration: 0.2f, strength: 0.2f);

        txtCrossfaderMultiplier.fontSize = 175;
        txtCrossfaderMultiplier.color = Color.cyan;

        Instantiate(multiplierUpEffect, txtCrossfaderMultiplier.transform.position, txtCrossfaderMultiplier.transform.rotation);

        yield return new WaitForSeconds(0.5f);

        txtCrossfaderMultiplier.fontSize = 125;
        txtCrossfaderMultiplier.color = Color.white;
    }

    public IEnumerator crossfaderTextDown()
    {
        CameraShake.Shake(duration: 0.3f, strength: 0.3f);

        txtCrossfaderMultiplier.fontSize = 175;
        txtCrossfaderMultiplier.color = Color.red;
        Instantiate(multiplierDownEffect, txtCrossfaderMultiplier.transform.position, txtCrossfaderMultiplier.transform.rotation);

        yield return new WaitForSeconds(0.5f);

        txtCrossfaderMultiplier.fontSize = 125;
        txtCrossfaderMultiplier.color = Color.white;

        crossfaderEnum = true;
    }

    public void normalHit()
    {
        currentScore += scorePerNote * currentMultiplier * currentCrossfaderMultiplier;
        backgroundScore += scorePerNote * currentMultiplier * currentCrossfaderMultiplier;
        noteHit();

        normalNoteHits++;
    }

    public void goodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier * currentCrossfaderMultiplier;
        backgroundScore += scorePerGoodNote * currentMultiplier * currentCrossfaderMultiplier;
        noteHit();

        goodNoteHits++;
    }

    public void perfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier * currentCrossfaderMultiplier;
        backgroundScore += scorePerPerfectNote * currentMultiplier * currentCrossfaderMultiplier;
        noteHit();

        perfectNoteHits++;
    }

    public void noteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        txtMultiplier.text = "x" + currentMultiplier;

        if (!multiplierEnum)
        {
            StartCoroutine(multiplierTextDown());

            multiplierEnum = true;
        }

        missedNotes++;
    }
}
