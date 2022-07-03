using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clothing", menuName = "Item/Clothing")]
public class Clothing : Item
{
    public enum BodyPart { Pants, Shirt, Hat }
    public BodyPart bodyPart;
    public int defense;

}
