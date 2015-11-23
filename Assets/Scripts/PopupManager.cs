using UnityEngine;
using System.Collections;

public class PopupManager : MonoBehaviour
{
    public PopupSimple PopupSimple;
    public PopupModifiersEditor PopupModifiersEditor;
    public PopupRename PopupRenameProfile;
    public PowerSelectorPopUp PowerSelectorPopUp;
    public PowerDescriptorPopup PowerDescriptorPopup;

    public bool CanOpen
    {
        get
        {
            return !gameObject.activeInHierarchy;
        }
    }

    public void CloseAll()
    {
        PopupSimple.Close();
        PopupModifiersEditor.Close();
        PopupRenameProfile.Close();
        PowerSelectorPopUp.Close();
        PowerDescriptorPopup.Close();
    }

    public int NumberOfActivePopups
    {
        get
        {
            int n = 0;

            if (PopupSimple.gameObject.activeInHierarchy) n++;
            if (PopupModifiersEditor.gameObject.activeInHierarchy) n++;
            if (PopupRenameProfile.gameObject.activeInHierarchy) n++;
            if (PowerSelectorPopUp.gameObject.activeInHierarchy) n++;
            if (PowerDescriptorPopup.gameObject.activeInHierarchy) n++;

            return n;
        }
    }

}
