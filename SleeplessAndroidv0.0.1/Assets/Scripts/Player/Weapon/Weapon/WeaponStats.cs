using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponStats : MonoBehaviour
{
    [SerializeField]
    private string _weaponDataAssetName;

    private TextMeshProUGUI _ammoText;

    public int CurrentAmmo { get; private set; }
    private int _ammoSize;

    public WeaponData Data { get; private set; }
    public PlayerStats PlayerStats { get; private set; }
    public Vector3 ShootJoystickInput;

    private void Start()
    {
        WeaponSetUp(_weaponDataAssetName);
    }

    private void Update()
    {
        ShootJoystickInput.x = PlayerStats.JoystickInput.ShootJoystick.Horizontal;
        ShootJoystickInput.y = PlayerStats.JoystickInput.ShootJoystick.Vertical;
    }

    protected void WeaponSetUp(string assetName)
    {
        Data = Resources.Load<WeaponData>("Data/WeaponData/" + assetName);
        PlayerStats = FindObjectOfType<PlayerStats>();

        _ammoSize = Data.AmmoSize;
        CurrentAmmo = _ammoSize;

        _ammoText = FindObjectOfType<TextMeshProUGUI>();
        _ammoText.SetText(CurrentAmmo + "/" + _ammoSize);

        gameObject.AddComponent<WeaponShoot>();
        gameObject.AddComponent<WeaponAim>();
        gameObject.AddComponent<WeaponReload>();
        gameObject.AddComponent<WeaponRecharge>();
    }

    public void AmmoModifier(int ammo)
    {
        CurrentAmmo += ammo;
        CurrentAmmo = (CurrentAmmo < 0) ? 0 : CurrentAmmo;
        if (_ammoText != null)
            _ammoText.SetText(CurrentAmmo + "/" + _ammoSize);
    }

    private void OnEnable()
    {
        if(_ammoText != null)
            _ammoText.SetText(CurrentAmmo + "/" + _ammoSize);
    }
}
