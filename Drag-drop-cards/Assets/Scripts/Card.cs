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

    public Transform parentToReturnTo = null;



    //OnValidate vai mostrar o objeto montado assim que o script card for atruibuido no slot
    private void OnValidate()
    {
        artworkImage.sprite = cardData.artwork;
        playerAction = cardData.playerAction.ToString();
    }

    //Esse método é a implementação da interface IBeginDragHandler.
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");

        //Esse é o pai "inicial"
        parentToReturnTo = this.transform.parent;

        //Aqui ele vai "deslocar"o parentesco para um nível acima.
        this.transform.SetParent(this.transform.parent.parent);

        //CanvasGroup é um componente que foi adicionado no prefab card.
        //no início do drag setamos blockRaycast para false, assim vai ser
        //possível identificar os eventos das areas que tem DropZone.cs
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");

        //No início do deslocamento, esse objeto ficou como filho direto do Canvas
        //assim que acaber o drag ele voltará a ser filho do panel inicial.
        this.transform.SetParent(parentToReturnTo);

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
