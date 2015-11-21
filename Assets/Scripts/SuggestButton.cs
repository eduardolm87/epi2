using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SuggestButton : MonoBehaviour
{
    [HideInInspector]
    public Modifier Modifier = null;
    public Text ModifierText;

    public void Assign(Modifier zModifier)
    {
        Modifier = zModifier;
        Refresh();
    }

    public void Refresh()
    {
        ModifierText.text = Modifier.Name;
    }

    public void ChooseClick()
    {
        AppManager.Instance.UIManager.PopupManager.PopupModifiersEditor.ChooseSuggestion(Modifier);
    }


}
