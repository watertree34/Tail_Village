using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrColliderClimbing : MonoBehaviour
{
    public OVRInput.Controller controller = OVRInput.Controller.None;    // ovr 컨트롤러(무슨 손인지)
    GameObject grabPoint;  //각각 손이 가지고있는 그랩포인트
    VRClimber climber;     //오르는 사람(플레이어)

    Vector3 lastPos;   // 손의 직전 위치
    public Vector3 beforeAfterDir;  //손의 직전 위치와 현재 위치의방향(직전위치-현재위치)

    float grabTime = 8;
  //  MeshRenderer handMesh;
    void Start()
    {
        lastPos = transform.position;   //손의 위치 판정을 위해 lastPos에 시작할때 손 위치를 저장한다

        //handMesh = GetComponentInChildren<MeshRenderer>();
        //handMesh.enabled = true;
    }
    private void FixedUpdate()
    {
        lastPos = transform.position;   //손의 위치 판정을 위해 FixedUpdate에서 매 프레임 lastPos에 손 위치를 저장한다
    }
    private void Update()
    {
        if (grabPoint)  //업데이트에서는 그랩포인트가 있으면 그랩함수를 실행함
        {
            UIText.Instance.UITEXT = "그립 버튼(중지 손가락)을 누르며 물체를 잡으세요";
            UIText.Instance.uiText.enabled = true;
            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, controller))  // 트리거 버튼을 누르고있으면
            {
                Grab();
                UIText.Instance.UITEXT = (int)(grabTime) + "초 안에 다른것을 잡지 않거나 그립버튼에서 손가락을 떼면 손이 물체에서 떨어집니다";
                UIText.Instance.uiText.enabled = true;
                grabTime -= Time.deltaTime;
                if (grabTime <= 0)
                    NoGrab();
            }
            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))   //트리거 버튼에서 손을 떼면
            {
                NoGrab();
            }

        }


    }
    private void LateUpdate()
    {
        beforeAfterDir = lastPos - transform.position;   //update작업을 끝낸 lateupdate에서 lastPos에 저장된 손 위치와 현재 손 위치를 비교한다
                                                         //-손의 이동방향과 반대방향인(몸이 위로 올라가려면 손을 아래로 내리므로) 직전위치-나중위치를 한다
                                                         //-이것은 vrclimber에서 사용된다
    }


    void Grab()   // 그랩포인트 잡기
    {
        

        climber.SetHand(this);  //손 셋팅-방금 잡은 손을 플레이어에 정보를 전달해서 손이 움직이면 플레이어는 그 반대방향으로 움직이게 함(손은 어차피 플레이어 따라다님)

        //손 투명하게 하기
        //handMesh.enabled=false;
        //transform.parent = grabPoint.transform; //손 모양만 그랩포인트를 부모로 해서 
        //transform.localPosition = Vector3.forward * 0.5f; //forward 0.5f 떨어진곳으로 손을 위치시킨다
    }

    void NoGrab()
    {
        if (grabTime <= 0)   // 제한시간 다돼서 호출된 경우 다시 초기화
        {
            grabTime = 8;
        }
        climber.ClearHand();  //climber에서 clearhand함수 실행(movescript작동)
        grabPoint = null;   //그랩포인트도 초기화


        //손 다시 불투명하게
       // handMesh.enabled = true;
        //transform.parent = null;   //손 모양만 붙어있는거 떨어뜨리고 초기화
    }



    private void OnTriggerEnter(Collider other)
    {
        // 손이 감지충돌 될때 현재 그랩포인트가 없고
        if (grabPoint == null)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("GrabPoint"))  // 오브젝트가 그랩포인트 레이어이면
            {
                grabPoint = other.gameObject; //그랩 포인트 저장
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("GrabPoint"))
            grabPoint = null; //그랩 포인트 삭제
    }

}
