using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyQueen : Enemy
{
    private float _spawnCooldown;
    private float _currentSpawnCooldown;

    [SerializeField]
    private GameObject _fly;

    private void Awake()
    {
        EnemySetUp(4, 50, 20);
        _spawnCooldown = 4f;
    }
    protected override void Start()
    {
        base.Start();
        _currentSpawnCooldown = _spawnCooldown;
        if (_fly != null)
            _fly.SetActive(false);
    }

    private void Update()
    {
        if (_currentSpawnCooldown > 0)
            _currentSpawnCooldown -= Time.deltaTime;
        if (_currentSpawnCooldown <= 0)
            SpawnFly();
    }

    private void SpawnFly()
    {
        GameObject _flyClone = Instantiate(_fly, transform.position - new Vector3(0, 1),Quaternion.identity);
        _flyClone.SetActive(true);
        _currentSpawnCooldown = _spawnCooldown;
    }
    
}
