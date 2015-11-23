using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ReferenceManager : MonoBehaviour
{
    [HideInInspector]
    public List<Modifier> Modifiers = new List<Modifier>();

    [HideInInspector]
    public List<Profile> DefaultProfiles = new List<Profile>();

    [HideInInspector]
    public List<Profile> UserProfiles = new List<Profile>();


    public List<ModifierExample> ModifierReferences = new List<ModifierExample>();

    public List<ProfileExample> ProfileReferences = new List<ProfileExample>();


    public void LoadReferences()
    {
        Modifiers.Clear();
        DefaultProfiles.Clear();
        UserProfiles.Clear();

        foreach (ModifierExample modf in ModifierReferences)
        {
            Modifiers.Add(modf.Modifier);
        }

        foreach (ProfileExample prof in ProfileReferences)
        {
            DefaultProfiles.Add(prof.Profile);
        }

        LoadUserProfiles();
    }

    public Profile FindProfile(Profile zProfile)
    {
        return FindProfile(zProfile.Name);
    }

    public Profile FindProfile(string zName)
    {
        Profile candidate = DefaultProfiles.FirstOrDefault(p => p.Name == zName);
        if (candidate != null)
        {
            return candidate;
        }

        candidate = UserProfiles.FirstOrDefault(p => p.Name == zName);
        if (candidate != null)
        {
            return candidate;
        }

        return null;
    }

    public void LoadUserProfiles()
    {
        List<string> files = IOManager.Instance.ListAllProfileFiles();
        foreach (string path in files)
        {
            Profile newProfile = IOManager.Instance.ReadProfile(path);
            if (newProfile != null && !UserProfiles.Any(pf => pf.Name == newProfile.Name))
            {
                UserProfiles.Add(newProfile);
            }
        }
    }


}
