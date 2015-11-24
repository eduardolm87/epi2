using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class LogPopup : MonoBehaviour
{
    /** Ejemplo de Popup en cualquier parte
     * Puedes añadir todos los mensajes que quieras, aparecerán cuando el LogPopup se abra, en orden de llegada
     
    LogPopup.AddNewMessage(new LogMessage(DateTime.Now, AppManager.Instance.UIManager.PopupManager.LogPopup.NoteColor, "Mi mensaje", "Ola ke ase. Esto lo hago por MIA!!\nPorque mola un puñao!!"));
    
     */


    public LogMessageEntry LogMessageEntryPrefab;
    public Transform List;

    public Color WarningColor = Color.red;
    public Color NoteColor = Color.blue;
    public Color TestsColor = Color.green;
    public Color DamageColor = Color.magenta;

    [HideInInspector]
    public List<LogMessageEntry> Entries = new List<LogMessageEntry>();

    List<LogMessage> Inbox = new List<LogMessage>();

    bool busy = false;

    public void Open()
    {
        if (!AppManager.Instance.UIManager.PopupManager.CanOpen)
            return;

        AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(true);

        gameObject.SetActive(true);

        StartCoroutine(LoadInbox());
    }

    public void Close()
    {
        if (busy)
            return;

        AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    public void OKButton()
    {
        Close();
    }

    public static void AddNewMessage(LogMessage zLogMessage)
    {
        AppManager.Instance.UIManager.PopupManager.LogPopup.Inbox.Add(zLogMessage);
    }

    IEnumerator LoadInbox()
    {
        busy = true;

        while (Inbox.Count > 0)
        {
            ShowNewMessage(Inbox[0]);

            if (Inbox[0].Animated)
            {
                GameObject obj = Entries.Last().gameObject;
                iTween.ScaleFrom(obj, iTween.Hash("scale", Vector3.zero, "time", 0.5f, "easetype", iTween.EaseType.spring));
                yield return new WaitForSeconds(0.55f);
            }

            Inbox.RemoveAt(0);
        }

        if (Entries.Count > 4)
        {
            SnapTo(Entries.Last().GetComponent<RectTransform>());
        }

        busy = false;
    }

    void ShowNewMessage(LogMessage zMessage)
    {
        GameObject entryObj = Instantiate(LogMessageEntryPrefab.gameObject) as GameObject;
        entryObj.transform.SetParent(List);
        entryObj.transform.localScale = LogMessageEntryPrefab.transform.localScale;

        LogMessageEntry entry = entryObj.GetComponent<LogMessageEntry>();
        entry.Assign(zMessage);

        Entries.Add(entry);
    }

    void RemoveAllEntries()
    {
        while (Entries.Count > 0)
        {
            Destroy(Entries[0].gameObject);
            Entries.RemoveAt(0);
        }
    }

    public void Clear()
    {
        Inbox.Clear();
        RemoveAllEntries();
    }


    // Snippet para centrar listas
    [SerializeField]
    ScrollRect scrollRect;

    [SerializeField]
    RectTransform contentPanel;

    public void SnapTo(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        contentPanel.anchoredPosition =
            (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
            - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
    }
    // ***
}
