using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    public EnemySpawnerPreferences EnemySpawnerPreferences;
    public EnemySpawner EnemySpawner;
    public Vector2 AltarPosition;

    public void StartWave()
    {
        int randomEnemyCount = Random.Range(3, 8);

        for (int i = 0; i < randomEnemyCount; i++)
        {
            int spawnPointIndex = Random.Range(0, EnemySpawner.EnemySpawnPoints.Count);
                int randomEnemy = Random.Range(0, EnemySpawnerPreferences.EnemyPrefabs.Length);

                GameObject enemy = Instantiate(EnemySpawnerPreferences.EnemyPrefabs[randomEnemy], EnemySpawner.EnemySpawnPoints[spawnPointIndex].position, Quaternion.identity);

                enemy.transform.SetParent(EnemySpawner.transform);

                print($"{EnemySpawnerPreferences.name}");
        }
    }
}
