using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Add new item/New Gun", order = 0)]
public class GunsPreferences : ScriptableObject
{
    public int WeaponId; 

    public int AmmoInTheClip;
    public int AllAmmo;
    public int MaxAmmo;
    public int MaxAmmoInTheClip;

    public float GunDamage;
    public float TimeToShoot;
    public float TimeToReload;

    public GameObject Bullet;
    public Sprite GunSprite;

    public Vector3 BulletSpawnPointPosition;

    [Header("Gun Settings")]
    public Vector2 GunCollisionOffset;
    public Vector2 GunCollisionSize;

    [Space]
    [Header("Hand Settings")]
    public Vector2 LeftHandPosition;
    public Vector2 RightHandPosition;
    public Vector3 LeftHandRotation;
    public Vector3 RightHandRotation;
}
