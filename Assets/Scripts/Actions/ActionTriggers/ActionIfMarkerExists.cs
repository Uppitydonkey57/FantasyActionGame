using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionIfMarkerExists : MonoBehaviour
{
    public Action[] actions;

    bool hasPerformed;

    public bool doOnce;

    public MarkerManager markerManager;

    public string marker;

    private void Update()
    {
        if (markerManager.markers.Contains(marker))
        {
            if ((hasPerformed && !doOnce) || !hasPerformed)
            {
                foreach (Action action in actions) action.PerformAction();
                hasPerformed = true;
            }
        }
    }
}
