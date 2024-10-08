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
    [TextArea(3, 10)]
    public string pcString4;
    [TextArea(3, 10)]
    public string pcString5;
    [TextArea(3, 10)]
    public string pcString6;
    [TextArea(3, 10)]
    public string pcString7;
    [TextArea(3, 10)]
    public string pcString8;
    [TextArea(3, 10)]
    public string pcString9;
    [TextArea(3, 10)]
    public string pcString10;
    [TextArea(3, 10)]
    public string pcString11;

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
    [TextArea(3, 10)]
    public string mayaString3;
    [TextArea(3, 10)]
    public string mayaString4;
    [TextArea(3, 10)]
    public string mayaString5;
    [TextArea(3, 10)]
    public string mayaString6;
    [TextArea(3, 10)]
    public string mayaString7;

    [Header("Devon")]
    public Text devonName;
    public Text devonSentence;
    public GameObject devonBubble;
    [TextArea(3, 10)]
    public string devonString1;
    [TextArea(3, 10)]
    public string devonString2;
    [TextArea(3, 10)]
    public string devonString3;
    [TextArea(3, 10)]
    public string devonString4;

    public void Start()
    {
        sentenceNumber = 0;

        zayBubble.SetActive(false);
        ashBubble.SetActive(false);
        mayaBubble.SetActive(false);
        pcBubble.SetActive(false);
        manBubble.SetActive(false);
        devonBubble.SetActive(false);
        btnNextText.SetActive(false);

        currentQuest.SetActive(false);
    }

    private void Update()
    {
        #region sentences

        if (sentenceNumber == 0)
        {
            StartCoroutine(startScene());
        }

        pcName.text = "Playable Character";

        zayName.text = "Zay";

        ashName.text = "Ash";

        mayaName.text = "Maya";

        manName.text = "Man";

        devonName.text = "Devon";

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

        //man walks away

        if (sentenceNumber == 11)
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(false);
            manBubble.SetActive(false);
            btnNextText.SetActive(false);
            manAnimator.SetTrigger("man_walkAway");
        }

        //pc interacts with maya

        if (sentenceNumber == 12) //maya 1
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(true);
            manBubble.SetActive(false);
            btnNextText.SetActive(true);

            mayaSentence.text = mayaString1;
        }

        if (sentenceNumber == 13) //pc 4
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            manBubble.SetActive(false);

            pcSentence.text = pcString4;
        }

        if (sentenceNumber == 14) //maya 2
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(true);
            manBubble.SetActive(false);

            mayaSentence.text = mayaString2;
        }

        if (sentenceNumber == 15) //pc 5
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            manBubble.SetActive(false);

            pcSentence.text = pcString5;
        }

        if (sentenceNumber == 16) //maya 3
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(true);
            manBubble.SetActive(false);

            mayaSentence.text = mayaString3;
        }

        if (sentenceNumber == 17) //pc 6
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            manBubble.SetActive(false);

            pcSentence.text = pcString6;
        }

        if (sentenceNumber == 18) //maya 4
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(true);
            manBubble.SetActive(false);

            mayaSentence.text = mayaString4;
        }

        if (sentenceNumber == 19) //pc 7
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            manBubble.SetActive(false);

            pcSentence.text = pcString7;
        }

        if (sentenceNumber == 20) //maya 5
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(true);
            manBubble.SetActive(false);

            mayaSentence.text = mayaString5;
        }

        if (sentenceNumber == 21) //maya 6
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(true);
            manBubble.SetActive(false);

            mayaSentence.text = mayaString6;
        }

        if (sentenceNumber == 22) //pc 8
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            manBubble.SetActive(false);

            pcSentence.text = pcString8;
        }

        if (sentenceNumber == 23) //maya 7
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            mayaBubble.SetActive(true);
            manBubble.SetActive(false);

            mayaSentence.text = mayaString7;
        }

        //zay option

        if (sentenceNumber == 24) //zay 3
        {
            zayBubble.SetActive(true);
            ashBubble.SetActive(false);
            manBubble.SetActive(false);
            mayaBubble.SetActive(false);
            pcBubble.SetActive(false);

            zaySentence.text = zayString3;
        }

        if (sentenceNumber == 25) //devon 1
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            manBubble.SetActive(false);
            mayaBubble.SetActive(false);
            pcBubble.SetActive(false);
            devonBubble.SetActive(true);

            devonSentence.text = devonString1;
        }

        if (sentenceNumber == 26) //zay 4
        {
            zayBubble.SetActive(true);
            ashBubble.SetActive(false);
            manBubble.SetActive(false);
            mayaBubble.SetActive(false);
            pcBubble.SetActive(false);

            zaySentence.text = zayString4;
        }

        if (sentenceNumber == 27) //pc 9
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            manBubble.SetActive(false);
            devonBubble.SetActive(false);

            pcSentence.text = pcString9;
        }

        if (sentenceNumber == 28) //devon 2
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            manBubble.SetActive(false);
            mayaBubble.SetActive(false);
            pcBubble.SetActive(false);
            devonBubble.SetActive(true);

            devonSentence.text = devonString2;
        }

        if (sentenceNumber == 29) //pc 10
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            manBubble.SetActive(false);
            devonBubble.SetActive(false);

            pcSentence.text = pcString10;
        }

        if (sentenceNumber == 30) //devon 3
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            manBubble.SetActive(false);
            mayaBubble.SetActive(false);
            pcBubble.SetActive(false);
            devonBubble.SetActive(true);

            devonSentence.text = devonString3;
        }

        if (sentenceNumber == 31) //pc 11
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            manBubble.SetActive(false);
            devonBubble.SetActive(false);

            pcSentence.text = pcString11;
        }

        if (sentenceNumber == 32) //devon 4
        {
            zayBubble.SetActive(false);
            ashBubble.SetActive(false);
            manBubble.SetActive(false);
            mayaBubble.SetActive(false);
            pcBubble.SetActive(false);
            devonBubble.SetActive(true);

            devonSentence.text = devonString4;
        }



        #endregion


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

        yield return new WaitForSeconds(1f);

        manAnimator.SetTrigger("man_walkTo");

        yield return new WaitForSeconds(5f);

        btnNextText.SetActive(true);
    }
}
