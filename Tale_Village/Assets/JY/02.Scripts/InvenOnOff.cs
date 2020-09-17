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
        // Y키 누르면 인벤 활성/비활성화
        //VR
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            isInvenActive = !isInvenActive;
            InventoryUI.SetActive(isInvenActive);
        }
        //PC
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            isInvenActive = !isInvenActive;
            InventoryUI.SetActive(isInvenActive);
        }
    }
}
