using UnityEngine;
using UnityEngine.SceneManagement;

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
    public AudioClip PlayerShoot;
    public bool isSoundOn = true;
    public bool isMusicOn = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            InitializeAudio();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeAudio();
    }

    void InitializeAudio()
    {
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;

        if (sfxSource != null)
            sfxSource.mute = !isSoundOn;

        if (musicSource != null)
        {
            musicSource.mute = !isMusicOn;
            if (!musicSource.isPlaying && isMusicOn)
                musicSource.Play();
            //if (SceneManager.GetActiveScene().name == "MainMenu")
            //{
                
            //}
            //else
            //{
            //    musicSource.Stop();
            //}
        }
    }

    public void SetSound(bool on)
    {
        isSoundOn = on;
        if (sfxSource != null)
            sfxSource.mute = !on;
        PlayerPrefs.SetInt("SoundOn", on ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetMusic(bool on)
    {
        isMusicOn = on;
        if (musicSource != null)
        {
            musicSource.mute = !on;
            if (on && !musicSource.isPlaying)
                musicSource.Play();
            else if (!on && musicSource.isPlaying)
                musicSource.Stop();
            //if (SceneManager.GetActiveScene().name == "MainMenu")
            //{
              
            //}
            //else
            //{
            //    musicSource.Stop();
            //}
        }
        PlayerPrefs.SetInt("MusicOn", on ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void PlayExplosionSound()
    {
        if (isSoundOn && sfxSource != null && explosionClip != null)
            sfxSource.PlayOneShot(explosionClip);
    }

    public void PlayCollectStarSound()
    {
        if (isSoundOn && sfxSource != null && collectStarClip != null)
            sfxSource.PlayOneShot(collectStarClip);
    }

    public void PlayItemSound()
    {
        if (isSoundOn && sfxSource != null && Item != null)
            sfxSource.PlayOneShot(Item);
    }
    public void PlayerShootsound()
    {
        if (isSoundOn && sfxSource != null && Item != null)
            sfxSource.PlayOneShot(Item);
    }
    public void PlayShieldUpSound()
    {
        if (isSoundOn && sfxSource != null && ShieldUp != null)
            sfxSource.PlayOneShot(ShieldUp);
    }

    public void PlayShieldDownSound()
    {
        if (isSoundOn && sfxSource != null && ShieldDown != null)
            sfxSource.PlayOneShot(ShieldDown);
    }

    public void PlayHealHpSound()
    {
        if (isSoundOn && sfxSource != null && HealHp != null)
            sfxSource.PlayOneShot(HealHp);
    }

    public void PlayExplosionPlayerSound()
    {
        if (isSoundOn && sfxSource != null && explosionplayerClip != null)
            sfxSource.PlayOneShot(explosionplayerClip);
    }
}