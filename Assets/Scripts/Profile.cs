using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Profile : MonoBehaviour
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
}
