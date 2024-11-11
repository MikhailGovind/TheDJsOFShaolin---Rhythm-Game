using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterTriggers : MonoBehaviour
{
    [Header("Misc")]
    public BackToSchoolJam backToSchoolJam;

    [Header("Maya")]
    public bool boolMayaTrigger;

    [Header("Devon")]
    public bool boolDevonTrigger;

    [Header("Coke La Rock")]
    public bool boolCokeTrigger;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (boolMayaTrigger)
        {
            if (other.tag == "Player")
            {
                backToSchoolJam.sentenceNumber = 12;
                backToSchoolJam.objMayaTrigger.SetActive(false);
            }
        }

        if (boolDevonTrigger)
        {
            if (other.tag == "Player")
            {
                backToSchoolJam.sentenceNumber = 25;
                backToSchoolJam.objDevonTrigger.SetActive(false);
            }
        }

        if (boolCokeTrigger)
        {
            if(other.tag == "Player")
            {
                backToSchoolJam.sentenceNumber = 34;
                backToSchoolJam.objCokeTrigger.SetActive(false);
            }
        }
    }
}
