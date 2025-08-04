using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    public EnemySpawnerPreferences EnemySpawnerPreferences;
    public Vector2 AltarPosition;

    public void EnemySpawner()
    {
        int enemyVar = Random.Range(0, EnemySpawnerPreferences.EnemyPrefabs.Length);

        Instantiate(EnemySpawnerPreferences.EnemyPrefabs[enemyVar], AltarPosition, Quaternion.identity);

        print($"{EnemySpawnerPreferences.name}");
    }
}
