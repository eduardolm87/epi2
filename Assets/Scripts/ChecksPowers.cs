using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class ChecksPowers : MonoBehaviour
{
    public Text ChosenPowerLabel;
    public Transform AdditionalOptionsList;
    public ComplexityInput ComplexityInput;
    public AdditionalOptionSlot AdditionalOptionSlotPrefab;
    public Color AdditionalOptionsCheckedColor = Color.yellow;

    [HideInInspector]
    PowerExample SelectedPowerExample = null;
    [HideInInspector]
    public List<AdditionalOptionSlot> AddOptsLoaded = new List<AdditionalOptionSlot>();



    public void Open()
    {
        AppManager.Instance.UIManager.ProfileInspector.CloseAllSubWindows(gameObject);
        gameObject.SetActive(true);
        AppManager.Instance.UIManager.StatusBar.Refresh();

        //No powers in profile: close
        if (ProfileInspector.CurrentProfile.Powers.Count < 1)
        {
            Close();
            return;
        }

        //Previously used profile: load that
        if (SelectedPowerExample != null)
        {
            if (ProfileInspector.CurrentProfile.Powers.ConvertAll(p => p.Name).Contains(SelectedPowerExample.Power.Name))
            {
                LoadPowerExample(SelectedPowerExample);
                return;
            }
        }

        //First time entering this section: load first power
        PowerExample powerToLoad = AppManager.Instance.ReferenceManager.PowerReferences.FirstOrDefault(p => p.Power.Name == ProfileInspector.CurrentProfile.Powers[0].Name);
        if (powerToLoad != null)
        {
            LoadPowerExample(powerToLoad);
        }
        else
        {
            Debug.LogError("Error reading power " + ProfileInspector.CurrentProfile.Powers[0].Name + " from profile.");
            ChangeSelectedPowerButton();
        }


    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void ClearLastlyUsedPower()
    {
        SelectedPowerExample = null;
    }

    public void LoadPowerExample(PowerExample zPowerExample)
    {
        SelectedPowerExample = zPowerExample;
        ChosenPowerLabel.text = SelectedPowerExample.Power.Name;

        RemoveAllAddOptions();

        //AddOptsLoaded.ForEach(x => x.Checked = false);
        ComplexityInput.Complexity = 0;
        ComplexityInput.MinComplexity = -10;
        ComplexityInput.MaxComplexity = 10;

        LoadAddOptions();
    }

    void RemoveAllAddOptions()
    {
        while (AddOptsLoaded.Count > 0)
        {
            Destroy(AddOptsLoaded[0].gameObject);
            AddOptsLoaded.RemoveAt(0);
        }
    }

    void LoadAddOptions()
    {
        foreach (PowerExample.AdditionalOption addop in SelectedPowerExample.AdditionalOptions)
        {
            AddAOPtoList(addop);
        }
    }

    void AddAOPtoList(PowerExample.AdditionalOption zAddOpp)
    {
        GameObject entryObj = Instantiate(AdditionalOptionSlotPrefab.gameObject) as GameObject;
        entryObj.transform.SetParent(AdditionalOptionsList);
        entryObj.transform.localScale = AdditionalOptionSlotPrefab.transform.localScale;

        AdditionalOptionSlot entry = entryObj.GetComponent<AdditionalOptionSlot>();
        entry.Assign(zAddOpp);
        entry.Checked = false;

        AddOptsLoaded.Add(entry);
    }

    public void InfoButton()
    {
        AppManager.Instance.UIManager.PopupManager.PowerDescriptorPopup.Open(SelectedPowerExample.Power);
    }

    public void ChangeSelectedPowerButton()
    {
        List<PowerExample> availablePowers = AppManager.Instance.ReferenceManager.PowerReferences.Where(x => ProfileInspector.CurrentProfile.Powers.ConvertAll(y => y.Name).Contains(x.Power.Name)).ToList();

        AppManager.Instance.UIManager.PopupManager.PowerSelectorPopUp.Open(availablePowers, new Action<Power>(delegate(Power zPower)
        {
            if (zPower.Name == SelectedPowerExample.Power.Name)
                return;

            PowerExample chosenPower = AppManager.Instance.ReferenceManager.PowerReferences.FirstOrDefault(p => p.Power.Name == zPower.Name);
            if (chosenPower != null)
            {
                LoadPowerExample(chosenPower);
            }
            else
            {
                Debug.LogError("Error selecting power reference");
            }
        }));
    }

    public void RecalculateComplexity(int zChange = 0)
    {
        int accumulatedComplexity = 0;
        foreach (AdditionalOptionSlot aoslot in AddOptsLoaded)
        {
            if (aoslot.Checked)
            {
                accumulatedComplexity += aoslot.AdditionalOption.Difficulty;
            }
        }

        if (accumulatedComplexity > 0)
        {
            ComplexityInput.MinComplexity = -10 + accumulatedComplexity;
        }
        else if (accumulatedComplexity < 0)
        {
            ComplexityInput.MaxComplexity = 10 + accumulatedComplexity;
        }
        else
        {
            ComplexityInput.MinComplexity = -10;
            ComplexityInput.MaxComplexity = 10;
        }


        ComplexityInput.Complexity = ComplexityInput.Complexity + zChange;

    }

    public void Throw()
    {
        if (SelectedPowerExample != null)
            AppManager.Instance.UIManager.ProfileInspector.ThrowPowerCheck(SelectedPowerExample.Power, ComplexityInput.Complexity);
    }
}
