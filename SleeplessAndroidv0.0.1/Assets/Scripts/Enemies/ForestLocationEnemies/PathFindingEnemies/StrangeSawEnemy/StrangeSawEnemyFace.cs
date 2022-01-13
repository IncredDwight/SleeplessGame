using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeSawEnemyFace : MonoBehaviour
{
    private Vector3 _sawPos;
    private Quaternion _startRotation;

    private void Awake()
    {
        _startRotation = transform.rotation;
    }
    private void Update()
    {
        transform.rotation = _startRotation;
    }
}
