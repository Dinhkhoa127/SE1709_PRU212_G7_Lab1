using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
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
<<<<<<< HEAD
        UpdateStars(score);
=======
>>>>>>> 91fe18daa2073e95350dc7d66a3509779775e89d
        gameObject.SetActive(true);

        // Lấy high score từ file JSON qua ScoreManager
        int highScore = ScoreManager.Instance.GetHighScore();
        highScoreText.text = highScore.ToString();
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

<<<<<<< HEAD
    private void UpdateStars(int score)
    {
        int starCount = 0;
        // Ví dụ: 1 sao cho >= 100, 2 sao cho >= 200, 3 sao cho >= 300
        if (score >= 300) starCount = 3;
        else if (score >= 200) starCount = 2;
        else if (score >= 100) starCount = 1;

        for (int i = 0; i < starImages.Length; i++)
        {
            if (i < starCount)
                starImages[i].sprite = goldStar;
            else
                starImages[i].sprite = emptyStar;
        }
    }
=======

>>>>>>> 91fe18daa2073e95350dc7d66a3509779775e89d
    private void Update()
    {
        if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            OnSubmitName();
        }
    }
}

