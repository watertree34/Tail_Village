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
    
    public GameObject slotItem_Axe;
    public GameObject slotItem_Bean;
    public GameObject slotItem_Key;
    public GameObject slotItem_Duck;
    public Transform[] slots;

    public void AddItem(Item item_)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].childCount == 0)
            {
                GameObject itemBtn;
                if (item_.itemName == "Axe")
                {
                    itemBtn = Instantiate(slotItem_Axe);
                }
                else if (item_.itemName == "Bean")
                {
                    itemBtn = Instantiate(slotItem_Bean);
                }
                else if (item_.itemName == "Key")
                {
                    itemBtn = Instantiate(slotItem_Key);
                }
                else if (item_.itemName == "Duck")
                {
                    itemBtn = Instantiate(slotItem_Duck);
                }
                else
                {
                    itemBtn = null;
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
