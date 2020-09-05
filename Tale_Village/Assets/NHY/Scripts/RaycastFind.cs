using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFind : MonoBehaviour
{
    //플레이어가 아이템 발견했을때-흰색으로 색깔 바뀌기   ==> 알파때 쉐이더 바뀌는걸로 바꾸기
    //아이템-거위, 콩, 도끼, 음식-c카를 누르면 인벤토리로

    //가이드-나비 - 나비를 보면 ui띄우기

    //grabpoint - 마우스 클릭한 포인트의 위치로 손이동하기


    LayerMask itemLayer;
    LayerMask butterflyLayer;
    LayerMask grabPointLayer;

    Renderer itemMat;
    Renderer grabMat;

   

    public Transform handPoint;
    public Transform playerHandPoint;
    Transform grabPoint;
    bool click;
    pcPlayerMove moveScript;
    float grabTime = 8;
    // Start is called before the first frame update
    void Start()
    {
        itemLayer = LayerMask.NameToLayer("Item");
        butterflyLayer = LayerMask.NameToLayer("Butterfly");
        grabPointLayer = LayerMask.NameToLayer("GranPoint");
        moveScript = gameObject.GetComponent<pcPlayerMove>();
        moveScript.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(transform.position);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 2f, out hit, 1f, 1 << itemLayer))  //만약 아이템이 레이에 검출되면
        {
            //색깔을 흰색으로 바꾸고
            itemMat = hit.transform.gameObject.GetComponent<Renderer>();
            itemMat.material.color = Color.white;

            //키를 누르면 인벤토리에 저장==>주연

        }

        if (Physics.SphereCast(ray, 2f, out hit, 5f, 1 << butterflyLayer)) //만약 나비가 레이에 검출되면
        {
            //색깔을 흰색으로 바꾸고
            itemMat = hit.transform.gameObject.GetComponent<Renderer>();
            itemMat.material.color = Color.white;
            //나비에 3d글자 출력
            Butterfly.Instance.look = true;

           
        }else
            Butterfly.Instance.look = false;



        //pc용 클라이밍
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseHit;

        if (Physics.SphereCast(mouseRay, 1f, out mouseHit,5f, 1 << grabPointLayer)) //만약 grabPoint가 마우스 위치의 레이에 검출되면
        {
            //파란색으로 색을 바꾸고
            grabMat = mouseHit.transform.gameObject.GetComponent<Renderer>();
            grabMat.material.color = Color.blue;


            //클릭을 누르면 
            if (Input.GetButtonDown("Fire1"))
            {
                print("클릭");
                grabPoint = mouseHit.transform;  //grabPoint 에 위치저장
                grabPoint.forward = mouseHit.transform.gameObject.transform.right;
                click = true;
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
            
            //초록으로 바뀐 후 손의 위치가 grabPoint 위치로 이동한다
            grabMat.material.color = Color.green;
            handPoint.position = grabPoint.position;
            transform.position = playerHandPoint.position;

            moveScript.enabled = false;   //movescipt는 꺼둠
        }
        else
        {

            moveScript.enabled = true;  //제힌시간 지나면 켜짐
        }
       
    }
}