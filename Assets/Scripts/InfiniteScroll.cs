using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class InfiniteScroll : MonoBehaviour
{
    public float scrollSpeed;
    private RectTransform rectTransform;
    private VerticalLayoutGroup verticalLayout;
    private bool onRePosition = false;

    private float value;
    void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        verticalLayout = GetComponent<VerticalLayoutGroup>();
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry onDrag = new EventTrigger.Entry();
        onDrag.eventID = EventTriggerType.Drag;
        onDrag.callback.AddListener(ScrollDrag);
        trigger.triggers.Add(onDrag);
        EventTrigger.Entry endDrag = new EventTrigger.Entry();
        endDrag.eventID = EventTriggerType.EndDrag;
        endDrag.callback.AddListener(EndDrag);
        trigger.triggers.Add(endDrag);
    }


    void Update()
    {
        if (onRePosition)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition
                            , new Vector2(158, 100), scrollSpeed * 2f * Time.deltaTime);
            if (Mathf.Abs(rectTransform.anchoredPosition.y) < 0.5f)
            {
                rectTransform.anchoredPosition = new Vector2(158, 100);
                onRePosition = false;
            }
        }
    }

    //Object Pool for Infinite Scrool 
    public void ScrollDrag(BaseEventData eventData)
    {
        value = verticalLayout.minHeight + verticalLayout.padding.top;

        onRePosition = false;
        PointerEventData pointer = (PointerEventData)eventData;
        rectTransform.anchoredPosition += new Vector2(0, pointer.delta.y * scrollSpeed);

        if (rectTransform.anchoredPosition.y < -value)
        {
            rectTransform.anchoredPosition = new Vector2(158, 100);
            transform.GetChild(transform.childCount - 1).SetSiblingIndex(0);
        }
        else if (rectTransform.anchoredPosition.y > value)
        {
            rectTransform.anchoredPosition = new Vector2(158, 100);
            transform.GetChild(0).SetSiblingIndex(transform.childCount - 1);
        }
    }

    public void EndDrag(BaseEventData eventData)
    {
        onRePosition = true;
    }
}
