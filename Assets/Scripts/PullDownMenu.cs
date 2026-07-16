using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PullDownMenu : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform panel;

    [SerializeField] private float closedY = 600f;
    [SerializeField] private float openedY = 0f;

    [SerializeField] private float openThreshold = 200f;

    private Vector2 startPointerPos;
    private bool dragging;

    public void OnPointerDown(PointerEventData eventData)
    {
        startPointerPos = eventData.position;
        dragging = true;
        Debug.Log("CLICK DETECTADO");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging)
            return;

        float delta = startPointerPos.y - eventData.position.y;

        delta = Mathf.Max(0, delta);

        float newY = closedY - delta;

        newY = Mathf.Clamp(newY, openedY, closedY);

        panel.anchoredPosition =
            new Vector2(panel.anchoredPosition.x, newY);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;

        float currentY = panel.anchoredPosition.y;

        if (closedY - currentY > openThreshold)
        {
            OpenPanel();
        }
        else
        {
            ClosePanel();
        }
    }

    private void OpenPanel()
    {
        panel.anchoredPosition =
            new Vector2(panel.anchoredPosition.x, openedY);
    }

    private void ClosePanel()
    {
        panel.anchoredPosition =
            new Vector2(panel.anchoredPosition.x, closedY);
    }
}
