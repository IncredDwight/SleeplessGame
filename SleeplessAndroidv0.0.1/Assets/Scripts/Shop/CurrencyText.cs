using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyText : MonoBehaviour
{
    private Text _currency;

    private void Start()
    {
        _currency = GetComponent<Text>();
    }

    private void Update()
    {
        if(_currency != null)
            _currency.text = "" + CurrencyManager.Instance.GetCurrency();
    }
}
