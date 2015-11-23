using UnityEngine;
using System.Collections;
using System.Linq;

[System.Serializable]
public class Power
{
    public string Name = "";

    public int Level
    {
        get
        {
            PowerExample exampleCointainer = AppManager.Instance.ReferenceManager.PowerReferences.FirstOrDefault(x => x.Power.Name == Name);
            if (exampleCointainer == null)
                return 0;

            return exampleCointainer.Level;
        }
    }
}
