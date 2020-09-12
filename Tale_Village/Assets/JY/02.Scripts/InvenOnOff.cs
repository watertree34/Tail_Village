using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenOnOff : MonoBehaviour
{
    public GameObject InventoryUI;  //인벤토리UI
    bool isInvenActive = false;     //인벤활성화체크

    void Start()
    {
        InventoryUI.SetActive(isInvenActive); //인벤활성/비활성화 초기설정
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
