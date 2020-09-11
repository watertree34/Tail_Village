using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //public static PickUp Instance;
    //private void Awake()
    //{
    //    Instance = this;
    //}

    //RaycastFind Ray;

    //public Transform toolPos;
    //public GameObject slotItem;    //UI버튼
    //public GameObject Inventory_;  //인벤토리UI (인벤토리 스크립트가 달려있는 오브젝트 연결해주면 됨)
    //Inventory inven;               //생성자 위해서 

    //public bool isPickUp = false;         //주웠나 안 주웠나

    //private void Start()
    //{
    //    inven = Inventory_.GetComponent<Inventory>();
    //    //Ray = GetComponent<RaycastFind>();
    //    isPickUp = false;
    //}

    //private void Update()
    //{
    //    if (RaycastFind.Instance.isRaySearchItem == true)
    //    {
    //        PickUpItem();
    //    }

    //    if (isPickUp == true)
    //    {
    //        if (this.gameObject.tag == "tool")
    //        {
    //            //this.gameObject.transform.position = toolPos.transform.position;   //위로...날아감....
    //        }
    //    }
    //}

    //void PickUpItem()
    //{
    //    for (int i = 0; i < inven.slots.Count; i++)
    //    {
    //        if (inven.slots[i].isEmpty)
    //        {
    //            Instantiate(slotItem, inven.slots[i].slotObj.transform, false);
    //            inven.slots[i].isEmpty = false;
    //            isPickUp = true;
    //            this.gameObject.SetActive(false);
    //            if (this.gameObject.tag == "tool")
    //            {
    //                this.gameObject.transform.position = toolPos.transform.position;
    //            }
    //            break;
    //        }
    //    }
    //}

    public Item item;
    public void OnClickButton()
    {
        // pick이 뭐냐에 따라서 아이템 처리를 하고싶다.
        print(item.itemName.ToString());
        Destroy(gameObject);
        //다시 이스엠티
    }
}
