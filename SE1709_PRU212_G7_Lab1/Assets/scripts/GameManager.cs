using System.Threading;
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
    private float currentTime = 0f;
    public float gameTime = 0f;

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
        gameTime += Time.deltaTime;
        gameSpeed += speedIncrease * Time.deltaTime;
        UpdateGameScore();
    }
    

    public void AddBonusScoreFromAsteroid(int amount)
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

        currentTime += Time.deltaTime;
        scoreText.text = "Score: " +Mathf.FloorToInt(score) + " Time: " + Mathf.FloorToInt(currentTime);
    }
}
