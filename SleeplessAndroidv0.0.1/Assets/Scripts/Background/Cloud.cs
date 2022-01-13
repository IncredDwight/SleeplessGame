using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float _minSpeed = 1;
    private float _maxSpeed = 3;
    private float _currentSpeed;

    private Color _cloudColor;

    private void Awake()
    {
        _cloudColor = GetComponent<SpriteRenderer>().color;
        _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
        GetComponent<SpriteRenderer>().color = new Color(_cloudColor.r, _cloudColor.g, _cloudColor.b, 0.5f);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right, _currentSpeed * Time.deltaTime);
    }
}
