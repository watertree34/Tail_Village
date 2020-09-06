using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;
    private void Awake()
    {
        Instance = this;
    }

    public List<Item> itemDB = new List<Item>();
}
