using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidSpawn;
    private float minX = -8f;
    private float maxX = 8f;
    private float timer = 0;
    [SerializeField] private float spamRate = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float[] rotationSpeeds = { 150f, 50f, 20f };
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float targetSpawnRate = Mathf.Lerp(2f, 0.5f, GameManager.instance.gameTime / 200f);
        spamRate = Mathf.Max(targetSpawnRate, 0.5f);
        if (timer > spamRate)
        {
            SpawnAsteroids();
            timer = 0;
        }
    }

    private void SpawnAsteroids()
    {
        int numberToSpawn = Random.Range(1, 3); 
        for (int i = 0; i < numberToSpawn; i++)
        {
            int index = Random.Range(0, asteroidSpawn.Length);
            float randomPosition = Random.Range(minX, maxX);
            Vector3 posSpawn = new Vector3(randomPosition, 6f, 0);
            GameObject obstacle = Instantiate(asteroidSpawn[index], posSpawn, Quaternion.identity);

            Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float baseRotationSpeed = rotationSpeeds[index];
                float randomVariation = Random.Range(-50f, 50f);
                rb.angularVelocity = (baseRotationSpeed + randomVariation) * (Random.Range(0, 2) == 0 ? 1 : -1);
            }
        }
    }
}
