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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadScores();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScores()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("points");
        if (jsonFile != null)
        {
            scoreData = JsonUtility.FromJson<ScoreData>(jsonFile.text);
        }
        else
        {
            scoreData = new ScoreData();
            scoreData.scores = new List<PlayerScore>();
        }
    }

    public List<PlayerScore> GetTopScores(int count = 10)
    {
        scoreData.scores.Sort((a, b) => b.score.CompareTo(a.score));
        return scoreData.scores.GetRange(0, Mathf.Min(count, scoreData.scores.Count));
    }
}