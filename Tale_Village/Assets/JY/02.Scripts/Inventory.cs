using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else
        {
            DestroyImmediate(this);
        }
    }

    //public List<Item> items = new List<Item>();         //아이템 리스트
    
    public GameObject slotItem_Axe;
    public GameObject slotItem_Bean;
    public GameObject slotItem_Key;
    public GameObject slotItem_Duck;
    public GameObject slotItem_Cheese;
    public GameObject slotItem_Food;
    public Transform[] slots;

    public bool isAxeUsed = false;
    public bool isKeyUsed = false;
    public bool isCheeseUsed = false;

    public void AddItem(Item item_)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].childCount == 0)
            {
                GameObject itemBtn;
                //--------------------아이템이 도끼라면--------------------
                if (item_.itemName == "Axe")
                {
                    itemBtn = Instantiate(slotItem_Axe);
                }
                //------------------아이템 타입이 푸드라면------------------
                else if (item_.itemType == Item.ItemType.Food)
                {
                    if (item_.itemName == "Bean")
                    {
                        itemBtn = Instantiate(slotItem_Bean);
                    }
                    else if (item_.itemName == "Cheese")
                    {
                        itemBtn = Instantiate(slotItem_Cheese);
                    }
                    else
                    {
                        itemBtn = Instantiate(slotItem_Food);
                    }
                }
                //--------------------아이템이 열쇠라면--------------------
                else if (item_.itemName == "Key")
                {
                    itemBtn = Instantiate(slotItem_Key);
                }
                //--------------------아이템이 거위라면--------------------
                else if (item_.itemName == "Duck")
                {
                    itemBtn = Instantiate(slotItem_Duck);
                }
                //--------------------------이외--------------------------
                else
                {
                    itemBtn = null;
                }

                itemBtn.transform.parent = slots[i];
                itemBtn.transform.localPosition = Vector3.zero;
                itemBtn.transform.localRotation = Quaternion.Euler(0, 0, 0);
                itemBtn.transform.localScale = new Vector3(1, 1, 1);
                itemBtn.GetComponent<PickUp>().item = item_;
                break;

            }
        }
    }
}
