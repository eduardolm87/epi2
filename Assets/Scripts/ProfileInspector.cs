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

    public void Open()
    {
        AppManager.Instance.UIManager.CloseAllWindows(gameObject, AppManager.Instance.UIManager.StatusBar.gameObject);

        AppManager.Instance.UIManager.StatusBar.Open();

        gameObject.SetActive(true);

        if (CurrentProfile == null)
        {
            LoadDefaultProfile();

            iTween.ScaleFrom(gameObject, iTween.Hash("scale", new Vector3(1, 0, 0), "time", 1f, "easetype", iTween.EaseType.easeOutExpo));
        }

    }

    public void Close()
    {
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
        //todo

    }

    public void ButtonDelete()
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
        EXP.text = "EXPERIENCIA: " + CurrentProfile.Experience.ToString();
        Cat.text = "CATARSIS: " + CurrentProfile.Catharsis.ToString();
        Health.text = "SALUD: " + Defines.HealthLevelToString(CurrentProfile.Health);
        //todo: portrait
    }
}
