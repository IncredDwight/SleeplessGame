using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flies : EnemyPathFinder
{
    private void Awake()
    {
        EnemySetUp(9, 5, 10);
    }

    protected override void Start()
    {
        base.Start();
        _enemyAi.enableRotation = true;
    }

    protected override void Die()
    {
        Destroy(gameObject);
        Destroy(_healthBar.gameObject);
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.GetComponent<PlayerStats>() != null)
            Die();
    }


}