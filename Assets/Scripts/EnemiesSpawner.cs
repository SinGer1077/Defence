using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] _enemiesSpawners;

    [SerializeField]
    private Transform[] _bossSpawners;

    [SerializeField]
    private GameObject _standartEnemy;

    [SerializeField]
    private GameObject _boss;

    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private TextMeshProUGUI _timerText;

    [SerializeField]
    private int _enemiesCount;
    public int EnemiesCount => _enemiesCount;

    private int _enemiesPerWave;

    private int _currentWave;

    private int _spawnedEnemies;


    private float _timer;
    private float _currentTiming;
    private bool _readyToSpawn;

    private Vector2[] _waveRandomTimers = new Vector2[]
    {
        new Vector2(5f, 10f),
        new Vector2(3f, 7f),
        new Vector2(2f, 4f)
    };

    private Vector2[] _waveRandomCount = new Vector2[]
    {
        new Vector2(1, 1),
        new Vector2(1, 2),
        new Vector2(2, 3)
    };

    private void Start()
    {
        _enemiesPerWave = _enemiesCount / 3;
        _currentWave = 0;
        _readyToSpawn = true;
    }

    private void FixedUpdate()
    {
        if (_readyToSpawn) {
            if (_timer <= 0)
            {
                _currentTiming = Random.Range(_waveRandomTimers[_currentWave].x, _waveRandomTimers[_currentWave].y);
                _timer += Time.fixedDeltaTime;
                Debug.Log(_currentTiming);
                _timerText.text = "Враг будет через: " + (int)_currentTiming;
            }
            else
            {
                _timer += Time.fixedDeltaTime;
                _timerText.text = "Враг будет через: " + (int)(_currentTiming - _timer);
                if (_timer >= _currentTiming)
                {
                    _timer = 0;
                    int enemiesCount = Random.Range((int)_waveRandomCount[_currentWave].x, (int)_waveRandomCount[_currentWave].y);
                    for (int i = 0; i < enemiesCount; i++)
                    {
                        SpawnEnemy(Random.Range(0, _enemiesSpawners.Length));
                        _timerText.text = "Враг будет через: 0";
                    }
                }
            }

            if (_spawnedEnemies >= _enemiesCount)
            {
                _readyToSpawn = false;
            }
            else
            {
                if (_spawnedEnemies / (_currentWave + 1) > _enemiesPerWave)
                {
                    Debug.Log("Пауза");
                    _readyToSpawn = false;
                    _currentWave++;
                    _timerText.text = "Следующая волна через: 7.5 сек!";
                    //_currentTiming = 0;
                    //_timer = Time.fixedDeltaTime;
                    Invoke("SetReadyToSpawn", 7.5f);
                }
            }
        }

        
    }

    private void SpawnEnemy(int spawnIndex)
    {
        Instantiate(_standartEnemy, _enemiesSpawners[spawnIndex].position, Quaternion.identity, _parent);
        _spawnedEnemies++;
    }

    private void SetReadyToSpawn()
    {
        _readyToSpawn = true;
    }
}
