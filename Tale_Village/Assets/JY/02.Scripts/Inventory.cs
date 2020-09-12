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

    public GameObject InventoryUI;  //인벤토리UI
    bool isInvenActive = false;     //인벤활성화체크

    public List<SlotData> slots = new List<SlotData>(); //슬롯생성
    private int maxSlot = 6;                            //슬롯최대갯수
    public GameObject slotPrefab;                       //슬롯프리펩

    public List<Item> items = new List<Item>();         //아이템 리스트

    /*------------------------------------------------------------------*/
    
    public GameObject itemFactory;
    public GameObject itemToolFactory;
    
    //public Transform[] slots;

    public void AddItem(Item item_)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].isEmpty)
            {
                GameObject item;
                if (item_.itemType == Item.ItemType.Tools)
                {
                    item = Instantiate(itemToolFactory, slots[i].slotObj.transform, false);
                    slots[i].isEmpty = false;
                }
                else
                {
                    item = Instantiate(itemFactory, slots[i].slotObj.transform, false);
                    slots[i].isEmpty = false;
                }

                //item.transform.parent = slots[i];
                item.transform.localPosition = Vector3.zero;
                item.GetComponent<PickUp>().item = item_;
                break;

            }
        }
    }
    /*------------------------------------------------------------------*/

    void Start()
    {
        InventoryUI.SetActive(isInvenActive); //인벤활성/비활성화 초기설정
        for (int i = 0; i < maxSlot; i++)     //인벤토리 생성
        {
            GameObject __slot = Instantiate(slotPrefab, InventoryUI.transform, false);
            __slot.name = "Slot_" + i;
            SlotData slot = new SlotData();
            slot.isEmpty = true;
            slot.slotObj = __slot;
            slots.Add(slot);

        }
    }

    void Update()
    {
        // i키 누르면 인벤 활성/비활성화
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInvenActive = !isInvenActive;
            InventoryUI.SetActive(isInvenActive);
        }
    }
}
