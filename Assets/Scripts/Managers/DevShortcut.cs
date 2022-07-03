using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "DevShortcut", menuName = "Managers/DevShortcut", order = 0)]
public class DevShortcut : ScriptableObject 
{
    public InputAction quitAction;

    public InputAction resetAction;

    public SceneChange baseScene;

    public bool devShortcutsEnabled;

    private void OnEnable() 
    {
        quitAction.performed += Quit;
        resetAction.performed += ResetGame;

        quitAction.Enable();
        resetAction.Enable();
    }

    private void OnDisable() 
    {
        quitAction.Disable();
        resetAction.Disable();        
    }

    void Quit(InputAction.CallbackContext context) 
    {
        if (devShortcutsEnabled)
            Application.Quit();
    }

    void ResetGame(InputAction.CallbackContext context) 
    {
        if (devShortcutsEnabled)
            baseScene.ChangeScene();
    }
}