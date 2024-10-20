using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public bool leftBeat;
    public bool rightBeat;
    public KeyCode keyToPress;
    Animator animator;

    public GameObject hitEffect, goodHitEffect, perfectHitEffect, missEffect;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                StartCoroutine(noteHit());

                if (Mathf.Abs(transform.position.x) > 0.25)
                {
                    Debug.Log("Hit");
                    RhythmGameManager.instance.normalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.x) > 0.05f)
                {
                    Debug.Log("Good Hit");
                    RhythmGameManager.instance.goodHit();
                    Instantiate(goodHitEffect, transform.position, goodHitEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Perfect Hit");
                    RhythmGameManager.instance.perfectHit();
                    Instantiate(perfectHitEffect, transform.position, perfectHitEffect.transform.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.tag == "Scratch Activator")
    //    {
    //        canBePressed = true;
    //    }
    //}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            canBePressed = false;

            RhythmGameManager.instance.noteMissed();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
        }
    }

    public IEnumerator noteHit()
    {
        if (leftBeat)
        {
            animator.SetTrigger("left_onHit");
        }
        else if(rightBeat)
        {
            animator.SetTrigger("right_onHit");

        }

        yield return new WaitForSeconds(0.2f);

        gameObject.SetActive(false);
    }
}