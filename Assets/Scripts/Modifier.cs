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
            if (Level < 0)
            {
                return "<color=red>" + Level.ToString() + "</color>";
            }
            else if (Level == 0)
            {
                return Level.ToString();
            }
            else
            {
                return "<color=green>" + "+" + Level.ToString() + "</color>";
            }
        }
    }
}
