using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public SoundManager SoundManager;

    void Start()
    {
        ReferenceManager.LoadReferences();
        UIManager.OpenFromStart();
    }

}
