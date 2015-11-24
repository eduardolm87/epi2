using UnityEngine;
using System.Collections;


public class PREDAttributes : MonoBehaviour
{
    public HealthBar HealthBar;
    public CatharsisBar CatharsisBar;

    public AttributeBar VigorBar;
    public AttributeBar DexterityBar;
    public AttributeBar IntelectBar;
    public AttributeBar PresenceBar;

    public void Open()
    {
        AppManager.Instance.UIManager.ProfileEditor.Tabs.LightTab(ProfileEditor.Sections.Attributes);
        gameObject.SetActive(true);

        Refresh();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    void Refresh()
    {
        //Status
        HealthBar.SetValue(ProfileEditor.CurrentlyEditingProfile.Health);
        CatharsisBar.SetValue(ProfileEditor.CurrentlyEditingProfile.Catharsis);

        //Attributes
        VigorBar.SetValue(ProfileEditor.CurrentlyEditingProfile.Vigor);
        DexterityBar.SetValue(ProfileEditor.CurrentlyEditingProfile.Dexterity);
        IntelectBar.SetValue(ProfileEditor.CurrentlyEditingProfile.Intelect);
        PresenceBar.SetValue(ProfileEditor.CurrentlyEditingProfile.Presence);
    }
}
