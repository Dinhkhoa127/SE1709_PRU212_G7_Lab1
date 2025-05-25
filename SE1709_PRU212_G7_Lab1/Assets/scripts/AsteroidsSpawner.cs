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
        if (timer > spamRate)
        {
            SpawnAsteroids();
            timer = 0;
        }
    }

    private void SpawnAsteroids()
    {
        int index = Random.Range(0, asteroidSpawn.Length);
        float randomPosition = Random.Range(minX, maxX);
        Vector3 posSpawn = new Vector3(randomPosition, 6f, 0);
        GameObject obstacle = Instantiate(asteroidSpawn[index], posSpawn, Quaternion.identity);
    }
}
