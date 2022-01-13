using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChangeSystem : MonoBehaviour
{
    public GameObject[] weapons;
    private int _currentWeapon;

    private void Start()
    {
        weapons[0] = FindObjectOfType<DefaultWeapon>().gameObject;
        weapons[1] = FindObjectOfType<ShotGunWeapon>().gameObject;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeWeapon(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeWeapon(1);
        ShowWeapon();
    }

    private void ChangeWeapon(int currWeapon)
    {
        _currentWeapon = currWeapon;
        foreach(GameObject weapon in weapons)
        {
            if (weapons[_currentWeapon] == weapon)
            {
                weapon.SetActive(true);
            }
            else
                weapon.SetActive(false);
        }
    }

    private void ShowWeapon()
    {
       // GameObject.Find("Text").GetComponent<Text>().text = weapons[_currentWeapon].GetComponent<Weapon>()._ammo.ToString() + "/30";
    }
}
