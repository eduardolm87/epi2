using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingPopup : MonoBehaviour
{
    public float WaitingTime = 1;

    public void Open()
    {
        AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(true);

        gameObject.SetActive(true);

        WaitingTime = WaitingTime * UnityEngine.Random.Range(0.75f, 2f);

        Invoke("Finish", WaitingTime);
    }

    public void Close()
    {
        gameObject.SetActive(false);

        if (AppManager.Instance.UIManager.PopupManager.NumberOfActivePopups == 0)
        {
            AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(false);
        }
    }


    void Finish()
    {
        Close();
        AppManager.Instance.UIManager.PopupManager.LogPopup.Open();
    }
}
