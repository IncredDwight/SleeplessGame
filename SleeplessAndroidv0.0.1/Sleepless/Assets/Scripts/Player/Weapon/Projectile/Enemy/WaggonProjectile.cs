using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaggonProjectile : Projectile
{
    private void Awake()
    {
        ProjectileSetUp(9, 50, 0.2f, GameObject.Find("DestroyEffect").GetComponent<ParticleSystem>(), GameObject.Find("ProjectileEffect").GetComponent<ParticleSystem>());
    }
}
