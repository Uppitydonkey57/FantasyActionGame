using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	#region Variables
	private Rigidbody rb;

	[SerializeField] private float speed = 6f;

	[SerializeField] private float turnSmoothTime = 0.1f;
	private float turnSmoothVelocity;

	[SerializeField] private float dialogueRange;
	[SerializeField] private LayerMask npcLayer;

	private float horizontal;
	private float vertical;

	private DialogueRunner dialogueRunner;

	private WeaponData currentWeapon;
	
	private Weapon weapon;

	private float initialRotation;

	[Tooltip("This variable must be set for the player to be able to properly take mouse inputs.")]
	[SerializeField] private Camera mainCamera;

	[SerializeField] private Transform hand1;
	[SerializeField] private Transform hand2;

	private Animator animator;

	[SerializeField] private PotionManager potionManager;
	[SerializeField] private HealthManager healthManager;
	[SerializeField] private InputManager inputManager;

	private Controls controls;

	private Actor actor;

	private float moveAngle;

	[SerializeField] private AudioSource source;

	[SerializeField] private AudioClip potionSound;

	[SerializeField] private ArmorManager armorManager;

	public Transform headPoint;

	private Quaternion handRotation;

	[SerializeField] private PauseState pauseState;

	#endregion
	
	#region Main
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		
		weapon = GetComponent<Weapon>();

		dialogueRunner = FindObjectOfType<DialogueRunner>();

		animator = GetComponent<Animator>();

		actor = GetComponent<Actor>();

		SetWeapon();

		RefreshClothing();

		armorManager.equipmentChanged.AddListener(ResetEquipment);

		handRotation = hand1.rotation;
	}

	private void Awake() 
	{
		controls = new Controls();
		
		controls.Player.Attack.performed += ctx => 
		{
			if (state == PlayerState.MOVING && !CanTalk() && !pauseState.IsPaused) 
			{
				state = PlayerState.ATTACKING;
				Attack();
			}
		};

		controls.Player.Dialogue.performed += ctx => 
		{
			if (state == PlayerState.MOVING) 
			{
				RunDialogue();
			}
		};

		controls.Player.Potion.performed += ctx => 
		{
			if (state == PlayerState.MOVING && healthManager.Hp > 0) 
			{
				UseHealthPotion();
			}
		};
	}

	private void OnEnable() 
	{
		controls.Enable();
	}

	private void OnDisable() 
	{
		controls.Disable();
	}

	enum PlayerState { MOVING, ATTACKING, DIALOGUE, FROZEN }
	PlayerState state = PlayerState.MOVING;

	private void Update() {
		Vector2 move = controls.Player.Move.ReadValue<Vector2>();
		horizontal = move.x;
		vertical = move.y;


		switch (state) 
		{
			case PlayerState.MOVING:
				
				if (animator.runtimeAnimatorController != null)
					animator.SetBool("Moving", (horizontal != 0 || vertical != 0));

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
					float moveAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
					transform.rotation = Quaternion.Euler(0f, moveAngle, 0f);

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

	bool CanTalk() 
	{
		Collider[] npc = Physics.OverlapSphere(transform.position, dialogueRange, npcLayer);

		if (npc.Length < 1) return false;

		DialogueObject npcDialogue = npc[0].GetComponent<DialogueObject>();

		if (npcDialogue != null) 
		{
			return true;
		}

		return false;
	}

	private void UseHealthPotion() 
	{
		if (potionManager.Potions > 0 && actor.health < actor.maxHealth) 
		{
			source.PlayOneShot(potionSound);
			potionManager.ChangePotions(-1);
			actor.ChangeHealth(actor.maxHealth);
		}
	}

	private void RefreshClothing()
	{
		headPoint.DeleteChildren();

		Clothing headClothing = armorManager.vanityHead;

		if (armorManager.vanityHead == null)
		{
			if (armorManager.head != null)
			{
				headClothing = armorManager.head;		
			}
		}

		if (headClothing != null)
			Instantiate(headClothing.itemModel, headPoint.transform.position, headPoint.transform.rotation, headPoint);
	}
	
	private void ResetEquipment()
    {
		RefreshClothing();
		SetWeapon();
    }

	#region Combat

	void Attack() 
	{
		//make this tween later
		Vector3 mousePos = Utilities.MouseWorldPos3D(Mouse.current.position.ReadValue(), mainCamera);

		float angle = 0;

		if (inputManager.Device == InputManager.InputDevice.Keyboard) 
		{
			Vector3 lookDirection = new Vector3(mousePos.x, rb.position.y, mousePos.z) - rb.position;
			angle = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg;
		} 
		else if (inputManager.Device == InputManager.InputDevice.Controller) 
		{
			angle = transform.eulerAngles.y;
		}
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
		animator.SetBool("Attacking", true);

		switch (currentWeapon.weaponType) 
		{
			case WeaponType.Melee:
				StartCoroutine(SwordAttack());
				break;

			case WeaponType.Ranged:
				StartCoroutine(BowAttack());
				break;
			
			default:
				state = PlayerState.MOVING;
				break;
		}
	}

	void SetWeapon() 
	{
		currentWeapon = armorManager.weapon;

		hand1.DeleteChildren();

		animator.runtimeAnimatorController = currentWeapon.animator;
		Instantiate(currentWeapon.itemModel, hand1.position, hand1.transform.parent.rotation * currentWeapon.itemModel.transform.rotation, hand1);
		switch (currentWeapon.weaponType) 
		{
			case WeaponType.Ranged:
				weapon.weaponType = Weapon.WeaponType.Projectile;
				weapon.projectilePrefab = currentWeapon.projectile;
				weapon.projectileSpeed = currentWeapon.projectileSpeed;

				foreach (Transform childObject in GetComponentsInChildren<Transform>()) 
				{
					if (childObject.name == "FirePoint") 
					{
						weapon.firePoint = childObject.transform;
					}
				}
				break;

			case WeaponType.Melee:
				weapon.weaponType = Weapon.WeaponType.Melee;
				weapon.attackRange = currentWeapon.range;
				weapon.attackOffset = currentWeapon.offset;
				weapon.damage = currentWeapon.damage;

				foreach (Transform childObject in GetComponentsInChildren<Transform>()) 
				{
					if (childObject.name == "FirePoint") 
					{
						weapon.firePoint = childObject.transform;
					}
				}
				break;

			default:
				//@TODO: FIX THIS LINE
				//animator.runtimeAnimatorController = data.defaultAnimator;
				break;
		}
	}

	IEnumerator SwordAttack() 
	{
		yield return new WaitForSeconds(0.15f); 

		weapon.Attack();

		yield return new WaitForSeconds(0.45f);

		state = PlayerState.MOVING;

		animator.SetBool("Attacking", false);

		
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