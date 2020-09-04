using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]

public class Duck : ItemFind  //ItemManager상속
{

    public static Duck Instance;
    private void Awake()
    {
        Instance = this;
    }

    //플레이어가 오리를 터치하면 색깔이 바뀌고 거인이 깨어나게 하자
    public bool duckTouch;
    public Transform playerTransform;
    Renderer mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>();
    }


    private void Update()
    {
        Vector3 dir = transform.position - playerTransform.position;
        float pdDis = dir.magnitude;
        if(pdDis<2)
        {
            mat.material.color = Color.red;
            duckTouch = true; //ItemManager꺼
        }
    }

}
