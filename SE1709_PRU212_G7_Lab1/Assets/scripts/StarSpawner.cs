using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;   // Tham chiếu đến prefab của sao
    [SerializeField] private float spamRate = 15f;   // Tần suất spawn sao (thời gian giữa các lần spawn, tính bằng giây)
    [SerializeField] private float spanwRateIncrease = 0.3f; // Tăng tần suất spawn sau mỗi lần spawn (giây)
    private float minX = -7.5f;
    private float maxX = 7.5f;
    private float timer = 0;


    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spamRate)
        {
            SpawnStar();
            timer = 0;
            spamRate += spanwRateIncrease;
        }
    }

    void SpawnStar()
    {
        
        float randomPosition = Random.Range(minX, maxX);
        Vector3 posSpawn = new Vector3(randomPosition, 2.5f, 0);
        GameObject star = Instantiate(starPrefab, posSpawn, Quaternion.identity);
    }
}
