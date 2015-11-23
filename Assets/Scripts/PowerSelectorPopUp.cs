using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class PowerSelectorPopUp : MonoBehaviour
{
    public PowerPickSlot PowerPickSlotPrefab;
    public Transform PowersList;

    List<PowerPickSlot> Entries = new List<PowerPickSlot>();

    public Color SelectionsColor = Color.red;

    [HideInInspector]
    PowerPickSlot chosenEntry = null;

    [SerializeField]
    GameObject MoreInfoTutorialObject;


    bool TutorialHasBeenShown = false;
    bool TipBeingShown = false;

    public void Open(List<PowerExample> zListOfPowersToShow)
    {
        if (!AppManager.Instance.UIManager.PopupManager.CanOpen)
            return;

        AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(true);

        gameObject.SetActive(true);

        LoadEntries(zListOfPowersToShow);

        chosenEntry = null;

        if (!TutorialHasBeenShown)
        {
            StartCoroutine(TutorialTip());
        }
    }

    public void Close()
    {
        AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(false);

        gameObject.SetActive(false);

        chosenEntry = null;

        MoreInfoTutorialObject.transform.SetParent(transform);

        if (!TutorialHasBeenShown && TipBeingShown)
        {
            HideTip();
        }

        //todo: cerrar también el popup de Info si estaba abierto :/
    }

    void RemoveAllEntries()
    {
        MoreInfoTutorialObject.transform.SetParent(transform);

        while (Entries.Count > 0)
        {
            Destroy(Entries[0].gameObject);
            Entries.RemoveAt(0);
        }
    }

    void LoadEntries(List<PowerExample> zListOfPowersToShow)
    {
        RemoveAllEntries();

        foreach (PowerExample power in zListOfPowersToShow)
        {
            AddPowerToList(power);
        }
    }

    void AddPowerToList(PowerExample zPower)
    {
        GameObject entryObj = Instantiate(PowerPickSlotPrefab.gameObject) as GameObject;
        entryObj.transform.SetParent(PowersList);
        entryObj.transform.localScale = PowerPickSlotPrefab.transform.localScale;

        PowerPickSlot entry = entryObj.GetComponent<PowerPickSlot>();
        entry.Assign(zPower);

        Entries.Add(entry);
    }

    public void Select(PowerPickSlot zSlot)
    {
        foreach (PowerPickSlot entry in Entries)
        {
            if (entry == zSlot)
            {
                chosenEntry = entry;
                entry.Select();
            }
            else
            {
                entry.Select(false);
            }
        }

        CenterOnEntry(chosenEntry);
    }

    public void Select(PowerExample zPower)
    {
        foreach (PowerPickSlot entry in Entries)
        {
            if (entry.PowerExample.Power.Name == zPower.Power.Name)
            {
                chosenEntry = entry;
                entry.Select();
            }
            else
            {
                entry.Select(false);
            }
        }

        if (chosenEntry == null)
        {
            Debug.LogError("Error selecting power");
            return;
        }

        CenterOnEntry(chosenEntry);
    }

    void CenterOnEntry(PowerPickSlot zSlot)
    {
        SnapTo(chosenEntry.GetComponent<RectTransform>());
    }

    IEnumerator TutorialTip()
    {
        yield return new WaitForSeconds(0.75f);

        MoreInfoTutorialObject.gameObject.SetActive(true);
        MoreInfoTutorialObject.transform.position = Entries.First().transform.GetChild(1).position;
        MoreInfoTutorialObject.transform.SetParent(Entries.First().transform.GetChild(1));

        iTween.ScaleFrom(MoreInfoTutorialObject, iTween.Hash("scale", Vector3.one * 0.25f, "time", 0.75f, "easetype", iTween.EaseType.easeOutBounce));
        yield return new WaitForSeconds(0.75f);

        TipBeingShown = true;

        yield return new WaitForSeconds(3);

        HideTip();
    }

    void HideTip()
    {
        if (!TutorialHasBeenShown && TipBeingShown)
        {
            StopCoroutine(TutorialTip());
            MoreInfoTutorialObject.transform.SetParent(transform);
            MoreInfoTutorialObject.gameObject.SetActive(false);
            TutorialHasBeenShown = true;
            TipBeingShown = false;
        }
    }

    public void ScrollingThroughList(Vector2 zScroll)
    {
        if (TipBeingShown)
        {
            HideTip();
        }
    }

    public void OKButton()
    {
        //todo
    }

    public void CancelButton()
    {
        TipBeingShown = true;
        TutorialHasBeenShown = false;
        HideTip();

        Close();
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
