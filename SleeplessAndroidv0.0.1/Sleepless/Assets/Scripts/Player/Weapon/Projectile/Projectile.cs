using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ParticleSystem _destroyEffect;
    private ParticleSystem _projectileEffect;
    private ParticleSystem _projectileEffectClone;

    private float _speed;
    private float _destroyPower;
    private float _damage;

    protected void Start()
    {
        _projectileEffectClone = Instantiate(_projectileEffect, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right, _speed * Time.deltaTime);
        if(_projectileEffectClone != null)
            _projectileEffectClone.transform.position = transform.position;
    }

    protected void ProjectileSetUp(float speed1, float damage1, float destroyPower1, ParticleSystem destroyEffect1, ParticleSystem projectileEffect1)
    {
        _speed = speed1;
        _damage = damage1;
        _destroyPower = destroyPower1;
        _destroyEffect = destroyEffect1;
        _projectileEffect = projectileEffect1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if(collisionGameObject.GetComponent<Weapon>() == null && collisionGameObject.layer != 8)
        {
            Debug.Log(collisionGameObject);
            CameraShakeController.instance.StartShake(0.5f, _destroyPower);
            
            ParticleSystem particleClone = Instantiate(_destroyEffect, transform.position, Quaternion.identity);
            if(collisionGameObject.GetComponent<IDamageable>() != null)
                collisionGameObject.GetComponent<IDamageable>().TakeDamage(_damage);
            Destroy(particleClone.gameObject, particleClone.startLifetime);
            Destroy(_projectileEffectClone.gameObject, 0.8f);
            Destroy(gameObject);
        }
    }
}
