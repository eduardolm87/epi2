using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class ProfileSelector : MonoBehaviour
{
    public Profileslot ProfileSlotPrefab;
    public Transform SelectorList;

    public Color UserProfileColor = Color.gray;
    public Color DefaultProfileColor = Color.blue;

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
        AppManager.Instance.ReferenceManager.LoadUserProfiles();

        //Remove deprecated
        List<Profileslot> deprecated = ProfileSlotsLoaded.Where(p =>
            !AppManager.Instance.ReferenceManager.DefaultProfiles.Any(x => x.Name == p.Profile.Name)
            && !AppManager.Instance.ReferenceManager.UserProfiles.Any(x => x.Name == p.Profile.Name)
             ).ToList();

        while (deprecated.Count > 0)
        {
            Destroy(deprecated[0].gameObject);
            ProfileSlotsLoaded.Remove(deprecated[0]);
            deprecated.RemoveAt(0);
        }


        //Add new profiles and refresh
        foreach (Profile profile in AppManager.Instance.ReferenceManager.UserProfiles)
        {
            if (!ProfileAlreadyLoaded(profile))
            {
                AddProfileToSelector(profile);
            }
            else
            {
                Profileslot slot = ProfileSlotsLoaded.FirstOrDefault(p => p.Profile.Name == profile.Name);
                if (slot != null)
                {
                    slot.Assign(profile);
                }
            }
        }
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
        return ProfileSlotsLoaded.Any(p => p.Profile.Name == zProfile.Name);
    }

    public void CreateNewProfile()
    {
        Profile newProfile = new Profile();
        newProfile.Name = Defines.newProfileName + " " + AppManager.Instance.ReferenceManager.UserProfiles.Count;

        AppManager.Instance.UIManager.PopupManager.PopupRenameProfile.Open(newProfile, new Action<string>(delegate(string zInput)
        {
            newProfile.Name = zInput;

            AppManager.Instance.UIManager.CloseAllWindows();
            IOManager.Instance.SaveProfile(newProfile);
            AppManager.Instance.ReferenceManager.LoadUserProfiles();

            AppManager.Instance.UIManager.ProfileInspector.LoadProfile(newProfile);
            AppManager.Instance.UIManager.ProfileEditor.Open();

            LogPopup.AddNewMessage(new LogMessage(DateTime.Now, AppManager.Instance.UIManager.PopupManager.LogPopup.NoteColor, zInput, Defines.newProfileGenericLog));

        }));
    }
}
