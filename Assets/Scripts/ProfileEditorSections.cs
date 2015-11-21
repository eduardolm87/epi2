using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProfileEditorSections : MonoBehaviour
{
    [SerializeField]
    UnityEngine.Color Graycolor = Color.gray;


    public Image SectionAttributes;
    public Image SectionModifiers;
    public Image SectionPowers;
    public Image SectionNotes;


    public void LightTab(ProfileEditor.Sections zSection)
    {
        if (zSection != ProfileEditor.Sections.Attributes)
        {
            SectionAttributes.color = Graycolor;
        }
        else
        {
            SectionAttributes.color = Color.white;
        }

        if (zSection != ProfileEditor.Sections.Modifiers)
        {
            SectionModifiers.color = Graycolor;
        }
        else
        {
            SectionModifiers.color = Color.white;
        }

        if (zSection != ProfileEditor.Sections.Powers)
        {
            SectionPowers.color = Graycolor;
        }
        else
        {
            SectionPowers.color = Color.white;
        }

        if (zSection != ProfileEditor.Sections.Notes)
        {
            SectionNotes.color = Graycolor;
        }
        else
        {
            SectionNotes.color = Color.white;
        }
    }

}
