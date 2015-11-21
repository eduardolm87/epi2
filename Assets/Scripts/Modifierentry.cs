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
        this.Modifier = new Modifier(zModifier); //< -- Aqui sale nulo el this.Modifier, no debería!!
        Debug.Log("Primera check de ser nulo?" + (this.Modifier == null)); //<--- Aqui es nulo!

        Refresh();
        Debug.Log("Pero justo aqui es nulo?" + (this.Modifier == null)); //<--- Aqui es nulo!
    }

    public void Refresh()
    {
        Name.text = Modifier.Name + " (" + Modifier.FormattedLevel + ")";
    }

    public void EditButton()
    {
        Debug.Log("Soy el modifierentry con el modificador " + Modifier.Name + ". Mi modifier es nulo?" + (Modifier == null ? "si" : "no"));
        AppManager.Instance.UIManager.PopupManager.PopupModifiersEditor.Open(Modifier);
    }

    public void RemoveButton()
    {
        //todo
    }
}
