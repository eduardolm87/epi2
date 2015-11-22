using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProfilePreview : MonoBehaviour
{
    public Text Name;
    public Text EXPCAT;

    public void Refresh()
    {
        Name.text = ProfileEditor.CurrentlyEditingProfile.Name;
        EXPCAT.text = "EXP: " + ProfileEditor.CurrentlyEditingProfile.Experience.ToString() + "   " + "CAT: " + ProfileEditor.CurrentlyEditingProfile.Catharsis.ToString();
    }

    public void BackButton()
    {
        ProfileEditor.CurrentlyEditingProfile = null;
        AppManager.Instance.UIManager.ProfileEditor.Close();
        AppManager.Instance.UIManager.ProfileInspector.Open();
    }

    public void DoneButton()
    {
        ProfileInspector.CurrentProfile = ProfileEditor.CurrentlyEditingProfile;
        ProfileInspector.SaveCurrentProfile();
        AppManager.Instance.UIManager.ProfileEditor.Close();
        AppManager.Instance.UIManager.ProfileInspector.Open();
    }
}
