using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveHelper;

public class WeaponChangeSystem : MonoBehaviour
{
    private Button _button;
    private GameObject[] _weapons;
    [SerializeField]
    private List<GameObject> _usingWeapons = new List<GameObject>();
    private int _currentWeapon = 1;

    private void Start()
    {
        StartCoroutine(LateStart());
        _button = GameObject.Find("ChangeWeapon").GetComponent<Button>();
        _button.onClick.AddListener(delegate { ChangeWeapon(_currentWeapon + 1); } );
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeWeapon(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeWeapon(1);
    }

    private void ChangeWeapon(int currWeapon)
    {
        if (currWeapon > _usingWeapons.Count - 1)
            _currentWeapon = 0;
        else
            _currentWeapon = currWeapon;
        foreach(GameObject weapon in _usingWeapons)
        {
            if (_usingWeapons[_currentWeapon] == weapon)
            {
                weapon.SetActive(true);
            }
            else
            {
                weapon.SetActive(false);
                weapon.transform.position = new Vector3(0, 0, -10);
            }
        }
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        _weapons = Resources.LoadAll<GameObject>("Prefabs/Player/Weapons");

        List<GameObject> loadedWeapons = new List<GameObject>();
        ListSave.LoadList(loadedWeapons, _weapons, "UsingWeapons");
        if(loadedWeapons.Count == 0)
        {
            loadedWeapons.Add(_weapons[0]);
            loadedWeapons.Add(_weapons[1]);
        }

        for(int i = 0; i < loadedWeapons.Count; i++)
        {
            GameObject weaponClone = Instantiate(loadedWeapons[i]);
            weaponClone.SetActive(false);
            _usingWeapons.Add(weaponClone);
        }

        _usingWeapons[_currentWeapon].SetActive(true);
    }
}
