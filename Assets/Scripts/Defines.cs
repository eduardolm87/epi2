using UnityEngine;
using System.Collections;

public enum ATTRIBUTES { VIGOR = 0, DESTREZA = 1, INTELECTO = 2, PRESENCIA = 3 };
public enum ATTRIBUTELEVELS { PESIMO = 0, POBRE = 1, MEDIOCRE = 2, COMPETENTE = 3, EXCELENTE = 4, PRODIGIOSO = 5 };
public enum SUCCESSLEVELS { CATASTROFE = 0, FRACASO = 1, FALLO = 2, ACIERTO = 3, EXTRAORDINARIO = 4, PERFECTO = 5 };
public enum HEALTHLEVELS { MUERTO = 0, MORIBUNDO = 1, GRAVE = 2, HERIDO = 3, MAGULLADO = 4, SANO = 5 };
public enum DAMAGELOCATIONS { INDETERMINADO = 0, EXTREMIDAD = 1, SUPERFICIAL = 2, PUNTOVITAL = 3 };
public enum DAMAGENATURES { ABRASION = 0, PERFORACION = 1, CORTE = 2, IMPACTO = 3 };



public class Defines : MonoBehaviour
{
    public static string HealthLevelToString(HEALTHLEVELS zHealthLevel)
    {
        switch (zHealthLevel)
        {
            case HEALTHLEVELS.SANO: return "Sano";
            case HEALTHLEVELS.MAGULLADO: return "Magullado";
            case HEALTHLEVELS.HERIDO: return "Herido";
            case HEALTHLEVELS.GRAVE: return "Grave";
            case HEALTHLEVELS.MORIBUNDO: return "Moribundo";
            case HEALTHLEVELS.MUERTO: return "Muerto";
        }
        return "???";
    }

    public static string AttributeLevelToString(ATTRIBUTELEVELS zAttributeLevel)
    {
        switch (zAttributeLevel)
        {
            case ATTRIBUTELEVELS.PRODIGIOSO: return "Prodigioso";
            case ATTRIBUTELEVELS.EXCELENTE: return "Excelente";
            case ATTRIBUTELEVELS.COMPETENTE: return "Competente";
            case ATTRIBUTELEVELS.MEDIOCRE: return "Mediocre";
            case ATTRIBUTELEVELS.POBRE: return "Pobre";
            case ATTRIBUTELEVELS.PESIMO: return "Pésimo";
        }
        return "???";
    }

    public static string SuccessLevelToString(SUCCESSLEVELS zSuccessLevel)
    {
        switch (zSuccessLevel)
        {
            case SUCCESSLEVELS.PERFECTO: return "Perfecto";
            case SUCCESSLEVELS.EXTRAORDINARIO: return "Extraordinario";
            case SUCCESSLEVELS.ACIERTO: return "Acierto";
            case SUCCESSLEVELS.FALLO: return "Fallo";
            case SUCCESSLEVELS.FRACASO: return "Fracaso";
            case SUCCESSLEVELS.CATASTROFE: return "Catástrofe";
        }
        return "???";
    }

    public static string AttributeNameToString(ATTRIBUTES zAttribute)
    {
        switch (zAttribute)
        {
            case ATTRIBUTES.VIGOR: return "VIGOR";
            case ATTRIBUTES.DESTREZA: return "DESTREZA";
            case ATTRIBUTES.INTELECTO: return "INTELECTO";
            case ATTRIBUTES.PRESENCIA: return "PRESENCIA";
        }
        return "???";
    }

    public static string DamageNatureToString(DAMAGENATURES zNature)
    {
        switch (zNature)
        {
            case DAMAGENATURES.ABRASION: return "ABRASION";
            case DAMAGENATURES.CORTE: return "CORTE";
            case DAMAGENATURES.IMPACTO: return "IMPACTO";
            case DAMAGENATURES.PERFORACION: return "PERFORACION";
        }
        return "???";
    }

    public static string DamageLocationToString(DAMAGELOCATIONS zLocation)
    {
        switch (zLocation)
        {
            case DAMAGELOCATIONS.INDETERMINADO: return "INDETERMINADO";
            case DAMAGELOCATIONS.EXTREMIDAD: return "EXTREMIDAD";
            case DAMAGELOCATIONS.PUNTOVITAL: return "PUNTO VITAL";
            case DAMAGELOCATIONS.SUPERFICIAL: return "SUPERFICIAL";
        }
        return "???";
    }

    public static string FormatComplexityNumber(int zLevel)
    {
        if (zLevel < 0)
        {
            return "<color=red>" + zLevel.ToString() + "</color>";
        }
        else if (zLevel == 0)
        {
            return zLevel.ToString();
        }
        else
        {
            return "<color=green>" + "+" + zLevel.ToString() + "</color>";
        }
    }

    public static bool AdviceForOverwritingExamplesDisplayed = false;


    public static int GetExperienceCost(ATTRIBUTELEVELS zLevel)
    {
        switch (zLevel)
        {
            case ATTRIBUTELEVELS.PESIMO: return 0;
            case ATTRIBUTELEVELS.POBRE: return 1;
            case ATTRIBUTELEVELS.MEDIOCRE: return 3;
            case ATTRIBUTELEVELS.COMPETENTE: return 8;
            case ATTRIBUTELEVELS.EXCELENTE: return 20;
            case ATTRIBUTELEVELS.PRODIGIOSO: return 50;
        }
        return 0;
    }

    public static int GetExperienceCostModifier(int zModifierLevel)
    {
        switch (zModifierLevel)
        {
            case 1: return 3;
            case 2: return 5;
            case 3: return 7;
            case 4: return 9;
            case 5: return 13;

            case -1: return -3;
            case -2: return -5;
            case -3: return -7;
            case -4: return -9;
        }
        return 0;
    }

    public static int GetExperienceCostPower(int zPowerLevel)
    {
        switch (zPowerLevel)
        {
            case 1: return 4;
            case 2: return 7;
            case 3: return 11;
            case 4: return 16;
        }
        return 0;
    }


    public static string defaultModifierName = "Nuevo";
    public static int defaultModifierLevel = 1;
    public static string defaultOKText = "OK";
    public static string errorPopupTitle = "Error";
    public static string defaultProfileCantDelete = "No se pueden borrar los perfiles de ejemplo.";
    public static string warningTitle = "Atención";
    public static string reallydeleteProfile = "¿De verdad quieres borrar el perfil";
    public static string yes = "SI";
    public static string no = "NO";
    public static string deleteConfirmation = "Borrado perfil ";
    public static string ProfilesPath = Application.persistentDataPath + "/Profiles";
    public static string profilesFileExtension = "ep3";
    public static string profileWontBeSavedBecauseDefault = "Este perfil es de ejemplo. No se guardarán los cambios que hayas hecho.";
    public static string newProfileName = "Nuevo Personaje";
    public static string levelName = "Nivel";
    public static string description = "Descripción";
    public static string additionalOptions = "Opciones adicionales";
    public static string health = "SALUD";
    public static string catharsis = "CATARSIS";
    public static string experience = "EXPERIENCIA";
    public static string expAbbreviation = "EXP";
    public static string cathAbbreviation = "CAT";
    public static string newProfileGenericLog = "Nuevo perfil creado.";
    public static string profileOpenLog = "Perfil seleccionado.";
    public static int maxModifierLevel = 5;
    public static int minModifierLevel = -4;
    public static string checksAttributes = "TIRADAS";
    public static string checksPowers = "PRODIGIOS";
    public static string checksDamages = "DAÑO";
    public static string noPowers = "Este perfil no tiene Prodigios que lanzar.";
    public static string dexteritySituationalModifier = "Se aplica cuando el personaje está intentando apuntar para impactar a un objetivo que tiene Destreza ";
    public static string dexteritySituationalModifierTitle = "Acertar a objetivo con DES ";
}
