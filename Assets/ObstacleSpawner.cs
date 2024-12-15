using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;    // The prefab for obstacles
    public Transform player;             // Reference to the player
    public float spawnDistance = 5f;     // Distance ahead of the player to spawn obstacles
    public float minSpawnInterval = 1f;  // Minimum spawn interval in seconds
    public float maxSpawnInterval = 3f;  // Maximum spawn interval in seconds

    private float nextSpawnTime;

    void Start()
    {
        ScheduleNextSpawn();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            ScheduleNextSpawn();
        }
    }

    void SpawnObstacle()
    {
        // Randomly choose a lane (-3, 0, 3)
        float[] lanes = { -3f, 0f, 3f };
        float randomLane = lanes[Random.Range(0, lanes.Length)];

        // Calculate spawn position
        Vector3 spawnPosition = new Vector3(randomLane, 1f, player.position.z + spawnDistance);

        // Instantiate the obstacle
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        // Destroy the obstacle after 15 seconds
        Destroy(obstacle, 15f);
    }

    void ScheduleNextSpawn()
    {
        // Schedule the next spawn time
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}
