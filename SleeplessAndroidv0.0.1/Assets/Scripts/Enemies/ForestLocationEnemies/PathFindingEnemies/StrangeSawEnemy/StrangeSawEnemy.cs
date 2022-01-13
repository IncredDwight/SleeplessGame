using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeSawEnemy : EnemyPathFinder
{
    private float _deathBuff;
    private float _deathBuffTime;

    private void Awake()
    {
        _deathBuff = 4.5f;
        _deathBuffTime = 8;
        EnemySetUp(2, 50, 40);
    }

    private void Update()
    {
        
    }

    protected override void Die()
    {
        base.Die();
        _playerStats.AddStatusEffect<SpeedUpStatusEffect>(_deathBuffTime, _deathBuff);
    }
}
