﻿using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // reference to the bullet prefab
    public Transform firePoint1;    // shooting point 1
    public Transform firePoint2;    // shooting point 2
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
        // Kiểm tra xem có đang ở chế độ rapid fire hay không
        if (isRapidFire)
        {
            // Lấy vị trí của firePoint1
            Vector3 adjustedFirePoint1 = firePoint1.position;
            adjustedFirePoint1.x -= 0.12f; // Dịch chuyển firePoint đầu tiên sang trái một chút

            // Tạo viên đạn từ điểm bắn chính (firePoint)
            GameObject laser1 = Instantiate(bulletPrefab, adjustedFirePoint1, firePoint1.rotation);
            Rigidbody2D rb1 = laser1.GetComponent<Rigidbody2D>();
            rb1.linearVelocity = transform.up * laserSpeed;

            // Tạo viên đạn từ điểm bắn thứ hai (firePoint2)
            GameObject laser2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
            Rigidbody2D rb2 = laser2.GetComponent<Rigidbody2D>();
            rb2.linearVelocity = transform.up * laserSpeed;
        }
        else
        {
            // Nếu không phải rapid fire, chỉ bắn từ điểm firePoint chính
            GameObject laser = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
            Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
            rb.linearVelocity = transform.up * laserSpeed;

        }
        AudioManager.instance.PlayerShootsound(); // Phát âm thanh bắn
    }
    public void ResetPlayer()
    {
        isRapidFire = false;
        fireRate = 0.5f;
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
    {
        isInvincible = true;
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
            AudioManager.instance.PlayExplosionPlayerSound();
            ResetPlayer();
        }
    }
    public void Pickup(DropItem item)
    {
        switch (item.itemType)
        {
            case ItemType.Ammo:
                ActivateRapidFire(0.15f, 5f); // Bắn nhanh trong 5 giây
                AudioManager.instance.PlayItemSound();
                break;

            case ItemType.Health:
                GameManager.instance.AddHealth(); // Tăng máu (tùy chỉnh trong GameManager)
                AudioManager.instance.PlayHealHpSound();
                break;

            case ItemType.Shield:
                StartCoroutine(ActivateShield(5f)); // Bật khiên trong 5 giây
                AudioManager.instance.PlayShieldUpSound();
                break;
        }
    }
    private Coroutine rapidFireCoroutine;
    private float rapidFireRemainingTime = 0f;

    private void ActivateRapidFire(float newRate, float duration)
    {
        if (!isRapidFire)
        {
            rapidFireRemainingTime = duration;
            rapidFireCoroutine = StartCoroutine(RapidFireRoutine(newRate));
        }
        else
        {
            if (rapidFireRemainingTime > 15f)
            {
                rapidFireRemainingTime = 15f;
            }
            else
            {
                rapidFireRemainingTime += duration;
            }
        }
    }

    private IEnumerator RapidFireRoutine(float newRate)
    {
        float originalRate = 0.5f;
        fireRate = newRate;
        isRapidFire = true;

        while (rapidFireRemainingTime > 0f)
        {
            yield return null;
            rapidFireRemainingTime -= Time.deltaTime;
        }

        // Khi hết thời gian
        fireRate = originalRate;
        isRapidFire = false;
        rapidFireCoroutine = null;
    }
    private IEnumerator ActivateShield(float duration)
    {
        isInvincible = true;
        shield.SetActive(true);

        yield return new WaitForSeconds(duration);

        shield.SetActive(false);
        isInvincible = false;
        AudioManager.instance.PlayShieldDownSound();
    }

}
