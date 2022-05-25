using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionOnButtonPressed : MonoBehaviour
{
    [SerializeField] private InputAction inputAction;

    [SerializeField] private Action[] actions;

    void OnEnable() 
    {
        inputAction.Enable();

        inputAction.performed += _ => { foreach (Action action in actions) action.PerformAction(); };
    }

    void OnDisable() 
    {
        inputAction.Disable();
    }
}
