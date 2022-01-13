using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveHelper;

public class TestShopScript : MonoBehaviour
{
    public Text CurrencyText;
    private CurrencyManager _currencyManager;
    private GameObject _chosenWeaponsList;
    private List<Transform> _weaponIcons = new List<Transform>();

    public GameObject[] Weapons { get; private set; }
    public List<WeaponData> WeaponsData = new List<WeaponData>();

    private GameObject _panelPrefab;

    public List<GameObject> BoughtWeapons = new List<GameObject>();
    public List<GameObject> UsingWeapons = new List<GameObject>();

    private void Awake()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();

        //CurrencyText = GameObject.Find("CurrencyText").GetComponent<Text>();
        //CurrencyText.text = "" + _currencyManager.GetCurrency();

        _chosenWeaponsList = GameObject.Find("ChosenWeapon");

        SetUp();
    }

    private void Update()
    {
        
    }

    private void SetUp()
    {
        Weapons = Resources.LoadAll<GameObject>("Prefabs/Player/Weapons");
        _panelPrefab = Resources.Load<GameObject>("Prefabs/UI/Panel");

        for (int i = 0; i < 7; i++)
        {
            WeaponsData.Add(Resources.Load<WeaponData>("Data/WeaponData/" + Weapons[i].name));
            Text priceText = _panelPrefab.transform.GetChild(2).GetComponent<Text>();
            priceText.text = "Price: " + WeaponsData[i].Price;
            AddShopItem(Weapons[i]);
        }

        ListSave.LoadList(BoughtWeapons, Weapons, "BoughtWeapons");
        ListSave.LoadList(UsingWeapons, Weapons, "UsingWeapons");
    }

    private void AddShopItem(GameObject item)
    {
        int itemID = System.Array.IndexOf(Weapons, item);

        GameObject panelClone = Instantiate(_panelPrefab, GameObject.Find("ShopContent").transform);
        panelClone.GetComponent<Panel>().weaponId = itemID;

        //Button button = panelClone.transform.GetChild(3).GetComponent<Button>();
        //button.GetComponent<ShopBuyButton>().WeaponId = itemID;

        _weaponIcons.Add(panelClone.transform.GetChild(3));
        _weaponIcons[itemID].GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;

        float iconWidth = item.transform.GetComponent<SpriteRenderer>().bounds.size.x;
        float iconHeight = item.transform.GetComponent<SpriteRenderer>().bounds.size.y;
        _weaponIcons[itemID].GetComponent<RectTransform>().sizeDelta = new Vector2(iconWidth * 45, iconHeight * 45);
    }

    public void AddBoughtItem(int itemId)
    {
        int itemPrice = WeaponsData[itemId].Price;
        CurrencyManager currency = CurrencyManager.Instance;

        if (CurrencyManager.Instance.GetCurrency() >= itemPrice)
        {
            BoughtWeapons.Add(Weapons[itemId]);
            CurrencyManager.Instance.ChangeCurrency(WeaponsData[itemId].Price * -1);
            ListSave.SaveList(BoughtWeapons, Weapons, "BoughtWeapons");
        }
    }

    public void AddUsingItem(GameObject item)
    {
        if (!UsingWeapons.Contains(item))
        {
            if (UsingWeapons.Count >= 2)
            {
                UsingWeapons.RemoveAt(0);
            }
            UsingWeapons.Add(item);
            ListSave.SaveList(UsingWeapons, Weapons, "UsingWeapons");
        }
    }

}
