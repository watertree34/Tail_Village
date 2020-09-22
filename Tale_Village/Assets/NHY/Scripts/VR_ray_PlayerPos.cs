
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class VR_ray_PlayerPos : MonoBehaviour
{
    VR_ray_Climing currentHand = null;

    public Transform leftHand;
    public Transform rightHand;

    //public bool grab = false;

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
    OVRPlayerController moveScript;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<OVRPlayerController>();
        moveScript.enabled = true;
    }


    public void MoveTargetPoint(Vector3 targetPos)
    {
        if (currentHand)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 6);   // 타겟으로 러프이동

        }

    }

    public void SetHand(VR_ray_Climing hand)
    {
        if (currentHand)       //만약 현재 저장된 손이 있으면 
            currentHand = null;  //없애고
        currentHand = hand;      //새로운 손을 현재 손에 갱신저장
        moveScript.enabled = false;   //이동 스크립트는 끄기
    }

    public void ClearHand()  //그립버튼에서 손을 떼거나 제한시간이 지나면
    {

        currentHand = null;  //현재 손 초기화
        moveScript.enabled = true;  //이동 가능
    }
}
