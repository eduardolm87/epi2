﻿using UnityEngine;
using System.Collections;

public class ChecksDamage : MonoBehaviour
{
    public void Open()
    {
        AppManager.Instance.UIManager.ProfileInspector.CloseAllSubWindows(gameObject);
        gameObject.SetActive(true);
        AppManager.Instance.UIManager.StatusBar.Refresh();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}