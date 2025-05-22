using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    {
        SceneManager.LoadScene("Game"); // Tên scene chính của game
    }

    public void ShowInstructions()
    {
        // Tạo scene mới tên là "Instructions" hoặc bật panel
        SceneManager.LoadScene("Instructions");
    }
    public void ShowLeaderboard()
    {
        // Tạo scene mới tên là "Leaderboard" hoặc bật panel
        SceneManager.LoadScene("LeaderBoard");
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); // Chỉ hoạt động khi build ra file
    }
    public void BackToMainMenu()
    {
        // Quay lại menu chính
        SceneManager.LoadScene("MainMenu");
    }
    public void ContinuePlay()
    {
        // Quay lại menu chính
        SceneManager.LoadScene("LeaderBoard");
    }
    public void ReplayGame()
    {
        // Bắt đầu trò chơi
        SceneManager.LoadScene("Game");
    }
}
