using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buffs : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _sprite;

    protected PlayerStats _playerStats;
    protected float _buffTime;
    protected float _buffAmount;

    private bool _isBuffed;
    private float _currentBuffTime;

    private void Awake()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_currentBuffTime > 0)
        {
            if(!_isBuffed) Buff();
            _isBuffed = true;
            _currentBuffTime -= Time.deltaTime;
        }
        else if(_isBuffed && _currentBuffTime <= 0)
        {
            EndBuff();
            _isBuffed = false;
        }
    }

    protected abstract void Buff();
    protected abstract void EndBuff();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerStats>() != null)
        {
            _currentBuffTime = _buffTime;
            _boxCollider2D.enabled = false;
            _sprite.enabled = false;
            Destroy(gameObject, _buffTime + 3);
        }
    }
}
