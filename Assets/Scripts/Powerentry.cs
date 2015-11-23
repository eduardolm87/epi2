using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    }

    public void InfoButton()
    {

    }
}
