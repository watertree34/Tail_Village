using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Food,
        Tools,
        Etc
    }

    public ItemType itemType;
    public string itemName;
}
