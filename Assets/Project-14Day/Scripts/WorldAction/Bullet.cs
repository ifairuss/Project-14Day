using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("⚙️ Bullet preferences")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private LayerMask _bulletLayer;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * _bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_bulletLayer == (_bulletLayer | (1 << collision.gameObject.layer)))
        {

            if (collision.gameObject.layer == LayerMask.NameToLayer("BulletBossInteraction"))
            {
                TakeDamageBoss(collision);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("BulletInteraction"))
            {
                TakeDamageEnemy(collision);
            }

            print($"Bullet hit: {collision.gameObject.name}");
            Destroy(gameObject);
        }
    }

    private void TakeDamageEnemy(Collider2D collision)
    {
        EnemyManager enemyManager = collision.gameObject.GetComponent<EnemyManager>();

        enemyManager?.TakeDamage(_bulletDamage);
    }

    private void TakeDamageBoss(Collider2D collision)
    {
        BossAbstract bossManager = collision.gameObject.GetComponent<BossAbstract>();

        bossManager?.TakeDamage(_bulletDamage);
    }
}
