using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttributeBar : MonoBehaviour
{
    public Slider Slider;
    public Text AttributeName;
    public Text AttributeTextValue;

    public ATTRIBUTES Attribute = ATTRIBUTES.VIGOR;

    public void OnValueChange(float zValue)
    {
        Refresh();
    }

    public void SetValue(ATTRIBUTELEVELS zLevel)
    {
        Slider.value = (int)zLevel;
        Refresh();
    }

    void Refresh()
    {
        AttributeName.text = Defines.AttributeNameToString(Attribute);

        ATTRIBUTELEVELS level = (ATTRIBUTELEVELS)Mathf.RoundToInt(Slider.value);

        AttributeTextValue.text = Defines.AttributeLevelToString(level);

        switch (Attribute)
        {
            case ATTRIBUTES.VIGOR:
                ProfileEditor.CurrentlyEditingProfile.Vigor = level;
                break;
            case ATTRIBUTES.DESTREZA:
                ProfileEditor.CurrentlyEditingProfile.Dexterity = level;
                break;
            case ATTRIBUTES.INTELECTO:
                ProfileEditor.CurrentlyEditingProfile.Intelect = level;
                break;
            case ATTRIBUTES.PRESENCIA:
                ProfileEditor.CurrentlyEditingProfile.Presence = level;
                break;
        }

        AppManager.Instance.UIManager.ProfileEditor.Refresh();
    }
}
