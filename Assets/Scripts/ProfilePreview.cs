using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProfilePreview : MonoBehaviour
{
    public Text Name;
    public Text EXPCAT;

    [HideInInspector]
    public int oldEXP = 0;

    bool refreshing = false;

    public void Refresh()
    {
        Name.text = ProfileEditor.CurrentlyEditingProfile.Name;
        StartCoroutine(RefreshEXPCoroutine());
    }

    IEnumerator RefreshEXPCoroutine()
    {
        while (refreshing)
        {
            yield return new WaitForSeconds(0.1f);
        }

        refreshing = true;

        int actualEXP = ProfileEditor.CurrentlyEditingProfile.Experience;
        while (oldEXP != actualEXP && refreshing)
        {
            if (oldEXP < actualEXP)
                oldEXP += 1;
            else if (oldEXP > actualEXP)
                oldEXP -= 1;

            EXPCAT.text = "EXP: " + "<color=orange>" + oldEXP.ToString() + "</color>";
            yield return new WaitForSeconds(0.05f);
        }

        oldEXP = actualEXP;
        EXPCAT.text = "EXP: " + oldEXP.ToString();

        refreshing = false;
    }

    public void BackButton()
    {
        ProfileEditor.CurrentlyEditingProfile = null;
        AppManager.Instance.UIManager.ProfileEditor.Close();
        AppManager.Instance.UIManager.ProfileInspector.Open(ProfileInspector.CurrentProfile);
    }

    public void DoneButton()
    {
        ProfileInspector.CurrentProfile = ProfileEditor.CurrentlyEditingProfile;
        ProfileInspector.SaveCurrentProfile();
        AppManager.Instance.UIManager.ProfileEditor.Close();
        AppManager.Instance.UIManager.ProfileInspector.Open(ProfileInspector.CurrentProfile);
    }
}
