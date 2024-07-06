
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class Slot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 originalPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = GetComponent<RectTransform>().anchoredPosition;
        originalParent = transform.parent;

        // Make the item follow the pointer and allow raycasts to pass through it
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform, false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move the item with the pointer
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out var localPoint);
        GetComponent<RectTransform>().localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Perform raycast to find the target slot
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.CompareTag("Slot"))
            {
                // Swap positions with the target slot
                var targetSlot = result.gameObject.GetComponent<RectTransform>();
                SwapItems(targetSlot);
                return;
            }
        }

        // If no valid target, reset to original position
        ResetPosition();
    }

    private void SwapItems(RectTransform targetSlot)
    {
        // Get the item in the target slot, if any
        var targetItem = targetSlot.GetComponentInChildren<Slot>(true);
        if (targetItem == null)
        {
            ResetPosition();
            return;
        }
        // Move the target item to the original slot
        targetItem.GetComponent<RectTransform>().anchoredPosition = originalPosition;
        targetItem.transform.SetParent(originalParent, false);
        // Move this item to the target slot
        GetComponent<RectTransform>().anchoredPosition = originalPosition;
        transform.SetParent(targetSlot, false);

        // Re-enable raycasts
        canvasGroup.blocksRaycasts = true;
    }

    private void ResetPosition()
    {
        // Reset this item to its original position
        GetComponent<RectTransform>().anchoredPosition = originalPosition;
        transform.SetParent(originalParent, false);

        // Re-enable raycasts
        canvasGroup.blocksRaycasts = true;
    }



    //------------------------------
    public ObjectOwn obj;


}
