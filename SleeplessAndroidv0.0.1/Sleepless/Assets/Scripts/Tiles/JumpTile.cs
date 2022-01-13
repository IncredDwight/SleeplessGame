using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTile : MonoBehaviour
{
    [SerializeField]
    private float _jumpPower = 15.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if(collision.gameObject.GetComponent<PlayerStats>() != null && collidedObject.transform.position.y > transform.position.y)
        {
            PlayerStats _playerStats = collidedObject.GetComponent<PlayerStats>();
            _playerStats.Rigidbody2D.velocity = new Vector2(_playerStats.Rigidbody2D.velocity.x, Vector2.up.y * _jumpPower);
        }
    }


}
