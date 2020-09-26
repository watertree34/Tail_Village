using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFind : MonoBehaviour
{
    //플레이어가 아이템 발견했을때-색깔 바뀌기  
    //아이템-거위, 콩, 도끼, 음식-i키를 누르면 인벤토리로


    //치즈-쥐 유인
    //열쇠-거위 탈출

    //가이드-나비 - 나비를 보면 글자띄우기

    //grabpoint - 마우스 클릭한 포인트의 위치로 손이동하기
    //grabpoint-rope-암벽 끝에서 벗어날수있게

    public static RaycastFind Instance;  //싱글톤
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
    public Transform vrToolPos;  // VR도구 위치
    //public Transform handPoint;   //granpoint 위치
    //public Transform playerHandPoint;//granpoint 플레이어 손 위치
    Transform grabPoint;
    bool click;  //암벽 클릭
    pcPlayerMove moveScript;  //움직이는 스크립트
    float grabTime = 8;  // 잡고있는 최대시간

    public GameObject cheeseObj;  // 치즈
    bool pickUpCheese = false; // 치즈 주웠는지 판별
    public bool mouseGo;

    public GameObject keyObj;  // 열쇠
    bool pickUpKey = false;    // 열쇠 주웠는지 판별

    GameObject axe = null;     // 도끼
    bool pickUpAxe = false;    // 도끼 주웠는지 판별

    void Start()
    {
        itemLayer = LayerMask.NameToLayer("Item");
        axeLayer = LayerMask.NameToLayer("Axe");
        duckLayer = LayerMask.NameToLayer("Duck");
        keyLayer = LayerMask.NameToLayer("Key");
        butterflyLayer = LayerMask.NameToLayer("Butterfly");
        cheeseLayer = LayerMask.NameToLayer("Cheese");
        grabPointLayer = LayerMask.NameToLayer("GrabPoint");
        spiderLayer = LayerMask.NameToLayer("Spider");

        //moveScript = gameObject.GetComponent<pcPlayerMove>();
        //moveScript.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 5f);


        /////////////아이템//////////// - 음식아이템!
        if (Physics.SphereCast(ray, 3f, out hit, 3f, 1 << itemLayer))  //만약 아이템이 레이에 검출되면
        {

            //ui띄우기
            UIText.Instance.UITEXT = "아이템을 주우려면 A를 누르세요";
            UIText.Instance.uiText.enabled = true;

            //키를 누르면 인벤토리에 저장
            //VR
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                Item item = hit.transform.GetComponent<Item>();
                if (item != null)
                {
                    Inventory.Instance.AddItem(item);
                }
                //인벤토리
                Destroy(hit.transform.gameObject);
            }
            //PC
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Item item = hit.transform.GetComponent<Item>();
                if (item != null)
                {
                    Inventory.Instance.AddItem(item);
                }
                //인벤토리
                Destroy(hit.transform.gameObject);
            }
        }





        ////////////////도끼/////////////////
        if (Physics.SphereCast(ray, 3f, out hit, 3f, 1 << axeLayer))  //만약 도끼가  레이에 검출되면
        {

            //ui띄우기
            UIText.Instance.UITEXT = "도끼를 주우려면 A를 누르세요";
            UIText.Instance.uiText.enabled = true;

            //키를 누르면 인벤토리에 저장
            //VR
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                //인벤토리에 저장되는 함수 호출
                Item item = hit.transform.GetComponent<Item>();
                if (item != null)
                {
                    Inventory.Instance.AddItem(item);
                    pickUpAxe = true;
                }
                axe = hit.transform.gameObject;
                axe.transform.parent = vrToolPos;
                axe.transform.localPosition = Vector3.zero;
                axe.SetActive(false);
            }
            //PC
            else if (Input.GetKeyDown(KeyCode.A))
            {
                //인벤토리에 저장되는 함수 호출
                Item item = hit.transform.GetComponent<Item>();
                if (item != null)
                {
                    Inventory.Instance.AddItem(item);
                    pickUpAxe = true;
                }
                axe = hit.transform.gameObject;
                axe.transform.parent = toolPos;
                axe.transform.localPosition = Vector3.zero;
                axe.SetActive(false);
            }

        }

        if (pickUpAxe == true)
        {
            if (Inventory.Instance.isAxeUsed == true)
            {
                axe.SetActive(true);
            }
            else
            {
                //print("raycast 도끼 상태 = " + PickUp.Instance.isAxeUsed);
                axe.SetActive(false);
            }
        }

        ///////////////거위//////////////////
        if (Duck.Instance.openCage)   // 거위케이지가 열렸을때
        {
            if (Physics.SphereCast(ray, 3f, out hit, 3f, 1 << duckLayer))  //만약 거위가 레이에 검출되면
            {
                //ui띄우기
                UIText.Instance.UITEXT = "거위를 주우려면 A를 누르세요";
                UIText.Instance.uiText.enabled = true;

                //키를 누르면 인벤토리에 저장
                //VR
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    //인벤토리에 저장되는 함수 호출
                    Item item = hit.transform.GetComponent<Item>();
                    if (item != null)
                    {
                        Inventory.Instance.AddItem(item);
                    }
                    hit.transform.gameObject.SetActive(false);
                }
                //PC
                else if (Input.GetKeyDown(KeyCode.A))
                {
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
        else
        {
            ////////////////열쇠 //////////////////
            if (Physics.SphereCast(ray, 1f, out hit, 0.5f, 1 << keyLayer)) //만약 열쇠가  레이에 검출되면
            {
                UIText.Instance.UITEXT = "열쇠를 주우려면 A를 누르세요";
                UIText.Instance.uiText.enabled = true;

                //VR
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    Item item = hit.transform.GetComponent<Item>();
                    if (item != null)
                    {
                        Inventory.Instance.AddItem(item);
                        pickUpKey = true;
                    }
                    keyObj = hit.transform.gameObject;
                    keyObj.transform.parent = vrToolPos;
                    keyObj.transform.localPosition = Vector3.zero;
                    keyObj.SetActive(false);
                }
                //PC
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    Item item = hit.transform.GetComponent<Item>();
                    if (item != null)
                    {
                        Inventory.Instance.AddItem(item);
                        pickUpKey = true;
                    }
                    keyObj = hit.transform.gameObject;
                    keyObj.transform.parent = toolPos;
                    keyObj.transform.localPosition = Vector3.zero;
                    keyObj.SetActive(false);
                }
            }
        }


        if (pickUpKey == true)
        {
            if (Inventory.Instance.isKeyUsed == true)
            {
                keyObj.SetActive(true);
                keyObj.transform.parent = null;
                pickUpKey = false;
            }
            else
            {
                keyObj.SetActive(false);
            }
        }
        else
        {
            keyObj.SetActive(true);
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
        if (Physics.SphereCast(ray, 1f, out hit,0.5f, 1 << cheeseLayer)) //만약 치즈가 레이에 검출되면
        {
            UIText.Instance.UITEXT = "치즈를 주우려면 A를 누르세요";
            UIText.Instance.uiText.enabled = true;

            //VR
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                Item item = hit.transform.GetComponent<Item>();
                if (item != null)
                {
                    Inventory.Instance.AddItem(item);
                    pickUpCheese = true;
                }
                cheeseObj = hit.transform.gameObject;
                cheeseObj.transform.parent = vrToolPos;
                cheeseObj.transform.localPosition = Vector3.zero;
                cheeseObj.SetActive(false);
            }
            //PC
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Item item = hit.transform.GetComponent<Item>();
                if (item != null)
                {
                    Inventory.Instance.AddItem(item);
                    pickUpCheese = true;
                }
                cheeseObj = hit.transform.gameObject;
                cheeseObj.transform.parent = toolPos;
                cheeseObj.transform.localPosition = Vector3.zero;
                cheeseObj.SetActive(false);
            }

        }

        if (pickUpCheese == true)
        {
            if (Inventory.Instance.isCheeseUsed == true)
            {
                cheeseObj.SetActive(true);
                UIText.Instance.UITEXT = "치즈를 자리에 놓으려면 B를 누르세요";
                UIText.Instance.uiText.enabled = true;

                //VR
                if (OVRInput.GetDown(OVRInput.Button.Two))
                {
                    cheeseObj.transform.parent = null;
                    mouseGo = true;
                    pickUpCheese = false;
                }
                //PC
                else if (Input.GetKeyDown(KeyCode.B))
                {
                    cheeseObj.transform.parent = null;
                    mouseGo = true;
                    pickUpCheese = false;
                }
            }
            else
            {
                cheeseObj.SetActive(false);
            }
        }


        ////////////////////////클라이밍////////////////////////////

        ////손(마우스)
        //Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit mouseHit;

        ////거미 마우스 포인트(손)이 닿았을때
        //if (Physics.SphereCast(mouseRay, 0.5f, out mouseHit, 10f, 1 << spiderLayer)) //만약 grabPoint가 마우스 위치의 레이에 검출되면
        //{
        //    LifeManager.Instance.LIFE -= 0.1f; //플레이어 라이프 감소
        //}

        ////pc용 클라이밍
        //if (Physics.SphereCast(mouseRay, 1f, out mouseHit, 5f, 1 << grabPointLayer)) //만약 grabPoint가 마우스 위치의 레이에 검출되면
        //{
        //    //파란색으로 색을 바꾸고
        //    grabMat = mouseHit.transform.gameObject.GetComponent<Renderer>();
        //    grabMat.material.color = Color.blue;

           
        //    //클릭을 누르면 
        //    if (Input.GetButtonDown("Fire1"))  // pc
        //    {

        //        grabPoint = mouseHit.transform;  //grabPoint 에 위치저장
        //        grabTime = 8;
        //        click = true;
        //    }

        //}


        //if (grabTime <= 0)   // 잡고있는거 제한시간
        //{
        //    grabTime = 8;
        //    click = false;
        //}
        //if (click)
        //{
        //    grabTime -= Time.deltaTime;

        //    UIText.Instance.UITEXT = (int)(grabTime) + "초 안에 다른것을 잡지 않으면 손이 떨어집니다. \n 손을 임의로 떨어뜨리고 싶으면 스페이스바를 누르세요";
        //    UIText.Instance.uiText.enabled = true;
        //    //초록으로 바뀐 후 손의 위치가 grabPoint 위치로 이동한다
        //    grabMat.material.color = Color.green;
        //    handPoint.position = grabPoint.position;
        //    handPoint.forward = grabPoint.forward;
        //    transform.position = Vector3.Lerp(transform.position, playerHandPoint.position, Time.deltaTime * 6);   // 타겟으로 러프이동

        //    moveScript.enabled = false;   //movescipt는 꺼둠

        //    //키를 누르면 떨어지기
        //    if (Input.GetButtonDown("Jump"))
        //    {
        //        grabTime = 0;
        //    }
        //}
        //else
        //{
        //    moveScript.enabled = true;  //제힌시간 지나거나 스페이스바 누르면 켜짐
        //}
    }
}