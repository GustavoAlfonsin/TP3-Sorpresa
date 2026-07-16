using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCelular : MonoBehaviour
{
    [Header("Posiciones")]
    [SerializeField] private Vector3 hiddenPosition;
    [SerializeField] private Vector3 shownPosition;

    [Header("Rotación")]
    [SerializeField] private Vector3 hiddenRotation;
    [SerializeField] private Vector3 shownRotation;

    [Header("Animación")]
    [SerializeField] private float animationTime = 0.4f;

    private bool isOpen = false;
    private bool isAnimating = false;

    private void Start()
    {
        transform.localPosition = hiddenPosition;
        transform.localRotation = Quaternion.Euler(hiddenRotation);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isAnimating)
        {
            if (isOpen)
            {
                StartCoroutine(AnimatePhone(hiddenPosition, hiddenRotation, false));
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                StartCoroutine(AnimatePhone(shownPosition, shownRotation, true));
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }       
        }
    }

    private IEnumerator AnimatePhone(Vector3 targetPosition, Vector3 targetRotation, bool openState)
    {
        isAnimating = true;

        Vector3 startPosition = transform.localPosition;
        Quaternion startRotation = transform.localRotation;

        Quaternion endRotation = Quaternion.Euler(targetRotation);

        float elapsed = 0f;

        while (elapsed < animationTime)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / animationTime;
            t = Mathf.SmoothStep(0f, 1f, t);

            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            transform.localRotation = Quaternion.Slerp(startRotation, endRotation, t);

            yield return null;
        }

        transform.localPosition = targetPosition;
        transform.localRotation = endRotation;

        isOpen = openState;
        isAnimating = false;
    }
    public bool IsPhoneOpen => isOpen;
}
