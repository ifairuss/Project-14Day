using UnityEngine;

public class PlayerControllerManager : MonoBehaviour
{
    public static PlayerControllerManager Instance { get; private set; }

    [Header("⚙️ Player preferences")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _cameraFollowSpeed;

    [Header("⚙️ Player ststs")]
    [SerializeField] private int _playerMaxHealth;

    [Header("⚙️ Player Components")]
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private ButtonSpawner _buttonSpawner;

    private int _playerHealth;

    private Rigidbody2D _playerRigidbody;

    private PlayerInput _playerInput;
    private CameraFollow _playerCamera;
    private Guns _playerGunsManager;
    private AnimationManager _playerAnimationManager;
    private HP_System _playerHealthBar;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialized()
    {
        _playerInput = GameObject.FindWithTag("PlayerInterface").GetComponent<PlayerInput>();
        _playerHealthBar = GameObject.FindWithTag("PlayerInterface").GetComponentInChildren<HP_System>();
        _playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
        _playerGunsManager = GameObject.FindWithTag("WeponsManager").GetComponent<Guns>();
        _playerAnimationManager = GetComponent<AnimationManager>();

        _playerRigidbody = GetComponent<Rigidbody2D>();

        AllButtons();

        _playerHealth = _playerMaxHealth;
    }

    private void FixedUpdate()
    {
        AllController();

        GameOver();

        StartCoroutine(_playerHealthBar.HealthSystems(_playerHealth));
    }

    private void GameOver()
    {
        if (_playerHealth <= 0)
        {
            Debug.Log("Player is dead");

            transform.position = _playerSpawnPoint.position;
            _playerHealth = _playerMaxHealth;
            EnemyCounterDead.Instance.RemoveCountedDead();
            FertilizerManager.Instance.RemoveMoney(10);
        }
    }

    private void AllController()
    {
        Move();
    }

    private void Move()
    {
        _playerInput.WeaponRotate(_playerGunsManager.transform);
        _playerRigidbody.linearVelocity = _playerInput.PlayerMovementInput(_playerGunsManager.transform, transform, _playerAnimationManager) * _playerSpeed;
        _playerCamera.CameraFollowAtPlayer(_cameraFollowSpeed);
        _playerInput.WeponShoots(_playerGunsManager);
    }

    private void AllButtons()
    {
        _playerInput.AllButtonInit(_playerGunsManager);
    }

    public void TakeDamage(int damage)
    {
        _playerHealth -= damage;

        Debug.Log($"Player health {_playerHealth}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Altar"))
        {
            EnemySpawner enemySpawner = other.GetComponent<EnemySpawner>();

            enemySpawner.ButtonSpawner(_buttonSpawner);

            _buttonSpawner.EnemySpawnerPreferences = enemySpawner?.EnemyThisSpawnerPreferences;
            _buttonSpawner.AltarPosition = enemySpawner.transform.position;
            _buttonSpawner.EnemySpawner = enemySpawner;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Altar"))
        {
            _buttonSpawner.gameObject.SetActive(false);
            _buttonSpawner.EnemySpawnerPreferences = null;
        }
    }
}
