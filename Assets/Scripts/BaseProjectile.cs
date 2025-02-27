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

    [SerializeField]
    private ParticleSystem _waterSplash;
    [SerializeField]
    private GameObject _waterDecal;

    [SerializeField]
    private ParticleSystem _shipDamageParticle;

    private Fire _heroFire;

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
        switch (other.tag)
        {
            case "Enemy":
                EnemyHP enemy = other.GetComponent<EnemyHP>();
                enemy.SetDamage(_damage);
                Instantiate(_shipDamageParticle, transform.position, Quaternion.LookRotation(Vector3.back, Vector3.up));
                DestroyProjectile();
                break;

            case "Rune":
                if (_heroFire != null)
                {
                    RuneInfo rune = other.GetComponentInParent<RuneInfo>();
                    _heroFire.ChangeProjectile(rune.Projectile, rune.WorkTime);
                    rune.Hitted();
                }
                DestroyProjectile();
                break;

            case "Water":
                Instantiate(_waterSplash, transform.position, Quaternion.identity);
                Instantiate(_waterDecal, transform.position, Quaternion.identity);
                DestroyProjectile();
                break;

            default:
                DestroyProjectile();
                break;

        }       

    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void SetFireComponent(Fire comp)
    {
        _heroFire = comp;
    }
}
