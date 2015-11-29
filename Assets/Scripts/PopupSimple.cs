using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/*
 * 
 * EJEMPLO DE USO DE UN POPUP-SIMPLE CON VARIOS BOTONES. COPYPASTEA Y RELLENA LAS STRINGS/MÉTODOS!
PopupManager.PopupSimple.Open("Título", "Texto del cuerpo de la ventana", new List<PopupButton>() { 
            new PopupButton("botón 1", 
                delegate 
                {
                    //
                    Debug.Log("Función 1");
                    //
                }), 
            new PopupButton("botón 2", 
                delegate 
                {
                    //
                    Debug.Log("Función 2");
                    //
                }) });

*/

public class PopupButton
{
    public string Text = "";
    public Action Action = null;
    public PopupButton() { }
    public PopupButton(string zText, Action zAction)
    {
        Text = zText;
        Action = zAction;
    }
}

public class PopupSimple : MonoBehaviour
{
    public Text Title;
    public Text Body;
    public List<Text> Buttons = new List<Text>();


    List<PopupButton> AssociatedActions = new List<PopupButton>();


    public void Open(string zTitle, string zBody, List<PopupButton> zButtons)
    {
        if (!AppManager.Instance.UIManager.PopupManager.CanOpen)
            return;

        AppManager.Instance.SoundManager.Play("Pick");

        AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(true);

        gameObject.SetActive(true);

        Title.text = zTitle;

        Body.text = zBody;

        if (zButtons.Count > 0)
        {
            AssociatedActions = zButtons;
        }
        else
        {
            PopupButton defaultButton = new PopupButton(Defines.defaultOKText, null);
            AssociatedActions.Add(defaultButton);
        }

        for (int i = 0; i < AssociatedActions.Count; i++)
        {
            if (i >= Buttons.Count)
                break;

            Buttons[i].text = AssociatedActions[i].Text;
            Buttons[i].transform.parent.gameObject.SetActive(true);
        }

        for (int j = AssociatedActions.Count; j < Buttons.Count; j++)
        {
            Buttons[j].transform.parent.gameObject.SetActive(false);
        }
    }

    public void Close()
    {
        AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(false);

        AssociatedActions.Clear();

        gameObject.SetActive(false);
    }


    public void DoActionButton(int zOrder)
    {
        if (zOrder >= AssociatedActions.Count)
            return;

        AppManager.Instance.SoundManager.Play("Pick");

        if (AssociatedActions[zOrder].Action != null)
        {
            AssociatedActions[zOrder].Action();
        }

        Close();
    }
}
