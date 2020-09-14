using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]

public class Duck : MonoBehaviour
{

    public static Duck Instance;
    private void Awake()
    {
        Instance = this;
    }

    //플레이어가 오리를 터치하면 색깔이 바뀌고 거인이 깨어나게 하자
    public bool openCage;
    public Transform playerPoisition;
    public float minPdDis = 8;
    public GameObject cageDoor;
    float openTime=2;

    private void Update()
    {
      
        Vector3 dir = playerPoisition.position - transform.position;   //플레이어가 거위에 가까이 있을때
        float pdDis = dir.magnitude;
        print(pdDis);
        if (pdDis <= minPdDis)
        {
            UIText.Instance.UITEXT = "열쇠를 사용해서 거위를 구출하세요";


            if (PickUp.Instance.isKeyUsed)// 열쇠를 사용하면 케이지 오픈  --> 거위 깨어남, 거위주울수 있음
            {
                cageDoor.transform.position += Vector3.up * 5 * Time.deltaTime;
                openTime -= Time.deltaTime;
                if(openTime<=0)
                    openCage = true;
            }
        }
    }

}
