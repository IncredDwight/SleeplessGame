using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private float _speed;
    private float _damage;

    private void Start()
    {
        _speed = 5.5f;
        _damage = 10;
        Destroy(gameObject, 5.5f);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + -transform.up, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats playerStats = collision.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
