using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CatharsisBar : MonoBehaviour
{
    public Slider Slider;
    public Text Name;
    public Text TextValue;

    [HideInInspector]
    public int CatharsisLevel = 0;

    public void OnValueChange(float zValue)
    {
        Refresh();
    }

    public void SetValue(int zCatharsisLevel)
    {
        Slider.value = zCatharsisLevel;
        Refresh();
    }

    public void Refresh()
    {
        Name.text = Defines.catharsis;

        CatharsisLevel = Mathf.RoundToInt(Slider.value);

        TextValue.text = CatharsisLevel.ToString();

        ProfileEditor.CurrentlyEditingProfile.Catharsis = CatharsisLevel;

        AppManager.Instance.UIManager.ProfileEditor.Refresh();
    }
}
