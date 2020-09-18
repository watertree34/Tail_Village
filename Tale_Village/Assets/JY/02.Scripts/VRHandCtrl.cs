using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRHandCtrl : MonoBehaviour
{
    LineRenderer layser;
    GameObject currentObject;
    public float raycastDistance = 100f;
    bool click = false;

    void Start()
    {
        layser = gameObject.AddComponent<LineRenderer>();
        // 라인이 가지개될 색상 표현
        layser.startColor=Color.white;
        //Material material = new Material(Shader.Find("Standard"));
        //material.color = Color.white;
        //layser.material = material;
        // 레이저의 꼭지점 2개
        layser.positionCount = 2;
        // 레이저 굵기 표현
        layser.startWidth = 0.05f;
        layser.endWidth = 0.05f;
    }

    void Update()
    {
        layser.SetPosition(0, transform.position); // 첫번째 시작점 위치
                                                   // 업데이트에 넣어 줌으로써, 플레이어가 이동하면 이동을 따라가게 된다.

        //선 만들기(충돌 감지를 위한)
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        // 충돌 감지 시
        if (Physics.Raycast(ray, out hitInfo, 5))
        {
            layser.SetPosition(1, hitInfo.point);

            // 충돌 객체의 태그가 Button인 경우
            if (hitInfo.collider.gameObject.CompareTag("Button"))
            {

                print("버튼 충돌");
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    print("클릭");
                    // 버튼에 등록된 onClick 메소드를 실행한다.
                    hitInfo.transform.gameObject.GetComponent<Button>().onClick.Invoke();
                    if (click == false)
                    {
                        click = true;
                    }
                }

                else
                {
                    hitInfo.transform.gameObject.GetComponent<Button>().OnPointerEnter(null);
                    currentObject = hitInfo.collider.GetComponent<Collider>().gameObject;
                    click = false;
                }
            }
        }
        // 충돌 감지 X
        else
        {
            // 레이저에 감지된 것이 없기 때문에 레이저 초기 설정 길이만큼 길게 만든다.
            layser.SetPosition(0, transform.position + (transform.forward * raycastDistance));

            // 최근 감지된 오브젝트가 Button인 경우
            // 버튼은 현재 눌려있는 상태이므로 이것을 풀어준다.
            if (currentObject != null)
            {
                currentObject.GetComponent<Button>().OnPointerExit(null);
                currentObject = null;
            }

        }
    }
}
