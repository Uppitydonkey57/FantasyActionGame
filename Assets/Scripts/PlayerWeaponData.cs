using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerWeaponData : MonoBehaviour
{
    public AnimatorController defaultAnimator;

    [Header("Sword")]

    public AnimatorOverrideController swordAnimator;
    public Vector3 swordAttackRange;
    public Vector3 swordAttackOffset;

    [Header("Bow")]

    public AnimatorOverrideController bowAnimator;
    public float projectileSpeed;
}