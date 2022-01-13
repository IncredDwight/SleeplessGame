using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private Vector2 _movementSpeed;
    [SerializeField]
    private float _movetime = 3f;
    private int _currentDirection = 1;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_movetime > 0)
        {
            _rigidbody2D.velocity = new Vector2(_movementSpeed.x * _currentDirection, _movementSpeed.y * _currentDirection);
            _movetime -= Time.deltaTime;
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
            _currentDirection = -_currentDirection;
            _movetime = 5f;
        }
    }
}
