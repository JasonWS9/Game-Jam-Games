using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager instance;


    [SerializeField] private Transform[] spawnpoints;

    [SerializeField] private GameObject enemyPrefab;

    public float spawnInterval = 3f;
    private float spawnTimer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        TimeCheck();
    }

    private void TimeCheck()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }

    private void SpawnEnemy()
    {
        int randSpawnPoint = Random.Range(0, spawnpoints.Length);
        Instantiate(enemyPrefab, spawnpoints[randSpawnPoint].position, Quaternion.identity);
    }
}
