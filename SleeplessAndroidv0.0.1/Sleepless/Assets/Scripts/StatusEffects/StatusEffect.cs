using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffect : MonoBehaviour
{
    private float _time;
    protected float _effectAmount;
    private bool _isEffects;
    private Image _img;

    private Sprite _icon;
    protected string _statusEffectName;

    public PlayerStats _playerStats;

    private void Awake()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
    }
    

    private void Update()
    {
        if (_time > 0)
        {
            if(!_isEffects)ApplyEffect();
            _time -= Time.deltaTime;
        }
        else if (_time <= 0 && _isEffects)
        {
            EndEffect();
            Destroy(_img.gameObject);
            Destroy(this);
            _isEffects = false;
        }
    }

    protected void ApplyEffect()
    {
        _isEffects = true;
        _img = Instantiate(new GameObject().AddComponent<Image>(), GameObject.Find("StatusEffect").transform);
         Effect();
        _icon = Resources.Load<Sprite>("Sprites/UI/StatusEffects/" + _statusEffectName);
        _img.sprite = _icon;
    }

    protected virtual void Effect()
    {

    }

    protected virtual void EndEffect()
    {

    }

    public void StatusEffectSetUp(float time, float effectAmount)
    {
        _time = time;
        _effectAmount = effectAmount;
        ApplyEffect();
    }
}
