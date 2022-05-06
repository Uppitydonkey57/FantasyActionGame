using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionIfDead : MonoBehaviour
{
    public Action[] actions;

    public bool doOnce;

    bool hasPerformed;

    public GameObject[] triggerObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((hasPerformed && !doOnce) || !hasPerformed)
            foreach (GameObject triggerObject in triggerObjects)
            {
                if (triggerObject != null)
                    return;
            }

            foreach (Action action in actions) action.PerformAction();
                hasPerformed = true;
    }
}
