using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "PotionManager", menuName = "Managers/PotionManager")]
public class PotionManager : ScriptableObject {
    
    [SerializeField] private int maxPotions;

    [System.NonSerialized] public UnityEvent<int> potionsChanged;

    private int potions;

    public int Potions { get {return potions; } private set { potions = value; } }

    public void ChangePotions(int amount) 
    {
        potions += amount;
        potionsChanged.Invoke(potions);
    }

    private void OnEnable() 
    {
        ResetHealth();

        potionsChanged = new UnityEvent<int>();
    }

    public void ResetHealth() 
    {
        potions = maxPotions;
    }

}