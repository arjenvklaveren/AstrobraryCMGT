using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScreenUI : MonoBehaviour, IScreenUI
{
    protected bool isActive;

    public void Open()
    {
        if (ScreenManager.Instance.HasScreenActive()) ScreenManager.Instance.GetCurrentScreen().Close();
        ScreenManager.Instance.SetCurrentScreen(this);
        gameObject.SetActive(true);
        isActive = true;
        OnOpen();
    }

    public void Open(object data = null)
    {
        if (ScreenManager.Instance.HasScreenActive()) ScreenManager.Instance.GetCurrentScreen().Close();
        ScreenManager.Instance.SetCurrentScreen(this);
        gameObject.SetActive(true);
        isActive = true;
        OnOpen(data);
    }

    public void Close()
    {
        if (ScreenManager.Instance.HasScreenActive()) ScreenManager.Instance.SetCurrentScreen(null);
        gameObject.SetActive(false);
        isActive = false;
        OnClose();
    }

    public bool GetActiveState() { return isActive; }

    protected abstract void OnOpen(object data = null);
    protected abstract void OnClose();
}
