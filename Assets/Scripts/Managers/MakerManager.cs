using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;
using UnityEditor;

[CreateAssetMenu(menuName = "Managers/MarkerManager")]
public class MakerManager : ScriptableObject
{
    public List<string> markers = new List<string>();
    private static List<string> staticMarkers;

    public static UnityEvent<string> questAdded;

    private void OnEnable()
    {
        questAdded = new UnityEvent<string>();
        questAdded.AddListener(AddQuest);

        markers = new List<string>();
        staticMarkers = markers;
    }

    [YarnCommand("create_marker")]
    public static void AssignQuest(string markerName)
    {
        questAdded.Invoke(markerName);
    }

    [YarnFunction("marker_exists")]
    public static bool MarkerExists(string markerName)
    {
        foreach (string marker in staticMarkers)
        {
            if (marker == markerName)
            {
                return true;
            }
        }

        return false;
    }

    void AddQuest(string markerName)
    {
        markers.Add(markerName);
        staticMarkers = markers;
    }
}
