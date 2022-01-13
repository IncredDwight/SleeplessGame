using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _slider;

    private bool _isChanging;
    private float _targetValue;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = _slider.maxValue;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))SetHealthBar(60);
        if (_isChanging)
            ChangeValue(_targetValue);
        _slider.transform.GetChild(0).GetComponent<Image>().color = new Color(Color.yellow.r, Color.yellow.g * _slider.value/100-0.1f, Color.yellow.b);
    }

    private void ChangeValue(float amount)
    {
        _slider.value = Mathf.MoveTowards(_slider.value, amount, Time.deltaTime * 35);
        if (_slider.value == amount)
            _isChanging = false;
    }

    public void SetHealthBar(float targetValue1)
    {
        _targetValue = targetValue1;
        _isChanging = true;
    }

}
