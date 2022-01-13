using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Transform _camTransform;
    private Vector3 _lastCameraPos;
    [SerializeField]
    private float _parallaxMultiplier;

    private void Start()
    {
        _camTransform = Camera.main.transform;
        _lastCameraPos = _camTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = _camTransform.position - _lastCameraPos;
        transform.position += new Vector3(deltaMovement.x * _parallaxMultiplier, 0, 0);
        _lastCameraPos = _camTransform.position;
    }
}
