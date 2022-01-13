using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnMenu : MonoBehaviour
{
    private List<GameObject> _enemies = new List<GameObject>();
    private GameObject _prevEnemy;

    private void Start()
    {
        foreach(Enemy _enemy in FindObjectsOfType<Enemy>())
        {
            _enemy.enabled = false;
            _enemy.gameObject.AddComponent<MenuEnemyMovement>();
            _enemy.gameObject.SetActive(false);
            _enemies.Add(_enemy.gameObject);
        }
        _prevEnemy = _enemies[0];
        InvokeRepeating("Spawn", 1, 4.5f);
    }

    private void Spawn()
    {
        Vector3 pos = new Vector3(-12.99f, 2.19f, 3);
        GameObject enemyToSpawn = _enemies[Random.Range(0, _enemies.Count)];
        if (_prevEnemy.name != enemyToSpawn.name)
        {
            _prevEnemy = Instantiate(enemyToSpawn, pos, Quaternion.identity);
            _prevEnemy.SetActive(true);
            Destroy(_prevEnemy, 12);
        }
        else
        {
            Debug.Log("!");
            Spawn();
        }
    }
}
