using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAction : Action
{
    [SerializeField] private float waitTime;

    [SerializeField] private Action[] finishedAction;

    public override void PerformAction() 
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait() 
    {
        yield return new WaitForSeconds(waitTime);

        foreach (Action action in finishedAction) 
        {
            action.PerformAction();
        }
    }
}
