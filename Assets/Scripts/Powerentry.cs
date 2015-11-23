using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Powerentry : MonoBehaviour
{
    [HideInInspector]
    public Power Power = null;
    public Text Name;

    public void Assign(Power zPower)
    {
        this.Power = zPower;
        Refresh();
    }

    public void Refresh()
    {
        Name.text = Power.Name;
    }

    public void RemoveButton()
    {
        ProfileEditor.CurrentlyEditingProfile.Powers.Remove(Power);
        AppManager.Instance.UIManager.ProfileEditor.Powers.LoadPowersFromProfile();
    }

    public void InfoButton()
    {

    }
}
