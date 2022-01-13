using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public int weaponId;

    private bool _isBought;
    private bool _isUsing;

    private TestShopScript _shop;

    private Slider _fireRateSlider;
    private Slider _damageSlider;
    private Slider _spreadSlider;
    private Slider _reloadTimeSlider;

    private Image _weaponImage;
    [SerializeField]
    private Sprite[] _rarityImages = new Sprite[3];
    private Image _rarityImage;

    private void Awake()
    {
        _fireRateSlider = transform.GetChild(0).GetComponent<Slider>();
        _damageSlider = transform.GetChild(1).GetComponent<Slider>();
        _spreadSlider = transform.GetChild(5).GetComponent<Slider>();
        _reloadTimeSlider = transform.GetChild(6).GetComponent<Slider>();

        _weaponImage = transform.GetChild(3).GetComponent<Image>();
        _rarityImage = transform.GetChild(4).GetComponent<Image>();

        _fireRateSlider.maxValue = 8;
        _damageSlider.maxValue = 80;
        _spreadSlider.maxValue = 50;
        _reloadTimeSlider.maxValue = 5;
        _shop = FindObjectOfType<TestShopScript>();
    }

    private void Start()
    {
        _fireRateSlider.value = 1 / _shop.WeaponsData[weaponId].FireRate;
        _damageSlider.value = _shop.WeaponsData[weaponId].Damage;
        _spreadSlider.value = _shop.WeaponsData[weaponId].Spread;
        _reloadTimeSlider.value = _shop.WeaponsData[weaponId].ReloadTime;
        _rarityImage.sprite = _rarityImages[(int)_shop.WeaponsData[weaponId].Rarity];
        GetComponent<Button>().onClick.AddListener(BuyWeapon);
    }

    private void Update()
    {
        UpdatePanel();
    }

    private void BuyWeapon()
    {
        if (!_isBought)
        {
            _shop.AddBoughtItem(weaponId);
            Debug.Log(_shop.Weapons[weaponId].name);
        }
        else
        {
            _shop.AddUsingItem(_shop.Weapons[weaponId]);
        }
    }

    public void UpdatePanel()
    {
        _isUsing = false;
        for (int i = 0; i < _shop.BoughtWeapons.Count; i++)
        {
            if (_shop.BoughtWeapons[i] == _shop.Weapons[weaponId])
                _isBought = true;
        }

        for(int i = 0; i < _shop.UsingWeapons.Count; i++)
        {
            if (_shop.UsingWeapons[i] == _shop.Weapons[weaponId])
                _isUsing = true;
        }

        if (_isBought)
        {
            transform.GetChild(2).GetComponent<Text>().text = "Sold";
            _weaponImage.color = Color.white;
        }
        else
            _weaponImage.color = Color.black;

        if (_isUsing)
            GetComponent<Image>().color = Color.red;
        else
            GetComponent<Image>().color = Color.white;
    }
}
