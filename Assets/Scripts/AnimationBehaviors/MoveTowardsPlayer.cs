using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : StateMachineBehaviour
{
    GameObject player;

    public float moveSpeed;

    public bool findObjectWithTag;
    public string findTag;

    Rigidbody rb;

    public bool freezeX;
    public bool freezeY;
    public bool freezeZ;

    public bool useVelocity;

    public Vector3 offset;

    public Vector3 velocitySpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<PlayerController>().gameObject;

        if (findObjectWithTag)
        {
            player = GameObject.FindGameObjectWithTag(findTag);
        }

        rb = animator.GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = animator.GetComponentInParent<Rigidbody>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null)
        {
            if (!useVelocity)
            {
                Vector3 movePosition = Vector3.MoveTowards(rb.position, player.transform.position + offset, moveSpeed * Time.deltaTime);
                rb.MovePosition(new Vector3(freezeX ? rb.transform.position.x : movePosition.x, freezeY ? rb.position.y : movePosition.y, freezeZ ? rb.position.z : movePosition.z));
            } else
            {
                Vector3 direction = new Vector3();
                direction.x = player.transform.position.x + offset.x < animator.transform.position.x ? -1 : 1;
                direction.y = player.transform.position.y + offset.y < animator.transform.position.y ? -1 : 1;
                direction.z = player.transform.position.z + offset.z < animator.transform.position.z ? -1 : 1;
                Vector3 velocityMovement = new Vector3(freezeX ? rb.velocity.x : velocitySpeed.x * direction.x, freezeY ? rb.velocity.y : velocitySpeed.y * direction.y, freezeZ ? rb.velocity.z : velocitySpeed.z * direction.z);
                rb.velocity = velocityMovement;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
