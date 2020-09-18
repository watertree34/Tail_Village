using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCliming : MonoBehaviour
{
    LayerMask grabPointLayer;
    LayerMask spiderLayer;
    Renderer grabMat;
   
    public Transform handPoint;   //granpoint 위치
    public Transform playerHandPoint;//granpoint 플레이어 손 위치
    Transform grabPoint;
    bool click;  //암벽 클릭
   
    float grabTime = 8;  // 잡고있는 최대시간
    // Start is called before the first frame update
    void Start()
    {
        grabPointLayer = LayerMask.NameToLayer("GranPoint");
        spiderLayer = LayerMask.NameToLayer("Spider");
      
    }

    // Update is called once per frame
    void Update()
    {
        
        //////////////////////클라이밍////////////////////////////

        //손(마우스)
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        
        //거미 마우스 포인트(손)이 닿았을때
        if (Physics.SphereCast(ray, 0.5f, out hit, 10f, 1 << spiderLayer)) //만약 grabPoint가 마우스 위치의 레이에 검출되면
        {
            LifeManager.Instance.LIFE -= 0.1f; //플레이어 라이프 감소
        }

        //pc용 클라이밍
        if (Physics.SphereCast(ray, 1f, out hit, 5f, 1 << grabPointLayer)) //만약 grabPoint가 마우스 위치의 레이에 검출되면
        {
            //파란색으로 색을 바꾸고
            grabMat = hit.transform.gameObject.GetComponent<Renderer>();
            grabMat.material.color = Color.blue;

            //그립   버튼을 누르면
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))  // vr
            {
               /* if(VRPlayerPos.Instance.grab)
                {
                    return;
                }*/

                VRPlayerPos.Instance.grab = true;
                grabPoint = hit.transform;  //grabPoint 에 위치저장
                VRPlayerPos.Instance.MoveTargetPoint(hit.transform.position);
                grabTime = 8;
                click = true;

                print("잡았다!!!!!!!!!!!!!!");
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

            UIText.Instance.UITEXT = (int)(grabTime) + "초 안에 다른것을 잡지 않으면 손이 떨어집니다. \n 손을 임의로 떨어뜨리고 싶으면 스페이스바를 누르세요";
            UIText.Instance.uiText.enabled = true;
            //초록으로 바뀐 후 손의 위치가 grabPoint 위치로 이동한다
            grabMat.material.color = Color.green;
            handPoint.position = grabPoint.position;
            handPoint.forward = grabPoint.forward;
            transform.position = Vector3.Lerp(transform.position, playerHandPoint.position, Time.deltaTime * 6);   // 타겟으로 러프이동


            //키를 누르면 떨어지기
            /*if (!(OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger)))
            {
                grabTime = 0;
            }*/
        }
        else
        {
             //제힌시간 지나거나 스페이스바 누르면 켜짐
            VRPlayerPos.Instance.grab = false;
        }
    }
}
