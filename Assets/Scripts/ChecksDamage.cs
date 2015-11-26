using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ChecksDamage : MonoBehaviour
{
    public Color SelectedSlotColor;
    public ToggleGroup IntensityToggleGroup;

    public List<DmgNatureSelector> NatureSlots = new List<DmgNatureSelector>();
    public List<DmgLocationSelector> LocationSlots = new List<DmgLocationSelector>();
    public List<Toggle> IntensityToggles = new List<Toggle>();


    int IntensitySelected = 1;

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

    DAMAGENATURES GetSelectedNature()
    {
        return NatureSlots.FirstOrDefault(x => x.Selected).Nature;
    }

    DAMAGELOCATIONS GetSelectedLocation()
    {
        return LocationSlots.FirstOrDefault(x => x.Selected).Location;
    }

    public int GetIntensitySelected()
    {
        return IntensityToggles.FindIndex(t => t.isOn) + 1;
    }

    public void Throw()
    {
        AppManager.Instance.UIManager.ProfileInspector.ThrowDamageCheck(GetSelectedNature(), GetSelectedLocation(), GetIntensitySelected());
    }
}
