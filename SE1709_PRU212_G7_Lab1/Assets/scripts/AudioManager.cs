using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource sfxSource;
    public AudioClip explosionClip;
    public AudioClip collectStarClip;
    public AudioClip Item;
    public AudioClip ShieldUp;
    public AudioClip ShieldDown;
    public AudioClip HealHp;
    public AudioClip explosionplayerClip;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayExplosionSound()
    {
        sfxSource.PlayOneShot(explosionClip);
    }

    public void PlayCollectStarSound()
    {
        sfxSource.PlayOneShot(collectStarClip);
    }

    public void PlayItemSound()
    {
        sfxSource.PlayOneShot(Item);
    }
    public void PlayShieldUpSound()
    {
        sfxSource.PlayOneShot(ShieldUp);
    }
    public void PlayShieldDownSound()
    {
        sfxSource.PlayOneShot(ShieldDown);
    }
    public void PlayHealHpSound()
    {
        sfxSource.PlayOneShot(HealHp);
    }
    public void PlayExplosionPlayerSound()
    {
        sfxSource.PlayOneShot(explosionplayerClip);
    }
}