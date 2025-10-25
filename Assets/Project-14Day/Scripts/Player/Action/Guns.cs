using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Guns : MonoBehaviour
{
    public static Guns Instance;

    [Header("⚙️ Guns preferences")]
    [SerializeField] private Transform _point;
    [SerializeField] private BoxCollider2D _boxColliderGunCollision;
    [SerializeField] private SkinManager _skinData;
    [Header("Hand properties")]
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;

    private int _allAmmo = 0;
    private int _ammoInTheClip = 0;
    private int _maxAmmo = 0;
    private int _maxAmmoInTheClip = 0;

    private float _timeToShoot;
    private float _timeToBtwShoot;
    private float _timeToReloadGun;

    private GameObject _bullet;
    private TextMeshProUGUI _ammoCounterText;

    private void Awake()
    {
        Instance = this;
    }


    public void Initialized()
    {
        _ammoCounterText = GameObject.FindWithTag("AmmoCounter").GetComponent<TextMeshProUGUI>();

        StartWeapon();
        _timeToShoot = _timeToBtwShoot;
        _timeToReloadGun = _skinData.WeaponId[_skinData.SkinData].TimeToReload;
    }

    public void StartWeapon()
    {
        HandPosition();

        _ammoInTheClip = _skinData.WeaponId[_skinData.SkinData].AmmoInTheClip;
        _allAmmo = _skinData.WeaponId[_skinData.SkinData].AllAmmo;
        _maxAmmo = _skinData.WeaponId[_skinData.SkinData].MaxAmmo;
        _maxAmmoInTheClip = _skinData.WeaponId[_skinData.SkinData].MaxAmmoInTheClip;
        _timeToBtwShoot = _skinData.WeaponId[_skinData.SkinData].TimeToShoot;
        _bullet = _skinData.WeaponId[_skinData.SkinData].Bullet;
        _timeToReloadGun = _skinData.WeaponId[_skinData.SkinData].TimeToReload;

        _point.localPosition = _skinData.WeaponId[_skinData.SkinData].BulletSpawnPointPosition;
        _boxColliderGunCollision.offset = _skinData.WeaponId[_skinData.SkinData].GunCollisionOffset;
        _boxColliderGunCollision.size = _skinData.WeaponId[_skinData.SkinData].GunCollisionSize;

        _ammoCounterText.text = $"{_ammoInTheClip}/{_allAmmo}";
    }

    private void HandPosition()
    {
        _leftHand.position = new Vector3(
            _skinData.WeaponId[_skinData.SkinData].LeftHandPosition.x,
            _skinData.WeaponId[_skinData.SkinData].LeftHandPosition.y);
        _rightHand.position = new Vector3(
            _skinData.WeaponId[_skinData.SkinData].RightHandPosition.x,
            _skinData.WeaponId[_skinData.SkinData].RightHandPosition.y);

        _rightHand.rotation = Quaternion.Euler(
            _skinData.WeaponId[_skinData.SkinData].RightHandRotation.x,
            _skinData.WeaponId[_skinData.SkinData].RightHandRotation.y,
            _skinData.WeaponId[_skinData.SkinData].RightHandRotation.z);
        _leftHand.rotation = Quaternion.Euler(
            _skinData.WeaponId[_skinData.SkinData].LeftHandRotation.x,
            _skinData.WeaponId[_skinData.SkinData].LeftHandRotation.y,
            _skinData.WeaponId[_skinData.SkinData].LeftHandRotation.z);
    }

    public void Shoot(Button reloadGun)
    {
        if (_ammoInTheClip > 0)
        {
            if (_timeToShoot <= 0)
            {
                _ammoInTheClip -= 1;
                _ammoCounterText.text = $"{_ammoInTheClip}/{_allAmmo}";
                _timeToShoot = _timeToBtwShoot;
                Instantiate(_bullet, _point.position, transform.rotation);

                if (_ammoInTheClip <= 0)
                {
                    StartCoroutine(Reload(reloadGun));
                }
            }
            else
            {
                _timeToShoot -= Time.deltaTime;
            }
        }
    }

    public IEnumerator Reload(Button reloadGun)
    {
        if (_allAmmo > 0 && _ammoInTheClip < _skinData.WeaponId[_skinData.SkinData].MaxAmmoInTheClip)
        {
            reloadGun.gameObject.SetActive(false);

            yield return new WaitForSeconds(_timeToReloadGun);

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

            yield return new WaitForSeconds(0.1f);
            reloadGun.gameObject.SetActive(true);

            StopCoroutine(Reload(reloadGun));
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
}
