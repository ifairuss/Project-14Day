using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy settings")]
    [SerializeField] private EnemyPreferences _enemyPreferences;

    private float _enemySpeed;
    private float _enemyAttackRange;
    private float _enemyAttackCooldown;
    private float _enemyDetectionRange;
    private float _enemyHPSliderDistanceFromHead;
    private float _enemyStopDistanceForPlayer;

    private int _enemyHealth;
    private int _enemyDamage;

    private EnemyMoving _enemyMoving;

    private Slider _enemyHealthSlider;

    private Transform _healthBarCanvas;

    private CircleCollider2D _enemyDetectionRangeCollider;

    private void Start()
    {
        InitializedEnemy();
    }

    public void InitializedEnemy()
    {
        _enemyMoving = GetComponent<EnemyMoving>();
        _enemyHealthSlider = GetComponentInChildren<Slider>();
        _healthBarCanvas = GameObject.Find("HealthBarCanvas").transform;
        _enemyDetectionRangeCollider = GetComponentInChildren<CircleCollider2D>();

        EnemySetSettings();
        SliderPreset();

        _enemyHealthSlider.gameObject.SetActive(false);
    }

    private void SliderPreset()
    {
        _enemyHealthSlider.maxValue = _enemyPreferences.Health;
        _enemyHealthSlider.value = _enemyHealth;

        _enemyHealthSlider.transform.SetParent(_healthBarCanvas);
    }

    private void Update()
    {
        if (_enemyAttackCooldown > 0)
        {
            _enemyAttackCooldown -= Time.deltaTime;
        }

        AttackPlayer();

        EnemyControllers();

        EnemyDead();
    }

    private void EnemyControllers()
    {
        _enemyMoving.MoveToTarget(PlayerControllerManager.Instance.transform, _enemySpeed, _enemyStopDistanceForPlayer);
        _enemyHealthSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, _enemyHPSliderDistanceFromHead, 0));
    }

    private void EnemySetSettings()
    {
        _enemySpeed = _enemyPreferences.Speed;
        _enemyAttackCooldown = _enemyPreferences.AttackCooldown;
        _enemyDetectionRange = _enemyPreferences.DetectionRange;
        _enemyHPSliderDistanceFromHead = _enemyPreferences.EnemyHPSliderDistanceFromHead;
        _enemyStopDistanceForPlayer = _enemyPreferences.EnemyStopDistanceForPlayer;

        _enemyHealth = _enemyPreferences.Health;
        _enemyDamage = _enemyPreferences.Damage;

        _enemyDetectionRangeCollider.radius = _enemyAttackRange;

    }

    private void EnemyDead()
    {
        if (_enemyHealth <= 0)
        {
            if (_enemyPreferences.EnemyDrop.Length > 0)
            {
                Instantiate(_enemyPreferences.EnemyDrop[Random.Range(0, _enemyPreferences.EnemyDrop.Length)], transform.position, Quaternion.identity);
            }

            Destroy(_enemyHealthSlider.gameObject);
            Destroy(gameObject);
            Debug.Log($"{gameObject.name} - Enemy is dead");
        }
    }

    public void TakeDamage(int damage)
    {
        _enemyHealthSlider.gameObject.SetActive(true);

        _enemyHealth -= damage;
        _enemyHealthSlider.value = _enemyHealth;

        Debug.Log($"{gameObject.name} - Enemy health - {_enemyHealth}");
    }

    public void AttackPlayer()
    {
        if (_enemyMoving.IsMoving) return;

        if (_enemyAttackCooldown <= 0)
        {
            PlayerControllerManager.Instance.TakeDamage(_enemyDamage);
            Debug.Log($"{gameObject.name} - Enemy attacked the player");

            _enemyAttackCooldown = _enemyPreferences.AttackCooldown;
        }
    }   
}
