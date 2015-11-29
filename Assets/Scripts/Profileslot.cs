using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Profileslot : MonoBehaviour
{
    public Text Text;
    public Image Icon;

    public Profile Profile = new Profile();

    public void ClickToEdit()
    {
        AppManager.Instance.SoundManager.Play("Pick");

        AppManager.Instance.UIManager.ProfileInspector.Open(Profile);
        LogPopup.AddNewMessage(new LogMessage(System.DateTime.Now, AppManager.Instance.UIManager.PopupManager.LogPopup.NoteColor, Profile.Name, Defines.profileOpenLog));
    }

    public void Assign(Profile zProfile)
    {
        Profile = zProfile;
        Refresh();
    }

    public void Refresh()
    {
        if (Profile.isDefaultProfile)
        {
            Text.color = AppManager.Instance.UIManager.ProfileSelector.DefaultProfileColor;
        }
        else
        {
            Text.color = AppManager.Instance.UIManager.ProfileSelector.UserProfileColor;
        }

        Text.text = Profile.Name;

        //todo: más cosas podrían venir aquí, como la hora de ultima modificacion, o autor, etc.
    }
}
