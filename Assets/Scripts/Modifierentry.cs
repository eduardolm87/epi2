using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Modifierentry : MonoBehaviour
{
    [HideInInspector]
    public Modifier Modifier = new Modifier();
    public Text Name;

    public void Assign(Modifier zModifier)
    {
        this.Modifier = zModifier;
        Refresh();
    }

    public void Refresh()
    {
        Name.text = Modifier.Name + " (" + Modifier.FormattedLevel + ")";
    }

    public void EditButton()
    {
        AppManager.Instance.UIManager.PopupManager.PopupModifiersEditor.Open(Modifier);
    }

    public void RemoveButton()
    {
        ProfileEditor.CurrentlyEditingProfile.Modifiers.RemoveAll(m => m.Name == Modifier.Name);
        Debug.Log("Eliminado " + Modifier.Name);
        Debug.Log("En el profile quedan: " + string.Join(",", ProfileEditor.CurrentlyEditingProfile.Modifiers.ConvertAll(m => m.Name).ToArray()));
        AppManager.Instance.UIManager.ProfileEditor.Modifiers.LoadModifiersFromProfile();
    }
}
