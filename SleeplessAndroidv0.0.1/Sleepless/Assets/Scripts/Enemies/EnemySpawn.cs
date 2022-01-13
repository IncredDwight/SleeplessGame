using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private Enemy[] _enemies;
    private int _currentActiveEnemiesCount;

    [SerializeField]
    public float MinXPos;
    [SerializeField]
    private float _maxXPos;
    [SerializeField]
    public float MinYPos;
    [SerializeField]
    public float MaxYPos;

    [SerializeField] private float _speedGame = 0;

    private void Awake()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }

    private void Start()
    {
        if (_enemies != null)
        {
            foreach(Enemy enemy in _enemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }
        StartCoroutine(CallSpawn());
    }

    private void Update()
    {
        _currentActiveEnemiesCount = FindObjectsOfType<Enemy>().Length;
        if (_currentActiveEnemiesCount < 1)
            Spawn();
    }

    private void Spawn()
    {
        float spawnPosX = Random.Range(MinXPos, _maxXPos);
        float spawnPosY = Random.Range(MinYPos, MaxYPos);
        GameObject obj = Instantiate(_enemies[Random.Range(0, _enemies.Length)].gameObject, new Vector2(spawnPosX, spawnPosY), Quaternion.identity);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(obj.transform.position, 2f);
        if (_currentActiveEnemiesCount < 9)
        {
            if (colliders.Length > 1)
            {
                Destroy(obj);
                Spawn();
            }
            else
            {
                obj.SetActive(true);
                //obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, -2);
                _speedGame += 15;
            }
        }
    }

    private IEnumerator CallSpawn()
    {
        Spawn();
        yield return new WaitForSeconds(11 - _speedGame / 50);
        StartCoroutine(CallSpawn());
    }


}
