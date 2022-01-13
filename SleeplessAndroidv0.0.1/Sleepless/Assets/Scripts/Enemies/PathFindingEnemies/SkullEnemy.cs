using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullEnemy : EnemyPathFinder
{
    private void Awake()
    {
        EnemySetUp(10, 20, 60);
    }
}
