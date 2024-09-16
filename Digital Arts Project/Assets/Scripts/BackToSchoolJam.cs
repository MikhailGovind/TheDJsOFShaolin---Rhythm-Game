using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class BackToSchoolJam : MonoBehaviour
{
    public int sentenceNumber;
    public GameObject btnNextText;

    [Header("Quest")]
    public GameObject currentQuest;
    public Text txtQuest;
    [TextArea(3, 10)]
    public string currentQuestString;

    [Header("PlayableCharacter")]
    public Text pcName;
    public Text pcSentence;
    public GameObject pcBubble;
    [TextArea(3, 10)]
    public string pcString1;
    [TextArea(3, 10)]
    public string pcString2;
    [TextArea(3, 10)]
    public string pcString3;

    [Header("Zay")]
    public Text zayName;
    public Text zaySentence;
    public GameObject zayBubble;
    public Animator zayAnimator;
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
    public Animator ashAnimator;
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

    [Header("The Man")]
    public Text manName;
    public Text manSentence;
    public GameObject manBubble;
    public Animator manAnimator;
    [TextArea(3, 10)]
    public string manString1;
    [TextArea(3, 10)]
    public string manString2;
    [TextArea(3, 10)]
    public string manString3;
    [TextArea(3, 10)]
    public string manString4;

    [Header("Maya")]
    public Text mayaName;
    public Text mayaSentence;
    public GameObject mayaBubble;
    [TextArea(3, 10)]
    public string mayaString1;
    [TextArea(3, 10)]
    public string mayaString2;

    public void Start()
    {
        sentenceNumber = 0;

        zayBubble.SetActive(false);
        ashBubble.SetActive(false);
        mayaBubble.SetActive(false);
        pcBubble.SetActive(false);
        manBubble.SetActive(false);
        btnNextText.SetActive(false);

        currentQuest.SetActive(false);
    }

    private void Update()
    {
        if (sentenceNumber == 0)
        {
            StartCoroutine(startScene());
        }

        pcName.text = "Playable Character";

        zayName.text = "Zay";

        ashName.text = "Ash";

        mayaName.text = "Maya";

        manName.text = "Man";

        if (sentenceNumber == 1) //zay 1
        {
            zayBubble.SetActive(true);
            ashBubble.SetActive(false);
            manBubble.SetActive(false);
            mayaBubble.SetActive(false);
            pcBubble.SetActive(false);

            zaySentence.text = zayString1;
        }

        if (sentenceNumber == 2) //ash 1
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(true);
            manBubble.SetActive(false);
            mayaBubble.SetActive(false);
            pcBubble.SetActive(false);

            ashSentence.text = ashString1;
        }

        if (sentenceNumber == 3) //zay 2
        {
            zayBubble.SetActive(true);
            ashBubble.SetActive(false);
            manBubble.SetActive(false);
            mayaBubble.SetActive(false);
            pcBubble.SetActive(false);

            zaySentence.text = zayString2;

            StartCoroutine(crewWalkAway());
        }

        //zay and ash walk off, the man approaches

        if (sentenceNumber == 4) //man 1
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            manBubble.SetActive(true);
            mayaBubble.SetActive(false);
            pcBubble.SetActive(false);

            manSentence.text = manString1;
        }

        if (sentenceNumber == 5) //pc 1
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            manBubble.SetActive(false);

            pcSentence.text = pcString1;
        }

        if (sentenceNumber == 6) //man 2
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            manBubble.SetActive(true);
            mayaBubble.SetActive(false);
            pcBubble.SetActive(false);

            manSentence.text = manString2;
        }

        if (sentenceNumber == 7) //pc 2
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            manBubble.SetActive(false);

            pcSentence.text = pcString2;
        }

        if (sentenceNumber == 8) //man 3
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            manBubble.SetActive(true);
            mayaBubble.SetActive(false);
            pcBubble.SetActive(false);

            manSentence.text = manString3;
        }

        if (sentenceNumber == 9) //pc 3
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            manBubble.SetActive(false);

            pcSentence.text = pcString3;
        }

        if (sentenceNumber == 10) //man 4
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            manBubble.SetActive(true);
            mayaBubble.SetActive(false);
            pcBubble.SetActive(false);

            manSentence.text = manString4;
        }
    }

    public void NextSentence()
    {
        sentenceNumber++;
    }

    public IEnumerator startScene()
    {
        yield return new WaitForSeconds(1.5f);

        sentenceNumber = 1;
        btnNextText.SetActive(true);
    }

    public IEnumerator crewWalkAway()
    {
        btnNextText.SetActive(false);

        yield return new WaitForSeconds(1f);

        zayAnimator.SetTrigger("zay_walkAway");
        ashAnimator.SetTrigger("ash_walkAway");
        manAnimator.SetTrigger("man_walkTo");


        yield return new WaitForSeconds(5f);

        btnNextText.SetActive(true);
    }
}
