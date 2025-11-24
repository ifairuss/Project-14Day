using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class BossAbstract : MonoBehaviour
{
    [Header("Boss help")]
    [SerializeField] private List<GameObject> _zombieVariable;
    [SerializeField] private List<Transform> _spawnVariable;
    [SerializeField] private List<GameObject> _allBossEnemy;

    [Header("Preferences")]
    [SerializeField] private float _speed;
    [SerializeField] private float _health;
    [SerializeField] private float _timeToAttack;
    [SerializeField] private string _bossName;

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

    private bool _bossSpawnedPhaseThree;
    private bool _bossSpawnedPhaseTwo;
    private bool _bossSpawnedPhaseOne;

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerControllerManager = GameObject.FindWithTag("Player").GetComponent<PlayerControllerManager>();
        _bossUITransform = GameObject.FindWithTag("WorldUI").GetComponent<Transform>();

        _bossHealthBar.transform.SetParent(_bossUITransform);

        _bossNameUIText.text = _bossName;

        _agent.speed = _speed;

        _bossSpawnedPhaseThree = true;

        _bossHealthBar.SetActive(true);

        _imageHealthForwardground.fillAmount = _health / 1000;
    }

    private void Update()
    {
        if (_health <= 0)
        {
            BossDead();
        }

        BossMoving();
        BossAttack();
        HealthUpdate();
        BossSpawnedEnemy();
    }

    private void BossSpawnedEnemy()
    {
        if (_bossSpawnedPhaseThree && _health <= 750)
        {
            for (int i = 0; i < _spawnVariable[2].childCount; i++)
            {
                int randomZombie = Random.Range(0, _zombieVariable.Count);

                var enemy = Instantiate(_zombieVariable[randomZombie], _spawnVariable[2].GetChild(i).transform.position, Quaternion.identity);

                _allBossEnemy.Add(enemy);
            }

            Debug.Log("Print phase 3 closed");

            _bossSpawnedPhaseThree = false;
            _bossSpawnedPhaseTwo = true;
        }
        else if (_bossSpawnedPhaseTwo && _health <= 500)
        {
            for (int o = 0; o < _spawnVariable[1].childCount; o++)
            {
                int randomZombie = Random.Range(0, _zombieVariable.Count);

                var enemy = Instantiate(_zombieVariable[randomZombie], _spawnVariable[1].GetChild(o).transform.position, Quaternion.identity);

                _allBossEnemy.Add(enemy);
            }

            Debug.Log("Print phase 2 closed");

            _bossSpawnedPhaseTwo = false;
            _bossSpawnedPhaseOne = true;
        }
        else if (_bossSpawnedPhaseOne && _health <= 250)
        {
            for (int k = 0; k < _spawnVariable[0].childCount; k++)
            {
                int randomZombie = Random.Range(0, _zombieVariable.Count);

                var enemy = Instantiate(_zombieVariable[randomZombie], _spawnVariable[0].GetChild(k).transform.position, Quaternion.identity);

                _allBossEnemy.Add(enemy);
            }

            Debug.Log("Print phase 1 closed");
            _bossSpawnedPhaseOne = false;
        }
    }

    private void HealthUpdate()
    {
        _imageHealthForwardground.fillAmount = _health / 1000;

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

    private void BossDead()
    {
        for (int i = 0; i < _allBossEnemy.Count; i++)
        {
            if (_allBossEnemy[i]!= null)
            {
                var enemy = _allBossEnemy[i].GetComponent<EnemyManager>();
                enemy.TakeDamage(1000);
            }
            else
            {
                _allBossEnemy.Remove(_allBossEnemy[i]);
            }
        }

        if (_allBossEnemy.Count == 0)
        {
            Destroy(_bossHealthBar.gameObject);
            Destroy(gameObject);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        _health -= damage;
    }
}
