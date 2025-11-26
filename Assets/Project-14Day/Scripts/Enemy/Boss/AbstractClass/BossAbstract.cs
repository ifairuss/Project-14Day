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

    [Header("Damage Preferences")]
    [SerializeField] private int _defaulthDamage;
    [SerializeField] private int _rangeDamage;
    [SerializeField] private int _splashDamage;

    [Header("Boss Attack Coldown")]
    [SerializeField] private float _defaulthDamageColdown;
    [SerializeField] private float _rangeDamageColdown;
    [SerializeField] private float _splashDamageColdown;

    [Header("Boss components")]
    [SerializeField] private GameObject _bossHealthBar;
    [SerializeField] private Image _imageHealthMiddleground;
    [SerializeField] private Image _imageHealthForwardground;
    [SerializeField] private TextMeshProUGUI _bossNameUIText;
    [SerializeField] private Transform _enemyContainer;

    [Header("System status")]
    [SerializeField] private bool _isMoving;

    private NavMeshAgent _agent;
    private PlayerControllerManager _playerControllerManager;
    private Transform _bossUITransform;

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerControllerManager = GameObject.FindWithTag("Player").GetComponent<PlayerControllerManager>();
        _bossUITransform = GameObject.FindWithTag("WorldUI").GetComponent<Transform>();

        _bossHealthBar.transform.SetParent(_bossUITransform);

        _bossNameUIText.text = _bossName;

        _agent.speed = _speed;

        _bossHealthBar.SetActive(true);

        _imageHealthForwardground.fillAmount = Health / 1000;
    }

    public virtual void Update()
    {
        if (Health <= 0)
        {
            BossDead();
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
        if (_playerControllerManager == null) return;

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
        if (_defaulthDamageColdown > 0)
        {
            _defaulthDamageColdown -= 0.1f * Time.deltaTime;
        }

        PlayerTakeDamage();
    }

    public void PlayerTakeDamage()
    {
        if (Vector3.Distance(transform.position, _playerControllerManager.transform.position) <= (_agent.stoppingDistance + 0.3f))
        {
            if (_defaulthDamageColdown <= 0 && _isMoving == false)
            {
                _playerControllerManager.TakeDamage(_defaulthDamage);

                print("Boss damage player");

                _defaulthDamageColdown = 0.2f;
            }
        }
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
