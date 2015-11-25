using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ChecksAttributes : MonoBehaviour
{
    public List<AttributeSelector> AttributeSelectors = new List<AttributeSelector>();

    public ComplexityInput ComplexityInput;

    public SituationalModifierSlot SituationalModifierSlotPrefab;

    public Transform SituationalModifiersList;

    public Color SituationalModifiersCheckedColor;
    public Color AttributesSelectedColor;

    List<SituationalModifierSlot> SituationalModifierEntries = new List<SituationalModifierSlot>();
    List<SituationalModifierSlot> SpecialSituationalModifierEntries = new List<SituationalModifierSlot>();

    public void Open()
    {
        AppManager.Instance.UIManager.ProfileInspector.CloseAllSubWindows(gameObject);
        gameObject.SetActive(true);
        AppManager.Instance.UIManager.StatusBar.Refresh();

        AttributeSelectors.ForEach(x => { x.Selected = false; x.Refresh(); });

        LoadSituationalModifiers();

        ComplexityInput.Complexity = 0;
        ComplexityInput.MinComplexity = -10;
        ComplexityInput.MaxComplexity = 10;

    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void LoadSituationalModifiers()
    {
        SituationalModifierEntries.ForEach(x => x.Checked = false);
        SpecialSituationalModifierEntries.ForEach(x => x.Checked = false);

        //Common situational modifiers
        foreach (SituationalModifier smod in AppManager.Instance.ReferenceManager.SituationalModifiers)
        {
            if (!SituationalModifierEntries.Any(x => x.SituationalModifier.Name == smod.Name))
            {
                AddSituationalModifier(smod);
            }
        }

        //Special situational modifiers
        //todo
    }

    void AddSituationalModifier(SituationalModifier zSituationalModifier)
    {
        GameObject entryObj = Instantiate(SituationalModifierSlotPrefab.gameObject) as GameObject;
        entryObj.transform.SetParent(SituationalModifiersList);
        entryObj.transform.localScale = SituationalModifierSlotPrefab.transform.localScale;

        SituationalModifierSlot entry = entryObj.GetComponent<SituationalModifierSlot>();
        entry.Assign(zSituationalModifier);

        SituationalModifierEntries.Add(entry);
    }

    public void RecalculateComplexity()
    {
        int accumulatedComplexity = 0;

        foreach (SituationalModifierSlot slot in SituationalModifierEntries)
        {
            if (slot.Checked)
            {
                accumulatedComplexity += slot.SituationalModifier.Difficulty;
            }
        }

        foreach (SituationalModifierSlot slot in SpecialSituationalModifierEntries)
        {
            if (slot.Checked)
            {
                accumulatedComplexity += slot.SituationalModifier.Difficulty;
            }
        }

        if (accumulatedComplexity > 0)
        {
            ComplexityInput.MinComplexity = -10 + accumulatedComplexity;
        }
        else if (accumulatedComplexity < 0)
        {
            ComplexityInput.MaxComplexity = 10 + accumulatedComplexity;
        }
        else
        {
            ComplexityInput.MinComplexity = -10;
            ComplexityInput.MaxComplexity = 10;
        }

        ComplexityInput.Complexity = ComplexityInput.Complexity;
    }
}
