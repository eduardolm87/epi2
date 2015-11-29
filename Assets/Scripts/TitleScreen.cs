using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    string websiteURL = "http://www.epiphanygame.com";

    public void Open()
    {
        AppManager.Instance.SoundManager.Play("IntroLoading");
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
        AppManager.Instance.SoundManager.Play("Pick");
        AppManager.Instance.UIManager.ProfileSelector.Open();
    }
}
