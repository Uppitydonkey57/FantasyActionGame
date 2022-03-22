using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

// TODO: Add dialogue advancement system.

public class DialogueObject : MonoBehaviour
{
    [SerializeField] private string node;

    private DialogueRunner dialogueRunner;
    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    public void StartDialogue() 
    {
        if (!dialogueRunner.IsDialogueRunning) 
            dialogueRunner.StartDialogue(node); 
    }
}
