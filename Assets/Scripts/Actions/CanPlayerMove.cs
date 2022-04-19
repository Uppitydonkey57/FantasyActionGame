using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPlayerMove : Action 
{
    private PlayerController player;

    [SerializeField] private bool canMove;

    private void Start() 
    {
        player = FindObjectOfType<PlayerController>();
    }

    public override void PerformAction()
    {
        player.IsPlayerFrozen(!canMove);
    }
}