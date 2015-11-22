using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class ProfileSelector : MonoBehaviour
{
    public Profileslot ProfileSlotPrefab;
    public Transform SelectorList;

    [HideInInspector]
    public List<Profileslot> ProfileSlotsLoaded = new List<Profileslot>();


    public void Open()
    {
        AppManager.Instance.UIManager.CloseAllWindows(gameObject);

        gameObject.SetActive(true);

        LoadAllProfiles();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void LoadAllProfiles()
    {
        LoadDefaultProfiles();

        LoadUserProfiles();
    }

    void LoadDefaultProfiles()
    {
        foreach (Profile profile in AppManager.Instance.ReferenceManager.DefaultProfiles)
        {
            if (!ProfileAlreadyLoaded(profile))
            {
                AddProfileToSelector(profile);
            }
        }
    }

    void LoadUserProfiles()
    {
        //todo
    }

    void AddProfileToSelector(Profile zProfile)
    {
        GameObject entryObj = Instantiate(ProfileSlotPrefab.gameObject) as GameObject;
        entryObj.transform.SetParent(SelectorList);
        entryObj.transform.localScale = ProfileSlotPrefab.transform.localScale;

        Profileslot entry = entryObj.GetComponent<Profileslot>();

        entry.Assign(zProfile);
        ProfileSlotsLoaded.Add(entry);
    }

    bool ProfileAlreadyLoaded(Profile zProfile)
    {
        return ProfileSlotsLoaded.Any(p => p.Profile == zProfile);
    }
}
