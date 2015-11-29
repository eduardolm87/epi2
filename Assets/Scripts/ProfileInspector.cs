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

    public ChecksAttributes AttributesWindow;
    public ChecksPowers PowersWindow;
    public ChecksDamage DamagesWindow;


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
        AppManager.Instance.UIManager.StatusBar.Refresh();
    }

    public void Close()
    {
        SaveCurrentProfile();

        CloseAllSubWindows();

        AppManager.Instance.UIManager.StatusBar.Close();

        gameObject.SetActive(false);
    }

    public void ButtonProfileEditor()
    {
        AppManager.Instance.SoundManager.Play("Pick");

        AppManager.Instance.UIManager.ProfileEditor.Open();
    }

    public void ButtonChecks()
    {
        AppManager.Instance.SoundManager.Play("Pick");

        AttributesWindow.Open();
    }

    public void ButtonDamage()
    {
        if (CurrentProfile.Health != HEALTHLEVELS.MUERTO)
        {
            AppManager.Instance.SoundManager.Play("Pick");
            DamagesWindow.Open();
        }
        else
        {
            AppManager.Instance.UIManager.PopupManager.PopupSimple.Open(Defines.warningTitle, Defines.deadCharacterNoDamageSection, new List<PopupButton>());
        }
    }

    public void ButtonPowers()
    {
        if (CurrentProfile.Powers.Count > 0)
        {
            AppManager.Instance.SoundManager.Play("Pick");
            PowersWindow.Open();
        }
        else
        {
            AppManager.Instance.UIManager.PopupManager.PopupSimple.Open(Defines.warningTitle, Defines.noPowers, new List<PopupButton>());
        }

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
        AppManager.Instance.SoundManager.Play("Pick");
        AppManager.Instance.UIManager.PopupManager.LogPopup.Open();
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

        AppManager.Instance.UIManager.StatusBar.Refresh();
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
        RefreshInspector();
    }

    public void CloseAllSubWindows(GameObject Exception = null)
    {
        if (Exception != AttributesWindow)
            AttributesWindow.Close();

        if (Exception != PowersWindow)
            PowersWindow.Close();

        if (Exception != DamagesWindow)
            DamagesWindow.Close();
    }

    public bool isAnySubwindowOpen
    {
        get
        {
            return (AttributesWindow.gameObject.activeInHierarchy || PowersWindow.gameObject.activeInHierarchy || DamagesWindow.gameObject.activeInHierarchy);
        }
    }

    public void BackToProfileSelector()
    {
        //Things to do when closing a profile

        PowersWindow.ClearLastlyUsedPower();
        CloseAllSubWindows();
        AppManager.Instance.UIManager.CloseAllWindows(AppManager.Instance.UIManager.ProfileSelector.gameObject);
        ProfileEditor.CurrentlyEditingProfile = null;
        ProfileInspector.CurrentProfile = null;
        AppManager.Instance.UIManager.PopupManager.LogPopup.Clear();
        AppManager.Instance.UIManager.ProfileSelector.Open();
    }




    public void ThrowAttributesCheck(List<ATTRIBUTES> zAttributes, int zComplexity)
    {
        int d20 = UnityEngine.Random.Range(1, 21);

        List<ATTRIBUTELEVELS> LevelsInvolved = new List<ATTRIBUTELEVELS>();
        if (zAttributes.Contains(ATTRIBUTES.VIGOR)) { LevelsInvolved.Add(CurrentProfile.Vigor); }
        if (zAttributes.Contains(ATTRIBUTES.DESTREZA)) { LevelsInvolved.Add(CurrentProfile.Dexterity); }
        if (zAttributes.Contains(ATTRIBUTES.INTELIGENCIA)) { LevelsInvolved.Add(CurrentProfile.Intelect); }
        if (zAttributes.Contains(ATTRIBUTES.PRESENCIA)) { LevelsInvolved.Add(CurrentProfile.Presence); }

        ATTRIBUTELEVELS Average = Defines.GetAttributesAverage(LevelsInvolved);

        switch (CurrentProfile.Health)
        {
            case HEALTHLEVELS.MAGULLADO:
                if (zAttributes.Contains(ATTRIBUTES.VIGOR) || zAttributes.Contains(ATTRIBUTES.DESTREZA))
                    zComplexity -= 1;
                break;

            case HEALTHLEVELS.HERIDO:
                if (zAttributes.Contains(ATTRIBUTES.VIGOR) || zAttributes.Contains(ATTRIBUTES.DESTREZA))
                    zComplexity -= 2;
                break;

            case HEALTHLEVELS.GRAVE:
            case HEALTHLEVELS.MORIBUNDO:
            case HEALTHLEVELS.MUERTO:
                zComplexity -= 2;
                break;
        }

        zComplexity = Mathf.Clamp(zComplexity, -10, 10);


        SUCCESSLEVELS successLevel = Defines.AttributesThrow(d20, zComplexity, Average);

        ProcessTrowResult(new LogMessage(DateTime.Now, AppManager.Instance.UIManager.PopupManager.LogPopup.TestsColor,
            "Tirada de atributos",
            string.Join("+", zAttributes.ConvertAll(a => Defines.AttributeNameToString(a)).ToArray()) + "\nResultado: " + "<b>" + Defines.SuccessLevelToString(successLevel).ToUpper() + "</b>",
            true
            ));
    }

    public void ThrowPowerCheck(Power zPower, int zComplexity)
    {
        int d20 = UnityEngine.Random.Range(1, 21);
        int expBonus = Mathf.FloorToInt(CurrentProfile.Experience / 50f);
        int final = d20 + expBonus + zComplexity;

        SUCCESSLEVELS successLevel = Defines.PowerThrow(final, zPower.Level);

        string message = "Resultado: " + "<b>" + Defines.SuccessLevelToString(successLevel).ToUpper() + "</b>";
        if ((int)successLevel <= (int)SUCCESSLEVELS.FALLO)
        {
            CurrentProfile.Catharsis = Mathf.Clamp(CurrentProfile.Catharsis + 1, 1, 10);
            message += "\n" + "Catarsis actual: " + CurrentProfile.Catharsis.ToString();
        }



        ProcessTrowResult(new LogMessage(DateTime.Now, AppManager.Instance.UIManager.PopupManager.LogPopup.TestsColor,
            "Lanza poder " + zPower.Name,
            message,
            true
            ));
    }

    public void ThrowDamageCheck(DAMAGENATURES zNature, DAMAGELOCATIONS zLocation, int zIntensity)
    {
        int d20 = UnityEngine.Random.Range(1, 21);
        int complexity = 0;
        switch (zLocation)
        {
            case DAMAGELOCATIONS.SUPERFICIAL: complexity += 2; break;
            case DAMAGELOCATIONS.PUNTOVITAL: complexity -= 3; break;
            case DAMAGELOCATIONS.INDETERMINADO:
            case DAMAGELOCATIONS.EXTREMIDAD:
                break;
        }

        if (zIntensity <= 1)
        {
            complexity += 0;
        }
        else if (zIntensity <= 2)
        {
            complexity -= 1;
        }
        else if (zIntensity <= 3)
        {
            complexity -= 3;
        }
        else if (zIntensity <= 4)
        {
            complexity -= 5;
        }

        SUCCESSLEVELS successLevel = Defines.AttributesThrow(d20, complexity, CurrentProfile.Vigor);

        Debug.Log("grado de exito en la tirada de vig: " + successLevel);

        string message = "";
        int damageLost = Defines.DamageLostThrow(successLevel, zIntensity);
        if (damageLost < 1)
        {
            message = "Evita el daño.";
        }
        else
        {

            CurrentProfile.Health = (HEALTHLEVELS)(Mathf.Clamp(((int)CurrentProfile.Health - damageLost), (int)HEALTHLEVELS.MUERTO, (int)HEALTHLEVELS.SANO));
            message = "Pierde " + damageLost + " niveles de salud.\nQueda en estado " + Defines.HealthLevelToString(CurrentProfile.Health);
        }



        ProcessTrowResult(new LogMessage(DateTime.Now, AppManager.Instance.UIManager.PopupManager.LogPopup.DamageColor,
            "Tirada de Daño",
            message,
            true
            ));
    }

    void ProcessTrowResult(LogMessage zProcessedMessage)
    {
        RefreshInspector();
        SaveCurrentProfile();

        LogPopup.AddNewMessage(zProcessedMessage);

        CloseAllSubWindows();

        AppManager.Instance.UIManager.PopupManager.LoadingPopup.Open();
    }


}
