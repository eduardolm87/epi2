using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class PREDNotes : MonoBehaviour
{
    public InputField Notes;
    public InputField Conduct;
    public Color SelectedSequelHandleColor = Color.yellow;

    public List<SequelEntry> SequelEntries = new List<SequelEntry>();

    [HideInInspector]
    public bool Loading = false; //dirty hacky trick :P

    public void Open()
    {
        AppManager.Instance.UIManager.ProfileEditor.Tabs.LightTab(ProfileEditor.Sections.Notes);
        gameObject.SetActive(true);

        Loading = true;

        Conduct.text = ProfileEditor.CurrentlyEditingProfile.Conduct;
        Notes.text = ProfileEditor.CurrentlyEditingProfile.Notes;
        LoadSequels();

        Loading = false;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }


    public void ChangeConduct()
    {
        ProfileEditor.CurrentlyEditingProfile.Conduct = Conduct.text;
        Debug.Log("Cambia conducta a " + ProfileEditor.CurrentlyEditingProfile.Conduct);
    }

    public void ChangeNotes()
    {
        ProfileEditor.CurrentlyEditingProfile.Notes = Notes.text;
    }

    void LoadSequels()
    {
        for (int i = 0; i < SequelEntries.Count; i++)
        {
            if (i < AppManager.Instance.ReferenceManager.Sequels.Count)
            {
                SequelEntries[i].Assign(AppManager.Instance.ReferenceManager.Sequels[i]);

                //Activate depending on profile
                if (ProfileEditor.CurrentlyEditingProfile.Sequels.Any(s => s == AppManager.Instance.ReferenceManager.Sequels[i].Name))
                {
                    SequelEntries[i].Slider.value = SequelEntries[i].Slider.maxValue;
                }
                else
                {
                    SequelEntries[i].Slider.value = SequelEntries[i].Slider.minValue;
                }
            }
        }
    }

    public void SelectSequel(SequelEntry zSequelEntry)
    {
        if (Loading)
            return;

        if (zSequelEntry.Checked && zSequelEntry.Sequel != null)
        {
            AppManager.Instance.UIManager.PopupManager.PopupSimple.Open(zSequelEntry.Sequel.Name, zSequelEntry.Sequel.Description, new List<PopupButton>());
        }

        string candidate = ProfileEditor.CurrentlyEditingProfile.Sequels.FirstOrDefault(s => s == zSequelEntry.Sequel.Name);
        if (zSequelEntry.Checked)
        {
            //Adding a sequel
            if (candidate == null)
            {
                ProfileEditor.CurrentlyEditingProfile.Sequels.Add(zSequelEntry.Sequel.Name);
            }
        }
        else
        {
            //Removing a sequel
            if (candidate != null)
            {
                ProfileEditor.CurrentlyEditingProfile.Sequels.RemoveAll(s => s == zSequelEntry.Sequel.Name);
            }
        }
    }

}
