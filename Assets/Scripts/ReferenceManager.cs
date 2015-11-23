using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ReferenceManager : MonoBehaviour
{
    [HideInInspector]
    public List<Modifier> Modifiers = new List<Modifier>();

    [HideInInspector]
    public List<Power> Powers = new List<Power>();

    [HideInInspector]
    public List<Profile> DefaultProfiles = new List<Profile>();

    [HideInInspector]
    public List<Profile> UserProfiles = new List<Profile>();


    public List<ModifierExample> ModifierReferences = new List<ModifierExample>();

    public List<ProfileExample> ProfileReferences = new List<ProfileExample>();

    public List<PowerExample> PowerReferences = new List<PowerExample>();

    public void LoadReferences()
    {
        Modifiers.Clear();
        Powers.Clear();
        DefaultProfiles.Clear();

        foreach (ModifierExample modf in ModifierReferences)
        {
            Modifiers.Add(modf.Modifier);
        }

        foreach (PowerExample pow in PowerReferences)
        {
            Powers.Add(pow.Power);
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
        UserProfiles.Clear();

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
