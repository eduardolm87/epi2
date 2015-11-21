using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Modifierentry : MonoBehaviour
{
    [HideInInspector]
    public Modifier Modifier = null;
    public Text Name;

    public void Assign(Modifier zModifier)
    {
        Modifier = zModifier;
        Refresh();
    }

    public void Refresh()
    {
        Name.text = Modifier.Name + " (" + Modifier.FormattedLevel + ")";
    }

    public void EditButton()
    {
        //todo
    }

    public void RemoveButton()
    {
        //todo
    }
}
