using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    public EnemySpawnerPreferences EnemySpawnerPreferences;
    public EnemySpawner EnemySpawner;
    public Vector2 AltarPosition;

    public void StartWave()
    {
        EnemySpawner.StartWave(this);
    }
}
