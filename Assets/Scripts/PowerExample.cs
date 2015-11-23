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
}
