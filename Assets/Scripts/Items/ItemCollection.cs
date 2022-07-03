using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ItemCollection", menuName = "ItemCollection")]
public class ItemCollection : ScriptableObject
{
    [SerializeField] private Item[] items;
    public Item[] Items { get { return items; } }

    List<Item> currentItems;

    [HideInInspector] public UnityEvent<Item> itemObtained;

    public void OnEnable()
    {
        currentItems = items.ToList();

        itemObtained = new UnityEvent<Item>();
    }

    public void AddCollection(ItemCollection collection)
    {
        foreach (Item item in collection.items)
        {
            currentItems.Add(item);
        }
    }

    public void AddItem(Item item)
    {
        currentItems.Add(item);
        itemObtained.Invoke(item);
    }

    public bool HasItem(Item item) 
    {
        return items.Contains(item); 
    }
}
