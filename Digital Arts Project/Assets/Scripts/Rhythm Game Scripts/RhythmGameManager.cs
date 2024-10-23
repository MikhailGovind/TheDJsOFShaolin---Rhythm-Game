using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmGameManager : MonoBehaviour
{
    #region Variables

    [Header("Misc")]
    public AudioSource music;
    public bool startPlaying;
    public BeatScroller leftBeatScroller;
    public BeatScroller rightBeatScroller;
    public KeyCode scratchKeyCode;

    public static RhythmGameManager instance;
    public recordSpin recordSpin;
    public recordSpin2 recordSpin2;

    public bool gameState1; //pick records
    public bool gamestate2;

    [Header("Gamestate 2")]
    public GameObject eqKnobsButtons, leftNoteHolder, rightNoteHolder, pnlOverlay, pnlCrossfader; 

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

    [Header("Records")]
    //left record
    public GameObject objLeftRecord;
    public int left_songNumber;
    public int left_maxSongNumber;
    public string left_recordTitle;
    public string left_artistName;
    public int left_bpm;

    public Image img_leftRecord;

    public KeyCode left_SongNext;

    public Text txt_left_recordTitle;
    public Text txt_left_artistName;
    public Text txt_left_bpm;

    //right record
    public GameObject objRightRecord;
    public int right_songNumber;
    public int right_maxSongNumber;
    public string right_recordTitle;
    public string right_artistName;
    public int right_bpm;

    public Image img_rightRecord;

    public KeyCode right_SongNext;

    public Text txt_right_recordTitle;
    public Text txt_right_artistName;
    public Text txt_right_bpm;

    //both records
    public GameObject objMatchingBPMs;
    public bool matchingBPMs;
    public Vector3 recordSpinRotation;
    public float recordSpinSpeed;

    //song sprites
    public Sprite sprite_loveSong28;
    public Sprite sprite_typeShi;

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

    [Header("Scratch")]
    public float scratchBonus;
    public float scratchPoints;
    public float scratchKeyTimeDown;
    public bool scratchKeyDown;

    public bool scratchQte;

    public float scratchRandomizer1;
    public int scratchRandomizer2;

    public GameObject scratchOverlay;

    #endregion

    void Start()
    {
        //game management
        instance = this;

        music.pitch = 1;

        gameState1 = true;
        gamestate2 = false;

        eqKnobsButtons.SetActive(false);
        leftNoteHolder.SetActive(false);
        rightNoteHolder.SetActive(false);
        pnlOverlay.SetActive(false);
        pnlCrossfader.SetActive(false);

        //score
        scorePerNote = 100;
        scorePerGoodNote = 125;
        scorePerPerfectNote = 150;
        currentScore = 0;
        backgroundScore = 0;
        txtScore.text = currentScore.ToString();

        //multiplier
        currentMultiplier = 1;
        multiplierEnum = true;

        totalNotes = FindObjectsOfType<NoteObject>().Length;

        //crossfader
        sldrCrossfader.value = 1f;
        sldrCrossfaderGuide.value = 5f;

        currentCrossfaderMultiplier = 1;

        crossfaderEnum = true;

        //records
        left_songNumber = 1;
        right_songNumber = 2;

        matchingBPMs = true;

        //scratch
        scratchKeyDown = false;
        scratchQte = false;
        scratchOverlay.SetActive(false);
    }

    void Update()
    {
        #region Start music

        if (!startPlaying && gamestate2)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                leftBeatScroller.hasStarted = true;
                rightBeatScroller.hasStarted = true;

                music.Play();

                recordSpin.goSpin = true;
                recordSpin2.goSpin = true;
            }
        }

        if (gamestate2)
        {
            eqKnobsButtons.SetActive(true);
            leftNoteHolder.SetActive(true);
            rightNoteHolder.SetActive(true);
            pnlOverlay.SetActive(true);
            pnlCrossfader.SetActive(true);
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

        #region Records

        txt_left_recordTitle.text = left_recordTitle.ToString();
        txt_left_artistName.text = left_artistName.ToString();
        txt_left_bpm.text = "BPM: " + left_bpm;

        if (gameState1)
        {
            if (Input.GetKeyDown(left_SongNext))
            {
                if (left_songNumber == (right_songNumber - 1))
                {
                    left_songNumber = left_songNumber + 2;
                }
                else
                {
                    left_songNumber++;
                }

                StartCoroutine(leftSongTextUp());

                matchingBPMs = false;

                if (left_songNumber == left_maxSongNumber + 1)
                {
                    left_songNumber = 1;
                }

                StartCoroutine(spinLeftRecord());
            }
        }

        if (left_songNumber == 1)
        {
            left_recordTitle = "Love Song 28";
            left_artistName = "Jullian Gomes";
            left_bpm = 120;
            img_leftRecord.sprite = sprite_loveSong28;
        }

        if (left_songNumber == 2)
        {
            left_recordTitle = "Type Shi";
            left_artistName = "I";
            left_bpm = 80;
            img_leftRecord.sprite = sprite_typeShi;
        }

        txt_right_recordTitle.text = right_recordTitle.ToString();
        txt_right_artistName.text = right_artistName.ToString();
        txt_right_bpm.text = "BPM: " + right_bpm;

        if (gameState1)
        {
            if (Input.GetKeyDown(right_SongNext))
            {
                if (right_songNumber == (left_songNumber - 1))
                {
                    right_songNumber = right_songNumber + 2;
                }
                else
                {
                    right_songNumber++;
                }

                StartCoroutine(rightSongTextUp());

                matchingBPMs = false;

                if (right_songNumber == right_maxSongNumber + 1)
                {
                    right_songNumber = 1;
                }

                StartCoroutine(spinRightRecord());
            }
        }

        if (right_songNumber == 1)
        {
            right_recordTitle = "Love Song 28";
            right_artistName = "Jullian Gomes";
            right_bpm = 120;
            img_rightRecord.sprite = sprite_loveSong28;
        }

        if (right_songNumber == 2)
        {
            right_recordTitle = "Type Shi";
            right_artistName = "I";
            right_bpm = 80;
            img_rightRecord.sprite = sprite_typeShi;
        }

        if (!matchingBPMs)
        {
            if (left_songNumber == right_songNumber)
            {
                StartCoroutine(matchingBPMS());
            }
        }

        #endregion

        #region Scratch

        if (Input.GetKeyDown(scratchKeyCode))
        {
            music.pitch = -1;
            scratchKeyDown = true;
        }

        if (Input.GetKeyUp(scratchKeyCode))
        {
            scratchKeyDown = false;
            music.pitch = 1;
        }

        if (scratchQte)
        {
            scratchOverlay.SetActive(true);
            if (scratchKeyDown)
            {
                scratchKeyTimeDown += Time.deltaTime;
            }
        }
        else
        {
            scratchOverlay.SetActive(false);
        }

        scratchRandomizer1 = UnityEngine.Random.Range(0, 100);
        scratchRandomizer2 = UnityEngine.Random.Range(0, 10);

        if (scratchRandomizer1 == scratchRandomizer2)
        {
            StartCoroutine(scratchZone());
        }

        #endregion
    }

    public IEnumerator scratchZone()
    {
        //scratchQte = true;

        yield return new WaitForSeconds(3f);

        //scratchQte = false;
        //scratchKeyTimeDown = scratchPoints;

        //scratchBonus = (500 + (scratchPoints * 100)) * currentMultiplier * currentCrossfaderMultiplier;

        //int intScratchBonus = Convert.ToInt32(Mathf.Round(scratchBonus));

        //currentScore = currentScore + intScratchBonus;

        //yield return new WaitForSeconds(0.01f);

        //scratchPoints = 0;
        //scratchBonus = 0;
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

    #region Coroutines

    public IEnumerator leftSongTextUp()
    {
        CameraShake.Shake(duration: 0.1f, strength: 0.1f);

        txt_left_bpm.fontSize = 85;
        txt_left_bpm.color = Color.cyan;

        yield return new WaitForSeconds(0.5f);

        txt_left_bpm.fontSize = 75;
        txt_left_bpm.color = Color.cyan;
    }

    public IEnumerator rightSongTextUp()
    {
        CameraShake.Shake(duration: 0.1f, strength: 0.1f);

        txt_right_bpm.fontSize = 85;
        txt_right_bpm.color = Color.cyan;

        yield return new WaitForSeconds(0.5f);

        txt_right_bpm.fontSize = 75;
        txt_right_bpm.color = Color.cyan;
    }

    public IEnumerator matchingBPMS()
    {
        Instantiate(objMatchingBPMs, txt_left_bpm.transform.position, txt_left_bpm.transform.rotation);
        Instantiate(objMatchingBPMs, txt_right_bpm.transform.position, txt_right_bpm.transform.rotation);

        //CameraShake.Shake(duration: 0.2f, strength: 0.3f);

        yield return new WaitForSeconds(0.25f);

        matchingBPMs = true;
        gameState1 = false;
        gamestate2 = true;
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

    public IEnumerator spinLeftRecord()
    {
        objLeftRecord.transform.eulerAngles = new Vector3(objLeftRecord.transform.eulerAngles.x, objLeftRecord.transform.eulerAngles.y, 90);

        yield return new WaitForSeconds(0.5f);

        objLeftRecord.transform.eulerAngles = new Vector3(objLeftRecord.transform.eulerAngles.x, objLeftRecord.transform.eulerAngles.y, 0);
    }

    public IEnumerator spinRightRecord()
    {
        objRightRecord.transform.eulerAngles = new Vector3(objRightRecord.transform.eulerAngles.x, objRightRecord.transform.eulerAngles.y, 90);

        yield return new WaitForSeconds(0.5f);

        objRightRecord.transform.eulerAngles = new Vector3(objRightRecord.transform.eulerAngles.x, objRightRecord.transform.eulerAngles.y, 0);
    }

    #endregion

    #region Note Hits

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

    #endregion
}
