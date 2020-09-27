
using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.PlayerLoop;

public class VR_ray_PlayerPos : MonoBehaviour
{
    VR_ray_Climing currentHand = null;

    public Transform leftHand;
    public Transform rightHand;
    CharacterController cc;
    bool handMove;
    Vector3 handMoveDir;
    Collider playerCol;

    OVRPlayerController moveScript;

    float currentTime = 0;
    float warpTime = 0.5f;


    public static VR_ray_PlayerPos Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    void Start()
    {
        moveScript = GetComponent<OVRPlayerController>();
        moveScript.enabled = true;
        cc = GetComponent<CharacterController>();
        playerCol = GetComponent<CapsuleCollider>();
    }

    //그랩포인트 앞으로 이동하기(워프로)
    IEnumerator MovePosition()
    {
        currentTime = 0;
        currentTime += Time.deltaTime;

        while (currentTime < warpTime)
        {
            if (currentHand != null)
            {
                //transform.position = Vector3.Lerp(transform.position, (currentHand.grabPoint.position + currentHand.grabPoint.forward * 5), currentTime / warpTime);   // 타겟으로 러프이동
                transform.position = Vector3.Lerp(transform.position, currentHand.playerHandPoint.position, currentTime / warpTime);   // 타겟으로 러프이동

                print("이동");
                handMove = true;
                yield return null;
            }
            //transform.position = currentHand.grabPoint.position + currentHand.grabPoint.forward * 5;


            else
            {
                print("currentHand가 없습니다");
                yield return null;
            }
        }
    }

    //손 스크립트를 통해 손 받아오기
    public void SetHand(VR_ray_Climing hand)
    {
        if (currentHand)
        {
            currentHand = null;

        }
        currentHand = hand;      //새로운 손을 현재 손에 갱신저장
        moveScript.enabled = false;   //이동 스크립트는 끄기
        cc.enabled = false;  // 캐릭터 콜라이더도 끄기\
        playerCol.enabled = false;
        print("저장 ");
        StartCoroutine(MovePosition());
    }

    //손을 떼면
    public void ClearHand()  //그립버튼에서 손을 떼거나 제한시간이 지나면
    {

        currentHand = null;  //현재 손 초기화
        moveScript.enabled = true;  //이동 가능
        cc.enabled = true;  // 캐릭터 콜라이더 켜기
        playerCol.enabled = true;  // 캡슐 콜라이더도 켜기
    }
}