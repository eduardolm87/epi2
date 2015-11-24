using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class LogMessage : MonoBehaviour
{
    public string Title = "";
    public string Text = "";
    public Color Color = Color.gray;
    public DateTime Date = new DateTime();
    public bool Animated = false;

    public LogMessage() { }
    public LogMessage(DateTime zDate, Color zColor, string zTitle, string zText, bool zAnimated = false)
    {
        Title = zTitle;
        Text = zText;
        Color = zColor;
        Date = zDate;
        Animated = zAnimated;
    }
}
