using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullKing : Enemy
{
    private BossPhase _phases = BossPhase.DashPhase;

    private float _dashTime;
    private Vector3 _targetPos;

    private void Awake()
    {
        _dashTime = 2.5f;
        EnemySetUp(10, 1000, 30);
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(DashPhase());
    }

    private void Update()
    {
        PhaseControl();
    }

    private void PhaseControl()
    {
        switch(_phases)
        {
            case BossPhase.DashPhase:
                Dashing();
                break;
        }
    }

    private void Dashing()
    {
        transform.position += _targetPos * _movementSpeed * Time.deltaTime;
    }

    private IEnumerator DashPhase()
    {
        _targetPos = (_playerStats.transform.position - transform.position).normalized;
        yield return new WaitForSeconds(_dashTime);
        StartCoroutine(DashPhase());
    }
}
