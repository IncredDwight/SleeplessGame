using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBee : EnemyPathFinder
{
    private float _abilityPower = 0.4f;

    private void Awake()
    {
        EnemySetUp(8, 40, 10);
    }

    protected override void Die()
    {
        _playerStats.AddStatusEffect<TimeSpeedUpStatusEffect>(30, _abilityPower);
        base.Die();
    }

}
