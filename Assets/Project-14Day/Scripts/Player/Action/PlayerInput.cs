using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Joystick _playerMovementJoystick;
    [SerializeField] private Joystick _playerAttackJoystick;

    private bool isFacingRight = true;

    public Vector2 PlayerMovementInput(Transform player)
    {
        float directionX = _playerMovementJoystick.Horizontal;
        float directionY = _playerMovementJoystick.Vertical;

        if (isFacingRight && directionX < 0 || !isFacingRight && directionX > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = player.localScale;
            localScale.x *= -1;
            player.localScale = localScale;
        }

        Vector2 moveDirection = new Vector2(directionX, directionY);

        return moveDirection;
    } 

}
