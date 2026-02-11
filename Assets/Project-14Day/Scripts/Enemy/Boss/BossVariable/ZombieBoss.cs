using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBoss : BossAbstract
{
    [Header("Damage Preferences")]
    public int DefaulthDamage;
    public int RangeDamage;
    public int SplashDamage;

    [Header("Boss help")]
    [SerializeField] private List<GameObject> _zombieVariable;
    [SerializeField] private List<Transform> _spawnVariable;
    [SerializeField] private List<GameObject> _allBossEnemy;

    [Header("Splash attack setting")]
    [SerializeField] private float _splashAttackWaitToStart = 5;
    [SerializeField] private GameObject _splashRangeCircle;

    [Header("Boss Attack Coldown")]
    [SerializeField] private float DefaulthDamageColdown = 1.5f;
    [SerializeField] private float SplashDamageColdown = 3f;
    [SerializeField] private float RangeDamageColdown = 6f;

    private float _splashDamageCooldown = 3f;
    private float _defaulthDamageCooldown = 1.5f;
    private float _rangeDamageCooldown = 6f;

    private bool _isAttack;

    private void Awake()
    {
        _splashRangeCircle.SetActive(false);

        _splashDamageCooldown = SplashDamageColdown;
        _defaulthDamageCooldown = DefaulthDamageColdown;
        _rangeDamageCooldown = RangeDamageColdown;
    }

    public override void Update()
    {
        base.Update();
        BossSpawnedEnemy();

        ColdownAttack();
    }

    private void ColdownAttack()
    {
        if (_splashDamageCooldown <= 0 && _rangeDamageCooldown <= 0 && _defaulthDamageCooldown <= 0) { return; }

        if (_splashDamageCooldown > 0)
        {
            _splashDamageCooldown -= 0.1f * Time.deltaTime;
        }

        if (_defaulthDamageCooldown > 0)
        {
            _defaulthDamageCooldown -= 0.1f * Time.deltaTime;
        }

        if (_rangeDamageCooldown > 0)
        {
            _rangeDamageCooldown -= 0.1f * Time.deltaTime;
        }
    }

    private void BossSpawnedEnemy()
    {
        if (BossSpawnedPhaseThree && Health <= HealthInPhaseThree)
        {
            for (int i = 0; i < _spawnVariable[2].childCount; i++)
            {
                int randomZombie = Random.Range(0, _zombieVariable.Count);

                var enemy = Instantiate(_zombieVariable[randomZombie], _spawnVariable[2].GetChild(i).transform.position, Quaternion.identity);

                _allBossEnemy.Add(enemy);
            }

            Debug.Log("Print phase 3 closed");

            BossSpawnedPhaseThree = false;
            BossSpawnedPhaseTwo = true;
        }
        else if (BossSpawnedPhaseTwo && Health <= HealthInPhaseTwo)
        {
            for (int o = 0; o < _spawnVariable[1].childCount; o++)
            {
                int randomZombie = Random.Range(0, _zombieVariable.Count);

                var enemy = Instantiate(_zombieVariable[randomZombie], _spawnVariable[1].GetChild(o).transform.position, Quaternion.identity);

                _allBossEnemy.Add(enemy);
            }

            Debug.Log("Print phase 2 closed");

            BossSpawnedPhaseTwo = false;
            BossSpawnedPhaseOne = true;
        }
        else if (BossSpawnedPhaseOne && Health <= HealthInPhaseOne)
        {
            for (int k = 0; k < _spawnVariable[0].childCount; k++)
            {
                int randomZombie = Random.Range(0, _zombieVariable.Count);

                var enemy = Instantiate(_zombieVariable[randomZombie], _spawnVariable[0].GetChild(k).transform.position, Quaternion.identity);

                _allBossEnemy.Add(enemy);
            }

            Debug.Log("Print phase 1 closed");
            BossSpawnedPhaseOne = false;
        }
    }

    public override void BossAttack()
    {
        if (Health > HealthInPhaseThree)
        {
            if (_defaulthDamageCooldown <= 0)
            {
                DefaulthAttack();
            }
        }
        else if (Health <= HealthInPhaseThree)
        {
            if (_defaulthDamageCooldown <= 0 && _splashDamageCooldown <= 0)
            {
                int randomAttack = Random.Range(0, 5);

                if (randomAttack == 3 && !IsMoving && !_isAttack)
                {
                    _isAttack = true;
                    StartCoroutine(SplashAttack());

                    print($"Boss attck ( <color=Orange> Splah attack </color> )");
                }
                else if (randomAttack != 3 && !_isAttack)
                {
                    DefaulthAttack();
                }
            }
        }
        else if (Health <= HealthInPhaseTwo)
        {
            if (_defaulthDamageCooldown <= 0 && _splashDamageCooldown <= 0 && _rangeDamageCooldown <= 0)
            {
                int randomAttack = Random.Range(0, 10);

                if (randomAttack == 3 && !IsMoving && !_isAttack)
                {
                    _isAttack = true;
                    StartCoroutine(SplashAttack());
                    _splashDamageCooldown = SplashDamageColdown;

                    print($"Boss attck ( <color=Orange> Splah attack </color> )");
                }
                else if (randomAttack == 6 && !IsMoving && !_isAttack)
                {
                    PlayerControllerManager.TakeDamage(RangeDamage);
                    _rangeDamageCooldown = RangeDamageColdown;

                    print($"Boss attck ( <color=red> Range attack </color> )");
                }
                else if (randomAttack != 3 && randomAttack != 6 && !_isAttack)
                {
                    DefaulthAttack();
                }
            }
        }
    }

    private void DefaulthAttack()
    {
        if (!IsMoving)
        {
            PlayerControllerManager.TakeDamage(DefaulthDamage);
            _defaulthDamageCooldown = DefaulthDamageColdown;

            print($"Boss attck ( <color=Yellow> Defaulth </color> )");
        }
    }

    private IEnumerator SplashAttack()
    {
        _splashRangeCircle.SetActive(true);

        yield return new WaitForSeconds(_splashAttackWaitToStart);

        PlayerControllerManager.TakeDamage(SplashDamage);
        _splashDamageCooldown = SplashDamageColdown;

        _splashRangeCircle.SetActive(false);
        _isAttack = false;
        StopCoroutine(SplashAttack());
    }

    public override void BossDead()
    {
        for (int i = 0; i < _allBossEnemy.Count; i++)
        {
            if (_allBossEnemy[i] != null)
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
            Destroy(gameObject);
        }
    }
}
