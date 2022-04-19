using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirection : StateMachineBehaviour
{
    [SerializeField] private float distanceMin;
    [SerializeField] private float distanceMax;
    private float distance;

    [SerializeField] private float waitTimeMin;
    [SerializeField] private float waitTimeMax;
    private float waitTime;

    [SerializeField] private float moveSpeed;

    Rigidbody rb;

    private float direction;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       StartPath();

       rb = animator.GetComponent<Rigidbody>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (distance > 0) 
        {
            distance -= Time.deltaTime;
            Vector2 directionAmount = Utilities.GetAngleDistance(direction) * moveSpeed;
            rb.velocity = new Vector3(directionAmount.x, rb.velocity.y, directionAmount.y);
        }
        else if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        } else 
        {
            StartPath();
        }
    }

    private void StartPath() 
    {
        direction = Random.Range(0, 360);
        distance = Random.Range(distanceMin, distanceMax);
        waitTime = Random.Range(waitTimeMin, waitTimeMax);
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
