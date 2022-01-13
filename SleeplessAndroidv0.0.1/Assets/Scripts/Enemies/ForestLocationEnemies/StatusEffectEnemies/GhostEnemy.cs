using UnityEngine;
using System.Collections;

public class GhostEnemy : EnemyPathFinder
{
    [SerializeField]
    private float _debuffAmount;
    [SerializeField]
    private float _debuffTime;
    private float _currentCoolDown;

    private void Awake()
    {
        EnemySetUp(3, 60, 30);
        _debuffAmount = 5.5f;
        _debuffTime = 5;
    }

    protected override void Update()
    {
        if (_currentCoolDown <= 0)
        {
            _currentCoolDown = _debuffTime * 3;
            _playerStats.AddStatusEffect<SlowDownStatusEffect>(_debuffTime, _debuffAmount);
        }
        else
        {
            _currentCoolDown -= Time.deltaTime;
        }
    }

    protected override void Die()
    {
        base.Die();
        _playerStats.AddStatusEffect<SlowDownStatusEffect>(_debuffTime, _debuffAmount);
    }

}
