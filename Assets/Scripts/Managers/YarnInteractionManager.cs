using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/YarnInteractionManager")]
public class YarnInteractionManager : ScriptableObject
{
    public MarkerManager markerManager;
    public QuestManager questManager;
}
