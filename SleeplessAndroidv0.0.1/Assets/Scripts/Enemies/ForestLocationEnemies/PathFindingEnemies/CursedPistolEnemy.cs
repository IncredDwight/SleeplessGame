using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedPistolEnemy : EnemyPathFinder
{
    private GameObject _projectile;

    private float _fireRate = 2f;
    private float _nextFire;
    private Pool _projectilesPool;

    private void Awake()
    {
        _flipY = true;
        EnemySetUp(3, 20, 15);
        if (PoolManager.Instance.GetPool("CursedPistolProjectile(Clone)") != null)
            _projectilesPool = PoolManager.Instance.GetPool("CursedPistolProjectile(Clone)");
        else
        {
            _projectile = FindObjectOfType<CursedWeaponProjectile>().gameObject;
            _projectilesPool = PoolManager.Instance.AddPool(_projectile, _projectile.name, 5);
            _projectilesPool.AddObject(_projectile);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        Vector3 difference = FindObjectOfType<PlayerStats>().transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotZ);
        transform.GetChild(0).rotation = transform.rotation;
        Shoot();
    }

    /*protected override void Die()
    {
        Destroy(gameObject);
        Destroy(_healthBar.gameObject);
    }*/

    private void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(0).position, transform.right);
        Debug.DrawLine(transform.GetChild(0).position, hit.point, Color.red);
        if (hit.collider.GetComponent<PlayerStats>() != null)
        {
            if (Time.time > _nextFire)
            {
                _nextFire = Time.time + _fireRate;
                GameObject readyProjectile = _projectilesPool?.GetObject();
                readyProjectile.transform.position = transform.GetChild(0).position;
                readyProjectile.transform.rotation = transform.rotation;
            }
        }
    }
}
