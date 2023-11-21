using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] _runeSpawnPoints;

    [SerializeField]
    private GameObject[] _runes;

    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private float _timeBetweenSpawning;

    private float _timer;

    private bool _readyToSpawn;

    private void Start()
    {
        _readyToSpawn = true;
    }

    private void FixedUpdate()
    {
        if (_readyToSpawn) {
            _timer += Time.fixedDeltaTime;
            if (_timer >= _timeBetweenSpawning)
            {
                SpawnRune(_runeSpawnPoints[Random.Range(0, _runeSpawnPoints.Length)].position, Random.Range(0, _runes.Length));
                _timer = 0;
            }
        }
    }

    public void SpawnRune(Vector3 place, int runeIndex)
    {
        GameObject rune = Instantiate(_runes[runeIndex], place, Quaternion.identity, _parent);
    }
}
