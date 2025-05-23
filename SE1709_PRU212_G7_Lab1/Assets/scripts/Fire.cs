using UnityEngine;

public class Fire : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // bullet goes up
        rb.linearVelocity = Vector2.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > Camera.main.transform.position.y + Camera.main.orthographicSize + 1f)
        {
            Destroy(gameObject);
        }
    }
}
