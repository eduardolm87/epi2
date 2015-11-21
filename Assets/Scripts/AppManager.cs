using UnityEngine;
using System.Collections;

public class AppManager : MonoBehaviour
{
    #region Singleton
    public static AppManager Instance = null;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Error: AppManager already created.");
            return;
        }

        Instance = this;
    }
    #endregion

    public UIManager UIManager;
    public ReferenceManager ReferenceManager;


    void Start()
    {
        UIManager.OpenFromStart();
    }

}
