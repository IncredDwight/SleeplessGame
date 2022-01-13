using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 _targetPos;
    private Vector3 _movePos;
    private float _speed;

    private void Start()
    {
        _speed = 0.95f;
    }

    private void FixedUpdate()
    {
        _targetPos = FindObjectOfType<PlayerController>().gameObject.transform.position;
        _movePos.x = _targetPos.x;
        _movePos.y = _targetPos.y;
        _movePos.z = -10;
        transform.position = Vector3.Lerp(transform.position, _movePos, _speed * Time.deltaTime);

    }
}
