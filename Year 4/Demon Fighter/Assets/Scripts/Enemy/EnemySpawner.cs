using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    // Timer variables
    [SerializeField] private float spawnDelay;

    void Start()
    {
        Invoke("SpawnEnemy", spawnDelay);
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
        Invoke("SpawnEnemy", spawnDelay);
    }
}
