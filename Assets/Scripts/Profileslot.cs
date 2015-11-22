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
        AppManager.Instance.UIManager.ProfileInspector.Open(Profile);
    }

    public void Assign(Profile zProfile)
    {
        Profile = zProfile;
        Refresh();
    }

    public void Refresh()
    {
        Text.text = Profile.Name;
        //todo: más cosas podrían venir aquí, como la hora de ultima modificacion, o autor, etc.
    }
}
