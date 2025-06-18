using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Joystick _playerMovementJoystick;
    [SerializeField] private Joystick _playerAttackJoystick;
    [Space]
    [SerializeField] private Button _buttonShoot;

    public Vector2 PlayerMovementInput(Transform Weapon, Transform player)
    {
        float directionX = _playerMovementJoystick.Horizontal;
        float directionY = _playerMovementJoystick.Vertical;

        Flip(player, Weapon);

        Vector2 moveDirection = new Vector2(directionX, directionY);

        return moveDirection;
    } 

    public void WeaponRotate(Transform Weapon)
    {
        if (_playerAttackJoystick.Horizontal > 0 || _playerAttackJoystick.Horizontal < 0 || _playerAttackJoystick.Vertical > 0 || _playerAttackJoystick.Vertical < 0)
        {
            if (_playerAttackJoystick.Horizontal > 0)
            {
                float weaponRotate = Mathf.Atan2(_playerAttackJoystick.Vertical, _playerAttackJoystick.Horizontal) * Mathf.Rad2Deg;
                Weapon.rotation = Quaternion.Euler(0, 0, weaponRotate);
            }
            else if (_playerAttackJoystick.Horizontal < 0)
            {
                float weaponRotate = Mathf.Atan2(_playerAttackJoystick.Vertical, -_playerAttackJoystick.Horizontal) * Mathf.Rad2Deg;
                Weapon.rotation = Quaternion.Euler(0, 180, weaponRotate);
            }
        }
    }

    public void WeponShoots(Guns gun)
    {
        if (_playerAttackJoystick.Horizontal > 0.5f || _playerAttackJoystick.Vertical > 0.5f || _playerAttackJoystick.Horizontal < -0.5f || _playerAttackJoystick.Vertical < -0.5f)
        {
            gun.Shoot();
        }
    }

    private void Flip(Transform player, Transform Weapon)
    {
        if (_playerAttackJoystick.Horizontal > 0)
        {
            player.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_playerAttackJoystick.Horizontal < 0)
        {
            player.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            if (_playerMovementJoystick.Horizontal > 0)
            {
                player.rotation = Quaternion.Euler(0, 0, 0);
                Weapon.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (_playerMovementJoystick.Horizontal < 0)
            {
                player.rotation = Quaternion.Euler(0, 180, 0);
                Weapon.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    public void AllButtonInit(Guns gun)
    {
        _buttonShoot.onClick.AddListener(() => {
            gun.GunSwitch();
        });
    }

}
