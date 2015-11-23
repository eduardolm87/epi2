using UnityEngine;
using System.Collections;

public class PowerExample : MonoBehaviour
{
    public Power Power = new Power();

    [TextArea]
    public string Description = "";

    void OnValidate()
    {
        gameObject.name = Power.Name;
    }

    public string AdditionalOptionsToString()
    {
        string d;

        d = "<b>+1</b> Ejemplo 1\n";
        d += "<b>+1</b> Ejemplo 2\n";
        d += "<b>+1</b> Ejemplo 3\n";
        d += "<b>+1</b> Ejemplo 4\n";

        return d;
    }
}
