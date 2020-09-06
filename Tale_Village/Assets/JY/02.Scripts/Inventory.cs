using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryUI;
    bool isInvenActive = false;

    void Start()
    {
        InventoryUI.SetActive(isInvenActive);
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
