using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
    }
     void FixedUpdate()
    {
        rb.linearVelocity = movement * GameManager.instance.GetPlayerSpeed();
    }
}
