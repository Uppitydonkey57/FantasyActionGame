using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;

    public WeaponType weaponType;

    public int damage;

    public GameObject projectile;

    public GameObject weaponModel;
}

public enum WeaponType
{
    Sword,
    Bow
}