using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterTriggers : MonoBehaviour
{
    [Header("Misc")]
    public BackToSchoolJam backToSchoolJam;

    [Header("Maya")]
    public GameObject objMayaTrigger;
    public bool boolMayaTrigger;

    [Header("Devon")]
    public GameObject objDevonTrigger;
    public bool boolDevonTrigger;

    [Header("Coke La Rock")]
    public GameObject objCokeTrigger;
    public bool boolCokeTrigger;

    private void Update()
    {
        if (backToSchoolJam.sentenceNumber == 11)
        {
            objMayaTrigger.SetActive(true);
        }

        if (backToSchoolJam.sentenceNumber == 24)
        {
            objDevonTrigger.SetActive(true);
        }

        if (backToSchoolJam.sentenceNumber == 33)
        {
            objCokeTrigger.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (boolMayaTrigger)
        {
            if (other.tag == "Player")
            {
                backToSchoolJam.sentenceNumber = 12;
                objMayaTrigger.SetActive(false);
            }
        }

        if (boolDevonTrigger)
        {
            if (other.tag == "Player")
            {
                backToSchoolJam.sentenceNumber = 25;
                objDevonTrigger.SetActive(false);
            }
        }

        if (boolCokeTrigger)
        {
            if(other.tag == "Player")
            {
                backToSchoolJam.sentenceNumber = 34;
                objCokeTrigger.SetActive(false);
            }
        }
    }
}
