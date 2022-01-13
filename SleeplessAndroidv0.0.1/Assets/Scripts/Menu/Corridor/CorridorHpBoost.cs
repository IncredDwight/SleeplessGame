using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorHpBoost : MonoBehaviour
{
    private SpriteRenderer _color;

    private float _price = 300;
    private float _hpBoostAmount = 10;

    private void Start()
    {
        _color = GetComponent<SpriteRenderer>();
    }

    private void OnMouseUp()
    {
        _color.color = Color.white;
        BoostHealth(_hpBoostAmount);
    }

    private void OnMouseDown()
    {
        _color.color = Color.gray;
    }

    private void BoostHealth(float boost)
    {
        if (PlayerPrefs.HasKey("PlayerHpBoost"))
        {
            float _currentBoost = PlayerPrefs.GetFloat("PlayerHpBoost");
            PlayerPrefs.SetFloat("PlayerHpBoost", _currentBoost + _hpBoostAmount);
        }
        else
            PlayerPrefs.SetFloat("PlayerHpBoost", _hpBoostAmount);
    }
}
