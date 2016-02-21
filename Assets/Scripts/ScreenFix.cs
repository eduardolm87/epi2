using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenFix : MonoBehaviour
{
    [SerializeField]
    CanvasScaler CanvasScaler;

    bool isFixed = false;



    void Awake()
    {
        Fix();
    }


    void Fix()
    {
        if (isFixed) return;


        float height = Screen.height;
        float width = Screen.width;

        float aspectRatio = width / height;

        if (Mathf.Abs(aspectRatio - (3f / 4f)) < 0.001f)
        {
            Debug.Log("Screen fix applied");
            CanvasScaler.referenceResolution = new Vector2(1024, CanvasScaler.referenceResolution.y);
        }

        isFixed = true;
        Destroy(this);
    }
}
