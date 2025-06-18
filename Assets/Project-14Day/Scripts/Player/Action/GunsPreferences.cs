using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Gun", menuName = "Add Gun/New Gun", order = 0)]
public class GunsPreferences : ScriptableObject
{
    public int WeaponId; 

    public int AmmoInTheClip;
    public int AllAmmo;
    public int MaxAmmo;

    public float GunDamage;
    public float TimeToShoot;

    public Sprite WeaponSprite;
    public GameObject Bullet;

    public Vector3 BulletSpawnPointPosition;
}
