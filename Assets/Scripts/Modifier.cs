using UnityEngine;
using System.Collections;

public class Modifier : MonoBehaviour
{
    public string Name = "";
    [HideInInspector]
    public int Level = 1;
    [TextArea]
    public string Description = "";

    public string FormattedLevel
    {
        get
        {
            return Defines.FormatComplexityNumber(Level);
        }
    }

    public Modifier() { }

    public Modifier(Modifier zOrigin)
    {
        Name = zOrigin.Name;
        Level = zOrigin.Level;
        Description = zOrigin.Description;
    }
}
