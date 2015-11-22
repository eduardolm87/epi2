using UnityEngine;
using System.Collections;

public class ModifierExample : MonoBehaviour
{
    public Modifier Modifier = new Modifier();
    [TextArea]
    public string Description = "";

    void OnValidate()
    {
        gameObject.name = Modifier.Name;
    }
}
