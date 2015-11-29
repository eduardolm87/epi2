using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI;

public class ProfileSelector : MonoBehaviour
{
    public enum FOLDERS { USER = 0, EXAMPLES = 1 };

    public Profileslot ProfileSlotPrefab;
    public Transform UserFolderList;
    public Transform ExamplesFolderList;


    public Color UserProfileColor = Color.gray;
    public Color DefaultProfileColor = Color.blue;

    public Color UnselectedFolder;
    public Color SelectedFolder;

    public Image UserFolderSelector;
    public Image ExamplesFolderSelector;

    public ScrollRect MaskListSwapper;

    public GameObject AddNewProfileButton;


    [HideInInspector]
    public FOLDERS CurrentlyOpenedFolder = FOLDERS.USER;

    [HideInInspector]
    public List<Profileslot> ProfileSlotsLoaded = new List<Profileslot>();

    static bool firstTimeInTheApp = true;


    public void Open()
    {
        AppManager.Instance.UIManager.CloseAllWindows(gameObject);

        gameObject.SetActive(true);

        LoadAllProfiles();

        if (firstTimeInTheApp)
        {
            if (ProfileSlotsLoaded.Any(p => !p.Profile.isDefaultProfile))
            {
                ChangeFolder(FOLDERS.USER);
            }
            else
            {
                ChangeFolder(FOLDERS.EXAMPLES);
            }

            firstTimeInTheApp = false;
        }
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
        if (zProfile.isDefaultProfile)
        {
            entryObj.transform.SetParent(ExamplesFolderList);
        }
        else
        {
            entryObj.transform.SetParent(UserFolderList);
        }

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

    public void SelectFolder(int zFolder)
    {
        FOLDERS wantToChange = (FOLDERS)zFolder;
        if (CurrentlyOpenedFolder == wantToChange)
            return;

        AppManager.Instance.SoundManager.Play("OptionPick");

        ChangeFolder(wantToChange);
    }

    void ChangeFolder(FOLDERS zFolderToChange)
    {
        switch (zFolderToChange)
        {
            case FOLDERS.USER:
                UserFolderSelector.color = SelectedFolder;
                ExamplesFolderSelector.color = UnselectedFolder;

                ExamplesFolderList.gameObject.SetActive(false);
                UserFolderList.gameObject.SetActive(true);
                MaskListSwapper.content = UserFolderList.GetComponent<RectTransform>();

                AddNewProfileButton.gameObject.SetActive(true);
                break;

            case FOLDERS.EXAMPLES:
                UserFolderSelector.color = UnselectedFolder;
                ExamplesFolderSelector.color = SelectedFolder;

                UserFolderList.gameObject.SetActive(false);
                ExamplesFolderList.gameObject.SetActive(true);
                MaskListSwapper.content = ExamplesFolderList.GetComponent<RectTransform>();
                AddNewProfileButton.gameObject.SetActive(false);
                break;
        }

        CurrentlyOpenedFolder = zFolderToChange;
    }
}
