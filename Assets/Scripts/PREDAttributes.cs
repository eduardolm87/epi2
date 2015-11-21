using UnityEngine;
using System.Collections;

public class PREDAttributes : MonoBehaviour
{
    public void Open()
    {
        AppManager.Instance.UIManager.ProfileEditor.Tabs.LightTab(ProfileEditor.Sections.Attributes);
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
