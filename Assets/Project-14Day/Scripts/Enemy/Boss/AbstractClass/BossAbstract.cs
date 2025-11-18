using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BossAbstract : MonoBehaviour
{
    [Header("Boss help")]
    [SerializeField] private List<GameObject> _zombieVariable;

    [Header("Preferences")]
    [SerializeField] private float _speed;
    [SerializeField] private int _health;
    [SerializeField] private float _timeToAttack;

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

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerControllerManager = GameObject.FindWithTag("Player").GetComponent<PlayerControllerManager>();

        _agent.speed = _speed;
    }

    private void Update()
    {
        BossMoving();
        BossAttack();
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

    public virtual void TakeDamage(int damage)
    {
        _health -= damage;
    }
}
