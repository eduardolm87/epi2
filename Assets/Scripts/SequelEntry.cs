using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SequelEntry : MonoBehaviour
{
    public Text Label;
    public Slider Slider;
    public Image Handle;

    [HideInInspector]
    public Sequel Sequel = null;


    public bool Checked
    {
        get
        {
            return (Slider.value == Slider.maxValue);
        }
    }

    public void Assign(Sequel zSequel)
    {
        Sequel = zSequel;

        Refresh();
    }

    public void Refresh()
    {
        Label.text = Sequel.Name;
    }

    public void ChangeValue()
    {
        AppManager.Instance.SoundManager.Play("OptionPick");

        AppManager.Instance.UIManager.ProfileEditor.Notes.SelectSequel(this);

        if(Checked)
        {
            Handle.color = AppManager.Instance.UIManager.ProfileEditor.Notes.SelectedSequelHandleColor;
        }
        else
        {
            Handle.color = Color.gray;
        }
    }
}
