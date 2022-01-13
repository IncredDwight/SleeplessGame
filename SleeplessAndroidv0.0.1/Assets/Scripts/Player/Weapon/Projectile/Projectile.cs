using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected enum targets { Enemy, Player, Everything};
    private targets _projectileTarget;

    private ParticleSystem _destroyEffect;
    private ParticleSystem _destroyEffectClone;
    private ParticleSystem _projectileEffect;
    private ParticleSystem _projectileEffectClone;

    private Pool _destroyEffectPool;
    private Pool _projectileEffectPool;

    private float _speed;
    private float _destroyPower;
    public float Damage;

    protected void Start()
    {
        if(_destroyEffect != null)
            _destroyEffectPool = PoolManager.Instance.AddPool(_destroyEffect.gameObject, _destroyEffect.name, 5);
        if (_projectileEffect != null)
        {
            _projectileEffectPool = PoolManager.Instance.AddPool(_projectileEffect.gameObject, _projectileEffect.name, 5);
            _projectileEffectClone = _projectileEffectPool.GetObject().GetComponent<ParticleSystem>();
        }
    }

    private void OnEnable()
    {
        if(_projectileEffectPool != null)
            _projectileEffectClone = _projectileEffectPool.GetObject().GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right, _speed * Time.deltaTime);
        if(_projectileEffectClone != null)
            _projectileEffectClone.transform.position = transform.position;
    }

    protected void ProjectileSetUp(float speed1, float destroyPower1, ParticleSystem destroyEffect1, ParticleSystem projectileEffect1, targets target, float damage1 = 0)
    {
        _speed = speed1;
        if (damage1 != 0)
            Damage = damage1;
        _destroyPower = destroyPower1;
        _destroyEffect = destroyEffect1;
        _projectileEffect = projectileEffect1;
        _projectileTarget = target;
    }

    private void DestroyProjectile(GameObject collisionGameObject)
    {
        if (name.Contains("(Clone)"))
            PoolManager.Instance.GetPool(name)?.AddObject(gameObject);

        CameraShakeController.instance.StartShake(0.5f, _destroyPower);

        if (_destroyEffectPool != null)
        {
            _destroyEffectClone = _destroyEffectPool.GetObject().GetComponent<ParticleSystem>();
            _destroyEffectClone.transform.position = gameObject.transform.position;
            _destroyEffectPool.AddObject(_destroyEffectClone.gameObject, 1);
        }
        _projectileEffectPool?.AddObject(_projectileEffectClone.gameObject);

        gameObject.SetActive(false);

        IDamageable damageable = collisionGameObject.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.TakeDamage(Damage);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.GetComponent<Projectile>() == null)
        {
            switch (_projectileTarget)
            {
                case targets.Enemy:
                    if (collisionGameObject.GetComponent<PlayerStats>() == null)
                    {
                        DestroyProjectile(collisionGameObject);
                    }

                    break;
                case targets.Player:
                    if (collisionGameObject.GetComponent<Enemy>() == null)
                    {
                        DestroyProjectile(collisionGameObject);
                        if (collision.gameObject.GetComponent<PlayerStats>() != null)
                        {
                            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
                            if (playerMovement.transform.position.x > transform.position.x)
                                playerMovement.KnockBack(new Vector2(1 * Damage / 2, 8));
                            else
                                playerMovement.KnockBack(new Vector2(-1 * Damage / 2, 8));
                        }
                    }
                    break;
                case targets.Everything:
                    DestroyProjectile(collisionGameObject);
                    break;
            }
        }
    }
}
