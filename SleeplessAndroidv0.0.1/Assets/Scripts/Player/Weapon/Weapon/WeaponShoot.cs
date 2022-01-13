using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    private WeaponStats _weaponStats;
    private WeaponReload _weaponReload;
    private WeaponRecharge _weaponRecharge;

    private Pool _projectilesPool;
    private GameObject _projectile;
    private Vector3 _startWeaponScale;

    private float _fireRate;
    private float _nextFire;

    private float _spread;

    public bool CanShoot = true;
    public int Flip;

    private void Start()
    {
        _weaponStats = GetComponent<WeaponStats>();
        _weaponReload = GetComponent<WeaponReload>();
        _weaponRecharge = GetComponent<WeaponRecharge>();

        _startWeaponScale = transform.localScale;

        if (_weaponStats != null)
        {
            _projectile = _weaponStats.Data.Projectile;
            _projectilesPool = PoolManager.Instance.AddPool(_projectile, _projectile.name, 5);
            _fireRate = _weaponStats.Data.FireRate;
            _spread = _weaponStats.Data.Spread;
        }
    }

    private void Update()
    {
        if(CanShoot)
            FollowCursor();
        WeaponAttach();
    }

    private void Shoot()
    {
        WeaponFlip();
        if (_weaponStats.CurrentAmmo > 0)
        {
            if (Time.time > _nextFire && _projectilesPool)
            {
                if (_weaponStats.Data.Multiple)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        PullOffBullet((i - 1) * 10);
                    }
                }
                else
                    PullOffBullet(0);

                CanShoot = false;
                _weaponRecharge.StartRecharge(this);
                _nextFire = Time.time + _fireRate;
            }
        }
        else if(_weaponReload != null)
        {
            _weaponReload.StartReload(_weaponStats);
        }
    }

    private void PullOffBullet(int angle)
    {
        GameObject _readyProjectile = _projectilesPool.GetObject();
        _readyProjectile.GetComponent<Projectile>().Damage = _weaponStats.Data.Damage;
        _readyProjectile.transform.position = transform.GetChild(0).position;
        _readyProjectile.transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, angle));
        _readyProjectile.transform.Rotate(new Vector3(0, 0, Random.Range(-_spread, _spread)));
        _weaponStats.AmmoModifier(-1);
    }

    private void FollowCursor()
    {
        Vector3 difference = _weaponStats.ShootJoystickInput - transform.position;

        if (_weaponStats.ShootJoystickInput != Vector3.zero)
        {
            float rotZ = Mathf.Atan2(_weaponStats.ShootJoystickInput.y, _weaponStats.ShootJoystickInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
            Shoot();
        }

        else if (CanShoot)
        {
            float targetZ;
            targetZ = (transform.localScale.y < 0) ? 180 : 0;
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, targetZ, transform.rotation.w);
        }
    }

    private void WeaponAttach()
    {
        Vector3 weaponPos = _weaponStats.PlayerStats.transform.position;
        transform.position = new Vector3(weaponPos.x, weaponPos.y, weaponPos.z - 1);
    }

    private void WeaponFlip()
    {
        Vector3 playerScale = _weaponStats.transform.localScale;
        Flip = (_weaponStats.PlayerStats.JoystickInput.ShootJoystick.Horizontal >= 0) ? 1 : -1;
        _weaponStats.PlayerStats.Sprite.flipX = (Mathf.Sign(playerScale.x) == Mathf.Sign(Flip)) ? true : false;
        transform.localScale = new Vector3(transform.localScale.x, _startWeaponScale.y * Flip, transform.localScale.z);
    }

    private void OnDisable()
    {
        CanShoot = true;
    }
}
