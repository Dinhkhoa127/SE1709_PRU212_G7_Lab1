using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_Text scoreText;

    private int currentScore;
    private bool hasSaved = false;
    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        ShowGameOver(finalScore);
    }


    /// <summary>
    /// Hiển thị UI game over và điểm hiện tại
    /// </summary>
    public void ShowGameOver(int score)
    {
        currentScore = score;
        scoreText.text = currentScore.ToString();
        nameInputField.text = "";
        nameInputField.ActivateInputField();
        hasSaved = false;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Gọi khi nhấn Enter để lưu điểm
    /// </summary>
    public void OnSubmitName()
    {
        if (hasSaved) return;

        string playerName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Please enter your name.");
            nameInputField.ActivateInputField();
            return;
        }

        ScoreManager.Instance.AddScore(playerName, currentScore);

        Debug.Log($"Saved score for {playerName} : {currentScore}");

        hasSaved = true;
        gameObject.SetActive(false);
    }


    private void Update()
    {
        if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            OnSubmitName();
        }
    }
}

