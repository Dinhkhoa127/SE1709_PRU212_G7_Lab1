using TMPro;
using UnityEngine;
using UnityEngine.UI; // Nếu dùng Text thường
// using TMPro; // Nếu dùng TextMeshPro

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public TMP_Text pauseScoreText; // Kéo Text này vào Inspector

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Lấy điểm hiện tại từ GameManager và hiển thị
        if (pauseScoreText != null && GameManager.instance != null)
        {
            float score = GameManager.instance.GetCurrentScore();
            pauseScoreText.text = "Score: " + Mathf.FloorToInt(score);
        }
    }
}