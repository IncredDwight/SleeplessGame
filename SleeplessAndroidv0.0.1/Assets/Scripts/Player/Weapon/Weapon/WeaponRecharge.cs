using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecharge : MonoBehaviour
{
    private WeaponStats _weaponStats;
    private WeaponShoot _weaponShoot;
    private Vector3 _currentScale;

    private int _flip = 1;
    private float _targetAngle;

    private void Awake()
    {
        _weaponStats = GetComponent<WeaponStats>();
        _currentScale = transform.localScale;
    }

    private IEnumerator Recharge()
    {
        transform.Rotate(0, 0, (_weaponShoot.Flip / -1));
        yield return new WaitForEndOfFrame();
        if (Mathf.FloorToInt(transform.eulerAngles.z) != Mathf.FloorToInt(_targetAngle))
            StartCoroutine(Recharge());
        else
        {
            _weaponShoot.CanShoot = true;
        }
    }

    public void StartRecharge(WeaponShoot weaponShoot)
    {
        _weaponShoot = weaponShoot;
        _targetAngle = transform.eulerAngles.z;
        transform.Rotate(0, 0, 100 * _weaponShoot.Flip * _weaponStats.Data.FireRate);
        StartCoroutine(Recharge());
    }
}
