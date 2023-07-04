using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropDuplicate : MonoBehaviour, IDragHandler
{
    RectTransform rect;
    [SerializeField] private Canvas canvas;
    public int id;

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
    }
}
