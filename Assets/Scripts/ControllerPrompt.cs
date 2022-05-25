using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPrompt : MonoBehaviour
{
    [SerializeField] private InputManager manager;

    [SerializeField] private GameObject keyboardObject;
    [SerializeField] private GameObject controllerObject;

    // Update is called once per frame
    void Update()
    {
        if (manager.Device == InputManager.InputDevice.Controller) 
        {
            controllerObject?.SetActive(true);
            keyboardObject?.SetActive(false);
        }        

        if (manager.Device == InputManager.InputDevice.Keyboard) 
        {
            controllerObject?.SetActive(false);
            keyboardObject?.SetActive(true);
        }        
    }
}
