using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeSawEnemy : EnemyPathFinder
{

    private void Awake()
    {
        EnemySetUp(2, 50, 40);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * 480 * Time.deltaTime);
    }
}
