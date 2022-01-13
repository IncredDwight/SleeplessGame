using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpeedUpStatusEffect : StatusEffect
{
    private bool _changed;

    private void OnEnable()
    {
        _statusEffectName = "TimeBuff";
    }

    protected override void Effect()
    {
        if (Time.timeScale < 3)
            _changed = true;
            Time.timeScale += _effectAmount;
    }

    protected override void EndEffect()
    {
        if(_changed)
            Time.timeScale -= _effectAmount;
    }
}
