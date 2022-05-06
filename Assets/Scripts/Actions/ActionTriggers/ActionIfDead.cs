using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionIfDestroyed : MonoBehaviour
{
    public Action[] actions;

    public bool doOnce;

    bool hasPerformed;

    public Actor[] triggerObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((hasPerformed && !doOnce) || !hasPerformed)
            foreach (Actor triggerObject in triggerObjects)
            {
                if (triggerObject != null)
                    if (triggerObject.health > 0)
                        return;
            }

            foreach (Action action in actions) action.PerformAction();
                hasPerformed = true;
    }
}
