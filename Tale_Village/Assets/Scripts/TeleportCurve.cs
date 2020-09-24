using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 곡선 텔레포트 기능 구현하기
public class TeleportCurve : MonoBehaviour
{
    // 테레포트 표시할 UI
    public Transform teleportCircleUI;
    // 선을 그릴 라인렌더러
    LineRenderer lr;

    // 최초 텔레포트 UI 크기
    Vector3 originScale = Vector3.one * 0.02f;

    // 커브의 부드러움 정도
    public int lineSmooth = 40;
    // 커브의 길이
    public float curveLength = 50;
    // 커브의 중력
    public float gravity = -60;
    // 곡선 시뮬레이션 간격 시간
    public float simulateTime = 0.02f;
    // 곡선을 이루는 점들을 기억할 리스트
    List<Vector3> lines = new List<Vector3>();

    void Start()
    {
        // 시작할 때 비활성화 시킨다.
        teleportCircleUI.gameObject.SetActive(false);
        // 라인렌더러 컴포넌트 얻어오기
        lr = GetComponent<LineRenderer>();
        // 라인렌더러의 선 너비를 지정
        lr.startWidth = 0.0f;
        lr.endWidth = 0.2f;
        // 컴포넌트가 없을 경우 컴포넌트 추가
        if (lr == null)
        {
            Material lrMat = Resources.Load<Material>("Line");
            if (lrMat)
            {
                lr = gameObject.AddComponent<LineRenderer>();
                lr.material = lrMat;
            }
        }
    }

    
    void Update()
    {
        
        // 왼쪽 컨트롤러의 One 버튼을 누르면
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
            // 텔레포트 UI 가 활성화 되어 있을 때
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
            // 주어진 길이크기의 커브를 만들고 싶다.
            MakeLines();
        }
    }
    // 라인렌더러를 이용한 점을 만들고 선을 그린다.
    void MakeLines()
    {
        // 리스트에 담긴 위치 정보들을 비워준다.
        lines.RemoveRange(0, lines.Count);
        // 선이 진행될 방향을 정한다.
        Vector3 dir = ARAVRInput.LHandDirection * curveLength;
        // 선이 그려질 위치의 초기 값을 설정한다.
        Vector3 pos = ARAVRInput.LHandPosition;
        // 최초 위치를 리스트에 담는다.
        lines.Add(pos);
        // lineSmooth 개수 만큼 반복한다.
        for (int i = 0; i < lineSmooth; i++)
        {
            // 현재 위치 기억
            Vector3 lastPos = pos;
            // 중력 적용한 속도 계산
            // v = v0 + at
            dir.y += gravity * simulateTime;
            // 등속운동으로 다음 위치 계산
            //P = P0 + vt
            pos += dir * simulateTime;

            // Ray 충돌 체크가 일어 났으면 
            if (CheckHitRay(lastPos, ref pos))
            {
                // 충돌 지점을 등록하고 종료
                lines.Add(pos);
                break;
            }
            else
            {
                // 텔레포트 UI 비활성화
                teleportCircleUI.gameObject.SetActive(false);
            }
            // 구해진 위치를 등록
            lines.Add(pos);
        }
        // 라인랜더러가 표현할 점의 개수를 등록된 개수 크기로 할당
        lr.positionCount = lines.Count;
        // 라인랜더러에 구해진 점의 정보를 지정
        lr.SetPositions(lines.ToArray());
    }

    // 앞 점의 위치와 다음 점의 위치를 받아 Ray 충돌을 체크
    private bool CheckHitRay(Vector3 lastPos, ref Vector3 pos)
    {
        // 앞점 lastPos 에서 다음점 pos 로 향하는 벡터 계산
        Vector3 rayDir = pos - lastPos;
        Ray ray = new Ray(lastPos, rayDir);
        RaycastHit hitInfo;
        
        // Raycast 할때 Ray 의 크기를 앞점과 다음점 사이의 거리로 한정한다.
        if (Physics.Raycast(ray, out hitInfo, rayDir.magnitude))
        {
            // 다음점의 위치를 충돌한 지점으로 설정
            pos = hitInfo.point;

            int layer = LayerMask.NameToLayer("Terrain");
            // Terrain 레이어와 충돌했을 경우에만 텔레포트 UI 가 표시 되도록 한다.
            if (hitInfo.transform.gameObject.layer == layer)
            {
                // 텔레포트 UI 활성화
                teleportCircleUI.gameObject.SetActive(true);
                // 텔레포트 UI 의 위치지정
                teleportCircleUI.position = pos;
                // 텔레포트 UI 의 방향 설정
                teleportCircleUI.forward = hitInfo.normal;
                float distance = (pos - ARAVRInput.LHandPosition).magnitude;
                // 텔레포트 UI 가 보여질 크기를 설정
                teleportCircleUI.localScale = originScale * Mathf.Max(1, distance);
            }
            return true;
        }
        return false;
    }

    
    private void OnDrawGizmos()
    {
        //MakeLines();
        //Gizmos.color = Color.red;
        //foreach (Vector3 pos in lines)
        //{
        //    Gizmos.DrawSphere(pos, 0.5f);
        //}
    }
    
}
