using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : Action
{
    [Header("Make sure to switch gm scene switch to normal scene switch")]

    public string sceneName;

    public bool useBuildOrder;

    public override void PerformAction()
    {
        Time.timeScale = 1f;

        if (!useBuildOrder)
        {
            //Replace if using custom scene loader
            SceneManager.LoadScene(sceneName);
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
