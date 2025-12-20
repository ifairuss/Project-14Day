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

    public Sprite BodyTopSprite;
    public Sprite BodyRightSprite;
    public Sprite BodyBottomSprite;
    public Sprite HeadTopSprite;
    public Sprite HeadRightSprite;
    public Sprite HeadBottomSprite;
    public Sprite LeftHandSprite;
    public Sprite RightHandSprite;
    public Sprite LeftLegSprite;
    public Sprite RightLegSprite;

    public GameObject[] EnemyDrop;
}
