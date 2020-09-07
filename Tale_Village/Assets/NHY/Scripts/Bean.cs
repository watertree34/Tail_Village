using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

//콩-도끼에 닿으면 콩나무에서 떨어지면서 밑으로 떨어진다 
public class Bean : MonoBehaviour
{
    Rigidbody rb;

    //public GameObject BeanPrefab;
    public bool isBeanActive = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        isBeanActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "tool")
        {
            rb.useGravity = true;
            transform.parent = null;
        }

        //임시코드 일단 플레이어와 닿으면 템먹는 걸로ㅠㅜ
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
            PickUp.Instance.PickUpItem();
        }
    }

    public void OnClickButton()
    {
        isBeanActive = !isBeanActive;
        //BeanPrefab.SetActive(isBeanActive);
    }

    //만약 인벤토리에서 사용되면
    //if(인벤토리에서 사용되면)
    // {
    //     LifeManager.Instance.LIFE += 5; // 라이프 회복
    // }
}
