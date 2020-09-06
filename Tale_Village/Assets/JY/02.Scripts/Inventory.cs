using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryUI;  //인벤토리UI
    bool isInvenActive = false;     //인벤활성화체크

    public List<SlotData> slots = new List<SlotData>(); //슬롯생성
    private int maxSlot = 6;                            //슬롯최대갯수
    public GameObject slotPrefab;                       //슬롯프리펩

    public List<Item> items = new List<Item>();         //아이템 리스트

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
