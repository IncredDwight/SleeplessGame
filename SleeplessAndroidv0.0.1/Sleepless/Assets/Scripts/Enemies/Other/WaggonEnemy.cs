using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaggonEnemy : Enemy
{
    private GameObject _bullet;
    private Vector2 _playerPosition;

    private float _fireRate;
    private float _nextFire;

    private float _shootCoolDown;
    private float _currentShootCoolDown;
    private float _shootTime;
    private float _currentShootTime;

    private bool _onCooldown;

    protected void Awake()
    {
        EnemySetUp(3, 65, 20);
        _fireRate = 0.9f;
        _shootCoolDown = 4f;
        _shootTime = 5f;
        _currentShootTime = _shootTime;

        _bullet = FindObjectOfType<WaggonProjectile>().gameObject;
    }

    private void Update()
    {
        if (_currentShootTime > 0)
        {
            ChangePosition();
            Shoot();
        }
        else if (_currentShootCoolDown <= 0 && !_onCooldown)
            _currentShootCoolDown = _shootCoolDown;

        if (_currentShootCoolDown > 0)
        {
            _onCooldown = true;
            _currentShootCoolDown -= Time.deltaTime;
        }
        else if (_onCooldown && _currentShootCoolDown <= 0)
            _currentShootTime = _shootTime;
    }

    private void Shoot()
    {
        _onCooldown = false;
        _currentShootTime -= Time.deltaTime;
        if(Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            Instantiate(_bullet, transform.GetChild(0).position, Quaternion.identity);
        }
    }

    private void ChangePosition()
    {
        _playerPosition = FindObjectOfType<PlayerStats>().transform.position;
        Vector3 targetPos = new Vector2(transform.position.x, _playerPosition.y);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * _movementSpeed);
    }

    
}
