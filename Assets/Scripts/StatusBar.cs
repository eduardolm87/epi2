using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Text Text;


    public void Open()
    {
        gameObject.SetActive(true);

        Text.text = ProfileInspector.CurrentProfile.Name ?? "???";
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void BackButton()
    {
        AppManager.Instance.UIManager.CloseAllWindows(AppManager.Instance.UIManager.ProfileSelector.gameObject);
        ProfileEditor.CurrentlyEditingProfile = null;
        ProfileInspector.CurrentProfile = null;
        AppManager.Instance.UIManager.ProfileSelector.Open();
    }
}
