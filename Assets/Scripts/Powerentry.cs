using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class Powerentry : MonoBehaviour
{
    [HideInInspector]
    public Power Power = null;
    public Text Name;

    public void Assign(Power zPower)
    {
        this.Power = zPower;
        Refresh();
    }

    public void Refresh()
    {
        Name.text = Power.Name;
    }

    public void RemoveButton()
    {
        AppManager.Instance.SoundManager.Play("OptionPick");

        ProfileEditor.CurrentlyEditingProfile.Powers.RemoveAll(p => p.Name == Power.Name);

        AppManager.Instance.UIManager.ProfileEditor.Refresh();
        AppManager.Instance.UIManager.ProfileEditor.Powers.LoadPowersFromProfile();
    }

    public void InfoButton()
    {
        AppManager.Instance.UIManager.PopupManager.PowerDescriptorPopup.Open(Power);
    }

    public void EditButton()
    {
        List<PowerExample> availablePowers = AppManager.Instance.ReferenceManager.PowerReferences.Where(x => x.Power.Name == Power.Name || !ProfileEditor.CurrentlyEditingProfile.Powers.ConvertAll(j => j.Name).Contains(x.Power.Name)).ToList();

        AppManager.Instance.UIManager.PopupManager.PowerSelectorPopUp.Open(availablePowers, new Action<Power>(delegate(Power zPower)
        {
            if (zPower.Name == Power.Name)
                return;


            int index = ProfileEditor.CurrentlyEditingProfile.Powers.FindIndex(p => p.Name == Power.Name);

            ProfileEditor.CurrentlyEditingProfile.Powers.RemoveAll(p => p.Name == Power.Name);
            ProfileEditor.CurrentlyEditingProfile.Powers.Insert(index, zPower);

            AppManager.Instance.UIManager.ProfileEditor.Refresh();
            AppManager.Instance.UIManager.ProfileEditor.Powers.LoadPowersFromProfile();

        }));
    }
}
