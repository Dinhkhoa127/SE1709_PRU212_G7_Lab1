using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidSpawn;
    private float minX = -8f;
    private float maxX = 8f;
    private float timer = 0;
    [SerializeField] private float spamRate = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float targetSpawnRate = Mathf.Lerp(2f, 1f, GameManager.instance.gameTime / 150f);
        spamRate = Mathf.Max(targetSpawnRate, 1f);
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
            Instantiate(asteroidSpawn[index], posSpawn, Quaternion.identity);
        }
    }
}
