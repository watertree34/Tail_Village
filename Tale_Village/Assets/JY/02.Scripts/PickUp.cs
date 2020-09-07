using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public static PickUp Instance;
    public Transform toolPos;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject slotItem;    //UI버튼
    public GameObject Inventory_;  //인벤토리UI (인벤토리 스크립트가 달려있는 오브젝트 연결해주면 됨)
    Inventory inven;               //생성자 위해서 

    bool isPickUp = false;         //주웠나 안 주웠나

    private void Start()
    {
        inven = Inventory_.GetComponent<Inventory>();
    }

    private void Update()
    {
        if (isPickUp == true)
        {
            if (this.gameObject.tag == "tool")
            {
                //this.gameObject.transform.position = toolPos.transform.position;   //위로...날아감....
            }
        }
    }

    public void PickUpItem()
    {
        for (int i = 0; i < inven.slots.Count; i++)
        {
            if (inven.slots[i].isEmpty)
            {
                Instantiate(slotItem, inven.slots[i].slotObj.transform, false);
                inven.slots[i].isEmpty = false;
                isPickUp = true;
                this.gameObject.SetActive(false);
                if (this.gameObject.tag == "tool")
                {
                    this.gameObject.transform.position = toolPos.transform.position;
                }
                break;
            }
        }
    }
}
