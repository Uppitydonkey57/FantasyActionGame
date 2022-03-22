
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    //Universal

    public WeaponType weaponType;

    public enum WeaponType { Projectile, Melee, Raycast }

    public float shootWait;
    bool canShoot = true;
    public string weaponName;

    public AudioSource audioSource;
    public AudioClip[] attackSounds;
    
    //Exclusive to melee weapons
    public Vector3 attackRange;

    //Exclusive to raycast weapons
    public float rayLength = 100;

    //Exclusive to melee and raycast weapons
    public string hitTag;
    public float damage = 1f;
    public Vector3 attackOffset;
    public Color weaponColor = Color.red;

    //Exlusive to projectile weapons
    public bool multipleFirePoints;
    public Transform firePoint;
    public Transform[] firePoints;
    public bool randomPoint;

    public GameObject projectilePrefab;
    public float projectileSpeed;

    public bool useFireChance;
    public float fireChance;

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }
    
    public void Attack()
    {
        if (canShoot)
        {
            if (attackSounds != null && attackSounds.Length > 0)
            {
                audioSource.PlayOneShot(attackSounds[UnityEngine.Random.Range(0, attackSounds.Length - 1)]);
            }

            switch (weaponType) {
                case WeaponType.Melee:
                    foreach (Collider collider in Physics.OverlapBox(GetPosition(), attackRange, transform.rotation))
                    {
                        Actor inRangeActor = collider.GetComponent<Actor>();

                        if (collider.GetComponent<Actor>() != null)
                        {
                            if (Array.Exists(inRangeActor.hitTags, element => element == hitTag))
                            {
                                inRangeActor.ChangeHealthKnockback(-damage, transform.right);
                            }
                        }
                    }
                    break;
                case WeaponType.Raycast:
                    RaycastHit hit;
                    if (Physics.Raycast(GetPosition(), transform.forward, out hit, rayLength))
                    {
                        if (hit.transform != null)
                        {
                            Actor inRangeActor = hit.transform.GetComponent<Actor>();
                            Debug.Log(hit.transform.gameObject.name);
                            if (inRangeActor != null)
                            {
                                if (Array.Exists(inRangeActor.hitTags, element => element == hitTag))
                                {
                                    inRangeActor.ChangeHealthKnockback(-damage, transform.right);
                                }
                            }
                        }
                    }
                    break;

                case WeaponType.Projectile:
                    if (multipleFirePoints) {
                        if (!randomPoint)
                        {
                            foreach (Transform tempFirePoint in firePoints)
                            {
                                FireProjectile(tempFirePoint);
                            }
                        } else
                        {
                            FireProjectile(firePoints[UnityEngine.Random.Range(0, firePoints.Length - 1)]);
                        }
                    } else 
                    {
                        FireProjectile(firePoint);
                    }
                    
                    break;
            }
            StartCoroutine(ShootWait());
        }
    }

    void FireProjectile(Transform temporaryFirePoint)
    {
        float fireChanceNumber = Mathf.Round(UnityEngine.Random.Range(0, fireChance));

        if (!useFireChance || (useFireChance && fireChanceNumber == 0))
        {
            GameObject projectile = Instantiate(projectilePrefab, temporaryFirePoint.position, temporaryFirePoint.rotation);

            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            projectileRb.AddForce(projectile.transform.forward * projectileSpeed, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = weaponColor;
        
        Vector3 scale = transform.localScale;
        Transform matrixTransform = transform;
        matrixTransform.localScale = Vector3.one;
        Gizmos.matrix = matrixTransform.localToWorldMatrix;
        transform.localScale = scale;

        switch (weaponType)
        {
            case WeaponType.Melee:
                Gizmos.DrawWireCube(attackOffset, attackRange);
                break;

            case WeaponType.Raycast:
                Ray ray = new Ray(attackOffset, transform.eulerAngles);
                //Gizmos.DrawRay(ray);
                Vector3 startPos = attackOffset;
                Gizmos.DrawLine(startPos, startPos + (transform.forward * rayLength));
                break;
        }
    }

    IEnumerator ShootWait()
    {
        canShoot = false;

        yield return new WaitForSeconds(shootWait);

        canShoot = true;
    }

    public Vector3 GetPosition()
    {
        Vector3 endPos = transform.position + (transform.forward * attackOffset.z);
        endPos += transform.up * attackOffset.y;
        endPos += transform.right * attackOffset.x;
        return endPos;
    }
}