using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneScreenManager : MonoBehaviour
{
    [Header("Pantalla de bloqueo")]
    [SerializeField] private GameObject lockScreen;

    [Header("Todas las pantallas")]
    [SerializeField] private List<GameObject> screens = new();

    private GameObject currentScreen;

    private Stack<GameObject> screenHistory = new();

    private void Start()
    {
        ShowLockScreen();
    }

    private void DisableAllScreens()
    {
        foreach (GameObject screen in screens)
        {
            if (screen != null)
                screen.SetActive(false);
        }

        if (lockScreen != null)
            lockScreen.SetActive(false);
    }

    public void ShowLockScreen()
    {
        DisableAllScreens();

        lockScreen.SetActive(true);

        currentScreen = lockScreen;

        screenHistory.Clear();
    }

    public void OpenScreen(GameObject screen)
    {
        if (screen == null)
            return;

        if (currentScreen != null && currentScreen != lockScreen)
        {
            screenHistory.Push(currentScreen);
        }

        DisableAllScreens();

        screen.SetActive(true);

        currentScreen = screen;
    }

    public void GoBack()
    {
        if (screenHistory.Count == 0)
            return;

        DisableAllScreens();

        GameObject previousScreen = screenHistory.Pop();

        previousScreen.SetActive(true);

        currentScreen = previousScreen;
    }

    public GameObject GetCurrentScreen()
    {
        return currentScreen;
    }
}
