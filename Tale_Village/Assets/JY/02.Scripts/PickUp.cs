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
    public bool isKeyUsed = false;
    public bool isCheeseUsed = false;

    public void OnClickButton()
    {
        // pick이 뭐냐에 따라서 아이템 처리를 하고싶다.
        print(" [" + item.itemName.ToString() + "] Click");
        
        //아이템타입이 푸드면
        if (item.itemType == Item.ItemType.Food)
        {
            //그 중에서 아이템이 콩이면
            if (item.itemName == "Bean")
            {
                LifeManager.Instance.LIFE += 5; // 라이프 회복
                Destroy(gameObject);
            }
            //그 중에서 아이템이 치즈면
            if (item.itemName == "Cheese")
            {
                isCheeseUsed = true;
                Destroy(gameObject);
            }
            //그 외의 음식들이면
            else
            {
                LifeManager.Instance.LIFE += 10; // 라이프 회복
                Destroy(gameObject);
            }
        }
        //아이템이 도끼면
        if (item.itemName == "Axe")
        {
            isAxeUsed = !isAxeUsed;
        }
        //아이템이 열쇠면
        if (item.itemName == "Key")
        {
            isKeyUsed = true;
            Destroy(gameObject);
        }
        
    }
}
