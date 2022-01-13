using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsSpawn : MonoBehaviour
{
    private Cloud[] _clouds;
    private EnemySpawn _enemySpawn;

    private void Start()
    {
        _enemySpawn = FindObjectOfType<EnemySpawn>();
        _clouds = FindObjectsOfType<Cloud>();
        InvokeRepeating("Spawn", 0, 2.5f);
    }

    private void Spawn()
    {
        Vector3 finalPos = new Vector3(_enemySpawn.MinXPos - 10, Random.Range(_enemySpawn.MinYPos, _enemySpawn.MaxYPos), Random.Range(0, -9));
        Instantiate(_clouds[Random.Range(0, _clouds.Length)], finalPos, Quaternion.identity);
    }
}
