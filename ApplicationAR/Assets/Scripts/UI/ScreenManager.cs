using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    [SerializeField] ScreenUI startupScreen;
    private ScreenUI currentScreen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        startupScreen.Open();
    }

    public void SetCurrentScreen(ScreenUI screen) { currentScreen = screen; }
    public ScreenUI GetCurrentScreen() { return currentScreen; }
    public bool HasScreenActive() { return currentScreen != null; }
}
