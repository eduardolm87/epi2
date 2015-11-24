using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Slider;
    public Text Name;
    public Text TextValue;

    [HideInInspector]
    public HEALTHLEVELS HealthLevel = HEALTHLEVELS.SANO;

    public void OnValueChange(float zValue)
    {
        Refresh();
    }

    public void SetValue(HEALTHLEVELS zHealthLevel)
    {
        Slider.value = (int)zHealthLevel;
        Refresh();
    }

    public void Refresh()
    {
        Name.text = Defines.health;

        HealthLevel = (HEALTHLEVELS)Slider.value;

        TextValue.text = Defines.HealthLevelToString(HealthLevel);

        ProfileEditor.CurrentlyEditingProfile.Health = HealthLevel;

        AppManager.Instance.UIManager.ProfileEditor.Refresh();
    }
}
