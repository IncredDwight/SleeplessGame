using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDebuffStatusEffect : StatusEffect
{
    private void OnEnable()
    {
        _statusEffectName = "TimeDebuff";
    }

    protected override void Effect()
    {
        Time.timeScale -= _effectAmount;
    }

    protected override void EndEffect()
    {
        Time.timeScale = 1;
    }
}
