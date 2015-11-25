using UnityEngine;
using System.Collections;

[System.Serializable]
public class SituationalModifier
{
    public string Name = "";
    public int Difficulty = -1;
    [TextArea]
    public string Description = "";
}
