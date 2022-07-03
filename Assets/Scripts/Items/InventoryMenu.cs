using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField] private ItemCollection inventory;

    private string selectedType;

    [SerializeField] private GameObject selectionBoxHolder;
    [SerializeField] private GameObject[] selectionBoxes;

    //    public enum OpenType { Weapon, Head, Body }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Item item in GetItemList<WeaponData>(inventory))
        {
            Debug.Log(item.name);
        }
    }

    public void OpenSelection(string type)
    {
        selectedType = type;

        List<Item> itemList = new List<Item>();

        switch (selectedType)
        {
            case "Head":
                List<Item> fullItemList = GetItemList<Clothing>(inventory);
                
                foreach (Item item in fullItemList.ToArray())
                {
                    if (((Clothing)item).bodyPart == Clothing.BodyPart.Hat)
                    {
                        itemList.Add(item);
                        Debug.Log(item.name);
                    }
                }
                break;

            case "Weapon":
                fullItemList = GetItemList<WeaponData>(inventory);

                foreach (Item item in fullItemList.ToArray())
                {
                    itemList.Add(item);
                    Debug.Log(item.name);
                }
                break;

            default: return;

        }

        if (itemList.Count > 0)
        {
            for (int i = 0; i < selectionBoxes.Length; i++)
            {
                if (i < itemList.Count && itemList[i] != null)
                {
                    selectionBoxes[i].SetActive(true);
                    Debug.Log("ITEM AMOUNT: " + itemList.Count.ToString() + " ITEM: " + itemList[i]);
                    selectionBoxes[i].GetComponent<InventorySlot>().SetItem(itemList[i], selectedType);
                } else
                {
                    selectionBoxes[i].SetActive(false);
                }
            }
        }
    }

    private List<Item> GetItemList<T>(ItemCollection collection)
    {
        List<Item> items = new List<Item>();

        foreach (Item item in collection.Items)
        {
            if (item != null)
            {
                if (item is T)
                {
                    items.Add(item);
                }
            }
        }

        return items;
    }

    public void HideSelections()
    {
        foreach (GameObject box in selectionBoxes)
        {
            box.SetActive(false);
        }
    }
}
