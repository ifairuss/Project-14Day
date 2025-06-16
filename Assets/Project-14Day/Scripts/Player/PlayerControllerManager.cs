using UnityEngine;

public class PlayerControllerManager : MonoBehaviour
{
    public static PlayerControllerManager Instance { get; private set; }

    [Header("⚙️ Player preferences")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _cameraFollowSpeed;

    private Rigidbody2D _playerRigidbody;

    private PlayerInput _playerInput;
    private CameraFollow _playerCamera;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialized()
    {
        _playerInput = GameObject.FindWithTag("PlayerInterface").GetComponent<PlayerInput>();
        _playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();

        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        AllController();
    }

    private void AllController()
    {
        Move();
    }

    private void Move()
    {
        _playerRigidbody.linearVelocity = _playerInput.PlayerMovementInput(transform) * _playerSpeed;
        _playerCamera.CameraFollowAtPlayer(_cameraFollowSpeed);
    }
}
