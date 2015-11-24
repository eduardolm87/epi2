using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogMessageEntry : MonoBehaviour
{
    public Text Title;
    public Text Message;
    public Text Time;
    public Image Colorbar;


    [HideInInspector]
    public LogMessage LogMessage = null;

    public void Assign(LogMessage zLogMessage)
    {
        LogMessage = zLogMessage;
        Refresh();
    }

    void Refresh()
    {
        Title.text = LogMessage.Title;
        Message.text = LogMessage.Text;
        Time.text = LogMessage.Date.ToString();
        Colorbar.color = LogMessage.Color;
    }
}
