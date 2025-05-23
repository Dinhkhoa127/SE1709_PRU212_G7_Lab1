using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Renderer bgRenderer;
    private Vector2 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgRenderer = GetComponent<Renderer>();
        offset = bgRenderer.material.mainTextureOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = GameManager.instance.GetGameSpeed();

        offset.y += speed * Time.deltaTime;

        bgRenderer.material.mainTextureOffset = offset;
    }
}
