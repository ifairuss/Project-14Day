using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("⚙️ Bullet preferences")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private LayerMask _bulletLayer;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * _bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_bulletLayer == (_bulletLayer | (1 << collision.gameObject.layer)))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
