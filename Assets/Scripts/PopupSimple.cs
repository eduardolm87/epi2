using UnityEngine;
using System.Collections;

public class PopupSimple : MonoBehaviour
{
    public void Open()
    {
        if (!AppManager.Instance.UIManager.PopupManager.CanOpen)
            return;

        AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(true);


        gameObject.SetActive(true);
    }

    public void Close()
    {
        AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }
}
