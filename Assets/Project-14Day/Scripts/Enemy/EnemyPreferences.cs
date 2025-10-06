using UnityEngine;

[CreateAssetMenu(menuName = "Create new enemy/Enemy", fileName = "New enemy", order = 1)]
public class EnemyPreferences : ScriptableObject
{
    public int Health;
    public int Damage;

    public float Speed;
    public float AttackCooldown;
    public float EnemyHPSliderDistanceFromHead;
    public float EnemyStopDistanceForPlayer;

    public GameObject[] EnemyDrop;
}
