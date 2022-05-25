using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ControllerText : MonoBehaviour
{
    [SerializeField] private InputManager manager;

    public TextMeshProUGUI text;

    public string keyboardText;

    [Tooltip("The way you can set specific controls is by typing in the general button inbetween a **.")]
    public string gamepadText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.Device == InputManager.InputDevice.Controller) 
        {
            string finalText = "";

            string[] textList = gamepadText.Split('*');

            bool isOnControl = false;

            foreach (string part in textList)
            {
                if (!isOnControl)
                {
                    finalText += part;
                } else
                {
                    finalText += Gamepad.current[part].shortDisplayName;
                }

                isOnControl = !isOnControl;
            }

            Debug.Log(finalText);

            text.text = finalText;
        } else 
        {
            text.text = keyboardText;
        }
    }
}
