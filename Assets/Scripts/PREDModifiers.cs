using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PREDModifiers : MonoBehaviour
{
    public Modifierentry Modifierentryprefab;

    public Transform ModifiersList;

    List<Modifierentry> InstantiatedEntries = new List<Modifierentry>();

    public void Open()
    {
        AppManager.Instance.UIManager.ProfileEditor.Tabs.LightTab(ProfileEditor.Sections.Modifiers);
        gameObject.SetActive(true);
        LoadModifiersFromProfile();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void AddNewModifier()
    {
        AppManager.Instance.UIManager.PopupManager.PopupModifiersEditor.Open();
    }

    public void LoadModifiersFromProfile()
    {
        RemoveDeprecatedEntries();

        foreach (Modifier modifier in ProfileEditor.CurrentlyEditingProfile.Modifiers)
        {
            Debug.Log("Repaso el modificador de mi perfil " + modifier.Name);
            Modifierentry entry = InstantiatedEntries.FirstOrDefault(m => m.Modifier.Name == modifier.Name);
            if (entry == null)
            {
                Debug.Log("No lo tengo asi que lo creo.");
                AddNewModifierToList(modifier);
            }
            else
            {
                Debug.Log("Ya lo tenía así que lo actualizo.");
                entry.Assign(modifier);
                //<-- despues de esto es nulo!
                Debug.Log("No deberia ser nulo. Lo es?" + (entry.Modifier == null).ToString());
            }
        }
    }

    void RemoveDeprecatedEntries()
    {
        List<Modifierentry> deprecatedEntries = InstantiatedEntries.Where(m => !ProfileEditor.CurrentlyEditingProfile.Modifiers.ConvertAll(x => x.Name).Contains(m.Modifier.Name)).ToList();
        while (deprecatedEntries.Count > 0)
        {
            Destroy(InstantiatedEntries[0].gameObject);
            InstantiatedEntries.Remove(deprecatedEntries[0]);
            deprecatedEntries.RemoveAt(0);
        }
    }

    void AddNewModifierToList(Modifier zModifier)
    {
        GameObject entryobj = Instantiate(Modifierentryprefab.gameObject) as GameObject;
        entryobj.transform.SetParent(ModifiersList);
        entryobj.transform.localScale = Modifierentryprefab.transform.localScale;

        Modifierentry entry = entryobj.GetComponent<Modifierentry>();
        entry.Assign(zModifier);

        InstantiatedEntries.Add(entry);
    }

}
