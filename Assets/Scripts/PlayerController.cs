using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerController : MonoBehaviour
{
    #region Variables
    private Rigidbody rb;

    [SerializeField] private float speed = 6f;

    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    [SerializeField] private Transform cam;

    private Vector3 velocity;

    [SerializeField] private float dialogueRange;
    [SerializeField] private LayerMask npcLayer;

    private float horizontal;
    private float vertical;

    private DialogueRunner dialogueRunner;

    public WeaponData currentWeapon;
    
    private Weapon weapon;

    private float initialRotation;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private Transform hand1;
    [SerializeField] private Transform hand2;

    private Animator animator;

    [SerializeField] private PlayerWeaponData data;

    #endregion
    
    #region Main
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        weapon = GetComponent<Weapon>();

        dialogueRunner = FindObjectOfType<DialogueRunner>();

        animator = GetComponent<Animator>();

        SetWeapon();
    }

    enum PlayerState { MOVING, ATTACKING, DIALOGUE, FROZEN }
    PlayerState state = PlayerState.MOVING;

    private void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        switch (state) 
        {
            case PlayerState.MOVING:
                
                animator.SetBool("Moving", (horizontal != 0 || vertical != 0));

                if (Input.GetButtonDown("Dialogue")) 
                {
                    RunDialogue();
                }

                if (Input.GetButtonDown("Fire1")) 
                {
                    state = PlayerState.ATTACKING;
                    Attack();
                }


                break;

            case PlayerState.DIALOGUE: 
                if (state == PlayerState.DIALOGUE) 
                {
                    if (!dialogueRunner.IsDialogueRunning) 
                    {
                        state = PlayerState.MOVING;
                    }
                }
                break;
            
            case PlayerState.FROZEN:
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (state) 
        {
            case PlayerState.MOVING:
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

                if (direction.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 30;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    rb.velocity = new Vector3((moveDir.normalized.x * speed), rb.velocity.y, (moveDir.normalized.z * speed));
                }
                break;
        } 
    }

    public void StartDialogue() 
    {
        state = PlayerState.DIALOGUE;
        animator.SetBool("Moving", false);
    }

    public void IsPlayerFrozen(bool isFrozen) 
    {
        state = isFrozen ? PlayerState.FROZEN : PlayerState.MOVING;
        animator.SetBool("Moving", false);
    }

    #endregion

    private void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }

    private void RunDialogue() 
    {
        Collider[] npc = Physics.OverlapSphere(transform.position, dialogueRange, npcLayer);

        if (npc.Length < 1) return;

        DialogueObject npcDialogue = npc[0].GetComponent<DialogueObject>();

        if (npcDialogue != null) 
        {
            npcDialogue.StartDialogue();
            StartDialogue();
        }
    }
    
    #region Combat

    void Attack() 
    {
        //make this tween later
        Vector3 mousePos = Utilities.MouseWorldPos3D(Input.mousePosition, mainCamera);
        Vector3 lookDirection = new Vector3(mousePos.x, rb.position.y, mousePos.z) - rb.position;
        Debug.Log(lookDirection);
        float angle = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
        animator.SetBool("Attacking", true);

        switch (currentWeapon.weaponType) 
        {
            case WeaponType.Sword:
                StartCoroutine(SwordAttack());
                break;

            case WeaponType.Bow:
                StartCoroutine(BowAttack());
                break;
            
            default:
                state = PlayerState.MOVING;
                break;
        }
    }

    void SetWeapon() 
    {
        switch (currentWeapon.weaponType) 
        {
            case WeaponType.Bow:
                animator.runtimeAnimatorController = data.bowAnimator;
                weapon.weaponType = Weapon.WeaponType.Projectile;
                weapon.projectilePrefab = currentWeapon.projectile;

                foreach (Transform childObject in GetComponentsInChildren<Transform>()) 
                {
                    if (childObject.name == "FirePoint") 
                    {
                        weapon.firePoint = childObject.transform;
                    }
                }
                break;

            case WeaponType.Sword:
                animator.runtimeAnimatorController = data.swordAnimator;
                weapon.weaponType = Weapon.WeaponType.Melee;
                weapon.attackRange = data.swordAttackRange;
                weapon.attackOffset = data.swordAttackOffset;
                weapon.damage = currentWeapon.damage;

                Instantiate(currentWeapon.weaponModel, hand1.position, Quaternion.identity, hand1);
                foreach (Transform childObject in GetComponentsInChildren<Transform>()) 
                {
                    if (childObject.name == "FirePoint") 
                    {
                        weapon.firePoint = childObject.transform;
                    }
                }
                break;

            default:
                animator.runtimeAnimatorController = data.defaultAnimator;
                break;
        }
    }

    IEnumerator SwordAttack() 
    {
        yield return new WaitForSeconds(0.15f); 

        weapon.Attack();

        yield return new WaitForSeconds(0.33f);

        animator.SetBool("Attacking", false);
        state = PlayerState.MOVING;
    }

    IEnumerator BowAttack() 
    {
        weapon.Attack();

        yield return new WaitForSeconds(0.2f);

        state = PlayerState.MOVING;

        yield break;
    }

    #endregion
}