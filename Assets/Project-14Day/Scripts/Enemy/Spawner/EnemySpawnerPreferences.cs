using UnityEngine;

[CreateAssetMenu(fileName = "New spawner", menuName = "Create new spawner/New spawner", order = 3)]
public class EnemySpawnerPreferences : ScriptableObject
{
    public GameObject[] EnemyPrefabs;
    public GameObject[] BossPrefabs;

    public Transform SpawnerPosition;

    [Header("Spawner preferences")]
    public float SpawnerReloadTime;
    public float TimeToEnemySpawned;
    public int BossSpawnWave;

    [Header("Spawner complication")]
    public int MinEnemySpawn;
    public int MaxEnemySpawn;
    [Space]
    public float MinTimeToEnemySpawned;

}
