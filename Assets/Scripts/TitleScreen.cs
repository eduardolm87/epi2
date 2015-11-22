using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    string websiteURL = "http://www.epiphanygame.com";

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void WebButton()
    {
        Application.OpenURL(websiteURL);
    }

    public void StartAppZone()
    {
        //AppManager.Instance.UIManager.ProfileInspector.Open();
        AppManager.Instance.UIManager.ProfileSelector.Open();
    }
}
