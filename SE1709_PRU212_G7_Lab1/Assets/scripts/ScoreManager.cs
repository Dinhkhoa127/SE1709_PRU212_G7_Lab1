using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class PlayerScore
{
    public string playerName;
    public int score;
    public string date;
}

[System.Serializable]
public class ScoreData
{
    public List<PlayerScore> scores;
}

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public ScoreData scoreData;
    private string filePath;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            filePath = Path.Combine(Application.persistentDataPath, "points.json");
            LoadScores();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScores()
    {
        //TextAsset jsonFile = Resources.Load<TextAsset>("points");
        //if (jsonFile != null)
        //{
        //    scoreData = JsonUtility.FromJson<ScoreData>(jsonFile.text);
        //}
        //else
        //{
        //    scoreData = new ScoreData();
        //    scoreData.scores = new List<PlayerScore>();
        //}
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            scoreData = JsonUtility.FromJson<ScoreData>(json);
            if (scoreData == null || scoreData.scores == null)
                scoreData = new ScoreData() { scores = new List<PlayerScore>() };
        }
        else
        {
            // Nếu chưa có file, load từ Resources hoặc tạo mới
            TextAsset jsonFile = Resources.Load<TextAsset>("points");
            if (jsonFile != null)
            {
                scoreData = JsonUtility.FromJson<ScoreData>(jsonFile.text);
            }
            if (scoreData == null || scoreData.scores == null)
            {
                scoreData = new ScoreData() { scores = new List<PlayerScore>() };
            }
            SaveScores(); // Lưu file mới ra persistentDataPath
        }
    }
    public void AddScore(string playerName, int score)
    {
        if (scoreData == null)
            scoreData = new ScoreData() { scores = new List<PlayerScore>() };

        PlayerScore newScore = new PlayerScore()
        {
            playerName = playerName,
            score = score,
            date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        scoreData.scores.Add(newScore);
        SaveScores();
    }
    public void SaveScores()
    {
        string json = JsonUtility.ToJson(scoreData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Scores saved to " + filePath);
    }


    public List<PlayerScore> GetTopScores(int count = 10)
    {
        scoreData.scores.Sort((a, b) => b.score.CompareTo(a.score));
        return scoreData.scores.GetRange(0, Mathf.Min(count, scoreData.scores.Count));
    }
}