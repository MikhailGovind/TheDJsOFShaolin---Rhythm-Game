using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using System;

public class IntroDialogue : MonoBehaviour
{
    public int sentenceNumber;
    public GameObject btnNextText;
    public GameObject pnlIntroDialogue;

    [Header("Movement")]
    public InputActionMap inputActionMap;

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
    [TextArea(3, 10)]
    public string zayString5;

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

    [Header("Maya")]
    public Text mayaName;
    public Text mayaSentence;
    public GameObject mayaBubble;
    [TextArea(3, 10)]
    public string mayaString1;
    [TextArea(3, 10)]
    public string mayaString2;

    [Header("Invite")]
    public AudioSource audioSource;
    public AudioClip inviteSound;
    public GameObject invite;
    public bool inviteSoundPlay;

    public void Start()
    {
        sentenceNumber = 0;
        zayBubble.SetActive(false);
        ashBubble.SetActive(false);
        mayaBubble.SetActive(false);
        allBubble.SetActive(false);
        pcBubble.SetActive(false);
        btnNextText.SetActive(false);

        invite.SetActive(false);
        inviteSoundPlay = false;

        currentQuest.SetActive(false);

        virtualCamera.m_Lens.OrthographicSize = initialZoom;
        Debug.Log(virtualCamera.m_Lens.OrthographicSize);

        inputActionMap.Enable();
    }

    public void Update()
    {
        #region sentences
        if (sentenceNumber == 0)
        {
            StartCoroutine(cameraZoom());
        }

        pcName.text = "Playable Character";

        allName.text = "All";

        zayName.text = "Zay";

        ashName.text = "Ash";

        mayaName.text = "Maya";

        if (sentenceNumber == 1) //zay 1
        { 
            zayBubble.SetActive(true);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            zaySentence.text = zayString1;
        }

        if (sentenceNumber == 2) //ash 1
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            ashSentence.text = ashString1;
        }

        if (sentenceNumber == 3) //zay 2
        {
            zayBubble.SetActive(true);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            zaySentence.text = zayString2;
        }

        if (sentenceNumber == 4) //all 1
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(true);

            allSentence.text = allString1;
        }

        if (sentenceNumber == 5) //ash 2
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            ashSentence.text = ashString2;
        }

        if (sentenceNumber == 6) //zay 3
        {
            zayBubble.SetActive(true);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            zaySentence.text = zayString3;
        }

        if (sentenceNumber == 7) //ash 3
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            ashSentence.text = ashString3;
        }

        if (sentenceNumber == 8) //pc opt 1
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

        if (sentenceNumber == 9) //ash 4
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            pcBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            btnNextText.SetActive(true);

            ashSentence.text = ashString4;
        }

        if (sentenceNumber == 10) //ash 5
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            pcBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            btnNextText.SetActive(true);

            ashSentence.text = ashString5;
        }

        if (sentenceNumber == 11) //maya 1
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(true);
            allBubble.SetActive(false);

            mayaSentence.text = mayaString1;
        }

        if (sentenceNumber == 12) //maya 2
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(true);
            allBubble.SetActive(false);

            mayaSentence.text = mayaString2;
        }

        if (sentenceNumber == 13)
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            StartCoroutine(openInvite());
        }

        if (sentenceNumber == 14) //zay 4
        {
            zayBubble.SetActive(true);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            invite.SetActive(false);
            btnNextText.SetActive(true);

            zaySentence.text = zayString4;
        }

        if (sentenceNumber == 15) //ash 6
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            ashSentence.text = ashString6;
        }

        if (sentenceNumber == 16) //zay 5
        {
            zayBubble.SetActive(true);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            zaySentence.text = zayString5;
        }

        if (sentenceNumber == 17) //ash 7
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            mayaBubble.SetActive(false);
            allBubble.SetActive(false);

            ashSentence.text = ashString7;
        }

        if (sentenceNumber == 18)
        {
            txtQuest.text = "Current Objective: " + currentQuestString;
            currentQuest.SetActive(true);
            btnNextText.SetActive(false);
            pnlIntroDialogue.SetActive(false);
        }
        #endregion  
    }

    public void NextSentence()
    {
        sentenceNumber++;
    }

    public void pcOption1()
    {
        sentenceNumber = 9;
    }

    public void pcOption2()
    {
        sentenceNumber = 10;
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

        yield return new WaitForSeconds(3f);

        sentenceNumber = 1;
        btnNextText.SetActive(true);
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

            yield return new WaitForSeconds(1f);

            inviteSoundPlay = true;
        }

        yield return new WaitForSeconds(1f);

        //show letter
        invite.SetActive(true);
    }
}
