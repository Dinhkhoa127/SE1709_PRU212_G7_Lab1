using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LeaderboardUI : MonoBehaviour
{
    public Transform scoreContainer; // Gán trong Inspector
    public GameObject scoreEntryPrefab; // Gán trong Inspector

    void Start()
    {
        //ShowLeaderboard();
        // Đảm bảo ScoreManager.Instance luôn tồn tại

        Debug.Log("LeaderboardUI Start, ScoreManager.Instance: " + (ScoreManager.Instance != null));
        if (ScoreManager.Instance != null)
        {
            ShowLeaderboard();
        }
        else
        {
            Debug.LogError("ScoreManager.Instance is null! Đảm bảo ScoreManager chỉ có ở scene MainMenu và dùng DontDestroyOnLoad.");
        }

    }

    public void ShowLeaderboard()
    {
        foreach (Transform child in scoreContainer)
        {
            Destroy(child.gameObject);
        }

        var topScores = ScoreManager.Instance.GetTopScores(5);
        for (int i = 0; i < topScores.Count; i++)
        {
            var entry = Instantiate(scoreEntryPrefab, scoreContainer);
            var texts = entry.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (i + 1).ToString(); // Rank
            texts[1].text = topScores[i].playerName;
            texts[2].text = topScores[i].score.ToString();
            texts[3].text = topScores[i].date;

            if (i == 0) // Top 1
            {
                foreach (var t in texts)
                    t.color = new Color32(255, 215, 0, 255); // Vàng: #FFD700
            }
            else if (i == 1) // Top 2
            {
                foreach (var t in texts)
                    t.color = new Color32(192, 192, 192, 255); // Bạc: #C0C0C0
            }
            else if (i == 2) // Top 3
            {
                foreach (var t in texts)
                    t.color = new Color32(205, 127, 50, 255); // Đồng: #CD7F32
            }
            else if (i > 2) // Top 3
            {
                foreach (var t in texts)
                    t.color = new Color32(255, 255, 255, 255); // Đồng: #FFFFFF
            }
        }
    }
}

