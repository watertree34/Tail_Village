using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_ray_Climing : MonoBehaviour
{

    public OVRInput.Controller controller = OVRInput.Controller.None;
    LayerMask grabPointLayer;
    LayerMask spiderLayer;
    Renderer grabMat;

    //public Transform handPoint;   //granpoint 위치
    //public Transform playerHandPoint;//granpoint 플레이어 손 위치
    public Transform grabPoint;
    bool click;  //암벽 클릭

    float grabTime = 8;  // 잡고있는 최대시간

    Vector3 nowForward;

    public Vector3 beforeAfterDir;

    void Start()
    {
        grabPointLayer = LayerMask.NameToLayer("GrabPoint");
        spiderLayer = LayerMask.NameToLayer("Spider");

    }


    void Update()
    {

        //////////////////////클라이밍////////////////////////////

        //손(마우스)
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //거미 마우스 포인트(손)이 닿았을때
        if (Physics.SphereCast(ray, 0.5f, out hit, 5f, 1 << spiderLayer)) //만약 grabPoint가 마우스 위치의 레이에 검출되면
        {
            LifeManager.Instance.LIFE -= 0.1f; //플레이어 라이프 감소
        }

        //클라이밍
        if (Physics.Raycast(ray, out hit, 7f, 1 << grabPointLayer)) //만약 grabPoint가 레이에 검출되면
        {
            UIText.Instance.UITEXT = "오르기를 하려면 레이저를 쏘며 \n그립버튼(중지 손가락)을 누르세요";
            //파란색으로 색을 바꾸고
            grabMat = hit.transform.gameObject.GetComponent<Renderer>();
            grabMat.material.color = Color.blue;

            //그립   버튼을 누르면
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))  // vr
            {


                grabPoint.position = hit.transform.position;  //grabPoint 에 위치저장
                grabPoint.forward = hit.normal;
                grabPoint.position = grabPoint.position + grabPoint.forward * 3;
               // nowForward = hit.normal;
                grabTime = 8;
                click = true;

                print("잡았다!");
            }


        }


        if (grabTime <= 0)   // 잡고있는거 제한시간
        {
            grabTime = 8;
            click = false;
        }
        if (click)
        {

            grabTime -= Time.deltaTime;

            UIText.Instance.UITEXT = "떨어지고 싶지 않으면\n" + (int)(grabTime) + "초 안에 다른것을 잡으세요";
            UIText.Instance.uiText.enabled = true;
            //초록으로 바뀐 후 손의 위치가 grabPoint 위치로 이동한다
            grabMat.material.color = Color.green;
            //handPoint.position = grabPoint.position;
            //handPoint.forward = nowForward;


            VR_ray_PlayerPos.Instance.SetHand(this);



            //* 버튼에서 두 손 다 떼면 떨어지기 *
            if (!(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)))
            {
                grabTime = 0;
            }
        }
        else
        {
            VR_ray_PlayerPos.Instance.ClearHand();  // 플레이어 손 클리어
        }
    }
}

