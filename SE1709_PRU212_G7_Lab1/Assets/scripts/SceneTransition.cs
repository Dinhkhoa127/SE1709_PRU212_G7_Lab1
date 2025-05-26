using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 3f; // Thời gian chuyển cảnh
    [SerializeField] private float playerFlyHeight = 15f; // Độ cao phi thuyền bay lên
    [SerializeField] private float flyTimePercentage = 0.4f; // % thời gian dành cho bay (0.4 = 40%)
    [SerializeField] private float fadeTimePercentage = 0.2f; // % thời gian dành cho fade (0.2 = 20%)
    [SerializeField] private AnimationCurve flyUpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // Đường cong bay lên
    [SerializeField] private AnimationCurve flyDownCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // Đường cong bay xuống
    
    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private BackgroundManager backgroundManager;
    [SerializeField] private CanvasGroup fadePanel; // UI Panel để fade màn hình
    
    private Vector3 originalPlayerPosition;
    private bool isTransitioning = false;
    private Collider2D playerCollider;
    private Collider playerCollider3D;
    private int originalLayer;
    
    void Start()
    {
        // Tự động tìm player nếu chưa gán
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerTransform = player.transform;
        }
        
        // Tự động tìm BackgroundManager nếu chưa gán
        if (backgroundManager == null)
        {
            backgroundManager = FindObjectOfType<BackgroundManager>();
        }
        
        // Lưu vị trí ban đầu của player và collider
        if (playerTransform != null)
        {
            originalPlayerPosition = playerTransform.position;
            
            // Tìm collider của player
            playerCollider = playerTransform.GetComponent<Collider2D>();
            playerCollider3D = playerTransform.GetComponent<Collider>();
            
            // Lưu layer ban đầu
            originalLayer = playerTransform.gameObject.layer;
        }
    }
    
    public void StartTransition()
    {
        if (isTransitioning) return;
        
        StartCoroutine(TransitionCoroutine());
    }
    
    private IEnumerator TransitionCoroutine()
    {
        isTransitioning = true;
        
        // Vô hiệu hóa điều khiển player và collision
        DisablePlayerControl(true);
        DisablePlayerCollision(true);
        PauseGameObjects(true);
        
        // Phase 1: Player bay lên khỏi màn hình
        yield return StartCoroutine(PlayerFlyUp());
        
        // Phase 2: Fade out màn hình
        yield return StartCoroutine(FadeOut());
        
        // Phase 3: Đổi background
        if (backgroundManager != null)
        {
            backgroundManager.ForceChangeBackground();
        }
        
        // Phase 4: Di chuyển player xuống dưới màn hình (không hiển thị)
        MovePlayerToBottom();
        
        // Phase 5: Fade in màn hình
        yield return StartCoroutine(FadeIn());
        
        // Phase 6: Player bay lên từ dưới màn hình về vị trí ban đầu
        yield return StartCoroutine(PlayerFlyFromBottom());
        
        // Kích hoạt lại điều khiển player và collision
        DisablePlayerControl(false);
        DisablePlayerCollision(false);
        PauseGameObjects(false);
        
        isTransitioning = false;
    }
    
    private IEnumerator PlayerFlyUp()
    {
        if (playerTransform == null) yield break;
        
        Vector3 startPos = playerTransform.position;
        Vector3 targetPos = startPos + Vector3.up * playerFlyHeight;
        
        float elapsed = 0f;
        float flyDuration = transitionDuration * flyTimePercentage;
        
        while (elapsed < flyDuration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / flyDuration;
            float curveValue = flyUpCurve.Evaluate(progress);
            
            playerTransform.position = Vector3.Lerp(startPos, targetPos, curveValue);
            yield return null;
        }
        
        playerTransform.position = targetPos;
    }
    
    private void MovePlayerToBottom()
    {
        if (playerTransform == null) return;
        
        // Di chuyển player xuống dưới màn hình (ngoài tầm nhìn)
        Vector3 bottomPosition = originalPlayerPosition;
        bottomPosition.y = originalPlayerPosition.y - playerFlyHeight; // Dưới vị trí ban đầu
        
        playerTransform.position = bottomPosition;
    }
    
    private IEnumerator PlayerFlyFromBottom()
    {
        if (playerTransform == null) yield break;
        
        Vector3 startPos = playerTransform.position; // Vị trí dưới màn hình
        Vector3 targetPos = originalPlayerPosition; // Vị trí ban đầu
        
        float elapsed = 0f;
        float flyDuration = transitionDuration * flyTimePercentage;
        
        while (elapsed < flyDuration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / flyDuration;
            float curveValue = flyDownCurve.Evaluate(progress);
            
            playerTransform.position = Vector3.Lerp(startPos, targetPos, curveValue);
            yield return null;
        }
        
        playerTransform.position = targetPos;
    }
    
    private IEnumerator FadeOut()
    {
        if (fadePanel == null) yield break;
        
        float elapsed = 0f;
        float fadeDuration = transitionDuration * fadeTimePercentage; // 20% thời gian để fade
        
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            fadePanel.alpha = alpha;
            yield return null;
        }
        
        fadePanel.alpha = 1f;
    }
    
    private IEnumerator FadeIn()
    {
        if (fadePanel == null) yield break;
        
        float elapsed = 0f;
        float fadeDuration = transitionDuration * fadeTimePercentage; // 20% thời gian để fade
        
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            fadePanel.alpha = alpha;
            yield return null;
        }
        
        fadePanel.alpha = 0f;
    }
    
    private void DisablePlayerControl(bool disable)
    {
        // Tìm và vô hiệu hóa script điều khiển player
        if (playerTransform != null)
        {
            MonoBehaviour[] playerScripts = playerTransform.GetComponents<MonoBehaviour>();
            foreach (var script in playerScripts)
            {
                if (script.GetType().Name.Contains("Player") || 
                    script.GetType().Name.Contains("Move") ||
                    script.GetType().Name.Contains("Control"))
                {
                    script.enabled = !disable;
                }
            }
        }
    }
    
    private void DisablePlayerCollision(bool disable)
    {
        if (playerTransform == null) return;
        
        if (disable)
        {
            // Vô hiệu hóa collision bằng cách:
            // 1. Tắt collider
            if (playerCollider != null)
                playerCollider.enabled = false;
            if (playerCollider3D != null)
                playerCollider3D.enabled = false;
                
            // 2. Chuyển sang layer không va chạm (nếu có)
            // Layer 2 thường là "Ignore Raycast" - không va chạm với gì
            playerTransform.gameObject.layer = 2;
        }
        else
        {
            // Kích hoạt lại collision
            // 1. Bật lại collider
            if (playerCollider != null)
                playerCollider.enabled = true;
            if (playerCollider3D != null)
                playerCollider3D.enabled = true;
                
            // 2. Trả về layer ban đầu
            playerTransform.gameObject.layer = originalLayer;
        }
    }
    
    // Hàm public để gọi từ BackgroundManager
    public bool IsTransitioning()
    {
        return isTransitioning;
    }
    
    private IEnumerator PlayerFlyDown()
    {
        // Hàm này không còn được sử dụng nhưng giữ lại để tránh lỗi
        yield break;
    }

    private void PauseGameObjects(bool pause)
    {
        // Tạm dừng/tiếp tục tất cả asteroid (3 loại)
        PauseObjectsByTag("AsteroidSmall", pause);
        PauseObjectsByTag("AsteroidMedium", pause);
        PauseObjectsByTag("AsteroidLarge", pause);
        
        // Tạm dừng/tiếp tục spawner
        GameObject spawner = GameObject.Find("Spawner");
        if (spawner != null)
        {
            MonoBehaviour[] spawnerScripts = spawner.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in spawnerScripts)
            {
                if (script != null && script.GetType().Name.Contains("Spawn"))
                    script.enabled = !pause;
            }
        }
    }
    
    private void PauseObjectsByTag(string tag, bool pause)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (script != null)
                    script.enabled = !pause;
            }
        }
    }
} 