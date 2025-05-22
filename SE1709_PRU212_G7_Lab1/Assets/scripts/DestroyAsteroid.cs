using UnityEngine;

public class DestroyAsteroid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float health = 1;
    private float currentGameTimePassed = 0f;
    private float healthIncreaseInterval = 60f; // Tăng máu mỗi 60 giây
    private float healthIncreaseAmount = 1f; // Mỗi lần tăng 1 máu
    
    void Start()
    {
        // Set initial health based on asteroid type
        if (CompareTag("AsteroidSmall")) health = 1;
        else if (CompareTag("AsteroidMedium")) health = 2;
        else if (CompareTag("AsteroidLarge")) health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyAsteroidSpawn(); 
        
        // Tăng thời gian đã trôi qua
        currentGameTimePassed = GameManager.instance.gameTime;
        
        // Kiểm tra nếu đã đủ thời gian để tăng máu
        if (currentGameTimePassed >= healthIncreaseInterval)
        {
            health += healthIncreaseAmount;
            healthIncreaseInterval += 60; // Reset thời gian
        }
    }

    private void DestroyAsteroidSpawn()
    {
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize - 1f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lazer"))
        {
            Destroy(collision.gameObject);
            health--;
            if (health <= 0)
            {
                int bonusScore = 0;
                
                if (CompareTag("AsteroidSmall")) bonusScore = 10;
                else if (CompareTag("AsteroidMedium")) bonusScore = 20;
                else if (CompareTag("AsteroidLarge")) bonusScore = 30;
                Destroy(gameObject);

                GameManager.instance.AddBonusScoreFromAsteroid(bonusScore);
            }
        }
    }
}
