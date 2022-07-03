using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ArmorManager", menuName = "Managers/ArmorManager")]
public class ArmorManager : ScriptableObject
{
    public Clothing head;
    public Clothing body;
    public Clothing pants;

    public Clothing vanityHead;
    public Clothing vanityBody;
    public Clothing vanitypants;

    public WeaponData weapon;

    public ItemCollection inventory;

    [HideInInspector] public UnityEvent equipmentChanged;

    private void OnEnable()
    {
        equipmentChanged = new UnityEvent();
    }

    public void Equip(Item item, string slot)
    {
        if (item == null) return;

        switch (slot) 
        {
            case "Head":
                head = (Clothing)item;
                break;

            case "Body":
                body = (Clothing)item;
                break;

            case "Pants":
                pants = (Clothing)item;
                break;

            case "Weapon":
                weapon = (WeaponData)item;
                break;
        }

        equipmentChanged.Invoke();
    }
}
