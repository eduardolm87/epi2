using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ProfileInspector : MonoBehaviour
{
    public Text Name;
    public Text EXP;
    public Text Cat;
    public Text Health;
    public Image Portrait;

    public static Profile CurrentProfile = null;

    public void Open(Profile zProfileToOpen = null)
    {
        AppManager.Instance.UIManager.CloseAllWindows(gameObject, AppManager.Instance.UIManager.StatusBar.gameObject);

        AppManager.Instance.UIManager.StatusBar.Open();

        gameObject.SetActive(true);

        if (CurrentProfile == null || (zProfileToOpen != null && zProfileToOpen != CurrentProfile))
        {
            //iTween.ScaleFrom(gameObject, iTween.Hash("scale", new Vector3(1, 0, 0), "time", 1f, "easetype", iTween.EaseType.easeOutExpo));
        }

        if (zProfileToOpen == null)
            LoadDefaultProfile();
        else
            LoadProfile(zProfileToOpen);
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
        //todo: Cuidado, renombrar tiene truco... hay que hacer esto:
        //Saca un popup para meter el nuevo nombre. Si se mete un nuevo nombre y se acepta, entonces...
        //Si es un perfil de los que hay por defecto, crea uno nuevo con este nombre y respeta el viejo.
        //Si es un perfil de usuario, entonces se carga el viejo y crea uno nuevo con este nombre.

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
                    AppManager.Instance.UIManager.PopupManager.PopupSimple.Open(Defines.warningTitle, Defines.deleteConfirmation + NameOfDeletedProfile, new List<PopupButton>());
                }), 
            new PopupButton(Defines.no, null) });
        }
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
        EXP.text = "EXPERIENCIA: " + CurrentProfile.Experience.ToString();
        Cat.text = "CATARSIS: " + CurrentProfile.Catharsis.ToString();
        Health.text = "SALUD: " + Defines.HealthLevelToString(CurrentProfile.Health);
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
                    AppManager.Instance.UIManager.PopupManager.PopupSimple.Open(Defines.warningTitle, Defines.profileWontBeSavedBecauseDefault, new List<PopupButton>());
                }
            }
        }
        else if (CurrentProfile.hasModifications)
        {
            IOManager.Instance.SaveProfile(CurrentProfile);
        }
    }
}
