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


    //void OnValidate()
    //{
    //    gameObject.name = Power.Name;
    //}

    public string AdditionalOptionsToString()
    {
        string d;

        d = "<b>+1</b> Ejemplo 1\n";
        d += "<b>+1</b> Ejemplo 2\n";
        d += "<b>+1</b> Ejemplo 3\n";
        d += "<b>+1</b> Ejemplo 4\n";

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
