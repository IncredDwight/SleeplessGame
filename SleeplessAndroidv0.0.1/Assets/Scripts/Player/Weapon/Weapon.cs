using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Weapon : MonoBehaviour
{
    public bool IsBought;

    private LineRenderer _lineRenderer;

    private Camera camera1;
    private TextMeshProUGUI _ammoText;

    public WeaponData Data { get; private set; }

    private GameObject _projectile;
    private Vector3 _difference;
    private Vector3 _weaponPos;
    private Vector3 _mouseInput;
    private PlayerStats _playerController;
    private float _fireRate;
    private float _nextFire;

    private float _reloadTime;
    private bool _isReloading;
    private int _currentAmmo;
    private int _maxAmmo;
    private int _ammoSize;
    private Slider _reloadBar;
    private Image[] _reloadBarView;
    private bool _isBarVisible;

    private bool _isRecharging;
    private float _targetAngle;
    private int flip = 1;

    private Vector3 _currentScale;

    private float rotZ;
    private float targetZ;
    private Pool _pool;

    private void OnEnable()
    {
        foreach (Image img in _reloadBarView)
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
    }

    private void Awake()
    {
        _currentScale = transform.localScale;
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        _playerController = FindObjectOfType<PlayerStats>();
        _ammoText = FindObjectOfType<TextMeshProUGUI>();
        camera1 = Camera.main;
        LineRendererSetUp();
        _reloadBar = GameObject.Find("ReloadBar").GetComponent<Slider>();
        _reloadBarView = _reloadBar.GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        ReloadBar();
        Aim();
        if (!_isRecharging)
            FollowCursor();
        WeaponAttach();
    }

    private void Shoot()
    {
        WeaponFlip();
        if (Input.GetKey(KeyCode.Mouse0) && _currentAmmo > 0)
        {
            if (Time.time > _nextFire && _pool)
            {
                GameObject _readyProjectile = _pool.GetObject();
                _readyProjectile.GetComponent<Projectile>().Damage = Data.Damage;
                _readyProjectile.transform.position = transform.GetChild(0).position;
                _readyProjectile.transform.rotation = transform.rotation;
                _isRecharging = true;
                _targetAngle = transform.eulerAngles.z;
                transform.Rotate(0, 0, 100 * flip * _fireRate);
                StartCoroutine(WeaponRecharge());
                AmmoModifier(-1);
                _nextFire = Time.time + _fireRate;
            }
        }
        if(_maxAmmo > 0 && _currentAmmo <= 0 && !_isReloading)
        {
            _isReloading = true;
            StartCoroutine(Reload());
        }
        
    }

    private void FollowCursor()
    {
        //_mouseInput.x = _joystick.Horizontal;
        //_mouseInput.y = _joystick.Vertical;

        _difference = _mouseInput - transform.position;
        if (_mouseInput != Vector3.zero)
        {
            rotZ = Mathf.Atan2(_mouseInput.y, _mouseInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
            targetZ = transform.rotation.z;
            Shoot();
        }

        else if(!_isRecharging)
        {
            float targetZ;
            targetZ = (transform.localScale.y < 0) ? 180 : 0;
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, targetZ, transform.rotation.w);
        }
    }

    private void WeaponAttach()
    {
        _weaponPos = _playerController.transform.position;
        transform.position = new Vector3(_weaponPos.x, _weaponPos.y, _weaponPos.z - 1);
    }

    private void WeaponFlip()
    {
        Vector3 playerScale = _playerController.transform.localScale;
        flip = (_playerController.JoystickInput.ShootJoystick.Horizontal >= 0) ? 1 : -1;
        _playerController.Sprite.flipX = (Mathf.Sign(playerScale.x) == Mathf.Sign(flip)) ? true : false;
        transform.localScale = new Vector3(transform.localScale.x, _currentScale.y * flip, transform.localScale.z);
    }

    private void Aim()
    {
        _mouseInput.x = _playerController.JoystickInput.ShootJoystick.Horizontal;
        _mouseInput.y = _playerController.JoystickInput.ShootJoystick.Vertical;
        if (_mouseInput != Vector3.zero)
        {
            _lineRenderer.enabled = true;
            RaycastHit2D hit = Physics2D.Raycast(_playerController.transform.position, _mouseInput);
            Debug.DrawLine(transform.position, hit.point, Color.red);
            _lineRenderer.SetPosition(0, _playerController.transform.position);
            _lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    } 

    private void ReloadBar()
    {
        if (_isReloading)
        {
            _reloadBar.maxValue = _reloadTime;
            _reloadBar.value += Time.deltaTime;
            if(_reloadBar.value > (_reloadBar.maxValue / 100) * 70)
                _isBarVisible = false;
        }
        ChangeReloadBarVisibility();
    }

    private void ChangeReloadBarVisibility()
    {
        foreach (Image img in _reloadBarView)
        {
            if (!_isBarVisible)
                img.color = Color.LerpUnclamped(img.color, new Color(img.color.r, img.color.g, img.color.b, 0), Time.deltaTime * 2.5f);
            else
                img.color = Color.LerpUnclamped(img.color, new Color(img.color.r, img.color.g, img.color.b, 1), Time.deltaTime * 2.5f);
        }
    }

    private IEnumerator WeaponRecharge()
    {
        transform.Rotate(0, 0, (flip / -1));
        yield return new WaitForEndOfFrame();
        if (Mathf.FloorToInt(transform.eulerAngles.z) != Mathf.FloorToInt(_targetAngle))
            StartCoroutine(WeaponRecharge());
        else
            _isRecharging = false;
    }

    private IEnumerator Reload()
    {
        _isBarVisible = true;
        yield return new WaitForSeconds(_reloadTime);
        AmmoModifier(_ammoSize);
        _isReloading = false;
        yield return new WaitForSeconds(0.5f);
        _reloadBar.value = 0;
    }

    private void AmmoModifier(int amount)
    {
        _currentAmmo += amount;
        if (_ammoText != null)
            _ammoText.SetText(_currentAmmo + "/" + _ammoSize);
    }

    protected void WeaponSetUp(string assetName)
    {
        Data = Resources.Load<WeaponData>("Data/WeaponData/" + assetName);
        _fireRate = Data.FireRate;
        _ammoSize = Data.AmmoSize;
        _reloadTime = Data.ReloadTime;
        _currentAmmo = _ammoSize;
        _projectile = Data.Projectile;
        _pool = PoolManager.Instance.AddPool(_projectile, _projectile.name, 5);
        _ammoText.SetText(_currentAmmo + "/" + _ammoSize);
    }

    private void LineRendererSetUp()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.5f;
        _lineRenderer.endWidth = 0.5f;
        _lineRenderer.startColor = new Color(1, 0, 0, 0.5f);
        _lineRenderer.endColor = Color.red;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    private void OnDisable()
    {
        _isReloading = false;
        _isRecharging = false;
        _reloadBar.value = 0;
    }
}
