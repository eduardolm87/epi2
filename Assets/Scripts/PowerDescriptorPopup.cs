using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;


public class PowerDescriptorPopup : MonoBehaviour
{
    public Text Title;
    public Text DescriptionTitle;
    public Text DescriptionText;
    public Text AdditionalOptsTitle;
    public Text AdditionalOptsText;

    PowerExample PowerExample;

    public void Open(Power zPower)
    {
        AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(true);

        gameObject.SetActive(true);

        PowerExample = AppManager.Instance.ReferenceManager.PowerReferences.FirstOrDefault(p => p.Power.Name == zPower.Name);
        if (PowerExample == null)
        {
            Debug.LogError("Error getting Power description " + zPower.Name);
        }

        Refresh();
    }

    public void Close()
    {
        gameObject.SetActive(false);

        if (AppManager.Instance.UIManager.PopupManager.NumberOfActivePopups == 0)
        {
            AppManager.Instance.UIManager.PopupManager.gameObject.SetActive(false);
        }
    }

    void Refresh()
    {
        Title.text = PowerExample.Power.Name + " (" + Defines.levelName + " " + PowerExample.Power.Level.ToString() + ")";
        DescriptionTitle.text = Defines.description;
        DescriptionText.text = PowerExample.Description;
        AdditionalOptsTitle.text = Defines.additionalOptions;
        AdditionalOptsText.text = PowerExample.AdditionalOptionsToString();
    }

    public void OKButton()
    {
        Close();
    }
}
