using UnityEngine;

public class PlayerControllerManager : MonoBehaviour
{
    public static PlayerControllerManager Instance { get; private set; }

    [Header("⚙️ Player preferences")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _cameraFollowSpeed;

    [Header("⚙️ Player ststs")]
    [SerializeField] private int _playerHealth;

    private Rigidbody2D _playerRigidbody;

    private PlayerInput _playerInput;
    private CameraFollow _playerCamera;
    private Guns _playerGunsManager;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialized()
    {
        _playerInput = GameObject.FindWithTag("PlayerInterface").GetComponent<PlayerInput>();
        _playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
        _playerGunsManager = GameObject.FindWithTag("WeponsManager").GetComponent<Guns>();

        _playerRigidbody = GetComponent<Rigidbody2D>();

        _playerGunsManager.Initialized();

        AllButtons();
    }

    private void FixedUpdate()
    {
        AllController();

        GameOver();
    }

    private void GameOver()
    {
        if (_playerHealth <= 0)
        {
            Debug.Log("Player is dead");

            Destroy(gameObject);
        }
    }

    private void AllController()
    {
        Move();
    }

    private void Move()
    {
        _playerInput.WeaponRotate(_playerGunsManager.transform);
        _playerRigidbody.linearVelocity = _playerInput.PlayerMovementInput(_playerGunsManager.transform, transform) * _playerSpeed;
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
    }
}
