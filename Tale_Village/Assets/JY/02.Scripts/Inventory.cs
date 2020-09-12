using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private void Awake()
    {
        Instance = this;
    }

    //public List<Item> items = new List<Item>();         //아이템 리스트
    
    public GameObject itemToolFactory;
    public GameObject itemFactory;
    public Transform[] slots;

    public void AddItem(Item item_)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].childCount == 0)
            {
                GameObject itemBtn;
                if (item_.itemType == Item.ItemType.Tools)
                {
                    itemBtn = Instantiate(itemToolFactory);
                    //slots[i].isEmpty = false;
                }
                else
                {
                    itemBtn = Instantiate(itemFactory);
                    //slots[i].isEmpty = false;
                }

                itemBtn.transform.parent = slots[i];
                itemBtn.transform.localPosition = Vector3.zero;
                itemBtn.GetComponent<PickUp>().item = item_;
                break;

            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
