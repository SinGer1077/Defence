using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float _hp;

    private GameController _controller;

    private void Start()
    {
        _controller = FindAnyObjectByType<GameController>();
    }

    public void SetDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            _controller.DestroyEnemy();
            Destroy(gameObject);
        }
    }
}
