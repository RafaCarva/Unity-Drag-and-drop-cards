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
    public Transform placeholderParent = null;

    public GameObject placeholder = null;



    //OnValidate vai mostrar o objeto montado assim que o script card for atruibuido no slot
    /*private void OnValidate()
    {
        artworkImage.sprite = cardData.artwork;
        playerAction = cardData.playerAction.ToString();
    }
    */

    //Esse método é a implementação da interface IBeginDragHandler.
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");

        //Esse é o pai "inicial"
        parentToReturnTo = this.transform.parent;

        //Cria um GO para manter o gap entre as cartas
        placeholder = new GameObject();
        //seta o parentesco desse novo GO para o "pai local" (porém, vai para o fim da lista)
        placeholder.transform.SetParent(this.transform.parent);
        //Cria, add e seta um LayoutElement para setar o tamanho desse GO
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.flexibleHeight = 0;
        le.flexibleWidth = 0;
        //Recupera o index do filho da carta selecionada e seta par ao placeholder
        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        placeholderParent = parentToReturnTo;

        

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

        if(placeholder.transform.parent != placeholderParent)
        {
            placeholder.transform.SetParent(placeholderParent);
        }

        //Lógica para mover o placeholder
        int newSiblingIndex = placeholderParent.childCount;

        for (int i=0; i < parentToReturnTo.childCount; i++) {
            if(this.transform.position.x < parentToReturnTo.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex) {
                    newSiblingIndex--;
                }
                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");

        //No início do deslocamento, esse objeto ficou como filho direto do Canvas
        //assim que acaber o drag ele voltará a ser filho do panel inicial.
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(placeholder);
    }
}
