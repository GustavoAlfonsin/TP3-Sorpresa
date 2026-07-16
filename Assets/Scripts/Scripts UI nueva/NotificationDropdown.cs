using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationDropdown : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private RectTransform panel;

    [Header("Contenido")]
    [SerializeField] private GameObject content;

    [Header("Alturas")]
    [SerializeField] private float closedHeight = 50f;
    [SerializeField] private float openHeight = 400f;

    [Header("Animación")]
    [SerializeField] private float animationDuration = 0.25f;

    private bool isOpen;
    private bool isAnimating;

    private void Start()
    {
        SetHeight(closedHeight);

        if (content != null)
            content.SetActive(false);
    }

    public void Toggle()
    {
        if (isAnimating)
            return;

        if (isOpen)
            StartCoroutine(CloseRoutine());
        else
            StartCoroutine(OpenRoutine());
    }

    private IEnumerator OpenRoutine()
    {
        isAnimating = true;

        if (content != null)
            content.SetActive(true);

        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;

            float t = Mathf.SmoothStep(0f, 1f,
                timer / animationDuration);

            float height =
                Mathf.Lerp(closedHeight, openHeight, t);

            SetHeight(height);

            yield return null;
        }

        SetHeight(openHeight);

        isOpen = true;
        isAnimating = false;
    }

    private IEnumerator CloseRoutine()
    {
        isAnimating = true;

        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;

            float t = Mathf.SmoothStep(0f, 1f,
                timer / animationDuration);

            float height =
                Mathf.Lerp(openHeight, closedHeight, t);

            SetHeight(height);

            yield return null;
        }

        SetHeight(closedHeight);

        if (content != null)
            content.SetActive(false);

        isOpen = false;
        isAnimating = false;
    }

    private void SetHeight(float height)
    {
        Vector2 size = panel.sizeDelta;
        size.y = height;
        panel.sizeDelta = size;
    }
}
