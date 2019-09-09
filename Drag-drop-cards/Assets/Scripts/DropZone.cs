using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //ventData.pointerDrag.name é o objeto que vc está arrastando
        //gameObject.name é o objeto que possúi o script DropZone
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        Card c = eventData.pointerDrag.GetComponent<Card>();
        if (c != null) c.placeholderParent = transform;


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
        if (eventData.pointerDrag == null)
        {
            return;
        }

        Card c = eventData.pointerDrag.GetComponent<Card>();
        if (c != null) c.parentToReturnTo = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
        if (eventData.pointerDrag == null)
        {
            return;
        }
        Card c = eventData.pointerDrag.GetComponent<Card>();
        if (c != null && c.placeholderParent==this.transform) c.placeholderParent = c.parentToReturnTo;
    }
}
