using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Button soundButton;
    public Button musicButton;
    public Button vibrationButton;
    public Button notificationsButton;

    private bool isSoundOn = true;
    private bool isMusicOn = true;
    private bool isVibrationOn = true;
    private bool isNotificationsOn = true;

    void Start()
    {
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;

        AudioManager.instance.SetSound(isSoundOn);
        AudioManager.instance.SetMusic(isMusicOn);

        soundButton.onClick.AddListener(ToggleSound);
        musicButton.onClick.AddListener(ToggleMusic);
        vibrationButton.onClick.AddListener(ToggleVibration);
        notificationsButton.onClick.AddListener(ToggleNotifications);
    }

    void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0); // Thêm dòng này
        AudioManager.instance.SetSound(isSoundOn);
        Debug.Log("Sound: " + (isSoundOn ? "On" : "Off"));
    }

    void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0); // Thêm dòng này
        AudioManager.instance.SetMusic(isMusicOn);
        Debug.Log("Music: " + (isMusicOn ? "On" : "Off"));
    }

    void ToggleVibration()
    {
        isVibrationOn = !isVibrationOn;
        // Thêm code để bật/tắt rung
        Debug.Log("Vibration: " + (isVibrationOn ? "On" : "Off"));
    }

    void ToggleNotifications()
    {
        isNotificationsOn = !isNotificationsOn;
        // Thêm code để bật/tắt thông báo
        Debug.Log("Notifications: " + (isNotificationsOn ? "On" : "Off"));
    }

}