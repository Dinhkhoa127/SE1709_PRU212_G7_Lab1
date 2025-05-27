using System.Collections.Generic;
using UnityEngine;

public class DestroyAsteroid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float health = 1;
    private float currentGameTimePassed = 0f;
    private float healthIncreaseInterval = 60f; // Tăng máu mỗi 60 giây
    private float healthIncreaseAmount = 1f; // Mỗi lần tăng 1 máu
    public GameObject explosionAsteroid; // Gắn prefab hiệu ứng nổ từ Inspector
    [System.Serializable]
    public class DropOption
    {
        public GameObject prefab;
        [Range(0f, 1f)] public float chance; // Tỉ lệ rơi
    }
    public List<DropOption> dropOptions;
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
    void TryDropItem()
    {
        foreach (DropOption option in dropOptions)
        {
            if (Random.value <= option.chance)
            {
                Instantiate(option.prefab, transform.position, Quaternion.identity);
                break; // chỉ rơi 1 vật phẩm
            }
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
                if (CompareTag("AsteroidSmall")) bonusScore = 2;
                else if (CompareTag("AsteroidMedium")) bonusScore = 3;
                else if (CompareTag("AsteroidLarge")) bonusScore = 4;
                if (explosionAsteroid != null)
                {
                    GameObject effect = Instantiate(explosionAsteroid, transform.position, Quaternion.identity);
                    Destroy(effect, 0.3f); // Hủy hiệu ứng sau 1 giây
                }
                Destroy(gameObject);
                AudioManager.instance.PlayExplosionSound();
                TryDropItem();
                GameManager.instance.AddBonusScoreFromAsteroid(bonusScore);
            }
        }
       
    }

  
}
