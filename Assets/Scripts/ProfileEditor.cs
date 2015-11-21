using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ProfileEditor : MonoBehaviour
{
    public static Profile CurrentlyEditingProfile = null;


    public ProfilePreview ProfilePreview;
    public ProfileEditorSections Tabs;

    public enum Sections { Attributes = 0, Modifiers = 1, Powers = 2, Notes = 3 };

    public PREDAttributes Attributes;
    public PREDModifiers Modifiers;
    public PREDPowers Powers;
    public PREDNotes Notes;


    public void Open()
    {
        AppManager.Instance.UIManager.CloseAllWindows(gameObject);

        gameObject.SetActive(true);

        //iTween.ScaleFrom(gameObject, iTween.Hash("scale", new Vector3(1, 0, 0), "time", 0.25f, "easetype", iTween.EaseType.easeOutExpo));

        OpenTab(Sections.Attributes);

        LoadProfile(new Profile(ProfileInspector.CurrentProfile)); //crea nuevo perfil copia del perfil que hay en el inspector
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OpenTab(int zSectionNumber)
    {
        OpenTab((Sections)zSectionNumber);
    }

    public void OpenTab(Sections zSection)
    {
        CloseAllTabs(zSection);

        switch (zSection)
        {
            case Sections.Attributes: Attributes.Open(); break;
            case Sections.Modifiers: Modifiers.Open(); break;
            case Sections.Powers: Powers.Open(); break;
            case Sections.Notes: Notes.Open(); break;
        }
    }

    public void CloseAllTabs(params Sections[] Exceptions)
    {
        if (!Exceptions.Contains(Sections.Attributes))
        {
            Attributes.Close();
        }
        if (!Exceptions.Contains(Sections.Modifiers))
        {
            Modifiers.Close();
        }
        if (!Exceptions.Contains(Sections.Powers))
        {
            Powers.Close();
        }
        if (!Exceptions.Contains(Sections.Notes))
        {
            Notes.Close();
        }
    }

    void LoadProfile(Profile zProfile)
    {
        CurrentlyEditingProfile = zProfile;
        //todo: refrescar EXP
        ProfilePreview.Refresh();
    }

    public void Refresh()
    {
        LoadProfile(CurrentlyEditingProfile);
    }
}
