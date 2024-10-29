using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using System;
using UnityEngine.UIElements;

public class IntroDialogue : MonoBehaviour
{
    #region variables

    [Header("Overall")]
    public int sentenceNumber;
    public GameObject btnNextText;
    public GameObject pnlIntroDialogue;
    public GameObject trgToJamScene;
    public Player player;

    [Header("Misc UI")]
    public UnityEngine.UI.Image pnlDialogue;
    public GameObject dateImg2024;
    public GameObject dateImg1973;

    [Header("Quest")]
    public GameObject currentQuest;
    public Text txtQuest;
    [TextArea(3, 10)]
    public string currentQuestString;

    [Header("Camera")]
    public CinemachineVirtualCamera virtualCamera;
    public float targetZoom = 3.5f;  //desired orthographic size for zoom
    public float zoomDuration = 2f;  //time in seconds for zoom to complete
    private float initialZoom = 7.5f; //starting zoom size
    private float elapsedTime = 0f;

    [Header("PlayableCharacter")]
    public Text pcName;
    public Text pcSentence1;
    public Text pcSentence2;
    public GameObject pcBubble;
    [TextArea(3, 10)]
    public string pcString1;
    [TextArea(3, 10)]
    public string pcString2;

    [Header("All")]
    public Text allName;
    public Text allSentence;
    public GameObject allBubble;
    [TextArea(3, 10)]
    public string allString1;

    [Header("Zay")]
    public Text zayName;
    public Text zaySentence;
    public GameObject zayBubble;
    [TextArea(3, 10)]
    public string zayString1;
    [TextArea(3, 10)]
    public string zayString2;
    [TextArea(3, 10)]
    public string zayString3;
    [TextArea(3, 10)]
    public string zayString4;

    public Animator zayAnimator;

    [Header("Ash")]
    public Text ashName;
    public Text ashSentence;
    public GameObject ashBubble;
    [TextArea(3, 10)]
    public string ashString1;
    [TextArea(3, 10)]
    public string ashString2;
    [TextArea(3, 10)]
    public string ashString3;
    [TextArea(3, 10)]
    public string ashString4;
    [TextArea(3, 10)]
    public string ashString5;
    [TextArea(3, 10)]
    public string ashString6;
    [TextArea(3, 10)]
    public string ashString7;

    public Animator ashAnimator;

    [Header("Maya")]
    public Text mayaName;
    public Text mayaSentence;
    public GameObject mayaBubble;
    [TextArea(3, 10)]
    public string mayaString1;
    [TextArea(3, 10)]
    public string mayaString2;

    public Animator mayaAnimator;
    public GameObject objMaya;

    [Header("Invite")]
    public AudioSource audioSource;
    public AudioClip inviteSound;
    public GameObject invite;
    public bool inviteSoundPlay;

    #endregion

    public void Start()
    {
        sentenceNumber = 0;
        player.moveSpeed = 0f;

        pnlIntroDialogue.SetActive(false);

        zayBubble.SetActive(false);
        ashBubble.SetActive(false);
        mayaBubble.SetActive(false);
        allBubble.SetActive(false);
        pcBubble.SetActive(false);
        btnNextText.SetActive(false);

        invite.SetActive(false);
        inviteSoundPlay = false;

        currentQuest.SetActive(false);
        trgToJamScene.SetActive(false);

        dateImg2024.SetActive(false);
        dateImg1973.SetActive(false);

        virtualCamera.m_Lens.OrthographicSize = initialZoom;
        Debug.Log(virtualCamera.m_Lens.OrthographicSize);
    }

    public void Update()
    {
        #region sentences
        if (sentenceNumber == 0)
        {
            StartCoroutine(cameraZoom());
            StartCoroutine(Date());
            //StartCoroutine(flashBlue());
        }

        pcName.text = "Playable Character";

        allName.text = "All";

        zayName.text = "Zay";

        ashName.text = "Ash";

        mayaName.text = "Maya";

        if (sentenceNumber == 1) //ash 1
        {
            pnlIntroDialogue.SetActive(true);

            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            ashSentence.text = ashString1;
        }

        if (sentenceNumber == 2) //zay 1
        {
            zayBubble.SetActive(true);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            zaySentence.text = zayString1;
        }

        if (sentenceNumber == 3) //all 1
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(true);

            allSentence.text = allString1;
        }

        if (sentenceNumber == 4) //ash 2
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            ashSentence.text = ashString2;
        }

        if (sentenceNumber == 5) //zay 2
        {
            zayBubble.SetActive(true);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            zaySentence.text = zayString2;
        }

        if (sentenceNumber == 6) //ash 3
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            ashSentence.text = ashString3;
        }

        if (sentenceNumber == 7) //pc opt 1
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            btnNextText.SetActive(false);

            pcSentence1.text = pcString1;
            pcSentence2.text = pcString2;
        }

        if (sentenceNumber == 8) //ash 4
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            pcBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            btnNextText.SetActive(true);
        }

        if (sentenceNumber == 9) //maya 1
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(true);
            allBubble.SetActive(false);

            mayaSentence.text = mayaString1;
        }

        if (sentenceNumber == 10) //maya 2
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(true);
            allBubble.SetActive(false);

            mayaSentence.text = mayaString2;
        }

        if (sentenceNumber == 11)
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            StartCoroutine(openInvite());
        }

        if (sentenceNumber == 12) //zay 3
        {
            invite.SetActive(false);

            zayBubble.SetActive(true);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            btnNextText.SetActive(true);

            zaySentence.text = zayString3;
        }

        if (sentenceNumber == 13) //ash 6
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            ashSentence.text = ashString6;
        }

        if (sentenceNumber == 14) //zay 4
        {
            zayBubble.SetActive(true);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            zaySentence.text = zayString4;
        }

        if (sentenceNumber == 15) //ash 7
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            ashSentence.text = ashString7;
        }

        if (sentenceNumber == 16)
        {
            txtQuest.text = currentQuestString;
            currentQuest.SetActive(true);
            btnNextText.SetActive(false);
            pnlIntroDialogue.SetActive(false);
            trgToJamScene.SetActive(true);

            player.moveSpeed = 5f;
            StartCoroutine(crewWalk());
        }
        #endregion  
    }
    
    public void NextSentence()
    {
        sentenceNumber++;
    }

    public void pcOption1()
    {
        sentenceNumber = 8;
        ashSentence.text = ashString4;

        StartCoroutine(mayaIdle());
    }

    public void pcOption2()
    {
        sentenceNumber = 8;
        ashSentence.text = ashString5;

        StartCoroutine(mayaIdle());
    }

    public IEnumerator cameraZoom()
    {
        yield return new WaitForSeconds(2f);

        // Check if the zooming duration has not been completed yet
        if (elapsedTime < zoomDuration)
        {
            // Calculate the fraction of time that has passed
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / zoomDuration;

            // Smoothly interpolate the orthographic size from initial to target value
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(initialZoom, targetZoom, t);
        }

        //yield return new WaitForSeconds(5f);
    }

    public IEnumerator Date()
    {
        yield return new WaitForSeconds(4f);
         
        dateImg2024.SetActive(true);

        yield return new WaitForSeconds(6f);

        dateImg2024.SetActive(false);
        dateImg1973.SetActive(true);
        pnlDialogue.color = new Color(0, 0, 1, 0.1f);

        yield return new WaitForSeconds(1f);

        dateImg2024.SetActive(true);
        dateImg1973.SetActive(false);
        pnlDialogue.color = new Color(0, 0, 1, 0f);

        yield return new WaitForSeconds(0.5f);

        dateImg2024.SetActive(false);
        dateImg1973.SetActive(true);
        pnlDialogue.color = new Color(0, 0, 1, 0.1f);

        yield return new WaitForSeconds(0.5f);

        dateImg2024.SetActive(true);
        dateImg1973.SetActive(false);
        pnlDialogue.color = new Color(0, 0, 1, 0f);

        yield return new WaitForSeconds(1f);

        dateImg2024.SetActive(false);
        dateImg1973.SetActive(false);

        sentenceNumber = 1;
        btnNextText.SetActive(true);
    }

    public IEnumerator mayaIdle()
    {
        mayaAnimator.SetTrigger("maya_walkToGroup");

        yield return new WaitForSeconds(2.3f);

        mayaAnimator.SetTrigger("maya_Idle");
    }

    public IEnumerator openInvite()
    {
        btnNextText.SetActive(false);

        if (inviteSoundPlay == false)
        {
            //play audio
            audioSource.clip = inviteSound;
            audioSource.Play();
            Debug.Log("Invite Sound");

            yield return new WaitForSeconds(0.25f);

            inviteSoundPlay = true;
        }

        mayaAnimator.SetTrigger("maya_afterInvite");

        yield return new WaitForSeconds(1f);

        //show letter
        invite.SetActive(true);

        yield return new WaitForSeconds(10f);
        objMaya.SetActive(false);
    }

    public IEnumerator crewWalk()
    {
        ashAnimator.SetTrigger("ash_walkToEntrance");
        zayAnimator.SetTrigger("zay_walkToEntrance");

        yield return new WaitForSeconds(6f);

        ashAnimator.SetTrigger("ash_Idle");
        zayAnimator.SetTrigger("zay_Idle");
    }
}
