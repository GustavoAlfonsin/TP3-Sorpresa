using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCameraController : MonoBehaviour
{
    [Header("Teléfono")]
    [SerializeField] private RectTransform phoneUI;

    [Header("Movimiento")]
    [SerializeField] private float dragSpeed = 1f;

    private bool cameraOpen;

    private Vector2 initialPosition;

    private void Start()
    {
        initialPosition = phoneUI.anchoredPosition;
    }

    private void Update()
    {
        if (!cameraOpen)
            return;

        if (Input.GetMouseButton(2))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            phoneUI.anchoredPosition +=
                new Vector2(mouseX, mouseY) * dragSpeed;
        }
    }

    public void OpenCamera()
    {
        cameraOpen = true;
    }

    public void CloseCamera()
    {
        cameraOpen = false;

        phoneUI.anchoredPosition = initialPosition;
    }
}
