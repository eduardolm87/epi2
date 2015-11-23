using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System;


[System.Serializable]
[XmlRoot("Profile")]
public class Profile
{
    [XmlElement("Name")]
    public string Name = "";

    [XmlElement("Health")]
    public HEALTHLEVELS Health = HEALTHLEVELS.SANO;

    [XmlElement("Vigor")]
    public ATTRIBUTELEVELS Vigor = ATTRIBUTELEVELS.POBRE;

    [XmlElement("Dexterity")]
    public ATTRIBUTELEVELS Dexterity = ATTRIBUTELEVELS.POBRE;

    [XmlElement("Intelect")]
    public ATTRIBUTELEVELS Intelect = ATTRIBUTELEVELS.POBRE;

    [XmlElement("Presence")]
    public ATTRIBUTELEVELS Presence = ATTRIBUTELEVELS.POBRE;

    [XmlArray("Modifiers")]
    [XmlArrayItem("Modifier")]
    public List<Modifier> Modifiers = new List<Modifier>();

    [XmlArray("Powers")]
    [XmlArrayItem("Power")]
    public List<Power> Powers = new List<Power>();

    [XmlElement("Notes")]
    public string Notes = "";

    [XmlElement("Catharsis")]
    public int Catharsis = 0;



    public int Experience
    {
        get
        {
            return 888; //todo
        }
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
            {
                return true;
            }

            return !this.IsIdenticalTo(originalProfile);
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

    public string FormatFileName
    {
        get
        {
            string result = Name;
            if (result.Length > 50)
                result = result.Remove(50);

            result = result.Replace("/", "");
            result = result.Replace("\\", "");
            result = result.Replace("<", "");
            result = result.Replace(">", "");
            result = result.Replace("ñ", "n");
            result = result.Replace(" ", "__");
            result = result.Replace("&", "");
            result = result.Replace("`", "");
            result = result.Replace("´", "");
            result = result.Replace("[", "");
            result = result.Replace("]", "");
            result = result.Replace(":", "");
            result = result.Replace(".", "");
            result = result.Replace(",", "");
            result = result.Replace(";", "");
            result = result.Replace("\"", "");
            result = result.Replace("$", "");
            result = result.Replace("ç", "c");
            result = result.Replace("{", "c");
            result = result.Replace("}", "c");


            return result + "." + Defines.profilesFileExtension;
        }
    }


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

        Powers.Clear();
        foreach (Power power in zOrigin.Powers)
        {
            Power newPower = new Power();
            newPower.Name = power.Name;
            Powers.Add(newPower);
        }

        Catharsis = zOrigin.Catharsis;

        Notes = zOrigin.Notes;
    }

    public static string Serialize(Profile zOrigin)
    {
        return zOrigin.ToXML();
    }

    public static Profile Deserialize(string filepath)
    {
        Profile profile = new Profile();
        XmlSerializer ser = new XmlSerializer((typeof(Profile)));
        TextReader reader = new StringReader(System.IO.File.ReadAllText(filepath));
        return (Profile)ser.Deserialize(reader);
    }

    public string ToXML()
    {
        XmlSerializer ser = new XmlSerializer(typeof(Profile));
        StringWriter writer = new StringWriter();
        ser.Serialize(writer, this);

        return writer.ToString();
    }
}
