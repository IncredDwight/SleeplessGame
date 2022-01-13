
public class SpeedBuff : Buffs
{
    private void Start()
    {
        _buffAmount = 3;
        _buffTime = 10;
    }

    protected override void Buff()
    {
        _playerStats.MovementSpeedModifier(_buffAmount);
    }

    protected override void EndBuff()
    {
        _playerStats.MovementSpeedModifier(-_buffAmount);
    }
}
