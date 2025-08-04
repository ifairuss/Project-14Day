using UnityEngine;

[CreateAssetMenu(fileName = "New spawner", menuName = "Create new spawner/New spawner", order = 3)]
public class EnemySpawnerPreferences : ScriptableObject
{
    public GameObject[] EnemyPrefabs;
    public GameObject[] BossPrefabs;

    public Transform SpawnerPosition;
}
