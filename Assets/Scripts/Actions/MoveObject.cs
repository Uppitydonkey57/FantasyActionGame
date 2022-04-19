using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveObject : Action
{
    public GameObject moveObject;

    public float moveTime;

    public Vector3 movePosition;
    Vector3 position;

    public bool moveBack;

    public Transform returnTransform;

    public Action[] actionsOnComplete;

    private void Awake()
    {
        position = transform.position + movePosition;
    }

    public override void PerformAction()
    {
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(moveObject.transform.DOMove(new Vector3(position.x, position.y, position.z), moveTime));

        if (moveBack)
        {          
            if (returnTransform == null)
            {
                //This variable is meant to get the original position of the move object.
                Vector3 movePos = (Vector3)(transform.InverseTransformPoint(transform.position) - movePosition);

                sequence.Append(moveObject.transform.DOMove(movePos, moveTime));
            } else
            {
                sequence.Append(moveObject.transform.DOMove(returnTransform.position, moveTime));
            }
        }

        StartCoroutine(ActivateCompleteAction());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(movePosition + transform.position, 0.3f);
    }

    IEnumerator ActivateCompleteAction()
    {
        if (moveBack)
        {
            yield return new WaitForSeconds(moveTime * 2);
        } else
        {
            yield return new WaitForSeconds(moveTime);
        }

        foreach (Action action in actionsOnComplete)
        {
            action.PerformAction();
        }
    }


}
