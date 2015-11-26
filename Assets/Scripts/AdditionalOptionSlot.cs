using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdditionalOptionSlot : MonoBehaviour
{
    public Slider Slider;
    public Text Text;
    public Image Handle;

    [HideInInspector]
    public PowerExample.AdditionalOption AdditionalOption = null;

    bool checked_ = false;
    public bool Checked
    {
        get { return checked_; }
        set
        {
            if (value)
                Slider.value = Slider.maxValue;
            else
                Slider.value = Slider.minValue;
        }
    }

    public void ValueChanged()
    {
        checked_ = (Slider.value == Slider.maxValue);
        if (checked_)
        {
            Handle.color = AppManager.Instance.UIManager.ProfileInspector.PowersWindow.AdditionalOptionsCheckedColor;
        }
        else
        {
            Handle.color = Color.gray;
        }

        if (checked_)
            AppManager.Instance.UIManager.ProfileInspector.PowersWindow.RecalculateComplexity(AdditionalOption.Difficulty);
        else
            AppManager.Instance.UIManager.ProfileInspector.PowersWindow.RecalculateComplexity(-AdditionalOption.Difficulty);

    }

    public void Assign(PowerExample.AdditionalOption zAdditionalOption)
    {
        AdditionalOption = zAdditionalOption;
        Refresh();
    }

    void Refresh()
    {
        Text.text = "<b>" + "(" + Defines.FormatComplexityNumber(AdditionalOption.Difficulty) + ") " + AdditionalOption.Name + "</b>";
    }
}
