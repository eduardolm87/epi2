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
        RemoveSpecialSituationalModifiers();
        AddSpecialSituationalModifiers();
    }

    void AddSpecialSituationalModifiers()
    {
        ATTRIBUTELEVELS DexLevel = ProfileInspector.CurrentProfile.Dexterity;
        int difference = (int)ATTRIBUTELEVELS.PRODIGIOSO - (int)DexLevel;

        for (int i = 1; i <= difference; i++)
        {
            ATTRIBUTELEVELS NextDexLevel = (ATTRIBUTELEVELS)((int)DexLevel + i);
            AddSpecialSituationalModifier(NextDexLevel, -i);
        }
    }

    void AddSpecialSituationalModifier(ATTRIBUTELEVELS zLevel, int zDifficulty)
    {
        GameObject entryObj = Instantiate(SituationalModifierSlotPrefab.gameObject) as GameObject;
        entryObj.transform.SetParent(SituationalModifiersList);
        entryObj.transform.localScale = SituationalModifierSlotPrefab.transform.localScale;

        SituationalModifierSlot entry = entryObj.GetComponent<SituationalModifierSlot>();

        SituationalModifier newSituationalModifier = new SituationalModifier();
        newSituationalModifier.Name = Defines.dexteritySituationalModifierTitle + Defines.AttributeLevelToString(zLevel);
        newSituationalModifier.Difficulty = zDifficulty;
        newSituationalModifier.Description = Defines.dexteritySituationalModifier + Defines.AttributeLevelToString(zLevel);

        entry.Assign(newSituationalModifier);

        SpecialSituationalModifierEntries.Add(entry);
    }

    void RemoveSpecialSituationalModifiers()
    {
        while (SpecialSituationalModifierEntries.Count > 0)
        {
            Destroy(SpecialSituationalModifierEntries[0].gameObject);
            SpecialSituationalModifierEntries.RemoveAt(0);
        }
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

    public void RecalculateComplexity(int zChange = 0)
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



        ComplexityInput.Complexity = ComplexityInput.Complexity + zChange;

    }

    List<ATTRIBUTES> GetInvolvedAttributes()
    {
        List<ATTRIBUTES> involved = new List<ATTRIBUTES>();

        foreach (AttributeSelector selector in AttributeSelectors)
        {
            if (selector.Selected)
            {
                involved.Add(selector.Attribute);
            }
        }

        return involved;
    }

    public void Throw()
    {
        List<ATTRIBUTES> involvedAttributes = GetInvolvedAttributes();

        if (involvedAttributes.Count > 0)
            AppManager.Instance.UIManager.ProfileInspector.ThrowAttributesCheck(involvedAttributes, ComplexityInput.Complexity);
    }
}
