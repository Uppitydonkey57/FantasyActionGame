using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SceneSwapManagement", menuName = "Managers/SceneSwapManagement", order = 0)]
public class SceneSwapManagement : ScriptableObject 
{
    UnityEvent sceneChanged;
}