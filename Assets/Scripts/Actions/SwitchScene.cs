using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : Action
{

    public SceneChange sceneChange;

    public override void PerformAction()
    {
        sceneChange.ChangeScene();
    }
}
