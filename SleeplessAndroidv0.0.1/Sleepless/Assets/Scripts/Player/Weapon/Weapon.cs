using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    private TextMeshProUGUI _ammoText;
    private GameObject _projectile;
    private Vector3 _difference;
    private Vector3 _weaponPos;
    private Vector3 _mouseInput;
    private PlayerStats _playerController;

    private float _fireRate;
    private float _nextFire;
    private int _ammo;
    private int _maxAmmo;

    private bool iss;
    public float s;
    private bool flip;

    private Vector3 _currentScale;

    private float rotZ;

    private void Awake()
    {
        _currentScale = transform.localScale;
        _playerController = FindObjectOfType<PlayerStats>();
        
    }

    private void Update()
    {
        //if(iss)
        //Bam(50);
        //if (iss = true)
        //   transform.Rotate(0, 0, -1);
        //if (Mathf.FloorToInt(transform.eulerAngles.z) == Mathf.FloorToInt(s))
            //iss = false;
        Shoot();
        if(!iss)FollowCursor();
        WeaponAttach();
        WeaponFlip();
        _ammoText = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
        if (_ammoText != null)
            _ammoText.SetText(_ammo + "/" + _maxAmmo);
    }

    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _ammo > 0)
        {
            if (Time.time > _nextFire)
            {
                Instantiate(_projectile, transform.GetChild(0).position, transform.rotation);
                iss = true;
                s = transform.eulerAngles.z;
                if (!flip) transform.Rotate(0, 0, 50);
                else transform.Rotate(0, 0, -50);
                StartCoroutine(Test());
                _ammo--;
                _nextFire = Time.time + _fireRate;
            }
        }
    }

    private void FollowCursor()
    {
        _mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _difference = _mouseInput - transform.position;
        rotZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private void WeaponAttach()
    {
        _weaponPos = _playerController.transform.position;
        transform.position = new Vector3(_weaponPos.x, _weaponPos.y, _weaponPos.z - 1);
    }

    private void WeaponFlip()
    {
        if (_mouseInput.x > _playerController.transform.position.x)
        {
            flip = false;
            transform.localScale = new Vector3(transform.localScale.x, _currentScale.y, transform.localScale.z);
        }
        else
        {
            flip = true;
            transform.localScale = new Vector3(transform.localScale.x, -_currentScale.y, transform.localScale.z);
        }
    }

    private void Bam(float target)
    {
        float startZ = transform.rotation.z;
        if(iss == false) transform.Rotate(0, 0, 1);
        if (transform.rotation.z == target)
            iss = true;
        if (iss == true)
            transform.Rotate(0, 0, -1);
    }

    private IEnumerator Test()
    {
        if (!flip) transform.Rotate(0, 0, -1);
        else transform.Rotate(0, 0, 1);
        yield return new WaitForSeconds(0.005f);
        if (Mathf.FloorToInt(transform.eulerAngles.z) != Mathf.FloorToInt(s))
            StartCoroutine(Test());
        else
            iss = false;
    }
    protected void WeaponSetUp(float fireRate1, int maxAmmo1, GameObject projectile1)
    {
        _fireRate = fireRate1;
        _maxAmmo = maxAmmo1;
        _ammo = _maxAmmo;
        _projectile = projectile1;
    }
}
