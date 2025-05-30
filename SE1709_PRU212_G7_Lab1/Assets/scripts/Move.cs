using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Vector2 movement;

    [SerializeField] private float minX = -8.47f;
    [SerializeField] private float maxX = 8.47f;
    [SerializeField] private float minY = -4.47f;
    [SerializeField] private float maxY = 4.47f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
    }

    void FixedUpdate()
    {
        Vector2 newPos = rb.position + movement * GameManager.instance.GetPlayerSpeed() * Time.fixedDeltaTime;
        newPos = ClampPosition(newPos);
        rb.MovePosition(newPos);
    }

    Vector2 ClampPosition(Vector2 pos)
    {
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        return pos;
    }
}
