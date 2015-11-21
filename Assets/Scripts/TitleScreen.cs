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
        //todo: entrar al ultimo perfil? o al selector de perfiles?
        AppManager.Instance.UIManager.ProfileInspector.Open();
    }
}
