using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEditor;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon")]
public class WeaponData : Item
{
    public WeaponType weaponType;

    public int damage;

    public GameObject projectile;

    public AnimatorOverrideController animator;

    public float projectileSpeed;

    public Vector3 range;
    public Vector3 offset;

    public float knockbackMultiplier = 1f;

}

public enum WeaponType
{
    Melee,
    Ranged,
    Raycast,
    Throwing
}