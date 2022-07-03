using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputManager", menuName = "Managers/InputManager", order = 0)]
public class InputManager : ScriptableObject 
{
    public enum InputDevice { Keyboard, Controller }

    private InputDevice device;
    [HideInInspector] public InputDevice Device { get { return device; } }

    public InputAction controllerInput;
    public InputAction keyboardInput;
        
    void OnEnable() 
    {
        controllerInput.Enable();
        keyboardInput.Enable();

        controllerInput.performed += UsingController;
        keyboardInput.performed += UsingKeyboard;
    }

    void OnDisable() 
    {
        controllerInput.Disable();
        keyboardInput.Disable();
    }

    void UsingController(InputAction.CallbackContext context) 
    {
        device = InputDevice.Controller;

        Cursor.visible = false;
    } 

    void UsingKeyboard(InputAction.CallbackContext context) 
    {
        device = InputDevice.Keyboard;

        Cursor.visible = true;
    }
}