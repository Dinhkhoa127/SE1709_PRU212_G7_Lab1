using UnityEngine;

public enum ItemType { Ammo, Health, Shield }

public class DropItem : MonoBehaviour
{
    public ItemType itemType;
    public float moveSpeed = 0.05f;

    private void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShoot player = collision.GetComponent<PlayerShoot>();
            if (player != null)
            {
                player.Pickup(this); // Truyền chính DropItem
            }

            Destroy(gameObject);
        }
    }
}
