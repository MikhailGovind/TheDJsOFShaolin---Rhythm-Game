using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toResults : MonoBehaviour
{
    public RhythmGameManager rhythmGameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            rhythmGameManager.gameState3 = true;
            rhythmGameManager.gameState2 = false;
        }
    }
}
