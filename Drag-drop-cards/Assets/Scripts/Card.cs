using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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

    //Esse método é a implementação da interface IBeginDragHandler
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }
}
