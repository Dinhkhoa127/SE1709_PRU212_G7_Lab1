using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] private Image[] heartImages;
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;

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
        scoreText.text = "Score: " + Mathf.FloorToInt(score) + " Time: " + Mathf.FloorToInt(currentTime);
    }
    private void UpdateHeartUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < heart)
            {
                heartImages[i].sprite = fullHeartSprite;
            }
            else
            {
                heartImages[i].sprite = emptyHeartSprite;
            }
        }
    }
    public void AddHealth()
    {
        if (heart == 3)
        {
            heart = 3;
        }
        else
        {
            heart++;
            UpdateHeartUI();
        }
    }
    public void TakeDamage()
    {
        //if (heart <= 0) return;

        heart--;
        UpdateHeartUI();


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerShoot>().ResetPlayer();
        }

        if (heart <= 0)
        {
            PlayerPrefs.SetInt("FinalScore", Mathf.FloorToInt(score));
            PlayerPrefs.Save();
            SceneManager.LoadScene("EndGame");
        }
    }
    public float GetCurrentScore()
    {
        return score;
    }

}
