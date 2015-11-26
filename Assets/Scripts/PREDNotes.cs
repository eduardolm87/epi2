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


    public void Open()
    {
        AppManager.Instance.UIManager.ProfileEditor.Tabs.LightTab(ProfileEditor.Sections.Notes);
        gameObject.SetActive(true);

        LoadSequels();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }


    public void ChangeConduct()
    {

    }

    public void ChangeNotes()
    {

    }

    void LoadSequels()
    {
        for (int i = 0; i < SequelEntries.Count; i++)
        {
            if (i < AppManager.Instance.ReferenceManager.Sequels.Count)
            {
                SequelEntries[i].Assign(AppManager.Instance.ReferenceManager.Sequels[i]);

                //todo activate depending on profile
                //SequelEntries[i].Slider.value = SequelEntries[i].Slider.minValue;
            }

        }
    }

    public void SelectSequel(SequelEntry zSequelEntry)
    {
        if (zSequelEntry.Checked && zSequelEntry.Sequel != null)
        {
            AppManager.Instance.UIManager.PopupManager.PopupSimple.Open(zSequelEntry.Sequel.Name, zSequelEntry.Sequel.Description, new List<PopupButton>());
        }


    }

}
