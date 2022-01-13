using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "Weapon Data", order = 51)]
public class WeaponData : ScriptableObject
{
    public float FireRate;
    public float Damage;
    public float Spread;
    public bool Multiple;

    public int AmmoSize;
    public float ReloadTime;

    public WeaponRarity Rarity;

    public int Price;

    public GameObject Projectile;


}
