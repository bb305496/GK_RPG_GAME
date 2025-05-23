using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float minSpawnTime;
    public float maxSpawnTime;
    public int maxEnemies;
    public bool instaSpawn = false;
    private int currentNumberOfEnemies = 0;

    private float timeUntilSpawn;

    private void Awake()
    {
        SetTimeUntilSpawn();
    }

    private void Update()
    {
        if(currentNumberOfEnemies >= maxEnemies)
        {
            return;
        }

        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            currentNumberOfEnemies += 1;

            enemy.GetComponent<EnemyHealth>().OnEnemyDestroyed += HandleEnemyDestroyed;

            SetTimeUntilSpawn();
        }

        if(instaSpawn)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            currentNumberOfEnemies += 1;

            enemy.GetComponent<EnemyHealth>().OnEnemyDestroyed += HandleEnemyDestroyed;

            SetTimeUntilSpawn();
            instaSpawn = false;
        }
        

    }


    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void HandleEnemyDestroyed()
    {
        currentNumberOfEnemies -= 1;
    }

    //Just to see spawner position
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 1);

    }
}
