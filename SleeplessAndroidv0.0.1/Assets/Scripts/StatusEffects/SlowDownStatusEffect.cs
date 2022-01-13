using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownStatusEffect : StatusEffect
{
    private void OnEnable()
    {
        _statusEffectName = "SpeedReduce";
    }

    protected override void Effect()
    {
        _playerStats.MovementSpeedModifier(-_effectAmount);
    }

    protected override void EndEffect()
    {
        _playerStats.MovementSpeedModifier(_effectAmount);
    }
}
