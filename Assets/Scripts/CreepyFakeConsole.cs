using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreepyFakeConsole : MonoBehaviour
{
    public Text Label;
    public float LinesSpeed = 0.1f;

    public int maxCapacity = 90;

    int linesFilled = 0;

    string[] Lines = new string[]{
        "The future is yours.",
        "Become who you are.",
        "It is an order to be happy.",
        "No place for chaos.",
        "H3LpM3-4L1C3",
        "Whatever you want, work for it.",
        "Global Environment Gorvernment is peace.",
        "Reflect on your actions, citizen.",
        "Report any suspicious activity to your local authorities.",
        "\n"
    };

    void Start()
    {
        Label.text = "";
        linesFilled = 0;
        InvokeRepeating("AddLine", 0, LinesSpeed);
    }

    void AddLine()
    {
        if (linesFilled < maxCapacity)
        {
            int line = UnityEngine.Random.Range(0, Lines.Length);
            Label.text += Lines[line];
            linesFilled++;
        }
        else
        {
            linesFilled = 0;
            Label.text = "";
        }
    }
}
