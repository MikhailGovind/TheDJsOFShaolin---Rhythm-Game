using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class IntroDialogue : MonoBehaviour
{
    public int sentenceNumber;
    public GameObject btnNextText;

    [Header("Camera")]
    public CinemachineVirtualCamera virtualCamera;
    public float targetZoom = 3.5f;  //desired orthographic size for zoom
    public float zoomDuration = 2f;  //time in seconds for zoom to complete
    private float initialZoom = 7.5f; //starting zoom size
    private float elapsedTime = 0f;

    [Header("Zay")]
    public Text zayName;
    public Text zaySentence;
    public GameObject zayBubble;
    [TextArea(3, 10)]
    public string zayString;

    [Header("Ash")]
    public Text ashName;
    public Text ashSentence;
    public GameObject ashBubble;
    [TextArea(3, 10)]
    public string ashString;

    [Header("Maya")]
    public Text mayaName;
    public Text mayaSentence;
    public GameObject mayaBubble;
    [TextArea(3, 10)]
    public string mayaString;
    [TextArea(3, 10)]
    public string mayaString2;

    [Header("Invite")]
    public AudioSource audioSource;
    public AudioClip inviteSound;
    public GameObject invite;

    public void Start()
    {
        sentenceNumber = 0;
        zayBubble.SetActive(false);
        ashBubble.SetActive(false);
        mayaBubble.SetActive(false);

        invite.SetActive(false);

        virtualCamera.m_Lens.OrthographicSize = initialZoom;
        Debug.Log(virtualCamera.m_Lens.OrthographicSize);
    }

    public void Update()
    {
        if (sentenceNumber == 0)
        {
            StartCoroutine(cameraZoom());
        }

        zayName.text = "Zay";
        zaySentence.text = zayString;

        ashName.text = "Ash";
        ashSentence.text = ashString;

        mayaName.text = "Maya";

        if (sentenceNumber == 1)
        { 
            zayBubble.SetActive(true);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
        }

        if (sentenceNumber == 2)
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            mayaBubble.SetActive(false);
        }

        if (sentenceNumber == 3)
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(true);
        }

        if (sentenceNumber == 3)
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(true);

            mayaSentence.text = mayaString;
        }
        if (sentenceNumber == 4)
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(true);

            mayaSentence.text = mayaString2;
        }

        if (sentenceNumber == 5)
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);

            StartCoroutine(openInvite());
        }
    }
    
    public void NextSentence()
    {
        sentenceNumber++;
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
    }

    public IEnumerator openInvite()
    {
        btnNextText.SetActive(false);

        //play audio
        audioSource.clip = inviteSound;
        audioSource.Play();
        Debug.Log("Invite Sound");

         yield return new WaitForSeconds(1f);

        //show letter
        invite.SetActive(true);
    }
}
