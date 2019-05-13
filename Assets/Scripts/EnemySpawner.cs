using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float enemyDelay;

    private float timeSinceLastSpawn;

    private void Update() {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > enemyDelay) {
            var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            timeSinceLastSpawn = 0;
        }
    }
}