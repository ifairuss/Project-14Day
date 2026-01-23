using UnityEngine;

public class TestAddAmmo : MonoBehaviour
{
    private float _timer;
    private float _timeToAddAmmo = 3f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponentInChildren<Guns>();

            if (_timer <= 0)
            {
                player.AddAmmo(5);
                _timer = _timeToAddAmmo;
            }
            else
            {
                _timer -= 0.1f;
            }
        }
    }
}
