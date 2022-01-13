using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private float _minSpeed = 8;
    private float _maxSpeed = 12;
    private float _currentSpeed;

    private void Awake()
    {
        _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right, _currentSpeed * Time.deltaTime);
    }
}
