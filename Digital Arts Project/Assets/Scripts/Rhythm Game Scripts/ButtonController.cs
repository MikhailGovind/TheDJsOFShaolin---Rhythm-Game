using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite imgDefault;
    public Sprite imgPressed;

    public KeyCode buttonKeyCode;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(buttonKeyCode))
        {
            spriteRenderer.sprite = imgPressed;
        }

        if (Input.GetKeyUp(buttonKeyCode))
        {
            spriteRenderer.sprite = imgDefault;
        }
    }
}
