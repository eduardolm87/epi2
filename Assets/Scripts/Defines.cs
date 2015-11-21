using UnityEngine;
using System.Collections;


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

    //todo: daños
}
