using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChecksDamage : MonoBehaviour
{
    public Color SelectedSlotColor;

    public List<DmgNatureSelector> NatureSlots = new List<DmgNatureSelector>();
    public List<DmgLocationSelector> LocationSlots = new List<DmgLocationSelector>();


    public void Open()
    {
        AppManager.Instance.UIManager.ProfileInspector.CloseAllSubWindows(gameObject);
        gameObject.SetActive(true);
        AppManager.Instance.UIManager.StatusBar.Refresh();

        SelectNature(NatureSlots[0]);
        SelectLocation(LocationSlots[0]);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SelectNature(DmgNatureSelector zSelector)
    {
        foreach (DmgNatureSelector selector in NatureSlots)
        {
            if (selector == zSelector)
            {
                selector.Selected = true;
            }
            else
            {
                selector.Selected = false;
            }
        }
    }


    public void SelectLocation(DmgLocationSelector zSelector)
    {
        foreach (DmgLocationSelector selector in LocationSlots)
        {
            if (selector == zSelector)
            {
                selector.Selected = true;
            }
            else
            {
                selector.Selected = false;
            }
        }
    }

}
