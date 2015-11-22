using UnityEngine;
using System.Collections;

public class ProfileExample : MonoBehaviour
{
    public Profile Profile = new Profile();

    void OnValidate()
    {
        gameObject.name = Profile.Name;
    }
}
