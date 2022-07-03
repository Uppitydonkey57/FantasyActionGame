using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PauseState", menuName = "Managers/PauseState")]
public class PauseState : ScriptableObject
{
    public bool IsPaused { get { return isPaused; } }
    private bool isPaused;

    private void OnEnable()
    {
        
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Unpuase()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
}
