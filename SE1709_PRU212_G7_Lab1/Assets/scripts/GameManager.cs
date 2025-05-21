using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float score = 0;
    private float highScore = 0;
    private int heart = 3;
    [SerializeField] float playerSpeed = 7.0f;
    [SerializeField]
    private float gameSpeed = 0.1f;
    [SerializeField]
    private float speedIncrease = 0.008f;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }    
    }

    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameSpeed += speedIncrease * Time.deltaTime;
        UpdateGameScore();
    }
    

    private void AddBonusScore(int amount)
    {
        score += amount;
        ShowScoreUI();
    }

    private void UpdateGameScore()
    {
        score += Time.deltaTime * 10;
        ShowScoreUI();
    }

    private void ShowScoreUI()
    {
        scoreText.text = "Score: " +Mathf.FloorToInt(score);
    }
}
