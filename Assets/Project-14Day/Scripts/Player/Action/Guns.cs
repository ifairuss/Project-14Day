using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Guns : MonoBehaviour
{
    public int AllAmmo = 0;

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
        AllAmmo = _allGuns[_weaponId].AllAmmo;
        _maxAmmo = _allGuns[_weaponId].MaxAmmo;
        _maxAmmoInTheClip = _allGuns[_weaponId].MaxAmmoInTheClip;
        _timeToBtwShoot = _allGuns[_weaponId].TimeToShoot;
        _bullet = _allGuns[_weaponId].Bullet;
        _weaponSprite.sprite = _allGuns[_weaponId].WeaponSprite;

        _point.localPosition = _allGuns[_weaponId].BulletSpawnPointPosition;

        _ammoCounterText.text = $"{_ammoInTheClip}/{AllAmmo}";
    }

    public void Shoot()
    {
        if (_ammoInTheClip > 0)
        {
            if (_timeToShoot <= 0)
            {
                _ammoInTheClip -= 1;
                _ammoCounterText.text = $"{_ammoInTheClip}/{AllAmmo}";
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
        if (AllAmmo > 0)
        {
            int needAddAmmo = _maxAmmoInTheClip - _ammoInTheClip;

            if (AllAmmo >= needAddAmmo){ 
                _ammoInTheClip += needAddAmmo;
                AllAmmo -= needAddAmmo;
            }
            else if (AllAmmo < needAddAmmo)
            {
                _ammoInTheClip += AllAmmo;
                AllAmmo -= AllAmmo;
            }

            _ammoCounterText.text = $"{_ammoInTheClip}/{AllAmmo}";
        }
    }

    public void GunSwitch()
    {
        if (_weaponId < _allGuns.Count - 1)
        {
            _weaponId += 1;

            _ammoInTheClip = _allGuns[_weaponId].AmmoInTheClip;
            AllAmmo = _allGuns[_weaponId].AllAmmo;
            _maxAmmo = _allGuns[_weaponId].MaxAmmo;
            _maxAmmoInTheClip = _allGuns[_weaponId].MaxAmmoInTheClip;
            _timeToBtwShoot = _allGuns[_weaponId].TimeToShoot;
            _bullet = _allGuns[_weaponId].Bullet;
            _weaponSprite.sprite = _allGuns[_weaponId].WeaponSprite;

            _point.localPosition = _allGuns[_weaponId].BulletSpawnPointPosition;

            _ammoCounterText.text = $"{_ammoInTheClip}/{AllAmmo}";
        }
        else
        {
            _weaponId = 0;

            _ammoInTheClip = _allGuns[_weaponId].AmmoInTheClip;
            AllAmmo = _allGuns[_weaponId].AllAmmo;
            _maxAmmo = _allGuns[_weaponId].MaxAmmo;
            _maxAmmoInTheClip = _allGuns[_weaponId].MaxAmmoInTheClip;
            _timeToBtwShoot = _allGuns[_weaponId].TimeToShoot;
            _bullet = _allGuns[_weaponId].Bullet;
            _weaponSprite.sprite = _allGuns[_weaponId].WeaponSprite;

            _point.localPosition = _allGuns[_weaponId].BulletSpawnPointPosition;

            _ammoCounterText.text = $"{_ammoInTheClip}/{AllAmmo}";
        }
    }
}
