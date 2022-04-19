using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class StartDialogue : Action
{
    private PlayerController player;
    private DialogueRunner runner;

    [SerializeField] private string node;

    public Action[] actionsOnComplete;

    bool hasStarted;



    private void Start() 
    {
        player = FindObjectOfType<PlayerController>();

        runner = FindObjectOfType<DialogueRunner>();
    }

    public override void PerformAction()
    {
        if (!runner.IsDialogueRunning) 
        {
            runner.StartDialogue(node);  
            player.StartDialogue();
            hasStarted = true;
        }
    }

    private void Update() 
    {
        if (runner.IsDialogueRunning && hasStarted) 
        {
            foreach (Action action in actionsOnComplete) 
            {
                action.PerformAction();
            }
        }
    }
}
