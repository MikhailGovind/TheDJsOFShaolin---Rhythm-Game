using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class OutroScene : MonoBehaviour
{
    #region variables

    [Header("Overall")]
    public int sentenceNumber;
    public GameObject btnNextText;
    public Player player;

    [Header("Camera")]
    public CinemachineVirtualCamera virtualCamera;
    public float targetZoom = 3.5f;  //desired orthographic size for zoom
    public float zoomDuration = 2f;  //time in seconds for zoom to complete
    private float initialZoom = 7.5f; //starting zoom size
    private float elapsedTime = 0f;

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
    [TextArea(3, 10)]
    public string mayaString8;

    [Header("Coke La Rock")]
    public Text cokeName;
    public Text cokeSentence;
    public GameObject cokeBubble;
    [TextArea(3, 10)]
    public string cokeString1;
    [TextArea(3, 10)]
    public string cokeString2;
    [TextArea(3, 10)]
    public string cokeString3;

    #endregion

    public void Start()
    {
        sentenceNumber = 0;
        player.moveSpeed = 0f;

        pcBubble.SetActive(false);
        mayaBubble.SetActive(false);
        cokeBubble.SetActive(false);
        btnNextText.SetActive(false);
    }

    private void Update()
    {
        #region sentences

        if (sentenceNumber == 0)
        {
            StartCoroutine(cameraZoom());
        }

        pcName.text = "Playable Character";

        mayaName.text = "Maya";

        cokeName.text = "A-1 Coke";

        if (sentenceNumber == 1) //coke 1
        {
            pcBubble.SetActive(false);
            mayaBubble.SetActive(false);
            cokeBubble.SetActive(true);

            cokeSentence.text = cokeString1;
        }

        if (sentenceNumber == 2) //coke 2
        {
            pcBubble.SetActive(false);
            mayaBubble.SetActive(false);
            cokeBubble.SetActive(true);

            cokeSentence.text = cokeString2;
        }

        if (sentenceNumber == 3) //maya 1
        {
            pcBubble.SetActive(false);
            mayaBubble.SetActive(true);
            cokeBubble.SetActive(false);

            mayaSentence.text = mayaString1;
        }

        if (sentenceNumber == 4) //maya 2
        {
            pcBubble.SetActive(false);
            mayaBubble.SetActive(true);
            cokeBubble.SetActive(false);

            mayaSentence.text = mayaString2;
        }

        if (sentenceNumber == 5) //coke 3
        {
            pcBubble.SetActive(false);
            mayaBubble.SetActive(false);
            cokeBubble.SetActive(true);

            cokeSentence.text = cokeString3;
        }

        if (sentenceNumber == 6) //pc 1
        {
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            cokeBubble.SetActive(false);

            pcSentence.text = pcString1;
        }

        if (sentenceNumber == 7) //maya 3
        {
            pcBubble.SetActive(false);
            mayaBubble.SetActive(true);
            cokeBubble.SetActive(false);

            mayaSentence.text = mayaString3;
        }

        if (sentenceNumber == 8) //maya 4
        {
            pcBubble.SetActive(false);
            mayaBubble.SetActive(true);
            cokeBubble.SetActive(false);

            mayaSentence.text = mayaString4;
        }

        if (sentenceNumber == 9) //maya 5
        {
            pcBubble.SetActive(false);
            mayaBubble.SetActive(true);
            cokeBubble.SetActive(false);

            mayaSentence.text = mayaString5;
        }

        if (sentenceNumber == 10) //maya 6
        {
            pcBubble.SetActive(false);
            mayaBubble.SetActive(true);
            cokeBubble.SetActive(false);

            mayaSentence.text = mayaString6;
        }

        if (sentenceNumber == 11) //maya 7
        {
            pcBubble.SetActive(false);
            mayaBubble.SetActive(true);
            cokeBubble.SetActive(false);

            mayaSentence.text = mayaString7;
        }

        if (sentenceNumber == 12) //pc 2
        {
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            cokeBubble.SetActive(false);

            pcSentence.text = pcString2;
        }

        if (sentenceNumber == 13) //pc 3
        {
            pcBubble.SetActive(true);
            mayaBubble.SetActive(false);
            cokeBubble.SetActive(false);

            pcSentence.text = pcString3;
        }

        if (sentenceNumber == 14) //maya 8
        {
            pcBubble.SetActive(false);
            mayaBubble.SetActive(true);
            cokeBubble.SetActive(false);

            mayaSentence.text = mayaString8;
        }

        #endregion
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

        yield return new WaitForSeconds(5f);

        sentenceNumber = 1;
        btnNextText.SetActive(true);
    }

    //public IEnumerator startScene()
    //{
    //    yield return new WaitForSeconds(1.5f);

    //    sentenceNumber = 1;
    //    btnNextText.SetActive(true);
    //}
}
