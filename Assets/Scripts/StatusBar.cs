using UnityEngine;
using System.Collections;

public class StatusBar : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);

    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
