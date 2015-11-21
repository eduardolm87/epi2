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

    public void Open()
    {
        if (!AppManager.Instance.UIManager.PopupManager.CanOpen)
            return;

        AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(true);

        gameObject.SetActive(true);

        LoadSuggestionsFromReferences();

        iTween.ScaleFrom(gameObject, iTween.Hash("scale", new Vector3(0, 0, 0), "time", 1.5f, "easetype", iTween.EaseType.easeOutElastic));
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
        //todo: Aplicar a la ficha

        Close();
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
    }
}
