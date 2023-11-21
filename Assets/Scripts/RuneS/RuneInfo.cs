using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject _thisProjectile;
    public GameObject Projectile => _thisProjectile;

    [SerializeField]
    private RuneEnum _runeType;
    public RuneEnum RuneTupe => _runeType;

    [SerializeField]
    private float _workTime;
    public float WorkTime => _workTime;

    [SerializeField]
    private float _lifeTime;

    private float _timer;

    public void Hitted()
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;
        if (_timer >= _lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
