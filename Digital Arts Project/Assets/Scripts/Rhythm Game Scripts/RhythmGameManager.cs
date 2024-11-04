using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
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
    public bool gameState2; //game time
    public bool gameState3; //results

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
    public Sprite sprite_getUpAndDance;
    public Sprite sprite_bustinLoose;

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
    public float scratchKeyTimeDown;
    public bool scratchKeyDown;

    public bool scratchQte;

    public float scratchRandomizer1;
    public int scratchRandomizer2;

    public GameObject objLightning;

    [Header("Results")]
    public GameObject pnlResults;
    public KeyCode resultKeyCode;

    public float totalBeats; 

    public float normalBeatHits;
    public Text txtNormalBeatHits;

    public float goodBeatHits;
    public Text txtGoodBeatHits;

    public float perfectBeatHits;
    public Text txtPerfectBeatHits;

    public float missedBeats;
    public Text txtMissedBeatHits;

    public float percentageHit;
    public Text txtPercentageHit;

    public float scratchBonus;
    public Text txtScratchBonus;

    public float totalScore;
    public Text txtTotalScore;


    #endregion

    void Start()
    {
        //game management
        instance = this;

        music.pitch = 1;

        gameState1 = true;
        gameState2 = false;

        eqKnobsButtons.SetActive(false);
        leftNoteHolder.SetActive(false);
        rightNoteHolder.SetActive(false);
        pnlOverlay.SetActive(false);
        pnlCrossfader.SetActive(false);

        //totalBeats = GameObject.FindGameObjectsWithTag("Beats").Length;

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
        objLightning.SetActive(false);

        //results
        pnlResults.SetActive(false);
    }

    void Update()
    {
        #region Start music

        if (!startPlaying && gameState2)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                //totalBeats = FindObjectsOfType<NoteObject>().Length;
                leftBeatScroller.hasStarted = true;
                rightBeatScroller.hasStarted = true;

                music.Play();

                recordSpin.goSpin = true;
                recordSpin2.goSpin = true;
            }
        }

        if (gameState2)
        {
            eqKnobsButtons.SetActive(true);
            leftNoteHolder.SetActive(true);
            rightNoteHolder.SetActive(true);
            pnlOverlay.SetActive(true);
            pnlCrossfader.SetActive(true);
        }

        if (gameState3)
        {
            pnlResults.SetActive(true);
            pnlOverlay.SetActive(false);
            pnlCrossfader.SetActive(false);
            eqKnobsButtons.SetActive(false);
            leftNoteHolder.SetActive(false);
            rightNoteHolder.SetActive(false);

            startPlaying = false;

            txtNormalBeatHits.text = normalBeatHits.ToString();
            txtGoodBeatHits.text = goodBeatHits.ToString();
            txtPerfectBeatHits.text = perfectBeatHits.ToString();
            txtMissedBeatHits.text = missedBeats.ToString();

            percentageHit = ((normalBeatHits + goodBeatHits + perfectBeatHits) / totalBeats) * 100f;
            txtPercentageHit.text = percentageHit.ToString("F1") + "%";

            scratchBonus = scratchKeyTimeDown * 1000;
            txtScratchBonus.text = scratchBonus.ToString("F0");

            totalScore = currentScore + scratchBonus;
            txtTotalScore.text = totalScore.ToString("F0");
        }

        if (Input.GetKeyDown(resultKeyCode))
        {
            gameState3 = true;
            gameState2 = false;
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
            left_recordTitle = "Get Up and Down";
            left_artistName = "Freedom";
            left_bpm = 107;
            img_leftRecord.sprite = sprite_getUpAndDance;
        }

        if (left_songNumber == 2)
        {
            left_recordTitle = "Bustin' Loose";
            left_artistName = "Chuck Brown and the Soul Searchers";
            left_bpm = 111;
            img_leftRecord.sprite = sprite_bustinLoose;
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

        if (left_songNumber == 1)
        {
            right_recordTitle = "Get Up and Down";
            right_artistName = "Freedom";
            right_bpm = 107;
            img_rightRecord.sprite = sprite_getUpAndDance;
        }

        if (left_songNumber == 2)
        {
            right_recordTitle = "Bustin' Loose";
            right_artistName = "Chuck Brown and the Soul Searchers";
            right_bpm = 111;
            img_rightRecord.sprite = sprite_bustinLoose;
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

            if (sldrCrossfader.value < 5)
            {
                recordSpin.goSpin = false;
                recordSpin.onScratch = true;
            }

            if (sldrCrossfader.value > 5)
            {
                recordSpin2.goSpin = false;
                recordSpin2.onScratch = true;
            }

            if (sldrCrossfader.value == 5)
            {
                recordSpin.goSpin = false;
                recordSpin.onScratch = true;
                recordSpin2.goSpin = false;
                recordSpin2.onScratch = true;
            }
        }

        if (Input.GetKeyUp(scratchKeyCode))
        {
            music.pitch = 1;
            scratchKeyDown = false;

            recordSpin.goSpin = true;
            recordSpin.onScratch = false;
            recordSpin2.goSpin = true;
            recordSpin2.onScratch = false;
        }

        if (scratchQte)
        {
            if (scratchKeyDown)
            {
                scratchKeyTimeDown += Time.deltaTime;
            }
        }
        else
        {
            if (scratchKeyDown)
            {
                scratchKeyTimeDown -= Time.deltaTime;

                if (scratchKeyTimeDown <= 0)
                {
                    scratchKeyTimeDown = 0;
                }
            }
        }

        scratchRandomizer1 = UnityEngine.Random.Range(0, 10000);
        scratchRandomizer2 = UnityEngine.Random.Range(0, 10);

        if (scratchRandomizer1 == scratchRandomizer2)
        {
            StartCoroutine(scratchTime());
        }

        #endregion
    }

    public IEnumerator scratchTime()
    {
        if (gameState2)
        {
            CameraShake.Shake(duration: 3f, strength: 0.3f);
            scratchQte = true;
            objLightning.SetActive(true);

            yield return new WaitForSeconds(3f);

            scratchQte = false;
            objLightning.SetActive(false);
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
        gameState2 = true;
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

        normalBeatHits++;
    }

    public void goodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier * currentCrossfaderMultiplier;
        backgroundScore += scorePerGoodNote * currentMultiplier * currentCrossfaderMultiplier;
        noteHit();

        goodBeatHits++;
    }

    public void perfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier * currentCrossfaderMultiplier;
        backgroundScore += scorePerPerfectNote * currentMultiplier * currentCrossfaderMultiplier;
        noteHit();

        perfectBeatHits++;
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

        missedBeats++;
    }

    #endregion
}
