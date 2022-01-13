using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private Transform _teleportPos;

    private float _cooldown;
    private float _currentCooldown;

    private void Start()
    {
        _cooldown = 3;
    }

    private void Update()
    {
        if (_currentCooldown > 0)
            _currentCooldown -= Time.deltaTime;
        else
            _teleportPos.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.GetComponent<PlayerStats>() != null && _currentCooldown <= 0)
        {
            _teleportPos.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            _currentCooldown = _cooldown;
            collidedObject.transform.position = _teleportPos.position;
        }
    }
}
