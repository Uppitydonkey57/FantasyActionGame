using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : StateMachineBehaviour
{
    [Tooltip("Leave empty to affect the object the animator is attached to")]
    [SerializeField] private string objectName;

    [SerializeField] private bool isActive;

    private enum RunTime { Start, Exit, Update }
    [SerializeField] private RunTime activationPart;

    private GameObject activationObject;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (Transform child in animator.transform)
        {
            if (child.gameObject.name == objectName)
            {
                activationObject = child.gameObject;
            }
        }

        if (string.IsNullOrEmpty(objectName))
        {
            activationObject = animator.gameObject;
        }

        if (activationPart == RunTime.Start)
        {
            activationObject.SetActive(isActive);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (activationPart == RunTime.Update)
        {
            activationObject.SetActive(isActive);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (activationPart == RunTime.Exit)
        {
            activationObject.SetActive(isActive);
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
