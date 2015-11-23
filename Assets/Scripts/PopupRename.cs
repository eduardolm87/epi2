using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class PopupRename : MonoBehaviour
{
    public InputField Input;

    Profile RenamingProfile = null;

    Action<string> ToDoWhenAccepted = null;


    public void Open(Profile zProfileToRename, Action<string> zToDoWhenAccepted)
    {
        if (!AppManager.Instance.UIManager.PopupManager.CanOpen)
            return;

        AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(true);

        gameObject.SetActive(true);

        RenamingProfile = zProfileToRename;
        ToDoWhenAccepted = zToDoWhenAccepted;

        Input.text = RenamingProfile.Name;

        //Set focus on the input field
        EventSystem.current.SetSelectedGameObject(Input.gameObject, null);
        Input.OnPointerClick(new PointerEventData(EventSystem.current));

    }

    public void Close()
    {
        gameObject.SetActive(false);

        AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(false);
    }

    public void OKButton()
    {
        if (RenamingProfile != null)
        {
            ToDoWhenAccepted(Input.text);
        }

        Close();
    }

    public void CancelButton()
    {
        Close();
    }
}
