using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyButton : MonoBehaviour
{
    private TestShopScript _shop;
    private Text _buttonText;
    private bool _isBought;

    public int WeaponId;

    public void Start()
    {
        _shop = FindObjectOfType<TestShopScript>();
        _buttonText = GetComponentInChildren<Text>();
        UpdateButton();
    }

    public void BuyWeapon()
    {
        /*if (!_isBought)
        {
            _shop.AddBoughtItem(_shop.Weapons[WeaponId]);
            UpdateButton();
        }
        else
        {
            _shop.AddUsingItem(_shop.Weapons[WeaponId]);
        }*/
    }

    private void UpdateButton()
    {
        for(int i = 0; i < _shop.BoughtWeapons.Count; i++)
        {
            if (i == WeaponId)
                _isBought = true;
        }

        _buttonText.text = (_isBought) ? "Sold" : "Buy";
    }
}
