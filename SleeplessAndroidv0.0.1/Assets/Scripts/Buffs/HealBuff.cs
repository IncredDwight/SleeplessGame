
public class HealBuff : Buffs
{
    private void Start()
    {
        _buffAmount = 40;
        _buffTime = 5;
    }

    protected override void Buff()
    {
        _playerStats.Heal(_buffAmount/2);
    }

    protected override void EndBuff()
    {
        _playerStats.Heal(_buffAmount / 2);
    }
}
