using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    private float damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats collidedObject = collision.gameObject.GetComponent<PlayerStats>();
        if(collidedObject != null)
        {
            collidedObject.TakeDamage(damage);
        }
    }
}
