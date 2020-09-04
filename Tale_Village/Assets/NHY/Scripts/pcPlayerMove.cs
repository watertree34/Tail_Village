using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class pcPlayerMove : MonoBehaviour
{
    CharacterController cc;
    public float moveSpeed = 8; //이동속도

    public float gravity = -9.8f; //중력
    float yVelocity; //수직속도




    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();

    }

    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //입력에 따른 방향 설정
        Vector3 dir = new Vector3(h, 0, v); //(right, up, forward)

        //카메라가 바라보는 방향으로 이동하고 싶다.
        dir = Camera.main.transform.TransformDirection(dir); // 메인 카메라가 향하는 방향으로 변형하겠다!!!
                                                             //로컬->월드 좌표로-TransformDirection   다른 오브젝트들의 위치와 상호작용할때 많이 쓰임
                                                             //월드->로컬좌표는 InverseTransformDirection  


        dir.Normalize(); //정규화


        //캐릭터가 바닥에 있다면 yvelocity는 0으로 초기화하고 싶다.
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            yVelocity = 0;

        }

        //중력적용_v = v0 + at
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        //이동하고싶다
        cc.Move(dir * moveSpeed * Time.deltaTime);



    }
}
