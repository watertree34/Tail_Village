using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public static PickUp Instance;
    private void Awake()
    {
        Instance = this;
    }

    public Item item;
    public bool isAxeUsed = false;

    public void OnClickButton()
    {
        // pick이 뭐냐에 따라서 아이템 처리를 하고싶다.
        print(" [" + item.itemName.ToString() + "] Click");

        //아이템이 콩이면
        if (item.itemName == "Bean")
        {
            LifeManager.Instance.LIFE += 10; // 라이프 회복
            Destroy(gameObject);
        }
        //아이템이 도끼면
        if (item.itemName == "Axe")
        {
            isAxeUsed = !isAxeUsed;
            if (isAxeUsed)
            {
                //도끼활성화
            }
        }
    }
}
