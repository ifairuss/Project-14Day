using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class BossAbstract : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToAttack;
    [SerializeField] private string _bossName;

    public float Health;

    [Space]
    [Header("Boss Phase HP")]
    public float HealthInPhaseThree;
    public float HealthInPhaseTwo;
    public float HealthInPhaseOne;

    [Header("Damage Preferences")]
    [SerializeField] private int _defaulthDamage;
    [SerializeField] private int _rangeDamage;
    [SerializeField] private int _splashDamage;

    [Header("Boss Attack Coldown")]
    [SerializeField] private float _defaulthDamageColdown;
    [SerializeField] private float _rangeDamageColdown;
    [SerializeField] private float _splashDamageColdown;

    [Header("System status")]
    [SerializeField] private bool _isMoving;

    private NavMeshAgent _agent;
    private PlayerControllerManager _playerControllerManager;
    private BossHPData _bossUITransform;

    private GameObject _bossHealthBar;
    private Image _imageHealthMiddleground;
    private Image _imageHealthForwardground;
    private TextMeshProUGUI _bossNameUIText;

    public bool BossSpawnedPhaseThree;
    public bool BossSpawnedPhaseTwo;
    public bool BossSpawnedPhaseOne;

    private void BossUIHealthGetComponents()
    {
        _bossHealthBar = _bossUITransform.BossHeatBar;
        _imageHealthMiddleground = _bossUITransform.ImageHealthMiddleground;
        _imageHealthForwardground = _bossUITransform.ImageHealthForwardground;
        _bossNameUIText = _bossUITransform.BossNameUIText;

        _bossNameUIText.text = _bossName;
        _bossHealthBar.SetActive(true);
        _imageHealthForwardground.fillAmount = Health / 1000;
    }

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerControllerManager = GameObject.FindWithTag("Player").GetComponent<PlayerControllerManager>();
        _bossUITransform = GameObject.FindWithTag("WorldUI").GetComponent<BossHPData>();

        _agent.speed = _speed;

        BossUIHealthGetComponents();

        BossSpawnedPhaseThree = true;
    }

    public virtual void Update()
    {
        if (Health <= 0)
        {
            BossDead();
            _bossHealthBar?.SetActive(false);
        }

        BossMoving();
        BossAttack();
        HealthUpdate();
    }

    public void HealthUpdate()
    {
        _imageHealthForwardground.fillAmount = Health / 1000;

        if (_imageHealthMiddleground.fillAmount > _imageHealthForwardground.fillAmount)
        {
            _imageHealthMiddleground.fillAmount -= 0.05f * Time.fixedDeltaTime;
        }
        else if (_imageHealthMiddleground.fillAmount < _imageHealthForwardground.fillAmount)
        {
            _imageHealthMiddleground.fillAmount = _imageHealthForwardground.fillAmount;
        }
    }

    private void Flip(Transform _playerControllerManager)
    {
        if (_playerControllerManager.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public virtual void BossMoving()
    {
        if (_playerControllerManager == null && Health > 0) return;

        _agent.SetDestination(_playerControllerManager.transform.position);

        if (Vector3.Distance(_playerControllerManager.transform.position, transform.position) <= (_agent.stoppingDistance + 0.2f))
        {
            _isMoving = false;
        }
        else
        {
            _isMoving = true;
        }

        Flip(_playerControllerManager.transform);
    }

    public virtual void BossAttack()
    {
        //Не готового убрав но не зробив - потім доробити

        if (_defaulthDamageColdown > 0)
        {
            _defaulthDamageColdown -= 0.1f * Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, _playerControllerManager.transform.position) <= (_agent.stoppingDistance + 0.3f))
        {
            if(_defaulthDamageColdown <= 0 && !_isMoving)
            {
                PlayerTakeDamage(_defaulthDamage);
            }
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        _playerControllerManager.TakeDamage(damage);
    }

    public virtual void BossDead()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
