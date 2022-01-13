using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Projectile
{
    private void Awake()
    {
        ProjectileSetUp(5.5f, 0, null, null, targets.Player, 10);
    }

    private void OnEnable()
    {
        if (gameObject.name != "Spike")
            Destroy(gameObject, 5.5f);
    }
}
