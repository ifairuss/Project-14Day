using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator _playerAnimator;

    private void Awake() // тестове метод буде  Initialized для  EntryPoint
    {
        _playerAnimator = GetComponent<Animator>();
    }

    public void PlayerMoveAnimation(float directionX, float directionY)
    {
        if (directionX != 0 || directionY != 0)
        {
            _playerAnimator.SetBool("isWalk", true);
        }
        else
        {
            _playerAnimator.SetBool("isWalk", false);
        }
    }
}
