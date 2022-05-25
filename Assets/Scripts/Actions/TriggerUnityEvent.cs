using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerUnityEvent : Action
{
    public UnityEvent events;

    public override void PerformAction()
    {
        events.Invoke();
    }
}
