using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // reference to the bullet prefab
    public Transform firePoint;    // shooting point
    public float laserSpeed = 10f; // speed of the bullet
    public float fireRate = 0.5f;  // fire rate in seconds
    private float nextFireTime = 0f;
    private Vector3 startPosition;
    public GameObject shield; 
    private bool isInvincible = false;
    private float invincibleDuration = 3f;
    public GameObject hitEffectPrefab;
    private bool isRapidFire = false;
    // Update is called once per frame

    void Start()
    {
        startPosition = transform.position;
        shield.SetActive(false); 
    }
    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject laser = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * laserSpeed;
    }
    public void ResetPlayer()
    {
        if (shield != null)
        {
            shield.SetActive(false);
        }
        transform.position = startPosition;
       
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;    
        }
        StopAllCoroutines();
        StartCoroutine(InvincibilityCoroutine());
    }
    private IEnumerator InvincibilityCoroutine()
    { isInvincible = true;
    shield.SetActive(true);

    yield return new WaitForSeconds(invincibleDuration);

    shield.SetActive(false);
    isInvincible = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.CompareTag("AsteroidSmall") || collision.CompareTag("AsteroidMedium") || collision.CompareTag("AsteroidLarge")) && !isInvincible)
        {
            if (hitEffectPrefab != null)
            {
                GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 0.4f);
            }
            Destroy(collision.gameObject);
            GameManager.instance.TakeDamage();   
            ResetPlayer();
        }
    }
    public void Pickup(DropItem item)
    {
        switch (item.itemType)
        {
            case ItemType.Ammo:
                ActivateRapidFire(0.15f, 5f); // Bắn nhanh trong 5 giây
                break;

            case ItemType.Health:
                GameManager.instance.AddHealth(); // Tăng máu (tùy chỉnh trong GameManager)
                break;

            case ItemType.Shield:
                StartCoroutine(ActivateShield(5f)); // Bật khiên trong 5 giây
                break;
        }
    }
    private Coroutine rapidFireCoroutine;



    private void ActivateRapidFire(float newRate, float duration)
    {
        if (rapidFireCoroutine != null)
        {
            StopCoroutine(rapidFireCoroutine);
            fireRate = 0.5f; // Reset về mặc định trước khi chạy lại
        }

        rapidFireCoroutine = StartCoroutine(RapidFireRoutine(newRate, duration));
    }

    private IEnumerator RapidFireRoutine(float newRate, float duration)
    {
        float originalRate = 0.5f; // mặc định
        fireRate = newRate;
        isRapidFire = true;

        yield return new WaitForSeconds(duration);

        fireRate = originalRate;
        isRapidFire = false;
        rapidFireCoroutine = null; // Xóa để tránh bug nếu bị gọi lại
    }
    private IEnumerator ActivateShield(float duration)
    {
        isInvincible = true;
        shield.SetActive(true);

        yield return new WaitForSeconds(duration);

        shield.SetActive(false);
        isInvincible = false;
    }

}
