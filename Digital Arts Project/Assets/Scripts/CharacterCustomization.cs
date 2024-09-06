using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomization : MonoBehaviour
{
    [Header("Hair")]
    public int hairStyle;
    public int hairColor;

    [Header("Jacket")]
    public int jacketStyle;
    public int jacketColor;

    [Header("Shirt")]
    public int shirtColor;

    [Header("Bottoms")]
    public int bottomsStyle;
    public int bottomsColor;

    [Header("Shoes")]
    public int shoeColor;

    private void Start()
    {
        hairStyle = 0;
        hairColor = 0;

        jacketStyle = 0;
        jacketColor = 0;

        shirtColor = 0;
        
        bottomsStyle = 0;
        bottomsColor = 0;

        shoeColor = 0;
    }

    private void Update()
    {
        if (hairStyle == 1
            && hairColor == 1
            && jacketStyle == 1
            && jacketColor == 1
            && shirtColor == 1
            && bottomsStyle == 1
            && bottomsColor == 1
            && shoeColor == 1
            )
        {
            //change sprite
        }
    }
}
