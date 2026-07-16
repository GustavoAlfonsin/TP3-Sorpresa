using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneUIController : MonoBehaviour
{
    [Header("Referencia")]
    [SerializeField] private RectTransform phonePanel;
    [SerializeField] private PhoneScreenManager _screenManager;

    [Header("Posiciones")]
    [SerializeField] private Vector2 hiddenPosition;
    [SerializeField] private Vector2 visiblePosition;

    [Header("Rotaciones")]
    [SerializeField] private float hiddenRotationZ = -20f;
    [SerializeField] private float visibleRotationZ = 0f;

    [Header("Animación")]
    [SerializeField] private float animationDuration = 0.4f;

    private bool isOpen;
    private bool isAnimating;

    private void Start()
    {
        phonePanel.anchoredPosition = hiddenPosition;
        phonePanel.localRotation = Quaternion.Euler(0f, 0f, hiddenRotationZ);
        phonePanel.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isAnimating)
                return;

            if (isOpen)
                StartCoroutine(HidePhone());
            else
                StartCoroutine(ShowPhone());
        }
    }

    private IEnumerator ShowPhone()
    {
        isAnimating = true;
        phonePanel.gameObject.SetActive(true);
        Vector2 startPosition = hiddenPosition;
        Vector2 endPosition = visiblePosition;

        Quaternion startRotation =
            Quaternion.Euler(0f, 0f, hiddenRotationZ);

        Quaternion endRotation =
            Quaternion.Euler(0f, 0f, visibleRotationZ);

        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;

            float t = timer / animationDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            phonePanel.anchoredPosition =
                Vector2.Lerp(startPosition, endPosition, t);

            phonePanel.localRotation =
                Quaternion.Slerp(startRotation, endRotation, t);

            yield return null;
        }

        phonePanel.anchoredPosition = endPosition;
        phonePanel.localRotation = endRotation;

        isOpen = true;
        isAnimating = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _screenManager.ShowLockScreen();
    }

    private IEnumerator HidePhone()
    {
        isAnimating = true;

        Vector2 startPosition = visiblePosition;
        Vector2 endPosition = hiddenPosition;

        Quaternion startRotation =
            Quaternion.Euler(0f, 0f, visibleRotationZ);

        Quaternion endRotation =
            Quaternion.Euler(0f, 0f, hiddenRotationZ);

        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;

            float t = timer / animationDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            phonePanel.anchoredPosition =
                Vector2.Lerp(startPosition, endPosition, t);

            phonePanel.localRotation =
                Quaternion.Slerp(startRotation, endRotation, t);

            yield return null;
        }

        phonePanel.anchoredPosition = endPosition;
        phonePanel.localRotation = endRotation;

        isOpen = false;
        isAnimating = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        phonePanel.gameObject.SetActive(false);
    }
    public bool IsOpen => isOpen;
}
