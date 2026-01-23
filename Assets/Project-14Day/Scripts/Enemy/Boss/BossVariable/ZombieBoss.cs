using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ZombieBoss : BossAbstract
{
    [Header("Damage Preferences")]
    public int DefaulthDamage;
    public int RangeDamage;
    public int SplashDamage;

    [Space]
    [SerializeField] private float _splashAttackRadius;

    [Space]
    [SerializeField] private LayerMask _splashDamageLayer;

    [Header("Boss help")]
    [SerializeField] private List<GameObject> _zombieVariable;
    [SerializeField] private List<Transform> _spawnVariable;
    [SerializeField] private List<GameObject> _allBossEnemy;

    [Header("Splash attack setting")]
    [SerializeField] private float _splashAttackWaitToStart = 5;
    [SerializeField] private GameObject _splashRangeCircle;

    [Header("Boss Attack Coldown")]
    [SerializeField] private float DefaulthDamageCooldown = 1.5f;
    [SerializeField] private float SplashDamageCooldown = 3f;
    [SerializeField] private float RageDamageCooldown = 6f;

    private float _splashDamageCooldown = 3f;
    private float _defaulthDamageCooldown = 1.5f;
    private float _rageDamageCooldown = 6f;

    private float _scale = 1;

    private bool _isAttack;

    private void Awake()
    {
        _splashRangeCircle.SetActive(false);

        _splashDamageCooldown = SplashDamageCooldown;
        _defaulthDamageCooldown = DefaulthDamageCooldown;
        _rageDamageCooldown = RageDamageCooldown;
    }

    public override void Update()
    {
        base.Update();
        BossSpawnedEnemy();

        ColdownAttack();

        if (_scale < (_splashAttackRadius * 2) && _isAttack)
        {
            _scale += 2.5f * Time.fixedDeltaTime;
        }

        _splashRangeCircle.transform.localScale = new Vector3(_scale, _scale, _scale);
    }

    private void ColdownAttack()
    {
        if (_splashDamageCooldown <= 0 && _rageDamageCooldown <= 0 && _defaulthDamageCooldown <= 0) { return; }

        if (_splashDamageCooldown > 0)
        {
            _splashDamageCooldown -= 0.1f * Time.deltaTime;
        }

        if (_defaulthDamageCooldown > 0)
        {
            _defaulthDamageCooldown -= 0.1f * Time.deltaTime;
        }

        if (_rageDamageCooldown > 0)
        {
            _rageDamageCooldown -= 0.1f * Time.deltaTime;
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
                int randomAttack = Random.Range(0, 6);

                if ((randomAttack == 3 || randomAttack == 6 )
                    && Vector3.Distance(transform.position, PlayerControllerManager.transform.position) <= (Agent.stoppingDistance + 0.3f) 
                    && !_isAttack)
                {
                    _isAttack = true;
                    StartCoroutine(SplashAttack());

                    print($"Boss attck ( <color=Orange> Splah attack </color> )");
                }
                else if (randomAttack != 3 && randomAttack != 6 && !_isAttack)
                {
                    DefaulthAttack();
                }
            }
        }
        else if (Health <= HealthInPhaseTwo)
        {
            if (_defaulthDamageCooldown <= 0 && _splashDamageCooldown <= 0 && _rageDamageCooldown <= 0)
            {
                int randomAttack = Random.Range(0, 12);

                if ((randomAttack == 3 || randomAttack == 6)
                    && Vector3.Distance(transform.position, PlayerControllerManager.transform.position) <= (Agent.stoppingDistance + 0.3f)
                    && !_isAttack)
                {
                    _isAttack = true;
                    StartCoroutine(SplashAttack());
                    _splashDamageCooldown = SplashDamageCooldown;

                    print($"Boss attck ( <color=Orange> Splah attack </color> )");
                }
                else if (randomAttack == 9 
                    && Vector3.Distance(transform.position, PlayerControllerManager.transform.position) <= (Agent.stoppingDistance + 0.3f)
                    && !_isAttack)
                {
                    PlayerControllerManager.TakeDamage(RangeDamage);
                    _rageDamageCooldown = RageDamageCooldown;

                    print($"Boss attck ( <color=red> Range attack </color> )");
                }
                else if (randomAttack != 3 && randomAttack != 6 && randomAttack != 9 && !_isAttack)
                {
                    DefaulthAttack();
                }
            }
        }
    }

    private void DefaulthAttack()
    {
        if (Vector3.Distance(transform.position, PlayerControllerManager.transform.position) <= (Agent.stoppingDistance + 0.3f))
        {
            PlayerControllerManager.TakeDamage(DefaulthDamage);
            _defaulthDamageCooldown = DefaulthDamageCooldown;

            print($"Boss attck ( <color=Yellow> Defaulth </color> )");
        }
    }

    private IEnumerator SplashAttack()
    {
        _splashRangeCircle.SetActive(true);
        _scale = 1;
        _splashRangeCircle.transform.localScale = new Vector3(_scale, _scale, _scale);

        yield return new WaitForSeconds(_splashAttackWaitToStart);


        if (Physics2D.OverlapCircle(transform.position, _splashAttackRadius, _splashDamageLayer))
        {
            PlayerControllerManager.TakeDamage(SplashDamage);
        }

        _splashDamageCooldown = SplashDamageCooldown;

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
