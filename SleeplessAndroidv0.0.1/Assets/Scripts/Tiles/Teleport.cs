using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private Transform _teleportPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.GetComponent<PlayerStats>() != null)
        {
            collidedObject.transform.position = _teleportPos.GetChild(0).position;

        }
    }
}
