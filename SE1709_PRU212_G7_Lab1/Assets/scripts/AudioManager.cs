using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource sfxSource;      // Dùng cho hiệu ứng
    public AudioSource musicSource;    // Dùng cho nhạc nền

    public AudioClip explosionClip;
    public AudioClip collectStarClip;
    public AudioClip Item;
    public AudioClip ShieldUp;
    public AudioClip ShieldDown;
    public AudioClip HealHp;
    public AudioClip explosionplayerClip;

    public bool isSoundOn = true;
    public bool isMusicOn = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        sfxSource.mute = !isSoundOn;
        musicSource.mute = !isMusicOn;
    }

    public void SetSound(bool on)
    {
        sfxSource.mute = isSoundOn; // Thay vì !isSoundOn
        sfxSource.mute = !on;
        PlayerPrefs.SetInt("SoundOn", on ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetMusic(bool on)
    {
        musicSource.mute = isMusicOn;
        musicSource.mute = !on;
        PlayerPrefs.SetInt("MusicOn", on ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void PlayExplosionSound()
    {
        if (isSoundOn)
            sfxSource.PlayOneShot(explosionClip);
    }

    public void PlayCollectStarSound()
    {
        if (isSoundOn)
            sfxSource.PlayOneShot(collectStarClip);
    }

    public void PlayItemSound()
    {
        if (isSoundOn)
            sfxSource.PlayOneShot(Item);
    }
    public void PlayShieldUpSound()
    {
        if (isSoundOn)
            sfxSource.PlayOneShot(ShieldUp);
    }
    public void PlayShieldDownSound()
    {
        if (isSoundOn)
            sfxSource.PlayOneShot(ShieldDown);
    }
    public void PlayHealHpSound()
    {
        if (isSoundOn)
            sfxSource.PlayOneShot(HealHp);
    }
    public void PlayExplosionPlayerSound()
    {
        if (isSoundOn)
            sfxSource.PlayOneShot(explosionplayerClip);
    }

}