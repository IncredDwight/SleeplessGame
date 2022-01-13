using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathFinder : Enemy
{
    protected AIPath _enemyAi;
    private AIDestinationSetter _aIDestinationSetter;

    private Transform _target;

    protected override void Start()
    {
        base.Start();
        _target = FindObjectOfType<PlayerStats>().transform;
        AiSetUp();
        TargetSetUp();
    }

    private void AiSetUp()
    {
        if (gameObject.GetComponent<AIPath>() == null)
            _enemyAi = gameObject.AddComponent<AIPath>();
        else
            _enemyAi = GetComponent<AIPath>();
        _enemyAi.radius = 0.7f;
        _enemyAi.maxSpeed = _movementSpeed;
        _enemyAi.orientation = OrientationMode.YAxisForward;
        _enemyAi.gravity = Vector3.zero;
        _enemyAi.pickNextWaypointDist = 1;
        _enemyAi.enableRotation = false;
        _enemyAi.maxSpeed = _movementSpeed;
        BoxCollider2D collider2D = gameObject.AddComponent<BoxCollider2D>();
        collider2D.isTrigger = true;
    }

    private void TargetSetUp()
    {
        if (gameObject.GetComponent<AIDestinationSetter>() == null)
            _aIDestinationSetter = gameObject.AddComponent<AIDestinationSetter>();
        else
            _aIDestinationSetter = GetComponent<AIDestinationSetter>();
        _aIDestinationSetter.target = _target;
    }
}
