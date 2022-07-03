using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "HealthManager", menuName = "Managers/HealthManager", order = 0)]
public class HealthManager : ScriptableObject {
    
    [SerializeField] private float maxHp;

    [System.NonSerialized] public UnityEvent<float> healthChanged;

    private float hp;

    public float Hp { get { return hp; } set 
    { 
        hp = value; 
        healthChanged.Invoke(hp);
    } }

    private void OnEnable() 
    {
        ResetHealth();

        healthChanged = new UnityEvent<float>();
    }

    public void ResetHealth() 
    {
        hp = maxHp;
    }

}