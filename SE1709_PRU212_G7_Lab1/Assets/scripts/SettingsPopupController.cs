using UnityEngine;

public class SettingsPopupController : MonoBehaviour
{
    public GameObject settingsPopup; // Kéo panel popup vào đây

    public void ShowPopup()
    {
        settingsPopup.SetActive(true);
    }

    public void HidePopup()
    {
        settingsPopup.SetActive(false);
    }
}