using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite imgDefault;
    public Sprite imgPressed;

    public KeyCode leftButtonKeyCode;
    public KeyCode rightButtonKeyCode;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(leftButtonKeyCode) || Input.GetKeyDown(rightButtonKeyCode))
        {
            spriteRenderer.sprite = imgPressed;
        }

        if (Input.GetKeyUp(leftButtonKeyCode) || Input.GetKeyUp(rightButtonKeyCode))
        {
            spriteRenderer.sprite = imgDefault;
        }
    }
}
