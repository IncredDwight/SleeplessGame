using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationsSpawn : MonoBehaviour
{
    private Cloud[] _clouds;
    private Bird _bird;
    private EnemySpawn _enemySpawn;

    private void Start()
    {
        _enemySpawn = FindObjectOfType<EnemySpawn>();
        _bird = FindObjectOfType<Bird>();
        _clouds = FindObjectsOfType<Cloud>();
        InvokeRepeating("SpawnCloud", 0, 5.5f);
        InvokeRepeating("SpawnBird", 0, 5f);
    }

    private void SpawnBird()
    {
        Vector3 finalPos = new Vector3(_enemySpawn.MinXPos - 10, Random.Range(_enemySpawn.MinYPos, _enemySpawn.MaxYPos), Random.Range(0, -9));
        GameObject decoration = Instantiate(_bird, finalPos, Quaternion.identity).gameObject;
        Destroy(decoration, 50);
    }

    private void SpawnCloud()
    {
        Vector3 finalPos = new Vector3(_enemySpawn.MinXPos - 10, Random.Range(_enemySpawn.MinYPos + 40, _enemySpawn.MaxYPos - 40), Random.Range(0, -9));
        GameObject decoration = Instantiate(_clouds[Random.Range(0, _clouds.Length)], finalPos, Quaternion.identity).gameObject;
        Destroy(decoration, 50);
    }
}
