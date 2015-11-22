using UnityEngine;
using System.Collections;

[System.Serializable]
public class Modifier
{
    public string Name = "";
    public int Level = 1;


    public string FormattedLevel
    {
        get
        {
            return Defines.FormatComplexityNumber(Level);
        }
    }

    public Modifier()
    {
        Name = "???";
        Level = 1;
    }

    public Modifier(Modifier zOrigin)
    {
        Name = zOrigin.Name;
        Level = zOrigin.Level;
    }
}
