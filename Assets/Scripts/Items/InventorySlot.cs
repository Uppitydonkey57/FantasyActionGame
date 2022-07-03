using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector] public Item item;
    public RawImage image;
    private string equipmentSlot;

    public ArmorManager equipment;

    public void SetItem(Item setItem, string slot)
    {
        item = setItem;

        image.texture = item.itemIcon;

        equipmentSlot = slot;
    }

    public void EquipItem()
    {
        equipment.Equip(item, equipmentSlot);
    }
}
