using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    private PlayerStats _playerStats;

    private LayerMask _isLadderHere;

    private float _ladderRange;
    private bool _isClimbing;

    private void Start()
    {
        _ladderRange = 10.0f;
        _playerStats = FindObjectOfType<PlayerStats>();
        _isLadderHere = LayerMask.GetMask("Ladder");
    }

    // Update is called once per frame
    private void Update()
    {
        Climbing();
    }

    private void Climbing()
    {
        RaycastHit2D _isLadder = Physics2D.Raycast(transform.position, Vector2.up, _ladderRange, _isLadderHere);
        _isClimbing = (_isLadder.collider != null) ? true : false;
        if (_isClimbing)
        {
            float directionY = Input.GetAxisRaw("Vertical");
            _playerStats.Rigidbody2D.velocity = new Vector2(_playerStats.Rigidbody2D.velocity.x, directionY * _playerStats.GetMovementSpeed());
            _playerStats.Rigidbody2D.gravityScale = 0;
        }
        else
            _playerStats.Rigidbody2D.gravityScale = 1;
    }
}
