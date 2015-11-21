using UnityEngine;
using System.Collections;

public class PopupManager : MonoBehaviour
{
    public PopupSimple PopupSimple;
    public PopupModifiersEditor PopupModifiersEditor;

    public bool CanOpen
    {
        get
        {
            return !gameObject.activeInHierarchy;
        }
    }

    public void CloseAll()
    {
        PopupModifiersEditor.Close();
        PopupModifiersEditor.Close();
    }
}
