using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerPickSlot : MonoBehaviour
{
    [HideInInspector]
    public PowerExample PowerExample;
    public Text Text;

    public Image ImageSlot;

    [HideInInspector]
    public bool Selected = false;

    public void Assign(PowerExample zPowerExample)
    {
        PowerExample = zPowerExample;
        Selected = false;
        Refresh();
    }

    public void Refresh()
    {
        Text.text = PowerExample.Power.Name + " (" + Defines.levelName + " " + PowerExample.Level.ToString() + ")";

        if (Selected)
        {
            Text.text = "<b>" + Text.text + "</b>";
            ImageSlot.color = AppManager.Instance.UIManager.PopupManager.PowerSelectorPopUp.SelectionsColor;
        }
        else
        {
            ImageSlot.color = Color.white;
        }
    }

    public void InfoButton()
    {
        AppManager.Instance.UIManager.PopupManager.PowerDescriptorPopup.Open(PowerExample.Power);
    }

    public void PickButton()
    {
        AppManager.Instance.UIManager.PopupManager.PowerSelectorPopUp.Select(this);
    }

    public void Select(bool zSelected = true)
    {
        Selected = zSelected;
        Refresh();
    }

}
