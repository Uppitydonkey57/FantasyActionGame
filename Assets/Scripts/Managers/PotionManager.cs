using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "PotionManager", menuName = "Managers/PotionManager")]
public class PotionManager : ScriptableObject {
    
    [SerializeField] private int maxPotions;
    public int MaxPotions { get { return maxPotions; } }

    [System.NonSerialized] public UnityEvent<int> potionsChanged;

    private int potions;

    public int Potions { get {return potions; } private set { potions = value; } }

    public void ChangePotions(int amount) 
    {
        potions += amount;

        if (potions > maxPotions) potions = maxPotions;

        potionsChanged.Invoke(potions);
    }

    private void OnEnable() 
    {
        ResetPotions();

        potionsChanged = new UnityEvent<int>();
    }

    public void ResetPotions() 
    {
        potions = maxPotions;
    }

}