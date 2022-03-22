using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : StateMachineBehaviour
{
    GameObject player;
    public bool findObjectWithTag;
    public string findTag;

    Rigidbody rb;

    Transform transform;

    public bool resetOnExit;

    public float offset;

    public bool freezeX;
    public bool freezeY;
    public bool freezeZ;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = animator.GetComponentInParent<Rigidbody>();
        }

        transform = animator.transform;

        if (rb == null)
        {
            transform = animator.GetComponentInParent<Transform>();
        }

        player = FindObjectOfType<PlayerController>().gameObject;

        if (findObjectWithTag)
        {
            player = GameObject.FindGameObjectWithTag(findTag);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null)
        {
            float xLookPos = freezeX ? transform.position.x : player.transform.position.x;
            float yLookPos = freezeY ? transform.position.y : player.transform.position.y;
            float zLookPos = freezeZ ? transform.position.z : player.transform.position.z;

            Vector3 direction = new Vector3(xLookPos, yLookPos, zLookPos);

            transform.LookAt(direction);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (resetOnExit)
        {
            rb.rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

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
