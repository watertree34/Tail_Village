using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //public static Item Instance;
    //private void Awake()
    //{
    //    Instance = this;
    //}

    public enum ItemType
    {
        Food,
        Tools,
        Etc
    }

    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
}
