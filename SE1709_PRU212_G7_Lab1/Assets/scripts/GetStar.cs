using UnityEngine;

public class GetStar : MonoBehaviour
{

    void Update()
    {
        DestroyStarSpawn();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the collided object has the tag "Player"
        if (collider.gameObject.CompareTag("Player"))
        {
            GameManager.instance.AddBonusScoreFromAsteroid(5);
            AudioManager.instance.PlayCollectStarSound();
            Destroy(gameObject);  
        }
        else
        {
            Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>());
        }
    }

    private void DestroyStarSpawn()
    {
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize - 1f)
        {
            Destroy(gameObject);
        }
    }
}
