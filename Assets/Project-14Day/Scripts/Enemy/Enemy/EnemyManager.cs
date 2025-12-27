using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy settings")]
    [SerializeField] private EnemyPreferences _enemyPreferences;

    public Transform EnemyRightHand;
    public Transform EnemyLeftHand;

    private float _enemySpeed;
    private float _enemyAttackCooldown;
    private float _enemyHPSliderDistanceFromHead;
   // private float _enemyStopDistanceForPlayer;

    private int _enemyHealth;
    private int _enemyDamage;

    private EnemyMoving _enemyMoving;

    private Slider _enemyHealthSlider;

    private Transform _worldSpaceCanvas;

    private Transform _parentTransform;

    private void Start()
    {
        InitializedEnemy();
    }

    public void InitializedEnemy()
    {
        _enemyMoving = GetComponent<EnemyMoving>();
        _enemyHealthSlider = GetComponentInChildren<Slider>();
        _worldSpaceCanvas = GameObject.Find("WorldSpaceCanvas").transform;
        _parentTransform = GetComponentInParent<Transform>();

        EnemySetSettings();
        SliderPreset();

        _enemyHealthSlider.gameObject.SetActive(false);
    }

    private void SliderPreset()
    {
        _enemyHealthSlider.maxValue = _enemyPreferences.Health;
        _enemyHealthSlider.value = _enemyHealth;

        _enemyHealthSlider.transform.SetParent(_worldSpaceCanvas);
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
        _enemyMoving.MoveToTarget(PlayerControllerManager.Instance.transform, _enemyPreferences,_enemySpeed);
        _enemyHealthSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, _enemyHPSliderDistanceFromHead, 0));
    }

    private void EnemySetSettings()
    {
        _enemySpeed = _enemyPreferences.Speed;
        _enemyAttackCooldown = _enemyPreferences.AttackCooldown;
        _enemyHPSliderDistanceFromHead = _enemyPreferences.EnemyHPSliderDistanceFromHead;
      //  _enemyStopDistanceForPlayer = _enemyPreferences.EnemyStopDistanceForPlayer;

        _enemyHealth = _enemyPreferences.Health;
        _enemyDamage = _enemyPreferences.Damage;

    }

    private void EnemyDead()
    {
        if (_enemyHealth <= 0)
        {
            if (_enemyPreferences.EnemyDrop.Length > 0)
            {
                Instantiate(_enemyPreferences.EnemyDrop[Random.Range(0, _enemyPreferences.EnemyDrop.Length)], transform.position, Quaternion.identity);
            }

            EnemyCounterDead.Instance.AddCountedDead();

            Destroy(_enemyHealthSlider.gameObject);
            Destroy(_parentTransform.gameObject);
            
            Debug.Log($"<color=blue> {gameObject.name} - Enemy is dead </color>");
        }
    }

    public void TakeDamage(int damage)
    {
        _enemyHealthSlider.gameObject.SetActive(true);

        _enemyHealth -= damage;
        _enemyHealthSlider.value = _enemyHealth;

        Debug.Log($"<color=blue> {gameObject.name} </color> - <color=red> Enemy health </color> -  <color=green>{_enemyHealth}</color>");
    }

    public void AttackPlayer()
    {
        if (_enemyMoving.IsMoving) return;

        if (_enemyAttackCooldown <= 0)
        {
            PlayerControllerManager.Instance.TakeDamage(_enemyDamage);
            Debug.Log($"<color=red> {gameObject.name} - Enemy attacked the player </color>");

            _enemyAttackCooldown = _enemyPreferences.AttackCooldown;
        }
    }
}
