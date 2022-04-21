using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

// TODO: Add dialogue advancement system.

public class DialogueObject : MonoBehaviour
{
    [SerializeField] private string node;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float dialogueRange;

    [SerializeField] private GameObject buttonPrompt;

    private DialogueRunner dialogueRunner;

    private bool canTalk = true;

    private bool finishedTalking = true;

    [SerializeField] private float dialogueWaitTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    public void StartDialogue() 
    {
        if (!dialogueRunner.IsDialogueRunning && canTalk) 
        {
            dialogueRunner.StartDialogue(node); 
            finishedTalking = false;
        }
    }

    private void Update() 
    {
        Collider[] isPlayer = Physics.OverlapSphere(transform.position, dialogueRange, playerLayer);

        if (isPlayer.Length > 0 && !dialogueRunner.IsDialogueRunning) 
        {
            buttonPrompt.SetActive(true);
        } else 
        {
            buttonPrompt.SetActive(false);
        }

        if (!dialogueRunner.IsDialogueRunning && !finishedTalking)
        {
            finishedTalking = true;
            StartCoroutine(DialogueWait());
        }
    }

    IEnumerator DialogueWait() 
    {
        canTalk = false;

        yield return new WaitForSeconds(dialogueWaitTime);

        canTalk = true;
    }
}
