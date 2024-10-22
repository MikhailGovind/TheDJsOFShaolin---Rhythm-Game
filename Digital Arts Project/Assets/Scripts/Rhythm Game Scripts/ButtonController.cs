using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public KeyCode leftButtonKeyCode;
    public KeyCode rightButtonKeyCode;

    Animator animator;
    public GameObject objKnob;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(leftButtonKeyCode) || Input.GetKeyDown(rightButtonKeyCode))
        {
            animator.SetTrigger("beatButton_onPress");
            objKnob.transform.eulerAngles = new Vector3(objKnob.transform.eulerAngles.x, objKnob.transform.eulerAngles.y, objKnob.transform.eulerAngles.z - 90);
        }

        if (Input.GetKeyUp(leftButtonKeyCode) || Input.GetKeyUp(rightButtonKeyCode))
        {
            animator.SetTrigger("beatButton_offPress");
            objKnob.transform.eulerAngles = new Vector3(objKnob.transform.eulerAngles.x, objKnob.transform.eulerAngles.y, objKnob.transform.eulerAngles.z + 90);
        }
    }
}
