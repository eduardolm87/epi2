using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


[System.Serializable]
public class Profile
{
    public string Name = "";

    public HEALTHLEVELS Health = HEALTHLEVELS.SANO;

    public ATTRIBUTELEVELS Vigor = ATTRIBUTELEVELS.POBRE;
    public ATTRIBUTELEVELS Dexterity = ATTRIBUTELEVELS.POBRE;
    public ATTRIBUTELEVELS Intelect = ATTRIBUTELEVELS.POBRE;
    public ATTRIBUTELEVELS Presence = ATTRIBUTELEVELS.POBRE;

    public List<Modifier> Modifiers = new List<Modifier>();
    public List<Power> Powers = new List<Power>();

    public string Notes = "";

    public int Experience
    {
        get
        {
            return 888; //todo
        }
    }

    public int Catharsis = 0;



    public Profile()
    {

    }

    public Profile(Profile zOrigin)
    {
        CopyValuesFrom(zOrigin);
    }

    public void CopyValuesFrom(Profile zOrigin)
    {
        Name = zOrigin.Name;
        Health = zOrigin.Health;

        Vigor = zOrigin.Vigor;
        Dexterity = zOrigin.Dexterity;
        Intelect = zOrigin.Intelect;
        Presence = zOrigin.Presence;

        Modifiers.Clear();
        foreach (Modifier modifier in zOrigin.Modifiers)
        {
            Modifier newModifier = new Modifier();
            newModifier.Name = modifier.Name;
            newModifier.Level = modifier.Level;
            Modifiers.Add(newModifier);
        }
        //Debug.Log("Copiados mods: " + string.Join(",", Modifiers.ConvertAll(m => m.Name.ToString()).ToArray()));

        Powers.Clear();
        foreach (Power power in zOrigin.Powers)
        {
            Power newPower = new Power();
            newPower.Name = power.Name;
            newPower.Level = power.Level;
            newPower.Description = power.Description;
            //todo: probablemente más cosas que añadir aquí??
            Powers.Add(newPower);
        }

        Catharsis = zOrigin.Catharsis;

        Notes = zOrigin.Notes;
    }

    public bool isDefaultProfile
    {
        get
        {
            return AppManager.Instance.ReferenceManager.DefaultProfiles.Any(p => p.Name == Name);
        }
    }

    public bool hasModifications
    {
        get
        {
            Profile originalProfile = AppManager.Instance.ReferenceManager.FindProfile(Name);
            if (originalProfile == null)
                return true;

            return this.IsIdenticalTo(originalProfile);
        }
    }

    bool IsIdenticalTo(Profile zProfile)
    {
        if (Name != zProfile.Name)
            return false;

        if (Health != zProfile.Health)
            return false;

        if (Vigor != zProfile.Vigor)
            return false;

        if (Dexterity != zProfile.Dexterity)
            return false;

        if (Intelect != zProfile.Intelect)
            return false;

        if (Presence != zProfile.Presence)
            return false;

        if (Notes != zProfile.Notes)
            return false;

        if (Catharsis != zProfile.Catharsis)
            return false;

        if (Modifiers.Count != zProfile.Modifiers.Count)
            return false;

        for (int i = 0; i < Modifiers.Count; i++)
        {
            if (Modifiers[i].Name != zProfile.Modifiers[i].Name)
                return false;

            if (Modifiers[i].Level != zProfile.Modifiers[i].Level)
                return false;
        }

        if (Powers.Count != zProfile.Powers.Count)
            return false;

        for (int i = 0; i < Powers.Count; i++)
        {
            if (Powers[i].Name != zProfile.Powers[i].Name)
                return false;
        }

        return true;
    }
}
