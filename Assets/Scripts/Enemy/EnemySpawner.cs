using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Boundary")]
    [SerializeField] private Vector2 horizontalRange;
    [SerializeField] private Vector2 verticalRange;

    [Header("Spawning")]
    [SerializeField] private float spawnRate;
    private float _timer;

    private void Awake() => _timer = spawnRate - 1f;
    private void Update()
    {
        // A Timer to control spawn rate
        _timer += Time.deltaTime;
        if(_timer >= spawnRate)
        {
            // Spawning the Enemy
            SpawnEnemy();

            _timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        // Generating a Random Location
        Vector2 location = new Vector2(Random.Range(horizontalRange.x, horizontalRange.y), Random.Range(verticalRange.x, verticalRange.y));

        // Spawning an Enemy Gameobject at that location
        GameObject enemy = Instantiate(enemyPrefab, location, Quaternion.identity);
    }
}