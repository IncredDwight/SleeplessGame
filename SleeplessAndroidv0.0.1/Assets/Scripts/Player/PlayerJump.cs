using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerStats _playerStats;
    private Joystick _moveJoystick;
    private LadderClimb _ladderClimb;

    private List<Collider2D> _sortedColliders = new List<Collider2D>();

    private bool _isJumping;
    private bool _grounded;

    private void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        _moveJoystick = _playerStats.JoystickInput.MoveJoystick;
        _ladderClimb = GetComponent<LadderClimb>();
    }

    private void Update()
    {
        GroundCheck();
        GetInput();
    }

    private void Jump()
    {
        _playerStats.Rigidbody2D.velocity = Vector2.up * _playerStats.GetJumpForce();
    }

    private void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.GetChild(0).position, 0.1f);
        _sortedColliders = new List<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            if (!collider.isTrigger)
            {
                _sortedColliders.Add(collider);
            }   
        }
        _grounded = _sortedColliders.Count > 1;
    }

    private void GetInput()
    {
        if (_grounded && _moveJoystick.Vertical > 0.3f && !_ladderClimb._isClimbing)
        {
            Jump();
            _playerStats.Animator.SetTrigger("TakeOff");
        }
        if (_grounded && _isJumping)
        {
            //_playerStats.Rigidbody2D.velocity = Vector2.zero;
            _isJumping = false;
        }
           
    }

}
