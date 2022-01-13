using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using SaveHelper;

public class EnemySpawn : MonoBehaviour
{
    private Button _button;
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

    private Transform _spawnParent;

    [SerializeField] private float _speedGame = 0;

    private void Awake()
    {
        GameObject blocks = GameObject.Find("Blocks");
        Renderer re = blocks.GetComponent<Renderer>();
        _enemies = FindObjectsOfType<Enemy>();
        //_button = GameObject.Find("SpawnButton").GetComponent<Button>();
        MaxYPos = re.bounds.max.y - 5;
        MinYPos = re.bounds.min.y + 5;
        MinXPos = re.bounds.min.x + 5;
        _maxXPos = re.bounds.max.x - 5;

        _spawnParent = GameObject.Find("[ENEMIES]")?.transform ?? new GameObject("[ENEMIES]").transform;
    }

    private void Start()
    {
        //_button.onClick.AddListener(delegate { Spawn(); });
        if (_enemies != null)
        {
            foreach(Enemy enemy in _enemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }
        InvokeRepeating(nameof(Spawn), 3, 11 - _speedGame / 50);
    }

    private void Update()
    {
        _currentActiveEnemiesCount = FindObjectsOfType<Enemy>().Length;
        if (_currentActiveEnemiesCount < 1)
            Spawn();
        if (Input.GetKeyDown(KeyCode.F))
            Spawn();
    }

    private void Spawn()
    {
        float spawnPosX = Random.Range(MinXPos, _maxXPos);
        float spawnPosY = Random.Range(MinYPos, MaxYPos);

        GameObject obj = Instantiate(_enemies[Random.Range(0, _enemies.Length)].gameObject, new Vector2(spawnPosX, spawnPosY), Quaternion.identity);
        obj.transform.parent = _spawnParent;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(obj.transform.position, 1f);

        if (_currentActiveEnemiesCount < 5)
        {
            if (colliders.Length > 0 || obj.GetComponent<Renderer>().isVisible)
            {
                Destroy(obj);
                Spawn();
            }
            else
            {
                obj.SetActive(true);
                _speedGame = (_speedGame < 460) ? _speedGame + 15 : _speedGame;
            }
        }
        else
            Destroy(obj);
    }

    private IEnumerator CallSpawn()
    {
        Spawn();
        yield return new WaitForSeconds(11 - _speedGame / 50);
        StartCoroutine(CallSpawn());
    }

}
