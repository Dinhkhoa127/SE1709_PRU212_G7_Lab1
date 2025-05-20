using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    public float scrollSpeed = 0.5f;
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
        // Increment the offset based on scroll speed and time
        offset.y += scrollSpeed * Time.deltaTime;

        // update the texture offset of the material
        bgRenderer.material.mainTextureOffset = offset;
    }
}
