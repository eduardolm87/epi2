using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttributeSelector : MonoBehaviour
{
    public Sprite CheckedImage;
    public Sprite UncheckedImage;
    public Image CheckMarkImage;

    public Text AttributeNameLabel;
    public Text AttributeValueLabel;

    public Image ButtonBackground;

    public ATTRIBUTES Attribute = ATTRIBUTES.VIGOR;


    public bool Selected = false;

    public void Clicked()
    {
        Selected = !Selected;
        Refresh();
    }

    public void Refresh()
    {
        AttributeNameLabel.text = Defines.AttributeNameToString(Attribute);
        ATTRIBUTELEVELS value = ATTRIBUTELEVELS.MEDIOCRE;
        switch (Attribute)
        {
            case ATTRIBUTES.VIGOR:
                value = ProfileInspector.CurrentProfile.Vigor;
                break;
            case ATTRIBUTES.DESTREZA:
                value = ProfileInspector.CurrentProfile.Dexterity;
                break;
            case ATTRIBUTES.INTELIGENCIA:
                value = ProfileInspector.CurrentProfile.Intelect;
                break;
            case ATTRIBUTES.PRESENCIA:
                value = ProfileInspector.CurrentProfile.Presence;
                break;
        }

        AttributeValueLabel.text = Defines.AttributeLevelToString(value);

        if (Selected)
        {
            ButtonBackground.color = AppManager.Instance.UIManager.ProfileInspector.AttributesWindow.AttributesSelectedColor;
            CheckMarkImage.sprite = CheckedImage;
        }
        else
        {
            ButtonBackground.color = Color.white;
            CheckMarkImage.sprite = UncheckedImage;
        }
    }
}
