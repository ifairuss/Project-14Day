using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Guns : MonoBehaviour
{
    private int _allAmmo = 0;
    private int _ammoInTheClip = 0;
    private int _maxAmmo = 0;
    private int _maxAmmoInTheClip = 0;

    private float _timeToShoot;

    private GameObject _bullet;

    private TextMeshProUGUI _ammoCounterText;


    [Header("⚙️ Guns preferences")]
    [SerializeField] private List<GunsPreferences> _allGuns;
    [SerializeField] private Transform _point;
    [SerializeField] private SpriteRenderer _weaponSprite;
    [Space]
    [SerializeField] private int _weaponId;
    [SerializeField] private float _timeToBtwShoot;



    public void Initialized()
    {
        _ammoCounterText = GameObject.FindWithTag("AmmoCounter").GetComponent<TextMeshProUGUI>();

        StartWeapon();
        _timeToShoot = _timeToBtwShoot;
    }

    private void StartWeapon()
    {
        _ammoInTheClip = _allGuns[_weaponId].AmmoInTheClip;
        _allAmmo = _allGuns[_weaponId].AllAmmo;
        _maxAmmo = _allGuns[_weaponId].MaxAmmo;
        _maxAmmoInTheClip = _allGuns[_weaponId].MaxAmmoInTheClip;
        _timeToBtwShoot = _allGuns[_weaponId].TimeToShoot;
        _bullet = _allGuns[_weaponId].Bullet;
        _weaponSprite.sprite = _allGuns[_weaponId].WeaponSprite;

        _point.localPosition = _allGuns[_weaponId].BulletSpawnPointPosition;

        _ammoCounterText.text = $"{_ammoInTheClip}/{_allAmmo}";
    }

    public void Shoot()
    {
        if (_ammoInTheClip > 0)
        {
            if (_timeToShoot <= 0)
            {
                _ammoInTheClip -= 1;
                _ammoCounterText.text = $"{_ammoInTheClip}/{_allAmmo}";
                _timeToShoot = _timeToBtwShoot;
                Instantiate(_bullet, _point.position, transform.rotation);
            }
            else
            {
                _timeToShoot -= Time.deltaTime;
            }
        }
    }

    public void Reload()
    {
        if (_allAmmo > 0)
        {
            int needAddAmmo = _maxAmmoInTheClip - _ammoInTheClip;

            if (_allAmmo >= needAddAmmo){ 
                _ammoInTheClip += needAddAmmo;
                _allAmmo -= needAddAmmo;
            }
            else if (_allAmmo < needAddAmmo)
            {
                _ammoInTheClip += _allAmmo;
                _allAmmo -= _allAmmo;
            }

            _ammoCounterText.text = $"{_ammoInTheClip}/{_allAmmo}";
        }
    }

    public void AddAmmo(int ammo)
    {
        if((ammo + _allAmmo) < _maxAmmo)
        {
            _allAmmo += ammo;
        }
        else
        {
            int needAmmo = _maxAmmo - _allAmmo;
            _allAmmo += needAmmo;
        }

        _ammoCounterText.text = $"{_ammoInTheClip}/{_allAmmo}";
    }

    public void GunSwitch()
    {
        if (_weaponId < _allGuns.Count - 1)
        {
            _weaponId += 1;

            _ammoInTheClip = _allGuns[_weaponId].AmmoInTheClip;
            _allAmmo = _allGuns[_weaponId].AllAmmo;
            _maxAmmo = _allGuns[_weaponId].MaxAmmo;
            _maxAmmoInTheClip = _allGuns[_weaponId].MaxAmmoInTheClip;
            _timeToBtwShoot = _allGuns[_weaponId].TimeToShoot;
            _bullet = _allGuns[_weaponId].Bullet;
            _weaponSprite.sprite = _allGuns[_weaponId].WeaponSprite;

            _point.localPosition = _allGuns[_weaponId].BulletSpawnPointPosition;

            _ammoCounterText.text = $"{_ammoInTheClip}/{_allAmmo}";
        }
        else
        {
            _weaponId = 0;

            _ammoInTheClip = _allGuns[_weaponId].AmmoInTheClip;
            _allAmmo = _allGuns[_weaponId].AllAmmo;
            _maxAmmo = _allGuns[_weaponId].MaxAmmo;
            _maxAmmoInTheClip = _allGuns[_weaponId].MaxAmmoInTheClip;
            _timeToBtwShoot = _allGuns[_weaponId].TimeToShoot;
            _bullet = _allGuns[_weaponId].Bullet;
            _weaponSprite.sprite = _allGuns[_weaponId].WeaponSprite;

            _point.localPosition = _allGuns[_weaponId].BulletSpawnPointPosition;

            _ammoCounterText.text = $"{_ammoInTheClip}/{_allAmmo}";
        }
    }
}
