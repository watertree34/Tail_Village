using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public static PickUp Instance;
    public Transform weaponPos;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject slotItem;
    public GameObject Inventory_;  //인벤토리UI (인벤토리 스크립트가 달려있는 오브젝트 연결해주면 됨)
    Inventory inven; //생성자 위해서 

    private void Start()
    {
        inven = Inventory_.GetComponent<Inventory>();
    }

    public void PickUpItem()
    {
        for (int i = 0; i < inven.slots.Count; i++)
        {
            if (inven.slots[i].isEmpty)
            {
                Instantiate(slotItem, inven.slots[i].slotObj.transform, false);
                inven.slots[i].isEmpty = false;
                this.gameObject.SetActive(false);
                if (this.gameObject.tag == "weapon")
                {
                    this.gameObject.transform.position = weaponPos.transform.position;
                }
                break;
            }
        }
    }
}
