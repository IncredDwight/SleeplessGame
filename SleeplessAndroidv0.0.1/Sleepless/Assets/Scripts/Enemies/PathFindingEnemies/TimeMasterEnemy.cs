using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMasterEnemy : EnemyPathFinder
{
    private float _abilityPower;

    private void Awake()
    {
        _abilityPower = 0.5f;
        EnemySetUp(7, 20, 10);
    }

    protected override void Die()
    {
        _playerStats.AddStatusEffect<TimeSpeedUpStatusEffect>(10, _abilityPower);
        base.Die();
    }
}
