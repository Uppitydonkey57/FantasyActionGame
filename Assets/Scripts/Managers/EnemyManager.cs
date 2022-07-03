using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Managers/EnemyManager")]
public class EnemyManager : ScriptableObject
{
    [HideInInspector] public UnityEvent<string[]> enemyKilled;

    // Start is called before the first frame update
    void OnEnable()
    {
        enemyKilled = new UnityEvent<string[]>();
    }

    public void EnemyKilled(string[] tags)
    {
        enemyKilled.Invoke(tags);
    }
}
