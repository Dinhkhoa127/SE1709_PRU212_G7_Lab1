using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [Header("Background Settings")]
    [SerializeField] private Material[] backgroundMaterials; // Mảng chứa các background materials
    [SerializeField] private float timeToChangeBackground = 30f; // Thời gian để chuyển background (giây)
    [SerializeField] private bool useTransitionEffect = true; // Có sử dụng hiệu ứng chuyển cảnh không
    
    [Header("References")]
    [SerializeField] private MeshRenderer backgroundMeshRenderer; // Cho 3D background với Material
    [SerializeField] private SceneTransition sceneTransition; // Reference đến SceneTransition
    
    private float currentTime = 0f;
    private int currentBackgroundIndex = 0;
    
    void Start()
    {
        // Tự động tìm MeshRenderer nếu chưa được gán
        if (backgroundMeshRenderer == null)
        {
            backgroundMeshRenderer = GetComponent<MeshRenderer>();
        }
        
        // Tự động tìm SceneTransition nếu chưa được gán
        if (sceneTransition == null)
        {
            sceneTransition = FindObjectOfType<SceneTransition>();
        }
        
        // Đặt background đầu tiên
        if (backgroundMeshRenderer != null && backgroundMaterials.Length > 0)
        {
            SetBackgroundMaterial(0);
        }
    }
    
    void Update()
    {
        // Không đếm thời gian nếu đang trong quá trình transition
        if (sceneTransition != null && sceneTransition.IsTransitioning())
            return;
            
        // Tăng thời gian hiện tại
        currentTime += Time.deltaTime;
        
        // Kiểm tra nếu đã đến thời gian chuyển background
        if (currentTime >= timeToChangeBackground)
        {
            ChangeToNextBackground();
            currentTime = 0f; // Reset thời gian
        }
    }
    
    private void ChangeToNextBackground()
    {
        if (backgroundMeshRenderer == null || backgroundMaterials.Length <= 1) return;
        
        if (useTransitionEffect && sceneTransition != null)
        {
            // Sử dụng hiệu ứng chuyển cảnh mượt mà
            sceneTransition.StartTransition();
        }
        else
        {
            // Chuyển background ngay lập tức (cách cũ)
            ForceChangeBackground();
        }
    }
    
    private void SetBackgroundMaterial(int index)
    {
        if (index < 0 || index >= backgroundMaterials.Length || backgroundMeshRenderer == null) return;
        
        backgroundMeshRenderer.material = backgroundMaterials[index];
    }
    
    // Hàm public để thay đổi thời gian chuyển background từ script khác
    public void SetChangeTime(float newTime)
    {
        timeToChangeBackground = newTime;
    }
    
    // Hàm để chuyển background ngay lập tức (được gọi từ SceneTransition)
    public void ForceChangeBackground()
    {
        if (backgroundMeshRenderer == null || backgroundMaterials.Length <= 1) return;
        
        // Chuyển đến background tiếp theo
        currentBackgroundIndex = (currentBackgroundIndex + 1) % backgroundMaterials.Length;
        SetBackgroundMaterial(currentBackgroundIndex);
        
        Debug.Log($"Changed to background material {currentBackgroundIndex + 1}");
    }
    
    // Hàm để lấy thời gian còn lại cho lần chuyển tiếp theo
    public float GetTimeUntilNextChange()
    {
        return timeToChangeBackground - currentTime;
    }
    
    // Hàm để chuyển đến background cụ thể
    public void SetSpecificBackground(int index)
    {
        if (backgroundMeshRenderer != null && backgroundMaterials.Length > 0)
        {
            if (index >= 0 && index < backgroundMaterials.Length)
            {
                currentBackgroundIndex = index;
                SetBackgroundMaterial(index);
            }
        }
    }
    
    // Hàm để bật/tắt hiệu ứng transition
    public void SetTransitionEffect(bool enabled)
    {
        useTransitionEffect = enabled;
    }
} 