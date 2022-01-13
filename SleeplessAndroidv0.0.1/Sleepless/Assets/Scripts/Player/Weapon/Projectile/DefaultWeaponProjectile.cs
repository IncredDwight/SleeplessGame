using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeaponProjectile : Projectile
{
    private void Awake()
    {
        ProjectileSetUp(17.5f, 5, 0.15f, GameObject.Find("DestroyEffect").GetComponent<ParticleSystem>(), GameObject.Find("ProjectileEffect").GetComponent<ParticleSystem>());
    }
}
