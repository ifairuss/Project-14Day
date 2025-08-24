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

    private void Update()
    {
        EnemyCountUpdate();
    }

    public void EnemyCountUpdate()
    {
        _enemyCount = transform.childCount;
    }
}
