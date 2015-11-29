using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DmgNatureSelector : MonoBehaviour
{
    public DAMAGENATURES Nature = DAMAGENATURES.ABRASION;
    public Image Background;
    public Text Value;

    bool selected = false;

    public bool Selected
    {
        get { return selected; }
        set
        {
            selected = value;
            Refresh();
        }
    }

    public void Refresh()
    {
        Value.text = Defines.DamageNatureToString(Nature);
        if (Selected)
        {
            Background.color = AppManager.Instance.UIManager.ProfileInspector.DamagesWindow.SelectedSlotColor;
        }
        else
        {
            Background.color = Color.white;
        }
    }

    public void Clicked()
    {
        AppManager.Instance.SoundManager.Play("OptionPick");
        AppManager.Instance.UIManager.ProfileInspector.DamagesWindow.SelectNature(this);
    }

}
