using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComplexityInput : MonoBehaviour
{
    private int complexity = 0;

    public int Complexity
    {
        get { return complexity; }
        set
        {
            complexity = Mathf.Clamp(value, MinComplexity, MaxComplexity);
            Refresh();
        }
    }

    public Text LevelLabel;

    [HideInInspector]
    public int MaxComplexity = 10;

    [HideInInspector]
    public int MinComplexity = -10;

    public void QuantityModify(int zQuantity)
    {
        AppManager.Instance.SoundManager.Play("OptionPick");

        complexity = Mathf.Clamp(complexity + zQuantity, MinComplexity, MaxComplexity);
        Refresh();
    }

    void Refresh()
    {
        LevelLabel.text = Defines.FormatComplexityNumber(complexity);
    }
}
