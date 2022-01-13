using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float currentScale;

    private PlayerStats _playerStats;
    private Joystick _shootJoystickInput;

    private float _direction;
    private bool _knockingBack;
    private float _knockBackCooldown = .2f;
    private float _currentKnockBackCooldown;

    private void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        _shootJoystickInput = _playerStats.JoystickInput.ShootJoystick;
        currentScale = _playerStats.gameObject.transform.localScale.x;
    }

    private void FixedUpdate()
    {

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
        float horizontal = _playerStats.JoystickInput.MoveJoystick.Horizontal;
        _direction = (Mathf.Abs(horizontal) < 0.5f && horizontal != 0) ? 0 * Mathf.Sign(horizontal) : horizontal; 
        if(!_knockingBack)
            _playerStats.Rigidbody2D.velocity = new Vector2(_direction * _playerStats.GetMovementSpeed(), _playerStats.Rigidbody2D.velocity.y);
        _playerStats.Animator.SetFloat("Speed", Mathf.Abs(_direction));
    }

    public void KnockBack(Vector2 direction)
    {
        _knockingBack = true;
        _playerStats.Rigidbody2D.velocity = Vector2.zero;
        _playerStats.Rigidbody2D.AddForce(direction, ForceMode2D.Impulse);
        _currentKnockBackCooldown = _knockBackCooldown;
    }

    private void Flip()
    {
        Vector3 shootJoystickAxis = Vector3.zero;
        if (_shootJoystickInput != null)
        {
            shootJoystickAxis.x = _shootJoystickInput.Horizontal;
            shootJoystickAxis.y = _shootJoystickInput.Vertical;
        }
        if (shootJoystickAxis == Vector3.zero)
        {
            if (_direction < 0)
            {
                _playerStats.Sprite.flipX = false;
            }
            else if (_direction > 0)
            {
                _playerStats.Sprite.flipX = true;
            }
        }
    }

}
