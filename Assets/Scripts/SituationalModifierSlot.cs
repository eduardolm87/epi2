using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class SituationalModifierSlot : MonoBehaviour
{

    public Slider Slider;
    public Text Text;
    public Image Handle;

    [HideInInspector]
    public SituationalModifier SituationalModifier = null;

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
        AppManager.Instance.SoundManager.Play("OptionPick");

        checked_ = (Slider.value == Slider.maxValue);
        if (checked_)
        {
            Handle.color = AppManager.Instance.UIManager.ProfileInspector.AttributesWindow.SituationalModifiersCheckedColor;
        }
        else
        {
            Handle.color = Color.gray;
        }

        if (checked_)
            AppManager.Instance.UIManager.ProfileInspector.AttributesWindow.RecalculateComplexity(SituationalModifier.Difficulty);
        else
            AppManager.Instance.UIManager.ProfileInspector.AttributesWindow.RecalculateComplexity(-SituationalModifier.Difficulty);
    }

    public void Assign(SituationalModifier zSituationalModifier)
    {
        SituationalModifier = zSituationalModifier;
        Refresh();
    }

    void Refresh()
    {
        Text.text = "<b>" + "(" + Defines.FormatComplexityNumber(SituationalModifier.Difficulty) + ") " + SituationalModifier.Name + "</b>";
    }

    public void InfoButton()
    {
        AppManager.Instance.UIManager.PopupManager.PopupSimple.Open(SituationalModifier.Name + " (" + Defines.FormatComplexityNumber(SituationalModifier.Difficulty) + ")", SituationalModifier.Description, new List<PopupButton>());
    }
}
