using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunProjectile : Projectile
{
    private void Awake()
    {
        ProjectileSetUp(11.5f,
            0.15f,
            GameObject.Find("DestroyEffect").GetComponent<ParticleSystem>(),
            GameObject.Find("ProjectileEffect").GetComponent<ParticleSystem>(),
            targets.Enemy);
    }

}
