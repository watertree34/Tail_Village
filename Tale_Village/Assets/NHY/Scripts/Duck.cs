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
    public Transform keyTransfom;
    
    



    private void Update()
    {
        Vector3 dir = keyTransfom.position-transform.position;   //열쇠랑 거위 사이 거리가 가까워지면 케이지 오픈
        float kdDis = dir.magnitude;
        if(kdDis<5)
        {
        
            openCage = true; 
        }
    }

}
