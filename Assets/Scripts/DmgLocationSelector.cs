using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DmgLocationSelector : MonoBehaviour
{
    public DAMAGELOCATIONS Location = DAMAGELOCATIONS.SUPERFICIAL;
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
        Value.text = Defines.DamageLocationToString(Location);
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
        AppManager.Instance.UIManager.ProfileInspector.DamagesWindow.SelectLocation(this);
    }

}
