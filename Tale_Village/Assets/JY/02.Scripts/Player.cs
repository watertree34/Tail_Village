using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController cc = null;
    public float moveSpeed = 5; //이동속도
    public float rotSpeed = 3; //회전속도
    public float gravity = -20; //중력
    float yVelocity; //수직속도
    float mouseX; //마우스 X좌표
    float mouseY; //마우스 Y좌표

    public GameObject leftHand; //왼손
    public GameObject rightHand; //오른손

    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
        mouseX = Input.GetAxis("Mouse X"); //마우스 X좌표
        mouseY = Input.GetAxis("Mouse Y"); //마우스 Y좌표
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //입력에 따른 방향 설정
        Vector3 dir = new Vector3(h, 0, v); //(right, up, forward)
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
        cc.Move(dir * moveSpeed * Time.deltaTime); //로컬방향 이동 수정필요

        //마우스 움직임으로 손 제어
        //float rotX = mouseX * rotSpeed * Mathf.Deg2Rad;
        //float rotY = mouseY * rotSpeed * Mathf.Deg2Rad;
        //rightHand.transform.Rotate(Vector3.up, -rotX);
        //rightHand.transform.Rotate(Vector3.right, rotY);

    }
}
