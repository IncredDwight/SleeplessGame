using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedWeaponProjectile : Projectile
{
    private void Awake()
    {
        ProjectileSetUp(13.5f, 8, 0.05f, GameObject.Find("DestroyEffect").GetComponent<ParticleSystem>(), GameObject.Find("ProjectileEffect").GetComponent<ParticleSystem>());
    }
}
