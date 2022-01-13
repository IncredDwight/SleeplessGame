using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEnemyMovement : MonoBehaviour
{
    private Animator _animator;
    public float speed;
    public float fr = 3;
    public float magnitude = 1;
    Vector3 pos;

    private bool _isDead;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        pos = transform.position;
        speed = Random.Range(3, 6);
        fr = Random.Range(2, 3);
        magnitude = Random.Range(0.4f, 2);
    }

    private void Update()
    {
        pos += transform.right * Time.deltaTime * speed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * fr) * magnitude;
        Death();
    }

    private void Death()
    {
        if (_isDead)
        {
            speed = Mathf.Lerp(speed, 0, 3f * Time.deltaTime);
            magnitude = Mathf.Lerp(magnitude, 0.3f, 3f * Time.deltaTime);
        }
        if (speed < 0.5f)
        {
            _animator.SetTrigger("IsDead");
            if (GetComponent<TrailRenderer>() != null)
                GetComponent<TrailRenderer>().enabled = false;
        }
    }

    private void OnMouseDown()
    {
        _isDead = true;
    }
}
