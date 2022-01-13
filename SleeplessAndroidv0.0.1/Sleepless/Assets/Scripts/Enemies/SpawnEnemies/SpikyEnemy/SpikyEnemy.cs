using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikyEnemy : Enemy
{
    [SerializeField]
    private GameObject _projectile;

    private float _coolDown;
    private float _currentCoolDown;

    protected void Awake()
    {
        EnemySetUp(3, 30, 20);
        _coolDown = 1.5f;
        if (_projectile != null)
            _projectile.SetActive(false);
    }

    protected override void Update()
    {
        if (_currentCoolDown <= 0)
        {
            SpawnSpikies(14);
            _currentCoolDown = _coolDown;
        }
        else
        {
            _currentCoolDown -= Time.deltaTime;
        }
    }

    private void SpawnSpikies(int spikesAmount)
    {
        Vector2 center = transform.position;
        for (int i = 0; i <= spikesAmount; i++)
        {
            int nextAngle = i * 25;
            Vector2 pos = RandomCircle(center, 0.1f, nextAngle);
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, center - pos);
            GameObject spikeClone = Instantiate(_projectile, pos, rot);
            spikeClone.SetActive(true);
        }
    }
    private Vector2 RandomCircle(Vector2 center, float radius, int angle)
    {
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        return pos;
    }
}
