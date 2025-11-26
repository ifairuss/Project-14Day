using System.Collections.Generic;
using UnityEngine;

public class ZombieBoss : BossAbstract
{
    [Header("Boss help")]
    [SerializeField] private List<GameObject> _zombieVariable;
    [SerializeField] private List<Transform> _spawnVariable;
    [SerializeField] private List<GameObject> _allBossEnemy;

    private bool _bossSpawnedPhaseThree;
    private bool _bossSpawnedPhaseTwo;
    private bool _bossSpawnedPhaseOne;

    public override void Update()
    {
        base.Update();
        BossSpawnedEnemy();
    }

    private void BossSpawnedEnemy()
    {
        if (_bossSpawnedPhaseThree && Health <= 750)
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
        else if (_bossSpawnedPhaseTwo && Health <= 500)
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
        else if (_bossSpawnedPhaseOne && Health <= 250)
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
