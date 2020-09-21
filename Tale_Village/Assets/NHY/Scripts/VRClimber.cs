using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRClimber : MonoBehaviour
{
    VrColliderClimbing currentHand=null;  //현재 그랩포인트 잡은 손(플레이어가 움직일 기준)
    pcPlayerMove moveScript;  //그냥 이동(중력포함)하는 플레이어의 이동 스크립트
    CharacterController cc;

    void Start()
    {
        moveScript = GetComponent<pcPlayerMove>();
        moveScript.enabled = true;
        cc = GetComponent<CharacterController>();
    }


    void Update()
    {
       // Vector3 moveDir = new Vector3(0, 0, 0);   //move벡터는(0,0,0) // 안되면 Vector3 move=transform.position;
        if (currentHand)  // 만약 grab한 손이 있으면
        {
           
          //  moveDir = currentHand.beforeAfterDir * 45;  //나중위치=현재 위치+이동할 방향*속도<- (0,0,0)=(0,0,0)+beforAfterDir*45
            cc.Move(currentHand.beforeAfterDir.normalized*45 * Time.deltaTime);    //손이 움직인 위치로 이동
           // transform.up = currentHand.beforeAfterDir;  // 방향 이상하거나 충돌될경우
        }
      
    }

    public void SetHand(VrColliderClimbing hand)
    {
        if (currentHand)       //만약 현재 저장된 손이 있으면 
            currentHand = null;  //없애고
        currentHand = hand;      //새로운 손을 현재 손에 갱신저장
        moveScript.enabled = false;   //이동 스크립트는 끄기
    }

    public void ClearHand()  //그립버튼에서 손을 떼거나 제한시간이 지나면
    {
        if(currentHand.beforeAfterDir.magnitude<15)  // 15 미터 이상 떨어졌으면 라이프 깎기
        {
            LifeManager.Instance.LIFE -= 10;
            UIText.Instance.UITEXT = "추락으로 데미지를 입었습니다";
            UIText.Instance.uiText.enabled = true;
        }
        currentHand = null;  //현재 손 초기화
        moveScript.enabled = true;  //이동 가능
    }
}
