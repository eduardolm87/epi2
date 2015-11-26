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
    [XmlElement("ProfileVersion")]
    [HideInInspector]
    public int Version = 1;

    [XmlElement("Name")]
    public string Name = "";

    [XmlElement("Health")]
    public HEALTHLEVELS Health = HEALTHLEVELS.SANO;

    [XmlElement("Catharsis")]
    public int Catharsis = 0;

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

    [XmlElement("Conduct")]
    public string Conduct = "";

    [XmlArray("Sequels")]
    [XmlArrayItem("Sequel")]
    public List<string> Sequels = new List<string>();





    public int Experience
    {
        get
        {
            int expfinal = 0;

            //Reglas de la experiencia aqui 

            //Attributes
            expfinal += Defines.GetExperienceCost(Vigor);
            expfinal += Defines.GetExperienceCost(Dexterity);
            expfinal += Defines.GetExperienceCost(Intelect);
            expfinal += Defines.GetExperienceCost(Presence);

            //Modifiers
            foreach (Modifier modifier in Modifiers)
            {
                expfinal += Defines.GetExperienceCostModifier(modifier.Level);
            }

            //Powers
            foreach (Power power in Powers)
            {
                expfinal += Defines.GetExperienceCostPower(power.Level);
            }

            return expfinal;
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

        if (Conduct != zProfile.Conduct)
            return false;

        if (Sequels.Count != zProfile.Sequels.Count)
            return false;

        for (int i = 0; i < Sequels.Count; i++)
        {
            if (Sequels[i] != zProfile.Sequels[i])
                return false;
        }

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
        Catharsis = zOrigin.Catharsis;

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

        Notes = zOrigin.Notes;

        Conduct = zOrigin.Conduct;

        Sequels.Clear();
        foreach (string sequel in zOrigin.Sequels)
        {
            Sequels.Add(sequel);
        }
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
