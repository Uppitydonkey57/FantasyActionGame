using UnityEngine;

public class PlayerActor : Actor 
{
    [SerializeField] private HealthManager healthManager;

    public override float health { get { return healthManager.Hp; } set { healthManager.Hp = value; }}
}