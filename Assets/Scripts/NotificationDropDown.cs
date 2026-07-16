using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationDropDown : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject topPanel;
    [SerializeField] private RectTransform notificationPanel;

    [Header("Posiciones")]
    [SerializeField] private Vector2 hiddenPosition;
    [SerializeField] private Vector2 visiblePosition;

    [Header("Animación")]
    [SerializeField] private float animationDuration = 0.25f;

    private bool isOpen;
    private bool isAnimating;

    private void Start()
    {
        topPanel.SetActive(true);

        notificationPanel.gameObject.SetActive(false);
        notificationPanel.anchoredPosition = hiddenPosition;

        isOpen = false;
    }

    public void Open()
    {
        if (isOpen || isAnimating)
            return;
        Debug.Log("SE APRETO EL BOTON");
        StartCoroutine(OpenRoutine());
    }

    public void Close()
    {
        if (!isOpen || isAnimating)
            return;

        StartCoroutine(CloseRoutine());
    }

    private IEnumerator OpenRoutine()
    {
        isAnimating = true;

        topPanel.SetActive(false);

        notificationPanel.gameObject.SetActive(true);

        Vector2 startPos = hiddenPosition;
        Vector2 endPos = visiblePosition;

        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;

            float t = timer / animationDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            notificationPanel.anchoredPosition =
                Vector2.Lerp(startPos, endPos, t);

            yield return null;
        }

        notificationPanel.anchoredPosition = endPos;

        isOpen = true;
        isAnimating = false;
    }

    private IEnumerator CloseRoutine()
    {
        isAnimating = true;

        Vector2 startPos = visiblePosition;
        Vector2 endPos = hiddenPosition;

        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;

            float t = timer / animationDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            notificationPanel.anchoredPosition =
                Vector2.Lerp(startPos, endPos, t);

            yield return null;
        }

        notificationPanel.anchoredPosition = endPos;

        notificationPanel.gameObject.SetActive(false);

        topPanel.SetActive(true);

        isOpen = false;
        isAnimating = false;
    }
}
