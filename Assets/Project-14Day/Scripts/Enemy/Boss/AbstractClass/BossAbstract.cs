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

    [Header("Dmage Preferences")]
    [SerializeField] private int _defaulthDamage;
    [SerializeField] private int _rangeDamage;
    [SerializeField] private int _splashDamage;

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

    }

    public virtual void TakeDamage(int damage)
    {
        _health -= damage;
    }
}
