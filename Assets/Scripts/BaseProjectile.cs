using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _maxDistance;

    [SerializeField]
    private float _damage;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    private float _timer;
    private float _timeToFly;

    private bool _reachTarget;

    private void Start()
    {
        _reachTarget = false;

        _startPosition = transform.position;
        _targetPosition = transform.forward * _maxDistance;

        _timeToFly = _maxDistance / _speed;
    }

    private void FixedUpdate()
    {
        if (_reachTarget == false)
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeToFly)
            {
                _reachTarget = true;
                DestroyProjectile();
            }
            else
            {
                Vector3 curPos = Vector3.Lerp(_startPosition, _targetPosition, _timer / _timeToFly);
                transform.position = curPos;
            }           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyHP enemy = other.GetComponent<EnemyHP>();
            enemy.SetDamage(_damage);
        }

        if (other.tag != "Player")
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
