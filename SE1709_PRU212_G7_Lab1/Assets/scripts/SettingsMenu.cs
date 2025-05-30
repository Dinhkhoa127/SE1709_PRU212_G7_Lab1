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
        // Đọc giá trị từ PlayerPrefs
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;

        // Đảm bảo AudioManager đã được khởi tạo
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetSound(isSoundOn);
            AudioManager.instance.SetMusic(isMusicOn);
        }

        // Thêm listeners cho các nút
        soundButton.onClick.AddListener(ToggleSound);
        musicButton.onClick.AddListener(ToggleMusic);
        vibrationButton.onClick.AddListener(ToggleVibration);
        notificationsButton.onClick.AddListener(ToggleNotifications);

        // Cập nhật trạng thái UI ban đầu
        UpdateButtonStates();
    }

    void UpdateButtonStates()
    {
        // Cập nhật màu sắc hoặc trạng thái của các nút dựa trên giá trị hiện tại
        soundButton.GetComponent<Image>().color = isSoundOn ? Color.green : Color.red;
        musicButton.GetComponent<Image>().color = isMusicOn ? Color.green : Color.red;
        vibrationButton.GetComponent<Image>().color = isVibrationOn ? Color.green : Color.red;
        notificationsButton.GetComponent<Image>().color = isNotificationsOn ? Color.green : Color.red;
    }

    void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetSound(isSoundOn);
        }
        UpdateButtonStates();
        Debug.Log("Sound: " + (isSoundOn ? "On" : "Off"));
    }

    void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetMusic(isMusicOn);
        }
        UpdateButtonStates();
        Debug.Log("Music: " + (isMusicOn ? "On" : "Off"));
    }

    void ToggleVibration()
    {
        isVibrationOn = !isVibrationOn;
        UpdateButtonStates();
        Debug.Log("Vibration: " + (isVibrationOn ? "On" : "Off"));
    }

    void ToggleNotifications()
    {
        isNotificationsOn = !isNotificationsOn;
        UpdateButtonStates();
        Debug.Log("Notifications: " + (isNotificationsOn ? "On" : "Off"));
    }
}