using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 직선 텔레포트 기능 구현하기
public class TeleportStraight : MonoBehaviour
{
    // 테레포트 표시할 UI
    public Transform teleportCircleUI;
    // 선을 그릴 라인렌더러
    LineRenderer lr;

    // 최초 텔레포트 UI 크기
    Vector3 originScale = Vector3.one * 0.02f;
    void Start()
    {
        // 시작할 때 비활성화 시킨다.
        teleportCircleUI.gameObject.SetActive(false);
        // 라인 랜더러 컴포넌트 얻어오기
        lr = GetComponent<LineRenderer>();
        // 컴포넌트가 없을 경우 컴포넌트 추가
        if(lr == null)
        {
            Material lrMat = Resources.Load<Material>("Line");
            if(lrMat)
            {
                lr = gameObject.AddComponent<LineRenderer>();
                lr.material = lrMat;
                lr.startWidth = 0;
                lr.endWidth = 1;
            }
        }
    }

    void Update()
    {
        // 왼쪽 컨트롤러의 One 버튼을 누름면
        if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            // 라인랜더러 컴포넌트 활성화
            lr.enabled = true;
        }
        // 왼쪽 컨트롤러의 One 버튼을 떼면
        else if (ARAVRInput.GetUp(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            // 라인랜더러 비활성화
            lr.enabled = false;
            if (teleportCircleUI.gameObject.activeSelf)
            {
                GetComponent<CharacterController>().enabled = false;
                // 텔레포트 UI 위치로 순간이동
                transform.position = teleportCircleUI.position + Vector3.up;
                GetComponent<CharacterController>().enabled = true;
            }
            // 텔레포트 UI 비활성화
            teleportCircleUI.gameObject.SetActive(false);
        }
        // 왼쪽 컨트롤러의 One 버튼을 누르고 있을때
        else if (ARAVRInput.Get(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            // 1. 왼쪽 컨트롤러를 기준으로 Ray 를 만든다.
            Ray ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("Terrain");
            // 2. Terrain 만 Ray 충돌 검출
            if (Physics.Raycast(ray, out hitInfo, 200, layer))
            {
                // 3. Ray 가 부딪힌 지점에 라인그리기
                lr.SetPosition(0, ray.origin);
                lr.SetPosition(1, hitInfo.point);

                // 4. Ray 가 부딪힌 지점에 텔레포트 UI 표시
                teleportCircleUI.gameObject.SetActive(true);
                teleportCircleUI.position = hitInfo.point;
                // 텔레포트 UI 가 위로 누워 있도록 방향 설정
                teleportCircleUI.forward = hitInfo.normal;
                // 텔레포트 UI 의 크기가 거리에 따라 보정 되도록 설정
                teleportCircleUI.localScale = originScale * Mathf.Max(1, hitInfo.distance);
            }
            else
            {
                // Ray 충돌이 발생하지 않으면 선이 Ray 방향으로 그려지도록 처리
                lr.SetPosition(0, ray.origin);
                lr.SetPosition(1, ray.origin + ARAVRInput.LHandDirection * 200);
                // 텔레포트 UI 는 화면에서 비활성화
                teleportCircleUI.gameObject.SetActive(false);
            }
        }
    }
}
