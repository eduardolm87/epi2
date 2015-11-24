using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class ProfileInspector : MonoBehaviour
{
    public Text Name;
    public Text EXPtitle;
    public Text EXP;
    public Text Healthtitle;
    public Text Health;
    public Text CATtitle;
    public Text CAT;
    public Image Portrait;

    public static Profile CurrentProfile = null;

    public void Open(Profile zProfileToOpen = null)
    {
        AppManager.Instance.UIManager.CloseAllWindows(gameObject, AppManager.Instance.UIManager.StatusBar.gameObject);

        gameObject.SetActive(true);

        if (CurrentProfile == null || (zProfileToOpen != null && zProfileToOpen != CurrentProfile))
        {
            //iTween.ScaleFrom(gameObject, iTween.Hash("scale", new Vector3(1, 0, 0), "time", 1f, "easetype", iTween.EaseType.easeOutExpo));
        }

        if (zProfileToOpen == null)
        {
            LoadDefaultProfile();
        }
        else
        {
            LoadProfile(zProfileToOpen);
        }

        AppManager.Instance.UIManager.StatusBar.Open();
    }

    public void Close()
    {
        SaveCurrentProfile();

        AppManager.Instance.UIManager.StatusBar.Close();

        gameObject.SetActive(false);
    }

    public void ButtonProfileEditor()
    {
        AppManager.Instance.UIManager.ProfileEditor.Open();
    }

    public void ButtonChecks()
    {
        //todo

    }

    public void ButtonDamage()
    {
        //todo

    }

    public void ButtonPowers()
    {
        //todo

    }

    public void ButtonRename()
    {
        AppManager.Instance.UIManager.PopupManager.PopupRenameProfile.Open(CurrentProfile, new Action<string>(delegate(string zInput)
            {
                AppManager.Instance.UIManager.ProfileInspector.RenameProfile(zInput);
            }));
    }

    public void ButtonDelete()
    {
        if (CurrentProfile.isDefaultProfile)
        {
            AppManager.Instance.UIManager.PopupManager.PopupSimple.Open(Defines.errorPopupTitle, Defines.defaultProfileCantDelete, new List<PopupButton>());
        }
        else
        {
            AppManager.Instance.UIManager.PopupManager.PopupSimple.Open(Defines.warningTitle, Defines.reallydeleteProfile + " " + CurrentProfile.Name + "?", new List<PopupButton>() { 
            new PopupButton(Defines.yes, 
                delegate 
                {
                    string NameOfDeletedProfile = CurrentProfile.Name;


                    if(!IOManager.Instance.DeleteProfile(CurrentProfile))
                    {
                        Debug.LogError("Error deleting profile");
                    }

                    CurrentProfile = null;
                    ProfileEditor.CurrentlyEditingProfile = null;
                    AppManager.Instance.UIManager.CloseAllWindows();
                    AppManager.Instance.UIManager.ProfileSelector.Open();

                    AppManager.Instance.UIManager.PopupManager.CloseAll();
                    Debug.Log("Deberia abrirse ventana de confirmacion justo ahora");
                   
                    //todo bug : Esto no se abre
                    AppManager.Instance.UIManager.PopupManager.PopupSimple.Open(Defines.warningTitle, Defines.deleteConfirmation + NameOfDeletedProfile, new List<PopupButton>());
                }), 
            new PopupButton(Defines.no, null) });
        }
    }

    public void ButtonLog()
    {
        //todo
    }

    void LoadDefaultProfile()
    {
        LoadProfile(AppManager.Instance.ReferenceManager.DefaultProfiles.First());
    }

    public void LoadProfile(Profile zProfile)
    {
        CurrentProfile = zProfile;

        RefreshInspector();
    }

    void RefreshInspector()
    {
        Name.text = CurrentProfile.Name;

        EXPtitle.text = Defines.experience;
        EXP.text = CurrentProfile.Experience.ToString();

        Healthtitle.text = Defines.health;
        Health.text = Defines.HealthLevelToString(CurrentProfile.Health);

        CATtitle.text = Defines.catharsis;
        CAT.text = CurrentProfile.Catharsis.ToString();

        //todo: portrait
    }

    public static void SaveCurrentProfile()
    {
        if (CurrentProfile == null)
            return;

        if (CurrentProfile.isDefaultProfile)
        {
            if (CurrentProfile.hasModifications)
            {
                if (!Defines.AdviceForOverwritingExamplesDisplayed)
                {
                    Defines.AdviceForOverwritingExamplesDisplayed = true;
                    //todo bug: esto no está saliendo, y debería
                    AppManager.Instance.UIManager.PopupManager.PopupSimple.Open(Defines.warningTitle, Defines.profileWontBeSavedBecauseDefault, new List<PopupButton>());
                }
            }
        }
        else if (CurrentProfile.hasModifications)
        {
            IOManager.Instance.SaveProfile(CurrentProfile);
        }
    }

    public void RenameProfile(string zInput)
    {

        if (!CurrentProfile.isDefaultProfile)
        {
            IOManager.Instance.DeleteProfile(CurrentProfile);
            CurrentProfile.Name = zInput;
        }
        else
        {
            //todo
            CurrentProfile = new Profile(CurrentProfile);
            CurrentProfile.Name = zInput;
        }

        IOManager.Instance.SaveProfile(CurrentProfile);
        AppManager.Instance.ReferenceManager.LoadUserProfiles();
        LoadProfile(CurrentProfile);
    }
}
