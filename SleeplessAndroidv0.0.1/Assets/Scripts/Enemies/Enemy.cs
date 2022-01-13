using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    protected Animator _animator;

    private ParticleSystem _bloodEffect;

    protected PlayerStats _playerStats;

    private LevelsTransition _levels;

    [SerializeField]
    protected float _movementSpeed;
    [SerializeField]
    protected float _damage;
    [SerializeField]
    protected float _health;
    protected float _maxHealth;
    protected EnemyHealthBarController _healthBar;

    private Vector3 _startScale;
    protected bool _flipY;

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
       // GetComponent<SpriteRenderer>().sortingOrder = 3;
        _healthBar = Instantiate(FindObjectOfType<EnemyHealthBarController>().gameObject, GameObject.Find("HealthBarCanvas").transform).GetComponent<EnemyHealthBarController>();
        _healthBar.SetHealthBarPos(transform);
        _healthBar.SetMaxValue(_maxHealth);

       // _bloodEffect = GameObject.Find("Blood").GetComponent<ParticleSystem>();
        _playerStats = FindObjectOfType<PlayerStats>();
        _health = _maxHealth;
        _startScale = transform.localScale;
        _levels = FindObjectOfType<LevelsTransition>();
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
        int totalMoney = Mathf.FloorToInt(_maxHealth / 5);
        CurrencyManager.Instance.ChangeCurrency(totalMoney);

        _levels.AddScore(10);

        if (_animator != null)
        {
            _animator.SetTrigger("IsDead");
            Destroy(gameObject, 2);
        }
        else
            Destroy(gameObject);

        _movementSpeed = 0;
        GetComponent<BoxCollider2D>().enabled = false;
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

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStats>() != null)
        {
            collision.GetComponent<PlayerStats>().TakeDamage(_damage);
            if (collision.gameObject.transform.position.x > transform.position.x)
                collision.GetComponent<PlayerMovement>().KnockBack(new Vector2(1 * _damage / 2, 8));
            else
                collision.GetComponent<PlayerMovement>().KnockBack(new Vector2(-1* _damage / 2, 8));
        }
    }


}
