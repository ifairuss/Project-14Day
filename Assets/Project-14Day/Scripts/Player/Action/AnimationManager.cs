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
        _playerAnimator.SetFloat("directionY", directionY);
        _playerAnimator.SetFloat("directionX", directionX);
    }
}
