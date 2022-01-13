using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public Rigidbody2D Rigidbody2D { get { return GetComponent<Rigidbody2D>(); } private set { } }
    public Animator Animator { get { return GetComponentInChildren<Animator>(); } private set { } }
    public SpriteRenderer Sprite { get { return GetComponentInChildren<SpriteRenderer>(); } private set { } }
    public JoystickInput JoystickInput { get { return FindObjectOfType<JoystickInput>(); } private set { } }

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
        Time.timeScale = 1;
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
        SceneManager.LoadScene("MainMenu");
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
        Sprite.material.color = new Color32(255, 61, 61, 255);
        yield return new WaitForSeconds(time);
        Sprite.material.color = Color.white;
        DamageImmunity = false;
    }

    private void StatsSetUp()
    {
        _movementSpeed = 8.7f;
        _jumpForce = 12.8f;
        _maxHealth = 300f;

        if (PlayerPrefs.HasKey("PlayerHpBoost"))
        {
            _maxHealth += PlayerPrefs.GetFloat("PlayerHpBoost");
        }

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
