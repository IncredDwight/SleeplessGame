using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponReload : MonoBehaviour
{
    private WeaponStats _weaponStats; 

    private Slider _reloadBar;
    private Image[] _reloadBarView;
    private bool _isBarVisible;

    private bool _isReloading;

    private void OnEnable()
    {
        foreach (Image img in _reloadBarView)
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
    }

    private void Awake()
    {
        _reloadBar = GameObject.Find("ReloadBar").GetComponent<Slider>();
        _reloadBarView = _reloadBar.GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        if (_isReloading)
            Reload();
        ChangeReloadBarVisibility();
    }

    private void Reload()
    {
        _reloadBar.maxValue = _weaponStats.Data.ReloadTime;
        _reloadBar.value += Time.deltaTime;
        if (_reloadBar.value > (_reloadBar.maxValue / 100) * 70)
            _isBarVisible = false;
        if (_reloadBar.value == _reloadBar.maxValue)
        {
            _reloadBar.value = 0;
            _isReloading = false;
            _weaponStats.AmmoModifier(_weaponStats.Data.AmmoSize);
        }
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

    public void StartReload(WeaponStats stats)
    {
        _weaponStats = stats;

        _isReloading = true;
        _isBarVisible = true;
    }
}
