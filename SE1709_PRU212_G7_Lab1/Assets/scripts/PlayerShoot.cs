using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // reference to the bullet prefab
    public Transform firePoint;    // shooting point
    public float laserSpeed = 10f; // speed of the bullet
    public float fireRate = 0.5f;  // fire rate in seconds
    private float nextFireTime = 0f;

    // Update is called once per frame
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
}
