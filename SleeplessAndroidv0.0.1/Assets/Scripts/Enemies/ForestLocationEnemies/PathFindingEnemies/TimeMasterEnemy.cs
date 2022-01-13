using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMasterEnemy : EnemyPathFinder
{
    private float _abilityPower;
    private bool _inRage;

    private void Awake()
    {
        _abilityPower = 0.5f;
        EnemySetUp(7, 20, 10);
    }

    protected override void Update()
    {
        base.Update();
        if (_health <= _maxHealth / 2 && !_inRage)
            TransformInRage();
            
    }

    private void TransformInRage()
    {
        float _startDamage = _damage;
        float _startSpeed = _movementSpeed;
        _animator.SetBool("Transformation", true);
        _damage *= 1.2f;
        _movementSpeed *= 1.5f;
        _enemyAi.maxSpeed = _movementSpeed;
        _inRage = true;
    }

    protected override void Die()
    {
        _playerStats.AddStatusEffect<TimeDebuffStatusEffect>(6, _abilityPower);
        base.Die();
    }
}
