using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PowerExample : MonoBehaviour
{
    public Power Power = new Power();

    public int Level = 1;

    [TextArea]
    public string Description = "";

    public List<AdditionalOption> AdditionalOptions = new List<AdditionalOption>();



    public string AdditionalOptionsToString()
    {
        string d = "";

        foreach (AdditionalOption addOption in AdditionalOptions)
        {
            d += "<b>" + "(" + Defines.FormatComplexityNumber(addOption.Difficulty) + ") " + addOption.Name + "</b>: " + addOption.Description + "\n";
        }

        return d;
    }

    [System.Serializable]
    public class AdditionalOption
    {
        public string Name = "";
        public int Difficulty = 1;
        [TextArea]
        public string Description = "";
    }
}
