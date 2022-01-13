using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    private ParticleSystem _bloodEffect;

    protected PlayerStats _playerStats;

    [SerializeField]
    protected float _movementSpeed;
    [SerializeField]
    protected float _damage;
    [SerializeField]
    protected float _health;
    protected float _maxHealth;
    private EnemyHealthBarController _healthBar;

    private Vector3 _startScale;
    protected bool _flipY;

    protected virtual void Start()
    {
        _healthBar = Instantiate(FindObjectOfType<EnemyHealthBarController>().gameObject, GameObject.Find("HealthBarCanvas").transform).GetComponent<EnemyHealthBarController>();
        _healthBar.SetHealthBarPos(transform);
        _healthBar.SetMaxValue(_maxHealth);

       // _bloodEffect = GameObject.Find("Blood").GetComponent<ParticleSystem>();
        _playerStats = FindObjectOfType<PlayerStats>();
        _health = _maxHealth;
        _startScale = transform.localScale;
    }

    protected virtual void Update()
    {
        if (FindObjectOfType<PlayerStats>().transform.position.x < transform.position.x)
            transform.localScale = (!_flipY) ? new Vector2(-_startScale.x, _startScale.y) : new Vector2(_startScale.x, -_startScale.y);
        else
            transform.localScale = new Vector2(_startScale.x, _startScale.y);
    }

    public void Heal(float heal)
    {
        _health += heal;
        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    { 
        _health -= damage;
        _healthBar.SetHealth(_health);
        StartCoroutine(TakingDamageReset());
        if (_health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Debug.Log("Враг должен умирать, но программисту тоже кушац хочется(((");
        //Instantiate(_bloodEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(_healthBar.gameObject);
    }

    private IEnumerator TakingDamageReset()
    {
        _healthBar.SetVisiblity(true);
        yield return new WaitForSeconds(3);
        _healthBar.SetVisiblity(false);
    }

    protected void EnemySetUp(float movementSpeed1, float maxHealth1, float damage1)
    {
        _movementSpeed = movementSpeed1;
        _maxHealth = maxHealth1;
        _damage = damage1;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStats>() != null)
            collision.GetComponent<PlayerStats>().TakeDamage(_damage);
    }

}
