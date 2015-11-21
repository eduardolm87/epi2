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
}
