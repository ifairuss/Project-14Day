using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner preferences")]
    [SerializeField] private float _spawnerReloadTime;
    [SerializeField] private float _timeToEnemySpawned;
    [Space]
    public EnemySpawnerPreferences EnemyThisSpawnerPreferences;
    public List<Transform> EnemySpawnPoints;

    [SerializeField] private SplashText _waveText;
    [SerializeField] private TextMeshProUGUI _timerText;

    private Transform _worldSpaceCanvas;
    private CircleCollider2D _circleCollider;

    [Space]
    [Header("Spawner data")]
    [SerializeField] private int _waves;
    [SerializeField] private int _enemyCount;

    [SerializeField] private int _minEnemySpawnInWaves;
    [SerializeField] private int _maxEnemySpawnInWaves;

    private int _maxEnemy;
    private int _bossWave;

    private void Start() //тимчасово
    {
        _bossWave = EnemyThisSpawnerPreferences.BossSpawnWave;
        _timeToEnemySpawned = EnemyThisSpawnerPreferences.TimeToEnemySpawned;
        _minEnemySpawnInWaves = EnemyThisSpawnerPreferences.MinEnemySpawnToStart;
        _maxEnemySpawnInWaves = EnemyThisSpawnerPreferences.MaxEnemySpawnToStart;
        _maxEnemy = EnemyThisSpawnerPreferences.MaxEnemy;

        _worldSpaceCanvas = GameObject.Find("WorldSpaceCanvas").transform;
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        EnemyCountUpdate();

        if (_enemyCount <= 0 && _spawnerReloadTime > 0)
        {
            _timerText.gameObject.SetActive(true);
            _timerText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f, 0));
            _spawnerReloadTime -= 1 * Time.deltaTime;
            ReloadAltarTimer();
            _circleCollider.enabled = false;
        }
        else
        {
            _timerText.transform.SetParent(transform.parent);
            _timerText.gameObject.SetActive(false);
            _circleCollider.enabled = true;
        }
    }

    private void ReloadAltarTimer()
    {
        string FormatTimer = string.Format("{0:0}", _spawnerReloadTime);
        _timerText.text = FormatTimer;
        _timerText.transform.SetParent(_worldSpaceCanvas);
    }

    public void EnemyCountUpdate()
    {
        _enemyCount = transform.childCount;
    }

    public void StartWave(ButtonSpawner buttonSpawner)
    {
        if (_enemyCount <= 0)
        {
            if (_waves == _bossWave)
            {
                buttonSpawner.gameObject.SetActive(false);
                _waveText.OpenSplashPanel($"Boss Wave");
                StartCoroutine(SpawnBossWave());
            }
            else if (_waves != _bossWave)
            {
                buttonSpawner.gameObject.SetActive(false);
                _waveText.OpenSplashPanel($"Wave {_waves}");
                StartCoroutine(SpawnNormalWave());
            }
        }
    }

    public void ButtonSpawner(ButtonSpawner button)
    {
        if (_enemyCount <= 0 && _spawnerReloadTime <= 0)
        {
            button.gameObject.SetActive(true);
        }
        else if (_enemyCount > 0) 
        {
            button.gameObject.SetActive(false);
        }
    }

    private IEnumerator SpawnNormalWave()
    {
        print($"Wave - {_waves}");

        int randomEnemyCount = Random.Range(_minEnemySpawnInWaves, _maxEnemySpawnInWaves);

            for (int i = 0; i < randomEnemyCount; i++)
            {
                int spawnPointIndex = Random.Range(0, EnemySpawnPoints.Count);
                int randomEnemy = Random.Range(0, EnemyThisSpawnerPreferences.EnemyPrefabs.Length);

                GameObject enemy = Instantiate(EnemyThisSpawnerPreferences.EnemyPrefabs[randomEnemy], EnemySpawnPoints[spawnPointIndex].position, Quaternion.identity);

                enemy.transform.SetParent(transform);

                print($"{EnemyThisSpawnerPreferences.name}");

                yield return new WaitForSeconds(_timeToEnemySpawned);
            }

            _waves += 1;

         if (_maxEnemy > _maxEnemySpawnInWaves)
         {
             _minEnemySpawnInWaves += 1;
             _maxEnemySpawnInWaves += 2;
         }
            _spawnerReloadTime = EnemyThisSpawnerPreferences.SpawnerReloadTime;

        StopCoroutine(SpawnNormalWave());
    }

    private IEnumerator SpawnBossWave()
    {
        print($"Boss - Wave");

        yield return new WaitForSeconds(1);

        _bossWave = _bossWave += EnemyThisSpawnerPreferences.BossSpawnWave;
        _waves += 1;
        if (_maxEnemy > _maxEnemySpawnInWaves)
        {
            _minEnemySpawnInWaves += 1;
            _maxEnemySpawnInWaves += 2;
        }
    }
}
