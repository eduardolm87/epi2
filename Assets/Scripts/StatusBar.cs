using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Text Text;


    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Refresh()
    {
        string name = ProfileInspector.CurrentProfile.Name ?? "???";
        if (!AppManager.Instance.UIManager.ProfileInspector.isAnySubwindowOpen)
        {
            Text.text = name;
        }
        else if (AppManager.Instance.UIManager.ProfileInspector.AttributesWindow.gameObject.activeInHierarchy)
        {
            Text.text = name + " > " + Defines.checksAttributes;
        }
        else if (AppManager.Instance.UIManager.ProfileInspector.PowersWindow.gameObject.activeInHierarchy)
        {
            Text.text = name + " > " + Defines.checksPowers;
        }
        else if (AppManager.Instance.UIManager.ProfileInspector.DamagesWindow.gameObject.activeInHierarchy)
        {
            Text.text = name + " > " + Defines.checksDamages;
        }
    }

    public void BackButton()
    {
        if (AppManager.Instance.UIManager.ProfileInspector.isAnySubwindowOpen)
        {
            AppManager.Instance.UIManager.ProfileInspector.CloseAllSubWindows();
            AppManager.Instance.UIManager.StatusBar.Refresh();
        }
        else
        {
            AppManager.Instance.UIManager.ProfileInspector.BackToProfileSelector();
        }
    }
}
