using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    //This is my scriptableObject
    public CardData cardData;

    public Image artworkImage;
    private string playerAction;

    //OnValidate vai mostrar o objeto montado assim que o script card for atruibuido no slot
    private void OnValidate()
    {
        artworkImage.sprite = cardData.artwork;
        playerAction = cardData.playerAction.ToString();
    }
}
