using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityBuff : Buffs
{
    private void Start()
    {
        _buffAmount = 0;
        _buffTime = 5;
    }

    protected override void Buff()
    {
        _playerStats.DamageImmunity = true;
    }

    protected override void EndBuff()
    {
        _playerStats.DamageImmunity = false;
    }
}
