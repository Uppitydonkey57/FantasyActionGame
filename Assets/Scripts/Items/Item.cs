using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Item/DefaultItem")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Texture2D itemIcon;

    public GameObject itemModel;
}