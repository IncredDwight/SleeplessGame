using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    private int _currency;

    private void Start()
    {
        _currency = (PlayerPrefs.HasKey("Currency")) ? PlayerPrefs.GetInt("Currency") : 10000;
    }

    public int GetCurrency()
    {
        return _currency;
    }

    public int ChangeCurrency(int modifier)
    {
        _currency += modifier;
        _currency = (_currency < 0) ? 0 : _currency;
        PlayerPrefs.SetInt("Currency", _currency);
        return _currency;
    }
}
