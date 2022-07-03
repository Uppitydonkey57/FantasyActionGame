using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEvent : MonoBehaviour
{
    [SerializeField] private Actor actor;
    [SerializeField] private EnemyManager manager;
    [SerializeField] private string[] tags;

    private bool hasTriggered;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((actor == null || actor.health <= 0) && !hasTriggered)
        {
            Debug.Log("Killed!");
            manager.EnemyKilled(tags);
            hasTriggered = true;
        }
    }
}
