using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneInteractionController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private ControlCelular phoneController;
    //[SerializeField] private MonoBehaviour playerLook;

    private bool previousState;

    private void Start()
    {
        previousState = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        bool isPhoneOpen = phoneController.IsPhoneOpen;

        if (isPhoneOpen != previousState)
        {
            UpdateInteractionMode(isPhoneOpen);
            previousState = isPhoneOpen;
        }
    }

    private void UpdateInteractionMode(bool phoneOpen)
    {
        if (phoneOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //if (playerLook != null)
            //    playerLook.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            //if (playerLook != null)
            //    playerLook.enabled = true;
        }
    }
}
