using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class PopupModifiersEditor : MonoBehaviour
{
    public InputField ModifierInput;
    public Text ModifierLevel;
    public Transform SuggestionsList;

    int ModifierFinalLevel = 1;

    public SuggestButton ModifiersSuggestionPrefab;

    bool SuggestionsLoaded = false;

    public void Open(Modifier zModelModifier = null)
    {
        if (!AppManager.Instance.UIManager.PopupManager.CanOpen)
            return;

        AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(true);

        gameObject.SetActive(true);

        LoadSuggestionsFromReferences();

        if (zModelModifier != null)
            LoadModelModifier(zModelModifier);
        else
        {
            ModifierInput.text = Defines.defaultModifierName;
            ModifierFinalLevel = Defines.defaultModifierLevel;
            RefreshLevel();
        }

        //iTween.ScaleFrom(gameObject, iTween.Hash("scale", new Vector3(0, 0, 0), "time", 1.5f, "easetype", iTween.EaseType.easeOutElastic));
    }

    public void Close()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(false);
        }
    }

    public void LoadSuggestionsFromReferences()
    {
        if (SuggestionsLoaded)
            return;

        foreach (Modifier mod in AppManager.Instance.ReferenceManager.Modifiers)
        {
            CreateSuggestionButton(mod);
        }

        SuggestionsLoaded = true;
    }

    void LoadModelModifier(Modifier zModifier)
    {
        ModifierInput.text = zModifier.Name;
        ModifierFinalLevel = zModifier.Level;
        RefreshLevel();
    }

    void CreateSuggestionButton(Modifier zModifier)
    {
        GameObject ModifierButtonObj = Instantiate(ModifiersSuggestionPrefab.gameObject) as GameObject;
        ModifierButtonObj.transform.SetParent(SuggestionsList);
        ModifierButtonObj.transform.localScale = ModifiersSuggestionPrefab.transform.localScale;

        SuggestButton SuggestButtonComponent = ModifierButtonObj.GetComponent<SuggestButton>();
        SuggestButtonComponent.Assign(zModifier);
    }

    public void ApplyChanges()
    {
        Modifier existingModifier = ProfileEditor.CurrentlyEditingProfile.Modifiers.FirstOrDefault(m => m.Name == ModifierInput.text);
        if (existingModifier != null)
        {
            existingModifier.Level = ModifierFinalLevel;
        }
        else
        {
            Modifier newModifier = new Modifier();
            newModifier.Name = ModifierInput.text;
            newModifier.Level = ModifierFinalLevel;

            ProfileEditor.CurrentlyEditingProfile.Modifiers.Add(newModifier);
        }

        Close();

        AppManager.Instance.UIManager.ProfileEditor.Refresh();
        AppManager.Instance.UIManager.ProfileEditor.Modifiers.LoadModifiersFromProfile();
    }

    public void CancelChanges()
    {
        Close();
    }

    public void ChooseSuggestion(Modifier zModifier)
    {
        ModifierInput.text = zModifier.Name;
    }

    public void ChangeLevel(int zQuantity)
    {
        ModifierFinalLevel = Mathf.Clamp(ModifierFinalLevel + zQuantity, -10, 10);
        RefreshLevel();
    }

    public void RefreshLevel()
    {
        ModifierLevel.text = Defines.FormatComplexityNumber(ModifierFinalLevel);
    }
}
