using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner preferences")]
    [SerializeField] private float _spawnerReloadTime;
    [Space]
    public EnemySpawnerPreferences EnemyThisSpawnerPreferences;
    public List<Transform> EnemySpawnPoints;

    [Space]
    [Header("Spawner data")]
    [SerializeField] private int _waves;
    [SerializeField] private int _enemyCount;

    private int _bossWave;

    private void Start() //тимчасово
    {
        _bossWave = EnemyThisSpawnerPreferences.BossSpawnWave;
    }

    private void Update()
    {
        EnemyCountUpdate();
    }

    public void EnemyCountUpdate()
    {
        _enemyCount = transform.childCount;
    }

    public void StartWave(ButtonSpawner buttonSpawner)
    {
        if (_enemyCount <= 0)
        {
            if (_waves == _bossWave)
            {
                buttonSpawner.gameObject.SetActive(false);
                SpawnBossWave();
            }
            else if (_waves != _bossWave)
            {
                buttonSpawner.gameObject.SetActive(false);
                SpawnNormalWave();
            }
        }
    }

    public void ButtonSpawner(ButtonSpawner button)
    {
        if (_enemyCount <= 0)
        {
            button.gameObject.SetActive(true);
        }
        else
        {
            button.gameObject.SetActive(false);
        }
    }

    private void SpawnNormalWave()
    {
        int randomEnemyCount = Random.Range(EnemyThisSpawnerPreferences.MinEnemySpawn, EnemyThisSpawnerPreferences.MaxEnemySpawn);

        for (int i = 0; i < randomEnemyCount; i++)
        {
            int spawnPointIndex = Random.Range(0, EnemySpawnPoints.Count);
            int randomEnemy = Random.Range(0, EnemyThisSpawnerPreferences.EnemyPrefabs.Length);

            GameObject enemy = Instantiate(EnemyThisSpawnerPreferences.EnemyPrefabs[randomEnemy], EnemySpawnPoints[spawnPointIndex].position, Quaternion.identity);

            enemy.transform.SetParent(transform);

            print($"{EnemyThisSpawnerPreferences.name}");
        }

        _waves += 1;
    }

    private void SpawnBossWave()
    {
        print($"Boss Spawned");
        _bossWave = _bossWave += EnemyThisSpawnerPreferences.BossSpawnWave;
        _waves += 1;
    }
}
