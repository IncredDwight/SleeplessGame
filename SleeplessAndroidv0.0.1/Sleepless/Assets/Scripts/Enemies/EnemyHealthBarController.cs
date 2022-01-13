using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarController : MonoBehaviour
{
    private Slider _barSlider;
    private Image[] _barView;

    private Transform _targetPos;

    private float _targetValue;
    private bool _visibility;

    private void OnEnable()
    {
        _barSlider = GetComponent<Slider>();
        _barView = GetComponentsInChildren<Image>();
        foreach (Image img in _barView)
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
        _targetValue = _barSlider.maxValue;
    }

    private void Update()
    {
        if (_targetPos != null)
            transform.position = _targetPos.position + Vector3.up * 1.5f;
        if (_targetValue != 0)
            _barSlider.value = Mathf.MoveTowards(_barSlider.value, _targetValue, Time.deltaTime * 35);
        ChangeVisibility();
    }

    public void SetHealthBarPos(Transform pos)
    {
        _targetPos = pos;
    }

    public void SetHealth(float health)
    {
        _targetValue = health;
    }

    public void SetMaxValue(float maxHealth)
    {
        _barSlider.maxValue = maxHealth;
        SetHealth(maxHealth);
    }

    public void SetVisiblity(bool isVisible)
    {
        _visibility = isVisible;
    }

    public void ChangeVisibility()
    {
        foreach (Image img in _barView)
        {
            if (!_visibility)
                img.color = Color.LerpUnclamped(img.color, new Color(img.color.r, img.color.g, img.color.b, 0), Time.deltaTime * 2.5f);
            else
                img.color = Color.LerpUnclamped(img.color, new Color(img.color.r, img.color.g, img.color.b, 1), Time.deltaTime * 2.5f);
        }
    }
}
