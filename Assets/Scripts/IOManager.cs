using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class IOManager : MonoBehaviour
{
    #region Singleton
    public static IOManager Instance = null;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Error: IOManager singleton already exists.");
        }
        Instance = this;
    }
    #endregion


    public void SaveProfile(Profile zProfile)
    {
        CreateDirectoryIfDoesntExist();

        string zFilename = zProfile.FormatFileName;
        string fullfilepath = Defines.ProfilesPath + "/" + zFilename;

        string[] serializedProfile = new string[1];
        serializedProfile[0] = Profile.Serialize(zProfile);

        try
        {
            File.WriteAllLines(fullfilepath, serializedProfile);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            return;
        }

        Debug.Log("Salvado con exito el profile " + zProfile.Name + " en " + Defines.ProfilesPath);
    }

    public Profile ReadProfile(string fullfilepath)
    {
        CreateDirectoryIfDoesntExist();

        if (!File.Exists(fullfilepath))
        {
            Debug.LogError("File " + fullfilepath + " does not exist.");
            return null;
        }

        return Profile.Deserialize(fullfilepath);
    }

    public void CreateDirectoryIfDoesntExist()
    {
        if (!Directory.Exists(Defines.ProfilesPath))
        {
            Directory.CreateDirectory(Defines.ProfilesPath);
        }
    }

    public List<string> ListAllProfileFiles()
    {
        CreateDirectoryIfDoesntExist();

        string[] files = Directory.GetFiles(Defines.ProfilesPath, "*." + Defines.profilesFileExtension);

        return files.ToList();
    }

    public bool DeleteProfile(Profile zProfile)
    {
        //Cargarse el archivo
        string filepath = Defines.ProfilesPath + "/" + zProfile.FormatFileName;

        if (!File.Exists(filepath))
        {
            Debug.LogError("File " + filepath + " does not exist.");
            return false;
        }

        try
        {
            File.Delete(filepath);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return false;
        }

        AppManager.Instance.ReferenceManager.LoadUserProfiles();
        return true;
    }

}
