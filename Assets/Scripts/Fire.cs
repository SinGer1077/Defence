using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private GameObject _currentProjectile;

    [SerializeField]
    private Transform[] _muzzleTransforms;

    [SerializeField]
    private float _delayBetweenShots;

    [SerializeField]
    private Transform _parentProjectile;

    [SerializeField]
    private AudioSource _source;

    private int _currentMuzzle = -1;

    private float _timer;

    private bool _readyToShot;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {            
            Shot();
        }        
    }

    private void FixedUpdate()
    {
        if (_readyToShot == false)
        {
            if (_timer < _delayBetweenShots)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                _readyToShot = true;
                _timer = 0;
            }
        }
    }

    private void Shot()
    {
        if (_readyToShot == true)
        {
            _source.Play();
            UpdateMuzzleNumber();
            Instantiate(_currentProjectile, _muzzleTransforms[_currentMuzzle].position, transform.rotation, _parentProjectile);

            _readyToShot = false;
        }
    }

    private void UpdateMuzzleNumber()
    {
        if (_currentMuzzle + 1 >= _muzzleTransforms.Length)
        {
            _currentMuzzle = -1;
        }
        _currentMuzzle += 1;
    }
}
