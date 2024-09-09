using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialogue : MonoBehaviour
{
    public int sentenceNumber;

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
        sentenceNumber = 1;
        zayBubble.SetActive(false);
        ashBubble.SetActive(false);
        mayaBubble.SetActive(false);

        invite.SetActive(false);
    }

    public void Update()
    {
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
            StartCoroutine(openInvite());
        }
    }
    
    public void NextSentence()
    {
        sentenceNumber++;
    }

    public IEnumerator openInvite()
    {
        //play audio
        audioSource.clip = inviteSound;
        audioSource.Play();
        Debug.Log("Invite Sound");

         yield return new WaitForSeconds(1f);

        //show letter
        invite.SetActive(true);
    }
}
