using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guns : MonoBehaviour
{
    private int _ammoInTheClip;
    private int _allAmmo;
    private int _maxAmmo;

    private float _timeToShoot;

    private GameObject _bullet;


    [Header("⚙️ Guns preferences")]
    [SerializeField] private List<GunsPreferences> _allGuns;
    [SerializeField] private Transform _point;
    [SerializeField] private SpriteRenderer _weaponSprite;
    [Space]
    [SerializeField] private int _weaponId;
    [SerializeField] private float _timeToBtwShoot;



    public void Initialized()
    {
        GunSwitch();

        _timeToShoot = _timeToBtwShoot;
    }

    public void Shoot()
    {
        if (_ammoInTheClip > 0)
        {
            if (_timeToShoot <= 0)
            {
                _ammoInTheClip -= 1;
                _timeToShoot = _timeToBtwShoot;
                Instantiate(_bullet, _point.position, transform.rotation);
            }
            else
            {
                _timeToShoot -= Time.deltaTime;
            }
        }
    }

    public void GunSwitch()
    {
        _ammoInTheClip = _allGuns[_weaponId].AmmoInTheClip;
        _allAmmo = _allGuns[_weaponId].AllAmmo;
        _maxAmmo = _allGuns[_weaponId].MaxAmmo;
        _timeToBtwShoot = _allGuns[_weaponId].TimeToShoot;
        _bullet = _allGuns[_weaponId].Bullet;
        _weaponSprite.sprite = _allGuns[_weaponId].WeaponSprite;

        _point.localPosition = _allGuns[_weaponId].BulletSpawnPointPosition;
    }
}
