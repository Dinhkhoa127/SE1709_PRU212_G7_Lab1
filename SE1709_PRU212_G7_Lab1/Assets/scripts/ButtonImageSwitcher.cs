using UnityEngine;
using UnityEngine.UI;

public class ButtonImageSwitcher : MonoBehaviour
{
    public GameObject imageNormal;    // Kéo Sound vào đây
    public GameObject imageActive;    // Kéo SoundActive vào đây

    // Hàm này sẽ chuyển đổi qua lại giữa 2 hình
    public void ToggleImage()
    {
        bool isActive = !imageActive.activeSelf;
        imageNormal.SetActive(!isActive);
        imageActive.SetActive(isActive);
    }
  
}