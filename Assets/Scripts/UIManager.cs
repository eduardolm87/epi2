using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public PopupManager PopupManager;
    public StatusBar StatusBar;
    public ProfileEditor ProfileEditor;
    public ProfileInspector ProfileInspector;
    public TitleScreen TitleScreen;
    public ProfileSelector ProfileSelector;


    public void OpenFromStart()
    {
        CloseAllWindows(TitleScreen.gameObject);
        TitleScreen.Open();
    }

    public void CloseAllWindows(params GameObject[] Exceptions)
    {
        if (!Exceptions.Contains(PopupManager.gameObject))
        {
            PopupManager.CloseAll();
        }

        if (!Exceptions.Contains(StatusBar.gameObject))
        {
            StatusBar.Close();
        }

        if (!Exceptions.Contains(ProfileEditor.gameObject))
        {
            ProfileEditor.Close();
        }

        if (!Exceptions.Contains(ProfileInspector.gameObject))
        {
            ProfileInspector.Close();
        }

        if (!Exceptions.Contains(TitleScreen.gameObject))
        {
            TitleScreen.Close();
        }

        if (!Exceptions.Contains(ProfileSelector.gameObject))
        {
            ProfileSelector.Close();
        }
    }
}
