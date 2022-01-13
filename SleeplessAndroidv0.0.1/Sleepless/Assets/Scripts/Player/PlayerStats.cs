using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public Rigidbody2D Rigidbody2D { get { return GetComponent<Rigidbody2D>(); } private set { } }
    public Animator Animator { get { return GetComponentInChildren<Animator>(); } private set { } }
    public SpriteRenderer Sprite { get { return GetComponentInChildren<SpriteRenderer>(); } private set { } }

    private HealthBar _healthBar;

    private float _maxMovementSpeed;
    [SerializeField] private float _movementSpeed;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;
    public bool DamageImmunity;

    [SerializeField] private float _maxJumpForce;
    [SerializeField] private float _jumpForce;

    private float _fireRate;
    
    private void Awake()
    {
        StatsSetUp();
        _healthBar = FindObjectOfType<HealthBar>();
    }

    public void TakeDamage(float damage)
    {
        Rigidbody2D.velocity = Vector2.zero;
        _healthBar.SetHealthBar(_health);
        if (!DamageImmunity)
        {
            _health -= damage;
            StartCoroutine(SetDamageImmunity(0.5f));
        }
        if (_health <= 0)
            Die();
    }

    public void Heal(float heal)
    {
        _healthBar.SetHealthBar(_health);
        _health += heal;
        if (_health >= _maxHealth)
            _health = _maxHealth;
    }

    private void Die()
    {
        Debug.Log("Ой, сорян, а смерть мне лень писать:(");
    }

    public void MovementSpeedModifier(float value)
    {
        _movementSpeed += value;
        if (_movementSpeed <= 0)
            _movementSpeed = 1;
    }

    public float GetMovementSpeed()
    {
        return _movementSpeed;
    }

    public void JumpForceModifier(float value)
    {
        _jumpForce += value;
    }

    public float GetJumpForce()
    {
        return _jumpForce;
    }

    public IEnumerator SetDamageImmunity(float time)
    {
        DamageImmunity = true;
        yield return new WaitForSeconds(time);
        DamageImmunity = false;
    }

    private void StatsSetUp()
    {
        _movementSpeed = 8.7f;
        _jumpForce = 5.8f;
        _maxHealth = 100f;
        _health = _maxHealth;
    }

    public void AddStatusEffect<T>(float timeEffect, float effectAmount) where T : MonoBehaviour
    {
        if (GetComponent<T>() == null)
        {
            T type1 = gameObject.AddComponent<T>();
            StatusEffect status = type1 as StatusEffect;
            if (status)
                status.StatusEffectSetUp(timeEffect, effectAmount);
        }
    }
}
