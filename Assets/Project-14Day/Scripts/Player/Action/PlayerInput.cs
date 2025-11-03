using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Joystick _playerMovementJoystick;
    [SerializeField] private Joystick _playerAttackJoystick;
    [Space]
    [SerializeField] private Button _buttonReload;

    [Header("Flip sprite")]
    [SerializeField] private SkinManager _skinManager;
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private SpriteRenderer _head;
    [SerializeField] private SpriteRenderer _gun;
    [SerializeField] private SpriteRenderer _leftHand;
    [SerializeField] private SpriteRenderer _rightHand;

    public Vector2 PlayerMovementInput(Transform Weapon, Transform player, AnimationManager playerAnimation)
    {
        float directionX = _playerMovementJoystick.Horizontal;
        float directionY = _playerMovementJoystick.Vertical;

        Flip(player, Weapon);

        Vector2 moveDirection = new Vector2(directionX, directionY);

        playerAnimation.PlayerMoveAnimation(_playerMovementJoystick.Horizontal, _playerMovementJoystick.Vertical);

        return moveDirection;
    } 

    public void WeaponRotate(Transform Weapon)
    {
       // print($"{_playerAttackJoystick.Horizontal} - Joystick debug");

        if (_playerAttackJoystick.Horizontal > 0 || _playerAttackJoystick.Horizontal < 0 || _playerAttackJoystick.Vertical > 0 || _playerAttackJoystick.Vertical < 0)
        {
            if (_playerAttackJoystick.Horizontal > 0)
            {
                float weaponRotate = Mathf.Atan2(_playerAttackJoystick.Vertical, _playerAttackJoystick.Horizontal) * Mathf.Rad2Deg;
                Weapon.rotation = Quaternion.Euler(0, 0, weaponRotate);

                PlayerRotate(weaponRotate);
            }
            else if (_playerAttackJoystick.Horizontal < 0)
            {
                float weaponRotate = Mathf.Atan2(_playerAttackJoystick.Vertical, -_playerAttackJoystick.Horizontal) * Mathf.Rad2Deg;
                Weapon.rotation = Quaternion.Euler(0, 180, weaponRotate);

                PlayerRotate(weaponRotate);
            }
        }
    }

    private void PlayerRotate(float weaponRotate)
    {
        if (weaponRotate > 45 && weaponRotate < 90)
        {
            _body.sprite = _skinManager.SkinId[_skinManager.SkinData].BodyTopSprite;
            _head.sprite = _skinManager.SkinId[_skinManager.SkinData].HeadTopSprite;
            SwitchSortingOrderInGun(false);
        }
        else if (weaponRotate < -45 && weaponRotate > -90)
        {
            _body.sprite = _skinManager.SkinId[_skinManager.SkinData].BodyBottomSprite;
            _head.sprite = _skinManager.SkinId[_skinManager.SkinData].HeadBottomSprite;
            SwitchSortingOrderInGun(true);
        }
        else if (weaponRotate < 45 && weaponRotate > -45)
        {
            _body.sprite = _skinManager.SkinId[_skinManager.SkinData].BodyRightSprite;
            _head.sprite = _skinManager.SkinId[_skinManager.SkinData].HeadRightSprite;
            SwitchSortingOrderInGun(true);
        }
    }

    public void WeponShoots(Guns gun)
    {
        if (_playerAttackJoystick.Horizontal > 0.5f || _playerAttackJoystick.Vertical > 0.5f || _playerAttackJoystick.Horizontal < -0.5f || _playerAttackJoystick.Vertical < -0.5f)
        {
            gun.Shoot(_buttonReload);
        }
    }

    private void Flip(Transform player, Transform Weapon)
    {
        if (_playerAttackJoystick.Horizontal > 0)
        {
            player.rotation = Quaternion.Euler(0, 0, 0);
            return;
        }
        else if (_playerAttackJoystick.Horizontal < 0)
        {
            player.rotation = Quaternion.Euler(0, 180, 0);
            return;
        }


        if (_playerMovementJoystick.Vertical > 0)
        {
            _body.sprite = _skinManager.SkinId[_skinManager.SkinData].BodyTopSprite;
            _head.sprite = _skinManager.SkinId[_skinManager.SkinData].HeadTopSprite;
            SwitchSortingOrderInGun(false);
        }
        else if (_playerMovementJoystick.Vertical < 0)
        {
            _body.sprite = _skinManager.SkinId[_skinManager.SkinData].BodyBottomSprite;
            _head.sprite = _skinManager.SkinId[_skinManager.SkinData].HeadBottomSprite;
            SwitchSortingOrderInGun(true);
        } 


        if (_playerMovementJoystick.Horizontal > 0 && _playerMovementJoystick.Vertical < 0.75 && _playerMovementJoystick.Vertical > -0.75)
        {
            _body.sprite = _skinManager.SkinId[_skinManager.SkinData].BodyRightSprite;
            _head.sprite = _skinManager.SkinId[_skinManager.SkinData].HeadRightSprite;
            player.rotation = Quaternion.Euler(0, 0, 0);
            SwitchSortingOrderInGun(true);
        }
        else if (_playerMovementJoystick.Horizontal < 0 && _playerMovementJoystick.Vertical > -0.75 && _playerMovementJoystick.Vertical < 0.75)
        {
            _body.sprite = _skinManager.SkinId[_skinManager.SkinData].BodyRightSprite;
            _head.sprite = _skinManager.SkinId[_skinManager.SkinData].HeadRightSprite;
            player.rotation = Quaternion.Euler(0, 180, 0);
            SwitchSortingOrderInGun(true);
        }
    }

    private void SwitchSortingOrderInGun(bool Variables)
    {
        if (Variables == true)
        {
            _gun.sortingOrder = 9;
            _leftHand.sortingOrder = 10;
            _rightHand.sortingOrder = 10;
        }
        else
        {
            _gun.sortingOrder = -10;
            _leftHand.sortingOrder = -9;
            _rightHand.sortingOrder = -9;
        }
    }

    public void AllButtonInit(Guns gun)
    {
        _buttonReload.onClick.AddListener(() => {
            StartCoroutine(gun.Reload(_buttonReload));
        });
    }
}
