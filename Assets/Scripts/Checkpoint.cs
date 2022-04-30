using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Save() 
    {
        //saveManager.CreateSave();
    }
}
