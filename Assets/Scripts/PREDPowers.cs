using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class PREDPowers : MonoBehaviour
{
    List<Powerentry> InstantiatedEntries = new List<Powerentry>();

    public Powerentry PowerEntryPrefab;

    public Transform PowerEntriesList;


    public void Open()
    {
        AppManager.Instance.UIManager.ProfileEditor.Tabs.LightTab(ProfileEditor.Sections.Powers);
        gameObject.SetActive(true);

        LoadPowersFromProfile();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }




    public void LoadPowersFromProfile()
    {
        RemoveDeprecatedEntries();

        foreach (Power power in ProfileEditor.CurrentlyEditingProfile.Powers)
        {
            Power powerDefinition = AppManager.Instance.ReferenceManager.Powers.FirstOrDefault(p => p.Name == power.Name);
            if (powerDefinition == null)
            {
                Debug.LogError("Unrecognized power " + power.Name + " in profile " + ProfileEditor.CurrentlyEditingProfile.Name);
                continue;
            }

            Powerentry entry = InstantiatedEntries.FirstOrDefault(p => p.Power.Name == powerDefinition.Name);
            if (entry == null)
            {
                AddNewPowerToList(powerDefinition);
            }
            else
            {
                entry.Assign(powerDefinition);
            }
        }

    }

    void RemoveDeprecatedEntries()
    {
        List<Powerentry> deprecatedEntries = InstantiatedEntries.Where(m => !ProfileEditor.CurrentlyEditingProfile.Powers.ConvertAll(x => x.Name).Contains(m.Power.Name)).ToList();
        while (deprecatedEntries.Count > 0)
        {
            Destroy(deprecatedEntries[0].gameObject);
            InstantiatedEntries.Remove(deprecatedEntries[0]);
            deprecatedEntries.RemoveAt(0);
        }
    }

    void AddNewPowerToList(Power zPower)
    {
        GameObject entryobj = Instantiate(PowerEntryPrefab.gameObject) as GameObject;
        entryobj.transform.SetParent(PowerEntriesList);
        entryobj.transform.localScale = PowerEntryPrefab.transform.localScale;

        Powerentry entry = entryobj.GetComponent<Powerentry>();
        entry.Assign(zPower);

        InstantiatedEntries.Add(entry);
    }

    public void AddNewPowerButton()
    {
        List<PowerExample> availablePowers = AppManager.Instance.ReferenceManager.PowerReferences.Where(x => !ProfileEditor.CurrentlyEditingProfile.Powers.ConvertAll(j => j.Name).Contains(x.Power.Name)).ToList();

        AppManager.Instance.UIManager.PopupManager.PowerSelectorPopUp.Open(availablePowers, new Action<Power>(delegate(Power zPower)
            {
                ProfileEditor.CurrentlyEditingProfile.Powers.Add(zPower);

                LoadPowersFromProfile();
            }));
    }




}
