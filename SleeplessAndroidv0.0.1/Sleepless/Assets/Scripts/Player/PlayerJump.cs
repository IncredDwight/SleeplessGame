using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerStats _playerStats;

    private float _jumpTime = 0.4f;
    private bool _isJumping;
    private bool _grounded;

    private void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Update()
    {
        GroundCheck();
        GetInput();
    }

    private void Jump()
    {
        _playerStats.Rigidbody2D.velocity = new Vector2(_playerStats.Rigidbody2D.velocity.x, Vector2.up.y * _playerStats.GetJumpForce());
    }

    private void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.GetChild(0).position, 0.3f);
        _grounded = colliders.Length > 1;
    }

    private void GetInput()
    {
        if (Input.GetButtonDown("Jump") && _grounded)
        {
            _isJumping = true;
            _jumpTime = 0.4f;
            Jump();
            _playerStats.Animator.SetTrigger("TakeOff");
        }
        if (_grounded && !Input.GetButtonDown("Jump"))
        {
            _playerStats.Animator.SetBool("IsJumping", false);
        }
        else
            _playerStats.Animator.SetBool("IsJumping", true);

        if (Input.GetButton("Jump") && _isJumping)
        {
            if (_jumpTime > 0)
            {
                _jumpTime -= Time.deltaTime;
                Jump();
            }
            else
                _isJumping = false;
        }
        else
            _isJumping = false;
    }

}
