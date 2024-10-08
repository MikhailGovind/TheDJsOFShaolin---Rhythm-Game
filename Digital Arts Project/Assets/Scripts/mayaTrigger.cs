using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mayaTrigger : MonoBehaviour
{
    public BackToSchoolJam backToSchoolJam;
    public GameObject objMayaTrigger;
    public GameObject objPlayer;

    private void Start()
    {
        objMayaTrigger.SetActive(true);
        objPlayer = GameObject.Find("Player");
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("in trigger");

        if (objPlayer)
        {
            Debug.Log("in trigger if");

            backToSchoolJam.sentenceNumber = 12;
            objMayaTrigger.SetActive(false);
        }
    }
}
