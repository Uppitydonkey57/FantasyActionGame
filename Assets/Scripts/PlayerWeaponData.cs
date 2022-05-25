using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Animations;

public class PlayerWeaponData : MonoBehaviour
{
    public RuntimeAnimatorController defaultAnimator;

    [Header("Sword")]

    public AnimatorOverrideController swordAnimator;
    public Vector3 swordAttackRange;
    public Vector3 swordAttackOffset;

    [Header("Bow")]

    public AnimatorOverrideController bowAnimator;
    public float projectileSpeed;
}