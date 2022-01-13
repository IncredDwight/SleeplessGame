using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats _playerStats;

    private float _direction;
    private float _currentScale;
    private bool _knockingBack;
    private float _knockBackCooldown = .2f;
    private float _currentKnockBackCooldown;

    private void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        _currentScale = _playerStats.gameObject.transform.localScale.x;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
            KnockBack(Vector2.right * -_direction);
        ApplyMovement();
        if (_knockingBack)
        {
            _currentKnockBackCooldown -= Time.deltaTime;
            if (_currentKnockBackCooldown <= 0)
                _knockingBack = false;
        }
    }

    private void ApplyMovement()
    {
        Flip();
        _direction = Input.GetAxisRaw("Horizontal");
        if(!_knockingBack)
            _playerStats.Rigidbody2D.velocity = new Vector2(_direction * _playerStats.GetMovementSpeed(), _playerStats.Rigidbody2D.velocity.y);
        _playerStats.Animator.SetFloat("Speed", Mathf.Abs(_direction));
    }

    public void KnockBack(Vector2 direction)
    {
        _knockingBack = true;
        _playerStats.Rigidbody2D.velocity = Vector2.zero;
        _playerStats.Rigidbody2D.velocity = direction;
        _currentKnockBackCooldown = _knockBackCooldown;
    }

    private void Flip()
    {
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
        {
            transform.localScale = new Vector2(_currentScale * -1, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(_currentScale, transform.localScale.y);
        }

        if (_direction == -1)
        {
            transform.localScale = new Vector2(_currentScale, transform.localScale.y);
        }
        else if (_direction == 1)
        {
            transform.localScale = new Vector2(_currentScale * -1, transform.localScale.y);
        }
    }

}
