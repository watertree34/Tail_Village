using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFind : MonoBehaviour
{
    //플레이어가 아이템 발견했을때-흰색으로 색깔 바뀌기   ==> 알파때 쉐이더 바뀌는걸로 바꾸기
    //아이템-거위, 콩, 도끼, 음식-i키를 누르면 인벤토리로


    //치즈-쥐 유인
    //열쇠-거위 탈출

    //가이드-나비 - 나비를 보면 ui띄우기

    //grabpoint - 마우스 클릭한 포인트의 위치로 손이동하기
    //grabpoint-rope-암벽 끝에서 벗어날수있게

    public static RaycastFind Instance;  //싱글톤
    private void Awake()
    {
        Instance = this;
    }

    LayerMask itemLayer;
    LayerMask axeLayer;
    LayerMask duckLayer;
    LayerMask keyLayer;
    LayerMask butterflyLayer;
    LayerMask cheeseLayer;
    LayerMask grabPointLayer;
    LayerMask spiderLayer;


    /// 암벽등반에 필요한 요소들
    Renderer grabMat;
    public Transform toolPos;  // 도구 위치
    public Transform handPoint;   //granpoint 위치
    public Transform playerHandPoint;//granpoint 플레이어 손 위치
    Transform grabPoint;
    bool click;  //암벽 클릭
    pcPlayerMove moveScript;  //움직이는 스크립트
    float grabTime = 8;  // 잡고있는 최대시간

   
    public bool isRaySearchItem = false; 


    public GameObject cheeseObj;  // 치즈
    bool cheesebool;
    public bool mouseGo;

    public GameObject keyObj;  // 열쇠
    bool keybool;

    void Start()
    {
        itemLayer = LayerMask.NameToLayer("Item");
        axeLayer = LayerMask.NameToLayer("Axe");
        duckLayer = LayerMask.NameToLayer("Duck");
        keyLayer = LayerMask.NameToLayer("Key");
        butterflyLayer = LayerMask.NameToLayer("Butterfly");
        cheeseLayer = LayerMask.NameToLayer("Cheese");
        grabPointLayer = LayerMask.NameToLayer("GranPoint");
        spiderLayer = LayerMask.NameToLayer("Spider");

        moveScript = gameObject.GetComponent<pcPlayerMove>();
        moveScript.enabled = true;

        isRaySearchItem = false;
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 5f);


        /////////////아이템////////////
        if (Physics.SphereCast(ray, 3f, out hit, 3f, 1 << itemLayer))  //만약 아이템이 레이에 검출되면
        {

            //ui띄우기
            UIText.Instance.UITEXT = "아이템을 주우려면 스페이스바를 누르세요";


            //키를 누르면 인벤토리에 저장
            if (Input.GetButtonDown("Jump"))
            {
                Item item = hit.transform.GetComponent<Item>();
                if (item != null)
                {
                    Inventory.Instance.AddItem(item);
                }
                //인벤토리 안될경우-바로 아이템먹기->라이프 회복
                //이걸 쓰려면 도끼는 item레이어말고 다른레이어로 바꿔서 따로 지정해줘야함
                
                Destroy(hit.transform.gameObject, 1);
            }

        }
        else
            UIText.Instance.UITEXT = "";


        ////////////////도끼/////////////////
        if (Physics.SphereCast(ray, 3f, out hit, 3f, 1 << axeLayer))  //만약 도끼가  레이에 검출되면
        {

            //ui띄우기
            UIText.Instance.UITEXT = "도끼를 주우려면 스페이스바를 누르세요";


            //키를 누르면 인벤토리에 저장
            if (Input.GetButtonDown("Jump"))
            {  //인벤토리 될경우-
                //인벤토리에 저장되는 함수 호출
                Item item = hit.transform.GetComponent<Item>();
                if (item != null)
                {
                    Inventory.Instance.AddItem(item);
                }
                hit.transform.gameObject.SetActive(false);
            }

        }


        /////////거위//////////////////
        if (Duck.Instance.openCage)
        {
            if (Physics.SphereCast(ray, 3f, out hit, 3f, 1 << duckLayer))  //만약 아이템이 레이에 검출되면
            {
                //ui띄우기
                UIText.Instance.UITEXT = "거위를 주우려면 스페이스바를 누르세요";


                //키를 누르면 인벤토리에 저장
                if (Input.GetButtonDown("Jump"))
                {  //인벤토리 될경우-
                   //인벤토리에 저장되는 함수 호출
                    Item item = hit.transform.GetComponent<Item>();
                    if (item != null)
                    {
                        Inventory.Instance.AddItem(item);
                    }
                    hit.transform.gameObject.SetActive(false);
                }

            }
        }


        ////////////////열쇠 //////////////////
        if (Physics.SphereCast(ray, 1f, out hit, 0.5f, 1 << keyLayer)) //만약 열쇠가  레이에 검출되면
        {
            print("g");
            UIText.Instance.UITEXT = "열쇠를 주우려면 스페이스바를 누르세요";
            if (Input.GetButtonDown("Jump"))
            {
                keybool = true;   //인벤토리 넣는걸로 바꿀것
                Item item = hit.transform.GetComponent<Item>();
                if (item != null)
                {
                    Inventory.Instance.AddItem(item);
                }
            }

        }
        if (keybool)
        {
            //인벤토리 넣는걸로 바꿀것
            keyObj.transform.parent = toolPos;
            keyObj.transform.position = toolPos.position;
            UIText.Instance.UITEXT = "열쇠를 놓으려면 마우스 왼쪽버튼을 누르세요";
            if (Input.GetButtonDown("Fire1"))
            {
                UIText.Instance.UITEXT = " ";
                keyObj.transform.parent = null;
                keybool = false;
            }if(Duck.Instance.openCage)
            {
                UIText.Instance.UITEXT = " ";
                keyObj.transform.parent = null;
                keybool = false;
            }
        }
       


        /////////////나비////////////////
        if (Physics.SphereCast(ray, 3f, out hit, 10f, 1 << butterflyLayer)) //만약 나비가 레이에 검출되면
        {
            //나비에 3d글자 출력
            Butterfly.Instance.look = true;


        }
        else
            Butterfly.Instance.look = false;




       ////////////////치즈//////////////////
        if (Physics.SphereCast(ray, 1f, out hit, 0.5f, 1 << cheeseLayer)) //만약 치즈가 레이에 검출되면
        {
            UIText.Instance.UITEXT = "치즈를 주우려면 스페이스바를 누르세요";
            if (Input.GetButtonDown("Jump"))
            {
                cheesebool = true;     //인벤토리 넣는걸로 바꿀것
                Item item = hit.transform.GetComponent<Item>();
                if (item != null)
                {
                    Inventory.Instance.AddItem(item);
                }

            }

        }
        if (cheesebool)    //인벤토리 넣는걸로 바꿀것
        {
            cheeseObj.transform.parent =toolPos;
            cheeseObj.transform.position = toolPos.position;
            UIText.Instance.UITEXT = "치즈를 자리에 놓으려면 마우스 왼쪽버튼을 누르세요";
            if (Input.GetButtonDown("Fire1"))
            {
                mouseGo = true;
                cheesebool = false;
            }
        }
        if(mouseGo)
        {
            cheeseObj.transform.parent = null;
           
        }

        
        //////////////////////클라이밍////////////////////////////

        //손(마우스)
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseHit;

        //거미 마우스 포인트(손)이 닿았을때
        if (Physics.SphereCast(mouseRay, 0.5f, out mouseHit, 5f, 1 << spiderLayer)) //만약 grabPoint가 마우스 위치의 레이에 검출되면
        {
            LifeManager.Instance.LIFE -= 0.1f; //플레이어 라이프 감소
        }

        //pc용 클라이밍
        if (Physics.SphereCast(mouseRay, 1f, out mouseHit, 5f, 1 << grabPointLayer)) //만약 grabPoint가 마우스 위치의 레이에 검출되면
        {
            //파란색으로 색을 바꾸고
            grabMat = mouseHit.transform.gameObject.GetComponent<Renderer>();
            grabMat.material.color = Color.blue;


            //클릭을 누르면 
            if (Input.GetButtonDown("Fire1"))
            {
                print("클릭");
                grabPoint = mouseHit.transform;  //grabPoint 에 위치저장

                click = true;
            }

            if (mouseHit.transform.gameObject.tag.Contains("rope"))   // 로프
            {
                //ui띄우기
                UIText.Instance.UITEXT = "줄타기를 멈추려면 스페이스바를 누르세요";


                //키를 누르면 떨어지기
                if (Input.GetButtonDown("Jump"))
                {
                    click = false;
                }

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
            handPoint.forward = grabPoint.right;
            transform.position = Vector3.Lerp(transform.position, playerHandPoint.position, Time.deltaTime * 6);   // 타겟으로 러프이동

            moveScript.enabled = false;   //movescipt는 꺼둠
        }
        else
        {
            moveScript.enabled = true;  //제힌시간 지나면 켜짐
        }

    }
}