using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private Vector2 _targetPos;
    private Vector3 _movePos;
    [SerializeField]
    private float _speed;

    private void Start()
    {
        _speed = 2f;
    }

    private void Update()
    {
        _targetPos = FindObjectOfType<PlayerStats>().gameObject.transform.position;
        _movePos.x = _targetPos.x;
        _movePos.y = _targetPos.y;
        _movePos.z = -10;
        transform.position = Vector3.Lerp(transform.position, _movePos, _speed * Time.deltaTime);

    }
}
