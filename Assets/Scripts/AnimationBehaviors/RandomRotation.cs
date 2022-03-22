using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;
using Random = UnityEngine.Random;

public class RandomRotation : StateMachineBehaviour
{
    [SerializeField] private Vector2 maxOffsetX = new Vector2(-180, 180);
    [SerializeField] private Vector2 maxOffsetY = new Vector2(-180, 180);
    [SerializeField] private Vector2 maxOffsetZ = new Vector2(-180, 180);

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeedOffset;

    [SerializeField] private bool killTweenOnExit = true;

    [SerializeField] private bool useParentRotation;
    private GameObject parentObject;

    private Vector3 initialRotation;


    private Quaternion startRotation;
    private Quaternion goalRotation;

    private float percentage = 0f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        initialRotation = animator.transform.eulerAngles;

        if (useParentRotation)
        {
            parentObject = animator.GetComponentInParent<Transform>().gameObject;
        }
        
        goalRotation = GetRotation(animator);
        Debug.Log(animator.gameObject.name + " " + goalRotation);
    }

    private Vector3 parentRotation { get { return (useParentRotation) ? parentObject.transform.eulerAngles : Vector3.zero; } }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (percentage < 1f) 
        {
            percentage += speed * Time.deltaTime;
            animator.transform.rotation = Quaternion.Lerp(startRotation, goalRotation * Quaternion.Euler(parentRotation), percentage);
            //animator.transform.rotation *= Quaternion.Euler(parentRotation);
        } else 
        {
            //Debug.Log(animator.transform.eulerAngles + " " + goalRotation);
            goalRotation = GetRotation(animator);
        }
    }

    // private void CreateRotation(Animator anim)
    // {
    //     Vector3 rotation = new Vector3(Random.Range(maxOffsetX.x, maxOffsetX.y), Random.Range(maxOffsetY.x, maxOffsetY.y), Random.Range(maxOffsetZ.x, maxOffsetZ.y));
    //     rotation += initialRotation;
    //     float currentSpeed = speed + Random.Range(-speed, speed);
    //     sequence.Append(anim.transform.DORotate(rotation, currentSpeed));
    // }

    private Quaternion GetRotation(Animator animator) 
    {
        startRotation = animator.transform.rotation;
        percentage = 0;
        return Quaternion.Euler(Random.Range(maxOffsetX.x, maxOffsetX.y), Random.Range(maxOffsetY.x, maxOffsetY.y), Random.Range(maxOffsetZ.x, maxOffsetZ.y)); 
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {

    // }

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

//new code
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;
using Random = UnityEngine.Random;

public class RandomRotation : StateMachineBehaviour
{
    [SerializeField] private Vector2 maxOffsetX = new Vector2(-180, 180);
    [SerializeField] private Vector2 maxOffsetY = new Vector2(-180, 180);
    [SerializeField] private Vector2 maxOffsetZ = new Vector2(-180, 180);

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeedOffset;

    [SerializeField] private bool killTweenOnExit = true;

    [SerializeField] private bool useParentRotation;
    private GameObject parentObject;

    private Vector3 initialRotation;


    private Quaternion startRotation;
    private Quaternion goalRotation;

    private float percentage = 0f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        initialRotation = animator.transform.eulerAngles;

        if (useParentRotation)
        {
            parentObject = animator.GetComponentInParent<Transform>().gameObject;
        }
        
        goalRotation = GetRotation(animator);
        Debug.Log(animator.gameObject.name + " " + goalRotation);
    }

    private Vector3 parentRotation { get { return (useParentRotation) ? parentObject.transform.eulerAngles : Vector3.zero; } }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (percentage < 1f) 
        {
            percentage += speed * Time.deltaTime;
            Quaternion currentRot = Quaternion.Lerp(startRotation * Quaternion.Euler(parentRotation), goalRotation * Quaternion.Euler(parentRotation), percentage);
            Vector3 rotationAmount = currentRot.eulerAngles - animator.transform.eulerAngles;
            animator.transform.RotateAround(rotationAmount, Space.World, parentObject);
            // animator.transform.rotation *= Quaternion.Euler(parentRotation);
        } else 
        {
            Debug.Log(animator.transform.eulerAngles + " " + goalRotation);
            goalRotation = GetRotation(animator);
        }
    }

    // private void CreateRotation(Animator anim)
    // {
    //     Vector3 rotation = new Vector3(Random.Range(maxOffsetX.x, maxOffsetX.y), Random.Range(maxOffsetY.x, maxOffsetY.y), Random.Range(maxOffsetZ.x, maxOffsetZ.y));
    //     rotation += initialRotation;
    //     float currentSpeed = speed + Random.Range(-speed, speed);
    //     sequence.Append(anim.transform.DORotate(rotation, currentSpeed));
    // }

    private Quaternion GetRotation(Animator animator) 
    {
        startRotation = animator.transform.rotation;
        percentage = 0;
        return Quaternion.Euler(Random.Range(maxOffsetX.x, maxOffsetX.y), Random.Range(maxOffsetY.x, maxOffsetY.y), Random.Range(maxOffsetZ.x, maxOffsetZ.y)); 
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {

    // }

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

*/